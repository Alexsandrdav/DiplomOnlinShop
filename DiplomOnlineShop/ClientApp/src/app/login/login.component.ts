
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  http: HttpClient;
  baseUrl: string;
  activatedRoute: Router;

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, activatedRoute: Router) {
    this.http = httpClient;
    this.baseUrl = baseUrl;
    this.activatedRoute = activatedRoute;
  }

  onSubmit(form: NgForm) {
    var loginForm = new LoginForm();
    loginForm.password = form.value.password;
    loginForm.email = form.value.email;

    this.http.post<LoginResult>(this.baseUrl + 'admin/login', loginForm).subscribe({
      next: data => {
        localStorage.setItem("token", data.token);
        this.activatedRoute.navigate(["/orders"]);
      },
      error: error => {
        alert("Email or password are incorrect");
      }
    });
  }



  }
export class LoginForm {
  email: string;
  password: string;
  

  constructor() {
    this.email = "";
    this.password = "";
    
  }
}
export interface LoginResult
{
  token: string;
}
