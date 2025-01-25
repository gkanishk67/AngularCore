import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-notification',
  template: `
    <div class="m-xl-4 notification-container rounded-3" *ngIf="message">
      {{ message }}
    </div>
  `,
  styles: [`
    .notification-container {
      position: fixed;
      top: 0;
      right: 0;
      background-color: green;
      color: #fff;
      padding: 10px;
      z-index: 999;
    }
  `]
})
export class NotificationComponent {
  @Input() message: string = "";
}
