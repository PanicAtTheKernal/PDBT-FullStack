import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { faBars, faBell, IconDefinition } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Output() sidebarbtn = new EventEmitter;

  faBars: IconDefinition = faBars;
  faBell: IconDefinition = faBell;

  constructor() { }

  ngOnInit(): void {
  }

  onSideBarClicked(): void {
    this.sidebarbtn.emit();
  }

}
