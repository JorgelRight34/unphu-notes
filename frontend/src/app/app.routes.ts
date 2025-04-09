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
        path: 'group/:id',
        loadComponent: () =>
          import('./components/group/group-view/group-view.component').then(
            (m) => m.SubjectViewComponent
          ),
      },
    ],
  },
  /* temporary fix
  {
    path: '**',
    redirectTo: '/',
  },
  */
];
