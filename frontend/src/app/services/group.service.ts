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
  baseUrl = environment.apiUrl + 'groups/';
  private groups = signal<Group[]>([]);
  private fetched = signal<boolean>(false);

  constructor(private http: HttpClient) { }

  getEnrolledGroups() {
    // Get current user enrolled subjects if not fetched
    if (this.fetched()) return this.groups();

    // Fetch the groups from the API and set them to the signal
    this.http.get<Group[]>(this.baseUrl).subscribe({
      next: (data) => {
        this.groups.set(data);
        this.fetched.set(true);
      },
    });

    return this.groups();
  }

  getGroup(id: number) {
    // Get the group with the corresponding id
    return this.http.get<Group>(this.baseUrl + `${id}`);
  }

  getGroupNotes(id: number) {
    // Get the notes related to the group with the correspondig id
    return this.http.get<Note[]>(this.baseUrl + `${id}/notes`);
  }

  getGroupMembers(id: number) {
    return this.http.get<GroupMember[]>(this.baseUrl + `${id}/members`);
  }
}
