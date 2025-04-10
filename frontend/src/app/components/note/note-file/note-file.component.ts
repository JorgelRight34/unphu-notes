import { DecimalPipe } from '@angular/common';
import { Component, computed, input, output } from '@angular/core';

@Component({
  selector: 'app-note-file',
  imports: [DecimalPipe],
  templateUrl: './note-file.component.html',
  styleUrl: './note-file.component.css',
})
export class NoteFileComponent {
  file = input.required<File>();
  fileUrl = computed(() => URL.createObjectURL(this.file()));
  onDelete = output<File>();

  handleDelete() {
    this.onDelete.emit(this.file());
  }
}
