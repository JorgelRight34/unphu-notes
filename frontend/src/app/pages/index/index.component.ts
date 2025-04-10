import { Component, signal } from '@angular/core';
import { NavbarComponent } from '../../components/common/navbar/navbar.component';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { GroupListComponent } from '../../components/group/group-list/group-list.component';
import { filter } from 'rxjs';
import { NavbarSmComponent } from '../../components/common/navbar-sm/navbar-sm.component';

@Component({
  selector: 'app-index',
  imports: [
    NavbarComponent,
    GroupListComponent,
    RouterOutlet,
    NavbarSmComponent,
  ],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css',
})
export class IndexComponent {
  constructor(private router: Router) {}
  isActiveRoute = signal<boolean>(false);

  ngOnInit() {
    this.isActiveRoute.set(this.router.url.includes('/group'));
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isActiveRoute.set(this.router.url.includes('/group'));
      });
  }
}
