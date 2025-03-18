import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userApiUrl = 'https://localhost:7251/api/User'; // Replace with your API URL for User
  private addressApiUrl = 'https://localhost:7251/api/Address'; // Replace with your API URL for Address

  constructor(private http: HttpClient) {}

  // Decode JWT token and return decoded object


  // Get the user profile by ID (from the server)
  getUserProfile(userId: number): Observable<any> {
    return this.http.get<any>(`${this.userApiUrl}/${userId}`);
  }

  // Update the user profile
  updateUserProfile(userId: number, userData: any): Observable<any> {
    return this.http.put<any>(`${this.userApiUrl}/${userId}`, userData);
  }

  // Delete user account
  deleteUserAccount(userId: number): Observable<any> {
    return this.http.delete<any>(`${this.userApiUrl}/${userId}`);
  }

  // Logout the user (clear session, localStorage, etc.)

  // Get user ID from the token (used for dynamic profile loading)

  // Get all addresses of a user by userId
  getUserAddresses(userId: number| null): Observable<any[]> {
    return this.http.get<any[]>(`${this.addressApiUrl}/user/${userId}`);
  }

  // Add a new address for the user
  addAddress(addressData: any): Observable<any> {
    return this.http.post<any>(this.addressApiUrl, addressData);
  }

  // Update an existing address
  updateAddress(addressId: number, addressData: any): Observable<any> {
    return this.http.put<any>(`${this.addressApiUrl}/${addressId}`, addressData);
  }

  // Delete an address for the user
  deleteAddress(addressId: number): Observable<any> {
    return this.http.delete<any>(`${this.addressApiUrl}/${addressId}`);
  }
}
