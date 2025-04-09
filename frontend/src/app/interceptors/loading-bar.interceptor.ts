import { HttpInterceptorFn } from '@angular/common/http';
import { LoadingBarService } from '../services/loading-bar.service';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';

export const loadingBarInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingBarService = inject(LoadingBarService);
  let requests = 0;
  requests += 1;
  loadingBarService.show();

  return next(req).pipe(
    finalize(() => {
      requests -= 1;
      if (requests === 0) {
        setTimeout(() => {
          loadingBarService.hide();
        }, 500);
      }
    })
  );
};
