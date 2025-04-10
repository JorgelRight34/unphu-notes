import { Component, computed, input } from '@angular/core';
import { GroupService } from '../../../services/group.service';
import { GroupCardComponent } from '../group-card/group-card.component';
import { Group } from '../../../models/group';

@Component({
  selector: 'app-group-list',
  imports: [GroupCardComponent],
  templateUrl: './group-list.component.html',
  styleUrl: './group-list.component.css',
})
export class GroupListComponent {
  groups = computed(() => this.groupService.getEnrolledGroups());
  maxHeight = input<string>('70dvh');

  constructor(private groupService: GroupService) {}

  handleSelectGroup(group: Group) {
    console.log(group);
    this.groupService.selectGroup(group.id);
  }
}
