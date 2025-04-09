import { Component, input, model, output, signal } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NoteService } from '../../../services/note.service';
import { FileUploadDirective } from '../../../directives/file-upload.directive';
import { Note } from '../../../models/note';
import { ModalComponent } from '../../common/modal/modal.component';

@Component({
  selector: 'app-create-note-button',
  imports: [FileUploadDirective, ModalComponent],
  templateUrl: './create-note-button.component.html',
  styleUrl: './create-note-button.component.css',
})
export class CreateNoteButtonComponent {
  groupId = input.required<number>();
  onSubmit = output<Note>();
  showModal = model<boolean>(false);
  files = signal<File[]>([]);

  constructor(
    private noteService: NoteService,
    private toastr: ToastrService
  ) {}

  handleAddNoteFile(file: File) {
    this.files.update((prev) => [...prev, file]);
  }

  handleCreateNote() {
    if (this.groupId() === 0 || this.files().length === 0) return;

    this.noteService.createNote(this.groupId(), this.files()).subscribe({
      next: (note) => {
        this.toastr.success('Note created');
        this.onSubmit.emit(note);
      },
    });
  }

  openModal() {
    this.showModal.set(true);
  }
}
