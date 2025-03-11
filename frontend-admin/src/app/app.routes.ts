import { Routes } from '@angular/router';
import { LoginComponent } from './screens/login/login.component';
import { RegisterComponent } from './screens/register/register.component';
import { UserManagementComponent } from './screens/user-management/user-management.component';
import { CategoryComponent } from './screens/categories/categories.component';
import { AdminDashboardComponent } from './screens/admin-dashboard/admin-dashboard.component';
import { AuthGuard } from './gaurd/AuthGaurd';
import { AdminProfileComponent } from './screens/admin-profile/admin-profile.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full', // Redirect to login page on the default path
},
    {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'register',
        component: RegisterComponent,
      },
      {
        path:'UserDashBoard',
        component: UserManagementComponent
      },
      {
        path:'Categories',
        component:CategoryComponent
      },
      {
        path:'adminDashboard',
        component:AdminDashboardComponent,
        canActivate: [AuthGuard], // Protect the route with AuthGuard
      },
      {
        path: 'adminProfile',
        component: AdminProfileComponent,
      }
      
];
