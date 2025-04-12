import { Component, input, output, signal } from '@angular/core';
import { NoteFileComponent } from '../note-file/note-file.component';
import { NoteService } from '../../../services/note.service';
import { ToastrService } from 'ngx-toastr';
import { Note } from '../../../models/note';
import { FormsModule, NgForm } from '@angular/forms';
import { FileUploadDirective } from '../../../directives/file-upload.directive';

@Component({
  selector: 'app-note-form',
  imports: [NoteFileComponent, FileUploadDirective, FormsModule],
  templateUrl: './note-form.component.html',
  styleUrl: './note-form.component.css',
})
export class NoteFormComponent {
  groupId = input.required<number>();
  week = input.required<number>();
  onSubmit = output<Note>();
  files = signal<File[]>([]);
  model = { title: '' };

  constructor(
    private noteService: NoteService,
    private toastr: ToastrService
  ) { }

  handleAddNoteFile(file: File) {
    if (this.groupId() === 0) return;

    this.files.update((prev) => [...prev, file]);
  }

  handleCreateNote(form: NgForm) {
    if (this.files().length === 0) {
      this.toastr.error('Debes agregar al menos un archivo');
      return;
    }

    if (
      this.groupId() === 0 ||
      form.invalid ||
      this.week() === 0
    )
      return;

    this.noteService
      .createNote(this.groupId(), this.files(), this.week(), this.model.title)
      .subscribe({
        next: (note) => {
          this.toastr.success('Nota creada!');
          this.onSubmit.emit(note);
          this.files.set([]);
          form.reset();
        },
      });
  }

  handleOnDelete(file: File) {
    this.files.update((prev) => [
      ...prev.filter((f) => !this.areFilesEqual(f, file)),
    ]);
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
