import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { User } from '../models/user';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  user = signal<User | null>(null);

  constructor(private http: HttpClient) { }

  login(token: string) {
    console.log(token);
    return this.http.post<User>(this.baseUrl + `auth/login`, { token }).pipe(
      tap(data => {
        this.user.set(data);
        localStorage.setItem("accessToken", data.token)
        return data;
      })
    )
  }

  logout() {
    localStorage.removeItem("accessToken");
  }
}
