import { Component, computed, input, output } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Note } from '../../../models/note';
import { CommentService } from '../../../services/comment.service';
import { FormsModule, NgForm } from '@angular/forms';
import { NoteComment } from '../../../models/noteComment';
import { UserPostCardComponent } from '../../common/user-post-card/user-post-card.component';

@Component({
  selector: 'app-comment-form',
  imports: [UserPostCardComponent, FormsModule],
  templateUrl: './comment-form.component.html',
  styleUrl: './comment-form.component.css',
})
export class CommentFormComponent {
  note = input.required<Note>();
  onSubmit = output<NoteComment>();
  user = computed(() => this.authService.user());
  model = { content: '' };

  constructor(
    private authService: AuthService,
    private commentService: CommentService
  ) {}

  handleSubmit(form: NgForm) {
    const noteId = this.note().id;
    if (!noteId) return;

    if (form.valid && this.model.content.length > 0) {
      this.commentService
        .createNoteComment({ content: this.model.content, noteId })
        .subscribe({
          next: (comment) => {
            this.onSubmit.emit(comment);
            form.reset();
          },
        });
    }
  }
}
