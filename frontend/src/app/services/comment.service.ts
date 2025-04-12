import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { NoteComment } from '../models/noteComment';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  baseUrl = environment.apiUrl + 'comments/';

  constructor(private http: HttpClient) { }

  createNoteComment(newComment: { content: string; noteId: number }) {
    return this.http.post<NoteComment>(this.baseUrl, newComment);
  }
}
