import { Component, input, signal } from '@angular/core';
import { NoteComment } from '../../../models/noteComment';
import { NoteService } from '../../../services/note.service';
import { Note } from '../../../models/note';
import { CommentCardComponent } from '../../comment/comment-card/comment-card.component';
import { CommentFormComponent } from '../../comment/comment-form/comment-form.component';
import { NoteFilesGalleryComponent } from '../note-files-gallery/note-files-gallery.component';

@Component({
  selector: 'app-note-comments',
  imports: [
    CommentCardComponent,
    CommentFormComponent,
    NoteFilesGalleryComponent,
  ],
  templateUrl: './note-comments.component.html',
  styleUrl: './note-comments.component.css',
})
export class NoteCommentsComponent {
  note = input.required<Note>();
  comments = signal<NoteComment[]>([]);

  constructor(private noteService: NoteService) {}

  ngOnInit() {
    const noteId = this.note().id;
    if (noteId === undefined) return;

    this.noteService.getNoteComments(noteId).subscribe({
      next: (comments) => this.comments.set(comments),
    });
  }

  handleOnSubmit(comment: NoteComment) {
    this.comments.update((prev) => [...prev, comment]);
  }
}
