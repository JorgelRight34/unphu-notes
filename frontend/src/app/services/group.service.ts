import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Group } from '../models/group';
import { Note } from '../models/note';
import { GroupMember } from '../models/groupMember';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  baseUrl = environment.apiUrl + '/groups';

  constructor(private http: HttpClient) {}

  getEnrolledGroups() {
    // Get current user enrolled subjects
    return this.http.get<Group[]>(this.baseUrl);
  }

  getGroupNotes(id: number) {
    // Get the notes related to the group with the correspondig id
    return this.http.get<Note[]>(this.baseUrl + `${id}/notes`);
  }

  getGroupMembers(id: number) {
    return this.http.get<GroupMember[]>(this.baseUrl + `${id}/members`);
  }
}
