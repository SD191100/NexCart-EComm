import { Injectable } from '@angular/core';
import { Cart } from '../models/cart/cart.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth-service.service';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseUrl = 'https://localhost:7251/api/cart'; // Update with your API URL
  private cartSubject = new BehaviorSubject<Cart | null>(null);

  cart$ = this.cartSubject.asObservable();

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) { }

  // Fetch the cart for a specific user
  getCart(userId: number): Observable<Cart> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.get<Cart>(`${this.baseUrl}/${userId}`, { headers });
  }

  // Load the cart and emit it to the cartSubject
  loadCart(userId: number): void {
    this.getCart(userId).subscribe((cart) => this.cartSubject.next(cart));
  }

  // Add an item to the cart
  addToCart(
    userId: number,
    productId: number,
    quantity: number,
  ): Observable<Cart> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.post<Cart>(
      `${this.baseUrl}/${userId}/add`,
      {
        productId,
        quantity,
      },
      { headers },
    );
  }

  // Update the quantity of a cart item
  updateCartItem(cartItemId: number, quantity: number): Observable<Cart> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });

    console.log("update cart hit :" + token)
    return this.http.put<Cart>(`${this.baseUrl}/item/${cartItemId}/?Quantity=${quantity}`, {} ,{ headers });
  }

  // Remove an item from the cart
  removeCartItem(cartItemId: number): Observable<Cart> {
const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.delete<Cart>(`${this.baseUrl}/item/${cartItemId}`, { headers });
  }

  // Clear the cart
  clearCart(userId: number | null ): Observable<void> {
const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    console.log("clearing Cart")
    return this.http.delete<void>(`${this.baseUrl}/${userId}/clear`, { headers });
  }
}
