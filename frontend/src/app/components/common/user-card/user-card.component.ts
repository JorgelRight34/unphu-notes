import { Component, input } from '@angular/core';
import { User } from '../../../models/user';
import { CloudinarySecurePipe } from '../../../pipes/cloudinary-secure.pipe';
import { UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-user-card',
  imports: [CloudinarySecurePipe, UpperCasePipe],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.css',
})
export class UserCardComponent {
  user = input.required<User>();
}
