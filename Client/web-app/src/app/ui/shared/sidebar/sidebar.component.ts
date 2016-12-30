import { Component, OnInit } from '@angular/core';

import { AuthService } from '../../../services';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  isUserLoggedIn: boolean;
  showMenu: string = '';

  constructor(private _authService: AuthService) { }

  ngOnInit() {
      this.isUserLoggedIn = this._authService.isLoggedIn();
      this._authService.authEvent.subscribe(isLoggedOn => this.isUserLoggedIn = isLoggedOn);
  }

  addExpandClass(element: string): void {
      if (element === this.showMenu) {
          this.showMenu = '0';
      } else {
          this.showMenu = element;
      }
  }

  logout(): void {
      this._authService.logout();
  }
}
