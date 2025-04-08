import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cloudinarySecure',
})
export class CloudinarySecurePipe implements PipeTransform {
  transform(value: string | null, ...args: unknown[]): unknown {
    if (!value) return;

    return value
      .replace('http', 'https')
      .replace('/upload/', '/upload/q_auto,f_auto/');
  }
}
