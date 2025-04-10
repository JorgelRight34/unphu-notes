import { Component, input, output } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Group } from '../../../models/group';
import { GroupSchedulePipe } from '../../../pipes/group-schedule.pipe';

@Component({
  selector: 'app-group-card',
  imports: [RouterModule, GroupSchedulePipe],
  templateUrl: './group-card.component.html',
  styleUrl: './group-card.component.css',
})
export class GroupCardComponent {
  group = input.required<Group>();
  clickGroup = output<Group>();

  handleOnClick() {
    this.clickGroup.emit(this.group());
  }
}
