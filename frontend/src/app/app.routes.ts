import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: '',
    loadComponent: () =>
      import('./pages/index/index.component').then((m) => m.IndexComponent),
    canActivate: [authGuard],
    children: [
      {
        path: 'subject:/id',
        loadComponent: () => import('./components/subject-view/subject-view.component').then((m) => m.SubjectViewComponent),
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/',
  },
];
