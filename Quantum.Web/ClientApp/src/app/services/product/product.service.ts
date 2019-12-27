import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../../models/goods/product';
import { Observable, of } from 'rxjs';
import { tap, catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private _baseUrl: string;
  public products: Product[];
  public headers: Headers;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  listAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this._baseUrl + 'products/index')
      .pipe(tap(), catchError(this.handleError<Product[]>('listAllProducts', [])));
  }

  //createProduct(product: any): Observable<Product> {
  //  const header = new HttpHeaders().set('Content-type', 'application/json');
  //  return this.http.post<Product>(this._baseUrl + 'products/create', JSON.stringify(product), { headers: header })
  //    .pipe(map((res: any) => res.json()), catchError(this.handleError<Product>('createProduct')));
  //}

  createProduct(product: any): Observable<Product> {
    return this.http.post<Product>(this._baseUrl + 'products/create', product, { headers: new HttpHeaders().set('Content-Type', 'application/json') })
      .pipe(tap(), catchError(this.handleError<Product>('createProduct')));
  }

  /**
* Handle Http operation that failed.
* Let the app continue.
* @param operation - name of the operation that failed
* @param result - optional value to return as the observable result
*/
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}
