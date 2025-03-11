import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product/product';
@Injectable({
  providedIn: 'root',
})
export class ProductBrowsingService {
  private readonly apiUrl = 'http://localhost:5087/api/Product/browse';

  constructor(private http: HttpClient) { }

  getProducts(queryParams: any): Observable<any> {
    return this.http.get<any>(this.apiUrl, { params: queryParams });
  }

  getProductsByCategoryId(categoryId: number): Observable<any[]> {
    const sortOption = 'price_asc'; // Default sort option
    const page = 1; // Default page
    const pageSize = 10; // Default page size

    return this.http.get<any[]>(
      `${this.apiUrl}/browse?categoryId=${categoryId}&sortOption=${sortOption}&page=${page}&pageSize=${pageSize}`,
    );
  }

  getProductById(productId: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${productId}`);
  }
}
