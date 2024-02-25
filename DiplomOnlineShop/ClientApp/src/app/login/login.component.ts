
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  http: HttpClient;
  baseUrl: string;

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = httpClient;
    this.baseUrl = baseUrl;

  }

  onSubmit(form: NgForm) {
    var loginForm = new LoginForm();
    loginForm.password = form.value.password;
    loginForm.email = form.value.email;

    this.http.post(this.baseUrl + 'admin/login', loginForm).subscribe({
      next: data => {

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
