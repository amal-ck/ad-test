import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MdlResponse } from '../../../models/commonModels';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-error-popup',
  imports: [CommonModule],
  templateUrl: './error-popup.html',
  styleUrl: './error-popup.css'
})
export class ErrorPopup {
  @Input() errorData: MdlResponse | null = null;

  @Output() close = new EventEmitter<void>();

  closeErrorPopup() {
    this.errorData = null;
    this.close.emit();
  }
}
