import { Component, OnInit, Inject, destroyPlatform } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { error } from 'protractor';
import { Product } from '../models/goods/product';
import { ProductService } from '../services/product/product.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: Product[];
  private _productService: ProductService;
  success: boolean;
  error: string;

  constructor(private productService: ProductService) {
    this._productService = productService;
  }

  get(): void {
    this._productService.listAllProducts().subscribe(products => this.products = products);
  }

  post(name: string, hsnCode: string, price: string, description: string): void {

    name = name.trim();
    hsnCode = hsnCode.trim();
    description = description.trim();


    //var newProduct: Product = { name: name, hsnCode: hsnCode, price: price, description: description };

    this._productService.createProduct({ name, hsnCode, price, description} as Product)
      .pipe(finalize(() => { }))
      .subscribe(result => {
        if (result) {
          this.success = true;
          this.get();
        }
      }, error => { this.error = error; });
  }

  ngOnInit() {
    this.get();
  }

}
