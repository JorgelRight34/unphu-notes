import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Note } from '../models/note';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs';
import { NoteComment } from '../models/noteComment';
import { DomSanitizer } from '@angular/platform-browser';
import { NoteFile } from '../models/noteFile';
import { GroupService } from './group.service';

@Injectable({
  providedIn: 'root',
})
export class NoteService {
  baseUrl = environment.apiUrl + 'notes/';

  constructor(private http: HttpClient, private sanitizer: DomSanitizer, private groupService: GroupService) { }

  createNote(
    subjectGroupId: number,
    files: File[],
    week: number,
    title: string
  ) {
    const data = new FormData();
    files.map((file) => data.append('files', file));

    data.append('subjectGroupId', String(subjectGroupId));
    data.append('week', String(week));
    data.append('title', title);

    return this.http.post<Note>(this.baseUrl, data).pipe(
      tap((note) => {
        this.groupService.addGroupNotes(subjectGroupId, [note]);
        return note;
      })
    );
  }

  getNoteFileDownloadUrl(noteFile: NoteFile) {
    const downloadUrl = this.sanitizer.bypassSecurityTrustUrl(
      this.getDownloadUrl(noteFile.url)
    );
    return downloadUrl;
  }

  deleteNote(id: number) {
    return this.http.delete(this.baseUrl + id);
  }

  downloadNote(noteFile: NoteFile) {
    this.http
      .get(this.getDownloadUrl(noteFile.url), { responseType: 'blob' })
      .subscribe((blob) => {
        const blobUrl = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = blobUrl;
        a.download = 'note';
        a.style.display = 'none';
        document.body.appendChild(a);
        a.click();

        // Cleanup
        setTimeout(() => {
          document.body.removeChild(a);
          URL.revokeObjectURL(blobUrl);
        }, 100);
      });
  }

  getNoteComments(id: number) {
    return this.http.get<NoteComment[]>(this.baseUrl + id + '/comments');
  }

  private getDownloadUrl(url: string): string {
    if (url.includes('cloudinary.com')) {
      // For Cloudinary URLs
      if (url.includes('upload/')) {
        const parts = url.split('upload/');
        return `${parts[0]}upload/fl_attachment/${parts[1]}`;
      }
      return url.includes('?')
        ? `${url}&fl_attachment`
        : `${url}?fl_attachment`;
    }
    return url;
  }
}
