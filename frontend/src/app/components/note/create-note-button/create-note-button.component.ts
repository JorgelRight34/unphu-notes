import { Component, input, model, output, signal } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NoteService } from '../../../services/note.service';
import { FileUploadDirective } from '../../../directives/file-upload.directive';
import { Note } from '../../../models/note';
import { ModalComponent } from '../../common/modal/modal.component';
import { NoteFileComponent } from '../note-file/note-file.component';

@Component({
  selector: 'app-create-note-button',
  imports: [FileUploadDirective, ModalComponent, NoteFileComponent],
  templateUrl: './create-note-button.component.html',
  styleUrl: './create-note-button.component.css',
})
export class CreateNoteButtonComponent {
  groupId = input.required<number>();
  onSubmit = output<Note>();
  showModal = model<boolean>(false);
  files = signal<File[]>([]);
  week = input.required<number>();

  constructor(
    private noteService: NoteService,
    private toastr: ToastrService
  ) { }

  handleAddNoteFile(file: File) {
    this.files.update((prev) => [...prev, file]);
  }

  handleCreateNote() {
    if (this.groupId() === 0 || this.files().length === 0) return;

    this.noteService
      .createNote(this.groupId(), this.files(), this.week())
      .subscribe({
        next: (note) => {
          this.toastr.success('Note created');
          this.onSubmit.emit(note);
          this.files.set([]);
          this.showModal.set(false);
        },
      });
  }

  handleOnDelete(file: File) {
    console.log('deleting');
    this.files.update((prev) => [
      ...prev.filter((f) => !this.areFilesEqual(f, file)),
    ]);
  }

  openModal() {
    this.showModal.set(true);
  }

  private areFilesEqual(file1: File, file2: File): boolean {
    return (
      file1.name === file2.name &&
      file1.size === file2.size &&
      file1.type === file2.type &&
      file1.lastModified === file2.lastModified
    );
  }
}
