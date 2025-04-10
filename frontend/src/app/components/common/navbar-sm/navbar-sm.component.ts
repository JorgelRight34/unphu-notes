import { Component, computed } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { GroupService } from '../../../services/group.service';
import { CreateNoteButtonComponent } from "../../note/create-note-button/create-note-button.component";

@Component({
  selector: 'app-navbar-sm',
  imports: [RouterModule, CreateNoteButtonComponent],
  templateUrl: './navbar-sm.component.html',
  styleUrl: './navbar-sm.component.css',
})
export class NavbarSmComponent {
  currentGroup = computed(() => this.groupService.currentGroup());
  week = computed(() => this.groupService.currentWeek());

  constructor(private groupService: GroupService) { }
}
