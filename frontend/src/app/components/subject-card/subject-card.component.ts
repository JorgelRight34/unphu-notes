import { Component, input } from '@angular/core';
import { Group } from '../../models/group';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-subject-card',
  imports: [RouterLink],
  templateUrl: './subject-card.component.html',
  styleUrl: './subject-card.component.css'
})
export class SubjectCardComponent {
  subject = input.required<Group>();

  constructor() { }
}
