import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      switch (error.status) {
        case 0:
          // Network error or server unreachable
          toastr.error(
            'No se pudo conectar con el servidor. Verifica tu conexión a internet.'
          );
          break;
        case 400:
          toastr.warning('Solicitud incorrecta. Verifica los datos enviados.');
          break;
        case 401:
          toastr.warning(
            'No estás autorizado para realizar esta acción. Inicia sesión.'
          );
          break;
        case 403:
          toastr.warning('No tienes permiso para acceder a este recurso.');
          break;
        case 404:
          toastr.info('Recurso no encontrado.');
          break;
        case 409:
          toastr.warning(
            'Conflicto detectado. Puede que ya exista el recurso.'
          );
          break;
        case 422:
          toastr.warning('Entidad no procesable. Revisa los datos enviados.');
          break;
        case 500:
          toastr.error('Oops! Ha ocurrido un error interno en el servidor.');
          break;
        case 503:
          toastr.error('Servicio no disponible. Intenta nuevamente más tarde.');
          break;
        default:
          toastr.error('Ha ocurrido un error inesperado.');
          break;
      }

      return throwError(() => error);
    })
  );
};
