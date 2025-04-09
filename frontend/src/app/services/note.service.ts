import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Note } from '../models/note';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs';
import { NoteComment } from '../models/noteComment';

@Injectable({
  providedIn: 'root',
})
export class NoteService {
  baseUrl = environment.apiUrl + 'notes/';

  constructor(private http: HttpClient) { }

  createNote(subjectGroupId: number, file: File) {
    const data = new FormData();
    data.append('file', file);
    data.append('subjectGroupId', String(subjectGroupId));

    return this.http.post<Note>(this.baseUrl, data);
  }

  deleteNote(id: number) {
    return this.http.delete(this.baseUrl + id);
  }

  getNoteComments(id: number) {
    return this.http.get<NoteComment[]>(this.baseUrl + id + '/comments');
  }
}
