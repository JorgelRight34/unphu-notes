import { Component, computed, input } from '@angular/core';
import { User } from '../../../models/user';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-user-card',
  imports: [TitleCasePipe],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.css',
})
export class UserCardComponent {
  user = input.required<User>();
  fullName = computed(() => {
    const names = this.user().fullName.split(' ');
    return `${names[0]} ${names[1]}`;
  });
}
