import { Component, input, output, signal } from '@angular/core';
import { Note } from '../../../models/note';
import { NoteService } from '../../../services/note.service';
import { ModalComponent } from '../../common/modal/modal.component';
import { NoteCommentsComponent } from '../note-comments/note-comments.component';
import { NoteFilesGalleryComponent } from '../note-files-gallery/note-files-gallery.component';
import { UserPostCardComponent } from '../../common/user-post-card/user-post-card.component';
import { NoteFile } from '../../../models/noteFile';
import { SafeUrl } from '@angular/platform-browser';

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
  noteFileUrl = signal<SafeUrl | null>(null);

  constructor(private noteService: NoteService) {}

  ngOnInit() {
    this.noteFileUrl.set(
      this.noteService.getNoteFileDownloadUrl(this.note().noteFiles[0])
    );
  }

  handleDelete() {
    const id = this.note().id;
    if (id === undefined) return;
    this.noteService.deleteNote(id).subscribe({
      next: () => this.onDelete.emit(this.note()),
    });
  }

  handleShowModal() {
    queueMicrotask(() => this.showModal.set(true));
  }

  handleCurrentImageChange(noteFile: NoteFile) {
    this.noteFileUrl.set(this.noteService.getNoteFileDownloadUrl(noteFile));
  }
}
