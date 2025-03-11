import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
constructor(private router: Router) {}

logout() {
  // Clear authentication tokens or session data
  localStorage.removeItem('authToken');
  sessionStorage.clear(); // Clear session storage if used

  // Navigate to the login page
  this.router.navigate(['/login']);
}
}
