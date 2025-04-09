import { Component, model } from '@angular/core';

@Component({
  selector: 'app-modal',
  imports: [],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css'
})
export class ModalComponent {
  isOpen = model<boolean>(false);

  open() {
    this.isOpen.set(true)
  }

  close() {
    this.isOpen.set(false)
  }
}
