import { Component, input, model, output, signal } from '@angular/core';
import { Note } from '../../../models/note';
import { ModalComponent } from '../../common/modal/modal.component';
import { Router } from '@angular/router';
import { NoteFormComponent } from '../note-form/note-form.component';

@Component({
  selector: 'app-create-note-button',
  imports: [ModalComponent, NoteFormComponent],
  templateUrl: './create-note-button.component.html',
  styleUrl: './create-note-button.component.css',
})
export class CreateNoteButtonComponent {
  groupId = input.required<number>();
  onSubmit = output<Note>();
  showModal = model<boolean>(false);
  files = signal<File[]>([]);
  week = input.required<number>();

  constructor(private router: Router) {}

  openModal() {
    if (this.groupId() === 0 || !this.router.url.includes('/group')) return;
    this.showModal.set(true);
  }

  handleOnSubmit(note: Note) {
    this.showModal.set(false);
    this.onSubmit.emit(note);
  }
}
