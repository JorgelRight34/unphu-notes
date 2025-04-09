import { Component, input, output, signal } from '@angular/core';
import { Note } from '../../../models/note';
import { UserCardComponent } from '../../common/user-card/user-card.component';
import { CloudinarySecurePipe } from '../../../pipes/cloudinary-secure.pipe';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { NoteService } from '../../../services/note.service';
import { ModalComponent } from '../../common/modal/modal.component';
import { NoteCommentsComponent } from '../note-comments/note-comments.component';
import { NoteFilesGalleryComponent } from '../note-files-gallery/note-files-gallery.component';
import { UserPostCardComponent } from '../../common/user-post-card/user-post-card.component';

@Component({
  selector: 'app-note-card',
  imports: [
    NoteFilesGalleryComponent,
    UserPostCardComponent,
    ModalComponent,
    NoteCommentsComponent,
  ],
  templateUrl: './note-card.component.html',
  styleUrl: './note-card.component.css',
})
export class NoteCardComponent {
  note = input.required<Note>();
  onDelete = output<Note>();
  showModal = signal<boolean>(false);

  constructor(private noteService: NoteService) {}

  handleDelete() {
    const id = this.note().id;
    if (id === undefined) return;
    this.noteService.deleteNote(id).subscribe({
      next: () => this.onDelete.emit(this.note()),
    });
  }

  handleShowModal() {
    this.showModal.set(true);
  }
}
