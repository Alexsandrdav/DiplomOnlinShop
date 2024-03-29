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
  productIds: number[] = [];
  http: HttpClient;
  baseUrl: string;

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = httpClient;
    this.baseUrl = baseUrl;

    this.http.get<Product[]>(baseUrl + 'products').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }
  onSubmit(form: NgForm) {
    var productOrder = new ProductOrder();
    productOrder.phone = form.value.phone;
    productOrder.email = form.value.email;
    productOrder.productIds = this.productIds;

    this.http.post(this.baseUrl + 'orders', productOrder).subscribe(() => {
      alert("Order created");
    });
  }

  onChange(product: Product) {
    if (this.productIds.includes(product.id)) {
      this.productIds = this.productIds.filter((item) => item !== product.id);
    } else {
      this.productIds.push(product.id);
    }
  }

  showCurrentPrice() {
    let self = this;
    let sum = 0;
    this.productIds.forEach(function (productId) {
      let res = self.products.find(function (product) {
        if (productId == product.id) {
          return true;
        } else { return false; }
      });

      sum = sum + res!.price;
    });

    return sum;
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


