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

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  toggleMenu(): void {
    this.menuOpen = !this.menuOpen;
  }

  isDropdownOpen: boolean = false;

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  ngOnInit(): void {
    this.userData = JSON.parse(String(localStorage.getItem('user')));

    console.log(this.userData, 'user');
  }
}
