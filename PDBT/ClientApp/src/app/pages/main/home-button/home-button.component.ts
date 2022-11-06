import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-button',
  templateUrl: './home-button.component.html',
  styleUrls: ['./home-button.component.css']
})
export class HomeButtonComponent implements OnInit {
  hideTitle: boolean = true;
  private readonly THRESHOLD: number = 500;

  constructor() {
    window.addEventListener('resize', () => {
      this.hideTitle = window.innerWidth > this.THRESHOLD;
    })
  }

  ngOnInit(): void {
  }

}
