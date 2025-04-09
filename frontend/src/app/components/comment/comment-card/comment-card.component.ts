import { Component, input } from '@angular/core';
import { NoteComment } from '../../../models/noteComment';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { UserPostCardComponent } from '../../common/user-post-card/user-post-card.component';

@Component({
  selector: 'app-comment-card',
  imports: [UserPostCardComponent, TitleCasePipe, DatePipe],
  templateUrl: './comment-card.component.html',
  styleUrl: './comment-card.component.css',
})
export class CommentCardComponent {
  comment = input.required<NoteComment>();
}
