import { Routes } from '@angular/router';
import { DashboardComponent } from './seller/dashboard/dashboard.component';
import { ProductManagementComponent } from './seller/product-management/product-management.component';
import { AddProductComponent } from './seller/add-product/add-product.component';
import { LoginComponent } from './seller/login/login.component';
import { SignupComponent } from './seller/signup/signup.component';
import { OrderManagementComponent } from './seller/order-management/order-management.component';

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'product-management', component: ProductManagementComponent },
  { path: 'order-management', component: OrderManagementComponent },
  { path: 'add-product', component: AddProductComponent },
  { path: 'login', component: LoginComponent },
  { path: '', component: SignupComponent },
];
