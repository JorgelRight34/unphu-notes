import { Component } from '@angular/core';
import { LoginBtnComponent } from '../../components/common/login-btn/login-btn.component';

@Component({
  selector: 'app-login',
  imports: [LoginBtnComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  constructor() {}
}
