import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Group } from '../models/group';
import { Note } from '../models/note';
import { GroupMember } from '../models/groupMember';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  baseUrl = environment.apiUrl + '/groups';
  private groups = signal<Group[]>([]);

  constructor(private http: HttpClient) { }

  getEnrolledGroups() {
    // Get current user enrolled subjects if not fetched
    if (this.groups().length > 0) return this.groups;

    // Fetch the groups from the API and set them to the signal
    this.http.get<Group[]>(this.baseUrl).subscribe({
      next: (data) => this.groups.set(data),
    });

    return this.groups;
  }

  getGroupNotes(id: number) {
    // Get the notes related to the group with the correspondig id
    return this.http.get<Note[]>(this.baseUrl + `${id}/notes`);
  }

  getGroupMembers(id: number) {
    return this.http.get<GroupMember[]>(this.baseUrl + `${id}/members`);
  }
}
