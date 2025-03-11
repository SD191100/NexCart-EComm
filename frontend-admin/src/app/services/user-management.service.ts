import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth-service.service';

@Injectable({
  providedIn: 'root'
})
export class UserManagementService {
  private userApiUrl = 'http://localhost:5087/api/User'; // Replace with your API URL for User

  constructor(private http: HttpClient , private authService:AuthService) {}

  // Decode JWT token and return decoded object


  // Get the user profile by ID (from the server)

  getAllUsers(): Observable<any>{
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.get<any>(`${this.userApiUrl}` , {headers});
  }
  getUserProfile(userId: number): Observable<any> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.get<any>(`${this.userApiUrl}/${userId}` ,{headers});
  }

  // Update the user profile
  updateUserProfile(userId: number, userData: any): Observable<any> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.put<any>(`${this.userApiUrl}/Admin/${userId}`, userData ,{headers});
  }

  // Delete user account
  deleteUserAccount(userId: number): Observable<any> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
    return this.http.delete<any>(`${this.userApiUrl}/${userId}`, {headers});
  }

  // Logout the user (clear session, localStorage, etc.)

  // Get user ID from the token (used for dynamic profile loading)

  // Get all addresses of a user by userId
}
