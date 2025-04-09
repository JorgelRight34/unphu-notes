import { DatePipe, TitleCasePipe } from '@angular/common';
import { Component, input } from '@angular/core';
import { User } from '../../../models/user';

@Component({
  selector: 'app-user-post-card',
  imports: [DatePipe, TitleCasePipe],
  templateUrl: './user-post-card.component.html',
  styleUrl: './user-post-card.component.css',
})
export class UserPostCardComponent {
  user = input.required<User>();
  date = input<Date | null | string>(null);
}
