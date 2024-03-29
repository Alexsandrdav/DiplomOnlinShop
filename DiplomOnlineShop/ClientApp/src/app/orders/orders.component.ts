import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../fetch-data/fetch-data.component';

@Component({
  selector: 'orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  public orders: Order[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    var token = localStorage.getItem("token");
    var headers = { 'Authorization': 'Bearer ' + token };
    http.get<Order[]>(baseUrl + 'orders', { headers } ).subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }
}

interface Order {
  email: string;
  phone: string;
  date: Date;
  id: number;
  total: number;
  products: Product[];
}
