import { Component, OnInit } from '@angular/core';
import { faBell, IconDefinition } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  faBell: IconDefinition = faBell;

  constructor() { }

  ngOnInit(): void {
  }

}
