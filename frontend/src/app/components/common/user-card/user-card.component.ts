import { Component, input } from '@angular/core';
import { User } from '../../../models/user';

@Component({
  selector: 'app-user-card',
  imports: [],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.css'
})
export class UserCardComponent {
  user = input.required<User>();
}
