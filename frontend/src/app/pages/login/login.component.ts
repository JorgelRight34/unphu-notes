declare var google: any;

import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(private authService: AuthService) { }

  ngOnInit() {
    const googleClientId = '932306178138-c8sf5ekiepq7cnlbiqjgfn6142pn854s.apps.googleusercontent.com';
    // eslint-disable-next-line @typescript-eslint/no-unsafe-call
    google.accounts.id?.initialize({
      client_id: googleClientId,
      callback: this.handleLogin.bind(this),
    });

    // eslint-disable-next-line @typescript-eslint/no-unsafe-call
    google.accounts?.id.renderButton(document.getElementById("login-btn"), {
      theme: "filled_blue",
      size: "large",
      text: "continue_with",
      shape: "pill",
    });
  }

  handleLogin(token: any) {
    this.authService.login(token.credential).subscribe({
      next: (data) => console.log(data)
    })
  }
}
