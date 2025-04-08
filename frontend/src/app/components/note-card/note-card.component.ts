import { Component, input } from '@angular/core';
import { Note } from '../../models/note';
import { UserCardComponent } from '../common/user-card/user-card.component';

@Component({
  selector: 'app-note-card',
  imports: [UserCardComponent],
  templateUrl: './note-card.component.html',
  styleUrl: './note-card.component.css'
})
export class NoteCardComponent {
  note = input.required<Note>();


}
