import { Component, input, output } from '@angular/core';
import { FileUploadDirective } from '../../directives/file-upload.directive';
import { NoteService } from '../../services/note.service';
import { Note } from '../../models/note';
import { Group } from '../../models/group';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-note-button',
  imports: [FileUploadDirective],
  templateUrl: './create-note-button.component.html',
  styleUrl: './create-note-button.component.css',
})
export class CreateNoteButtonComponent {
  groupId = input.required<number>();
  onSubmit = output<Note>();

  constructor(
    private noteService: NoteService,
    private toastr: ToastrService
  ) {}

  handleCreateNote(file: File) {
    if (this.groupId() === 0) return;

    this.noteService.createNote(this.groupId(), file).subscribe({
      next: (note) => {
        this.onSubmit.emit(note);
        this.toastr.success('Note created');
      },
    });
  }
}
