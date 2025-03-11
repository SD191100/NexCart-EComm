import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth-service.service';

export interface Category {
  categoryId?: number; // Optional, for updates
  name: string;
  description : string;
}

@Injectable({
  providedIn: 'root'
})

export class CategoryService {
  private apiUrl = 'http://localhost:5087/api/category'
  constructor(private http: HttpClient, private authService : AuthService) { }

  getAllCategories(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`)
  }

 // Add a new category
 addCategory(category: Category): Observable<Category> {
  const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
  return this.http.post<Category>(this.apiUrl, category, {headers});
}

// Update a category
updateCategory(id: number, category: Category): Observable<Category> {
  const url = `${this.apiUrl}/${id}`;
  const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
    });
  return this.http.put<Category>(url, category, {headers});
}

// Delete a category
deleteCategory(id: number): Observable<void> {
  const url = `${this.apiUrl}/${id}`;
  const token = this.authService.getToken(); // Retrieve the token from AuthService
  const headers = new HttpHeaders({
    Authorization: `Bearer ${token}`, // Attach the token to the Authorization header
  });
  return this.http.delete<void>(url , {headers});
}
}
