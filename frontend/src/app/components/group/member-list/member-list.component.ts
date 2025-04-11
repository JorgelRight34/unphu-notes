import { Component, input, model } from '@angular/core';
import { GroupMember } from '../../../models/groupMember';
import { ModalComponent } from '../../common/modal/modal.component';
import { TitleCasePipe, UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-member-list',
  imports: [ModalComponent, TitleCasePipe, UpperCasePipe],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css',
})
export class MemberListComponent {
  members = input.required<GroupMember[]>();
  showModal = model<boolean>(false);

  getMembersNamesListAsString(): string {
    return this.members()
      .map((member) => member.student.fullName.split(' ')[0])
      .join(', ');
  }

  openModal() {
    this.showModal.set(true);
  }
}
