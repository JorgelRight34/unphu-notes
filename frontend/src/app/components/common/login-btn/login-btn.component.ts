declare var google: any;

import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-login-btn',
  imports: [],
  templateUrl: './login-btn.component.html',
  styleUrl: './login-btn.component.css',
})
export class LoginBtnComponent {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    const googleClientId = environment.googleClientId;
    // eslint-disable-next-line @typescript-eslint/no-unsafe-call
    google.accounts.id?.initialize({
      client_id: googleClientId,
      callback: this.handleLogin.bind(this),
    });

    // eslint-disable-next-line @typescript-eslint/no-unsafe-call
    google.accounts?.id.renderButton(document.getElementById('login-btn'), {
      theme: 'filled_blue',
      size: 'large',
      text: 'continue_with',
      shape: 'pill',
    });
  }

  handleLogin(token: any) {
    this.authService.login(token.credential).subscribe({
      next: () => this.router.navigate(['/']),
    });
  }
}
