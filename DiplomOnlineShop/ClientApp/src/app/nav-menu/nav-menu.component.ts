import { Component } from '@angular/core';
import { jwtDecode } from "jwt-decode";
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  activatedRoute: Router;
  public email: string = '';

  constructor( activatedRoute: Router) {

    this.activatedRoute = activatedRoute;
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    localStorage.removeItem("token");
    this.activatedRoute.navigate(["/"]);
  }
  authorized() {
    var token = localStorage.getItem("token");
    if (token == null) {
      this.email = '';
      return false;
    }
    else {
      var decoded = jwtDecode<EmailToken>(token);
      this.email = decoded.email;
      return true;
    }


  }
}

interface EmailToken {
  email: string;
}
