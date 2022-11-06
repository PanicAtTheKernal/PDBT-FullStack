import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  @Input() collapseState:boolean = true;
  private sidebarWidth: string = '330px'; 

  constructor() { 
    window.addEventListener('resize', () => {
      this.getWidth();
    });
  }

  ngOnInit(): void {
  }

  getWidth(): string {
    let sidebarWidth: string = (window.innerWidth < 600) ? '100vh' : this.sidebarWidth;   
    return this.collapseState ? '0px' : sidebarWidth;
  }
}
