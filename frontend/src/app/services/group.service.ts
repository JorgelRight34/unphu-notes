import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Group } from '../models/group';
import { Note } from '../models/note';
import { GroupMember } from '../models/groupMember';
import { finalize, map, single, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  baseUrl = environment.apiUrl + 'groups/';
  currentGroup = signal<Group | undefined>(undefined);
  currentWeek = signal(1);
  private groups = signal<Group[]>([]);
  private cacheGroups: Record<number, Group> = {};
  private fetched = signal<boolean>(false);

  constructor(private http: HttpClient) {}

  getEnrolledGroups() {
    // Get current user enrolled subjects if not fetched
    if (this.fetched()) return this.groups();

    // Fetch the groups from the API and set them to the signal
    this.http.get<Group[]>(this.baseUrl).subscribe({
      next: (data) => {
        this.groups.set(data);
        this.currentGroup.set(data[0]);
        this.fetched.set(true);
      },
    });

    return this.groups();
  }

  getGroup(id: number) {
    // Get the group with the corresponding id
    return this.http.get<Group>(this.baseUrl + `${id}`).pipe(
      map((data) => {
        this.groups.update((prev) =>
          prev.map((group) => {
            if (group.id !== data.id) return group;
            return data;
          })
        );
        this.cacheGroups[id] = data;
        return data;
      })
    );
  }

  getGroupNotes(id: number) {
    // Get the notes related to the group with the correspondig id
    return this.http.get<Note[]>(this.baseUrl + `${id}/notes`);
  }

  getGroupMembers(id: number) {
    return this.http.get<GroupMember[]>(this.baseUrl + `${id}/members`);
  }

  selectGroup(id: number) {
    this.currentWeek.set(1);
    this.currentGroup.set(this.groups().find((g) => g.id === id));
  }

  selectDefaultGroup() {
    // Select the first group in the list of groups
    const group = this.groups()[0];
    if (group) {
      this.currentGroup.set(group);
      return group;
    }
    return undefined;
  }
}
