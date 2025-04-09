import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/common/navbar/navbar.component';
import { RouterOutlet } from '@angular/router';
import { GroupListComponent } from '../../components/group/group-list/group-list.component';

@Component({
  selector: 'app-index',
  imports: [NavbarComponent, GroupListComponent, RouterOutlet],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css',
})
export class IndexComponent {}
