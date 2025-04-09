import { Component, input } from '@angular/core';
import { NoteComment } from '../../../models/noteComment';
import { UserCardComponent } from '../../common/user-card/user-card.component';

@Component({
  selector: 'app-comment-card',
  imports: [UserCardComponent],
  templateUrl: './comment-card.component.html',
  styleUrl: './comment-card.component.css',
})
export class CommentCardComponent {
  comment = input.required<NoteComment>();
}
