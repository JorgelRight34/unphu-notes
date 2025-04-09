import { Component, input, signal } from '@angular/core';
import { NoteComment } from '../../models/noteComment';
import { NoteService } from '../../services/note.service';
import { Note } from '../../models/note';

@Component({
  selector: 'app-note-comments',
  imports: [],
  templateUrl: './note-comments.component.html',
  styleUrl: './note-comments.component.css'
})
export class NoteCommentsComponent {
  note = input.required<Note>();
  comments = signal<NoteComment[]>([]);

  constructor(private noteService: NoteService) { }

  ngOnInit() {
    const noteId = this.note().id;
    if (noteId === undefined) return;

    this.noteService.getNoteComments(noteId).subscribe({
      next: (comments) => this.comments.set(comments),
    });
  }
}
