import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Console } from 'console';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent implements OnInit {
  
  isExpanded = false;
  isAdmin = false;
    
  constructor(private router: Router) {}
  
  ngOnInit(){
    this.isAdmin = this.checkIsAdmin();
  }
  
  collapse() {
    this.isExpanded = false;
  }

  checkIsAdmin():boolean{
    console.log(localStorage.getItem('token'));
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
      var userRole = payLoad.role;
      if (userRole == 'Administrator') {
        return true;
      }
    return false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
