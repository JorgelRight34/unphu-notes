import { Directive, ElementRef, HostListener, output } from '@angular/core';

@Directive({
  selector: '[appFileUpload]',
})
export class FileUploadDirective {
  fileSelected = output<File>();

  constructor(private el: ElementRef) {}

  @HostListener('click') onClick() {
    this.triggerFileInput();
  }

  triggerFileInput() {
    const fileInput = document.createElement('input');
    fileInput.type = 'file';
    fileInput.style.display = 'none';

    fileInput.addEventListener('change', (event) => {
      const file = (event.target as HTMLInputElement).files?.[0];
      if (file) {
        this.fileSelected.emit(file);
      }
    });

    document.body.appendChild(fileInput);
    fileInput.click();
    document.body.removeChild(fileInput);
  }
}
