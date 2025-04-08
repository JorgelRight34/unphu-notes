import { Component, input } from '@angular/core';
import { Group } from '../../models/group';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-subject-card',
  imports: [RouterModule],
  templateUrl: './subject-card.component.html',
  styleUrl: './subject-card.component.css',
})
export class SubjectCardComponent {
  subject = input.required<Group>();

  constructor(private router: Router) {}
}
