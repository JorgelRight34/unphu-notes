import { Component, signal } from '@angular/core';
import { GroupService } from '../../../services/group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Note } from '../../../models/note';
import { GroupMember } from '../../../models/groupMember';
import { NoteCardComponent } from '../../note/note-card/note-card.component';
import { map, of } from 'rxjs';
import { CreateNoteButtonComponent } from '../../note/create-note-button/create-note-button.component';

@Component({
  selector: 'app-subject-view',
  imports: [NoteCardComponent, CreateNoteButtonComponent],
  templateUrl: './group-view.component.html',
  styleUrl: './group-view.component.css',
})
export class SubjectViewComponent {
  week = signal<number>(1);
  notes = signal<Note[]>([]);
  members = signal<GroupMember[]>([]);
  groupId = signal<number | null>(null);

  constructor(
    private groupService: GroupService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.params
      .pipe(
        map((params) => {
          const groupId = Number(params['id']);

          if (isNaN(groupId)) {
            this.router.navigate(['/']);
            return of(null); // Return empty observable
          }

          this.groupId.set(groupId);

          this.groupService
            .getGroupMembers(groupId)
            .pipe(
              map((data) => {
                this.members.set(data);
                return data;
              })
            )
            .subscribe();

          // Return combined observables
          this.groupService
            .getGroupNotes(groupId)
            .pipe(
              map((notes) => {
                this.notes.set(notes);
                return notes;
              })
            )
            .subscribe();

          return params;
        })
      )
      .subscribe();
  }

  changeWeekBy(n: number) {
    if (this.week() + n < 1) return;
    this.week.update((prev) => prev + n);
  }

  handleAddNote(note: Note) {
    this.notes.update((prev) => [...prev, note]);
  }

  handleOnDelete(note: Note) {
    this.notes.update((prev) => [...prev.filter((n) => n.id != note.id)]);
  }
}
