import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  userData: any = '';
  menuOpen: boolean = false;
  currActive: string | undefined = 'home';

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.userData = JSON.parse(String(localStorage.getItem('user')));
    this.currActive = this.activatedRoute.routeConfig?.path;
  }

  toggleMenu(): void {
    this.menuOpen = !this.menuOpen;
  }

  isDropdownOpen: boolean = false;

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
