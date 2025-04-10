import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'schedule',
})
export class GroupSchedulePipe implements PipeTransform {
  transform(value: string, ...args: unknown[]): unknown {
    return value.replace(/\((.*?)\)/g, ' | ').slice(0, -3);
  }
}
