import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Note } from '../models/note';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class NoteService {
  baseUrl = environment.apiUrl + 'notes';

  constructor(private http: HttpClient) {}

  createNote(note: Note) {
    return this.http.post<Note>(this.baseUrl, note);
  }

  setNoteFile(file: File) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post(this.baseUrl + 'add-file', formData);
  }
}
