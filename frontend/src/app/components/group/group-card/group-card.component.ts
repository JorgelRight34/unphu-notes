import { Component, input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Group } from '../../../models/group';

@Component({
  selector: 'app-group-card',
  imports: [RouterModule],
  templateUrl: './group-card.component.html',
  styleUrl: './group-card.component.css',
})
export class GroupCardComponent {
  group = input.required<Group>();
}
