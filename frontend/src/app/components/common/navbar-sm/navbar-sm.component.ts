import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { GroupService } from '../../../services/group.service';

@Component({
  selector: 'app-navbar-sm',
  imports: [RouterModule],
  templateUrl: './navbar-sm.component.html',
  styleUrl: './navbar-sm.component.css',
})
export class NavbarSmComponent {
  constructor(private router: Router, private groupService: GroupService) {}

  handleNavigateGroup() {
    const currentGroup = this.groupService.currentGroup();
    console.log(currentGroup);
    if (currentGroup) {
      this.router.navigate([`/group`, currentGroup.id]);
    }
  }
}
