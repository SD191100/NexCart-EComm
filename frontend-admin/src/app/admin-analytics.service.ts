import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth-service.service';
// import { environment } from '../../../environments/environment';

export interface DashboardAnalytics {
  totalUsers: number;
  totalSellers: number;
  totalOrders: number;
  totalProducts : number;
  totalCategories : number;
  orderTrend: { date: Date; count: number }[];
  revenueTrend: { date: Date; totalAmount: number }[];
  
}

@Injectable({
  providedIn: 'root'
})
export class AdminAnalyticsService {
  private apiUrl = "https://localhost:7251/api/order";
  private apiUrlUser = "https://localhost:7251/api/"

  constructor(private http: HttpClient , private authService: AuthService) {}

  getDashboardAnalytics(range: 'week' | 'month' = 'week'): Observable<DashboardAnalytics> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
        const headers = new HttpHeaders({
          Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
        });
    return this.http.get<any>(`${this.apiUrl}/dashboard-analytics?range=${range}`,{headers});
  }

  getUserList(type: 'User' | 'Seller' = 'User', 
    page: number = 1, 
    pageSize: number = 10): Observable<any> {
    return this.http.get(`${this.apiUrlUser}/User`, {
        params: { 
        type, 
        page: page.toString(), 
        pageSize: pageSize.toString() 
    }
    });
}

toggleUserStatus(userId: string, status: 'enable' | 'disable'): Observable<any> {
return this.http.put(`${this.apiUrl}/User/${userId}/status`, { status });
}


}