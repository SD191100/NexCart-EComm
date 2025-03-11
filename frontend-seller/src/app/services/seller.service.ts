import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class SellerService {
  private apiUrl = 'http://localhost:5087/api/Seller';

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) { }

  // GET: Fetch seller by ID
  getSellerById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // PUT: Update seller by ID
  updateSeller(id: number, sellerData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, sellerData);
  }

  // GET: Fetch seller by userId
  getSellerByUserId(userId: number | null): Observable<any> {
    let token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });

    return this.http.get<any>(`${this.apiUrl}/user/${userId}`, { headers });
  }

  // POST: Add a new seller
  addSeller(sellerData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, sellerData);
  }

  // GET: Fetch order details for a seller by sellerId
  getOrderDetailsBySellerId(sellerId: number): Observable<any> {
    let token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.get<any>(
      `${this.apiUrl}/seller/${sellerId}/order-details`,
      { headers}
    );
  }

  // GET: Fetch products for a seller by sellerId
  getProductsBySellerId(sellerId: number): Observable<any> {
    let token = this.authService.getToken();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.get<any>(`${this.apiUrl}/seller/${sellerId}/products`, {headers});
  }
}
