import { Component, computed } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { SubjectCardComponent } from '../subject-card/subject-card.component';

@Component({
  selector: 'app-subject-list',
  imports: [SubjectCardComponent],
  templateUrl: './subject-list.component.html',
  styleUrl: './subject-list.component.css'
})
export class SubjectListComponent {
  subjects = computed(() => this.groupService.getEnrolledGroups()());

  constructor(private groupService: GroupService) { }
}
