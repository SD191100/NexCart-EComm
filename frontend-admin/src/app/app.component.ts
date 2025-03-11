import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from "./screens/login/login.component";
import { UserManagementComponent } from './screens/user-management/user-management.component';
import { AdminDashboardComponent } from './screens/admin-dashboard/admin-dashboard.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,
    FooterComponent,
    HeaderComponent, LoginComponent,
  UserManagementComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'NexCartAdmin';
}
