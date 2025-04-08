import { TitleCasePipe } from '@angular/common';
import { Component, computed, signal } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group';
import { Router } from '@angular/router';
import { Note } from '../../models/note';
import { GroupMember } from '../../models/groupMember';

@Component({
  selector: 'app-subject-view',
  imports: [TitleCasePipe],
  templateUrl: './subject-view.component.html',
  styleUrl: './subject-view.component.css'
})
export class SubjectViewComponent {
  days = ["lunes", "martes", "miércoles", "jueves", "viernes", "sábado", "domingo"];
  notes = signal<Note[]>([]);
  members = signal<GroupMember[]>([]);

  constructor(private groupService: GroupService, private router: Router) { }

  ngOnInit() {
    const groupId = this.router.getCurrentNavigation()?.extras.state?.['groupId'] as string;
    const groupIdNumber = Number(groupId);

    if (!isNaN(groupIdNumber)) {
      // Get group notes
      this.groupService.getGroupNotes(groupIdNumber).subscribe({
        next: (data) => this.notes.set(data)
      })

      // Get group members
      this.groupService.getGroupMembers(groupIdNumber).subscribe({
        next: (data) => this.members.set(data)
      })

    } else {
      this.router.navigate(['/index']);
    }
  }
}
