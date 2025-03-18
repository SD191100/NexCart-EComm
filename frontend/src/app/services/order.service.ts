import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth-service.service';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private readonly apiUrl = 'https://localhost:7251/api/order';

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) { }

  getCheckout(body: any): Observable<any> {
    let token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.post<any>(`${this.apiUrl}/checkout`, body, { headers });
  }

  processPayment(paymentData: {
    paymentMethod: string;
    amount: number;
  }): Observable<any> {
    let token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.post(`${this.apiUrl}/process-payment`, paymentData, {
      headers,
    });
  }

  confirmOrder(orderData: any): Observable<any> {
    let token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.post(`${this.apiUrl}/confirm-order`, orderData, {
      headers,
    });
  }

  getOrdersByUserId(): Observable<any> {
    let token = this.authService.getToken();
    let userId = this.authService.getUserId();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.get(`${this.apiUrl}/user/${userId}`, {
      headers,
    });
  }
}
