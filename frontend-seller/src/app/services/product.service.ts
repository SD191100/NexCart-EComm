import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = 'http://localhost:5087/api/product'; // Update with your actual backend URL

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) { }

  getProducts(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  deleteProduct(productId: number): Observable<void> {
    let token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });

    return this.http.delete<void>(`${this.apiUrl}/${productId}`, { headers });
  }

  addProduct(productData: any): Observable<any> {
    let token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });

    return this.http.post<any>(this.apiUrl, productData, { headers });
  }
  //
  //updateProduct(product: any): Observable<any> {
  //  return this.http.put(
  //    `http://localhost:5087/api/product/${product.productId}`,
  //    product,
  //    { headers },
  //  );
  //}

  updateProduct(product: any): Observable<any> {
    // Ensure the payload matches the DTO structure expected by the backend
    const payload = {
      id: product.productId, // Map 'productId' to 'id' for the request
      name: product.name,
      price: product.price,
      stock: product.stock,
      categoryId: product.categoryId,
    };

    let token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });

    return this.http.put(
      `http://localhost:5087/api/product/${product.productId}`,
      payload,
      { headers }
    );
  }

  getCategories(): Observable<{ id: number; name: string }[]> {
    return this.http.get<{ id: number; name: string }[]>(
      'http://localhost:5087/api/category',
    );
  }
}
