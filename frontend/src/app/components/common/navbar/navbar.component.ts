import { Component, computed } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { UserCardComponent } from '../user-card/user-card.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [UserCardComponent, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  user = computed(() => this.authService.user());

  constructor(private authService: AuthService) {}
}
