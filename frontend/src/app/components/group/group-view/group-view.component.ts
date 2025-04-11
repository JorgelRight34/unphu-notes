import { Component, computed, signal } from '@angular/core';
import { GroupService } from '../../../services/group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Note } from '../../../models/note';
import { GroupMember } from '../../../models/groupMember';
import { NoteCardComponent } from '../../note/note-card/note-card.component';
import { map, of } from 'rxjs';
import { CreateNoteButtonComponent } from '../../note/create-note-button/create-note-button.component';
import { Group } from '../../../models/group';
import { MemberListComponent } from '../member-list/member-list.component';

@Component({
  selector: 'app-subject-view',
  imports: [NoteCardComponent, CreateNoteButtonComponent, MemberListComponent],
  templateUrl: './group-view.component.html',
  styleUrl: './group-view.component.css',
})
export class SubjectViewComponent {
  week = computed(() => this.groupService.currentWeek());
  weekNotes = computed<Note[]>(
    () => this.group()?.notes.filter((note) => note.week === this.week()) || []
  );
  group = signal<Group | null>(null);
  groupId = signal<number | null>(null);
  loading = signal(true);

  constructor(
    private groupService: GroupService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.params
      .pipe(
        map((params) => {
          const groupId = Number(params['id']);

          if (isNaN(groupId)) {
            this.router.navigate(['/']);
            return of(null); // Return empty observable
          }

          this.groupId.set(groupId);

          this.groupService.getGroup(groupId).subscribe({
            next: (data) => {
              this.group.set(data);
            },
          });

          return params;
        })
      )
      .subscribe();
  }

  changeWeekBy(n: number) {
    if (this.week() + n < 1) return;
    this.groupService.currentWeek.update((prev) => prev + n);
  }

  handleAddNote(note: Note) {
    const g = this.group();
    if (g) this.group.update(() => ({ ...g, notes: [...g.notes, note] }));
  }

  handleOnDelete(note: Note) {
    const g = this.group();
    if (g)
      this.group.update(() => ({
        ...g,
        notes: [...g.notes.filter((n) => n.id != note.id)],
      }));
  }
}
