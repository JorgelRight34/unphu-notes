import { Component, input, output } from '@angular/core';
import { Note } from '../../models/note';
import { UserCardComponent } from '../common/user-card/user-card.component';
import { CloudinarySecurePipe } from '../../pipes/cloudinary-secure.pipe';
import { DatePipe } from '@angular/common';
import { NoteService } from '../../services/note.service';

@Component({
  selector: 'app-note-card',
  imports: [UserCardComponent, CloudinarySecurePipe, DatePipe],
  templateUrl: './note-card.component.html',
  styleUrl: './note-card.component.css',
})
export class NoteCardComponent {
  note = input.required<Note>();
  onDelete = output<Note>();

  constructor(private noteService: NoteService) {}

  handleDelete() {
    const id = this.note().id;
    if (id === undefined) return;
    this.noteService.deleteNote(id).subscribe({
      next: () => this.onDelete.emit(this.note()),
    });
  }
}
