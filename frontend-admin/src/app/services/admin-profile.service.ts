import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UserProfile {
  userId: number;
  firstName: string;
  lastName: string;
  email: string;
  contactNumber: string;
  isActive: boolean;
  role: string;
  passwordHash?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AdminProfileService {
  private apiUrl = "http://localhost:5087/api";

  constructor(private http: HttpClient) {}

  // Get user profile by ID
  getUserProfile(userId: number): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.apiUrl}/User/${userId}`);
  }

  // Update user profile
  updateUserProfile(userId: number, profileData: Partial<UserProfile>): Observable<UserProfile> {
    return this.http.put<UserProfile>(`${this.apiUrl}/User/${userId}`, profileData);
  }

  // Change password
  changePassword(userId: number, currentPassword: string, newPassword: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/User/change-password`, {
      userId,
      currentPassword,
      newPassword
    });
  }

  // Logout (typically handled by authentication service)
  logout(): void {
    // Remove auth token from local storage
    localStorage.removeItem('jwtToken');
    // Redirect to login page or trigger logout in auth service
    // This is usually handled by an AuthService
  }
}
