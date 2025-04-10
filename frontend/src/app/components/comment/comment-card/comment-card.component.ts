import { Component, input } from '@angular/core';
import { NoteComment } from '../../../models/noteComment';
import { UserPostCardComponent } from '../../common/user-post-card/user-post-card.component';

@Component({
  selector: 'app-comment-card',
  imports: [UserPostCardComponent],
  templateUrl: './comment-card.component.html',
  styleUrl: './comment-card.component.css',
})
export class CommentCardComponent {
  comment = input.required<NoteComment>();
}
