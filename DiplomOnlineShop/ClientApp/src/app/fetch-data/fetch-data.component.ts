import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent {
  public products: Product[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Product[]>(baseUrl + 'products').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }
  onSubmit(form: NgForm) {
    var GOWNO = new ProductOrder();

    GOWNO.phone = form.value.phone;
    GOWNO.email = form.value.email;


  }
}

export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
}

export class ProductOrder {
  email: string;
  phone: string;
  productIds: number[];

  constructor() {
    this.email = "";
    this.phone = "";
    this.productIds = [];
  }
}


