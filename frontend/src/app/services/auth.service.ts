import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { User } from '../models/user';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  user = signal<User | null>(null);

  constructor(private http: HttpClient) { }

  login(token: string) {
    // Log user in, save the token and set the user.
    return this.http.post<User>(this.baseUrl + `login`, { token }).pipe(
      tap((data) => {
        this.user.set(data); // Set user
        localStorage.setItem('accessToken', data.token); // Save token
        return data;
      })
    );
  }

  logout() {
    this.user.set(null); // Set user to null
    localStorage.removeItem('accessToken'); // Remove token
  }

  loadUser() {
    // Get user information (username, email, etc.)
    return this.http
      .get<User>(this.baseUrl)
      .pipe(tap((data) => this.user.set(data)));
  }
}
