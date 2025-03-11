import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { User } from '../../../models/User';
import { AuthService } from '../../services/auth.service';
import { SellerService } from '../../services/seller.service';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  seller: User = {
    firstName: '',
    lastName: '',
    userId: 0,
    email: '',
  };
  totalProducts = 110;
  pendingOrders = 5;
  deliveredOrders = 10;
  totalOrders = 15;
  orders: any[] = [];
  products: any[] = [];
  sellerId: number = 0;

  totalRevenue = 0;
  activities = [
    'Added a new product: "Gaming Laptop"',
    'Updated stock for "Wireless Mouse"',
    'Marked an order as shipped',
    'Received a new order for "Smartphone"',
  ];

  constructor(
    private authService: AuthService,
    private sellerService: SellerService,
  ) { }

  ngOnInit(): void {
    let id = this.authService.getUserId();
    this.authService.getUserProfile(id).subscribe({
      next: (res) => {
        //console.log(res);
        this.seller.firstName = res.firstName;
        this.seller.lastName = res.lastName;
        this.seller.userId = res.userId;
        this.seller.email = res.email;
      },
    });
    this.sellerService.getSellerByUserId(id).subscribe({
      next: (res) => {
        //console.log(res);
        this.sellerId = res.sellerId;
        this.getOrderDetails();
        this.getProducts();
      },
    });
  }

  getOrderDetails(): void {
    this.sellerService.getOrderDetailsBySellerId(this.sellerId).subscribe({
      next: (res) => {
        //console.log(res);
        this.orders = res;
        console.log(this.orders);
        this.totalOrders = this.orders.length;
        this.totalRevenue = this.getTotalRevenue(this.orders);
      },
    });
  }

  getProducts(): void {
    this.sellerService.getProductsBySellerId(this.sellerId).subscribe({
      next: (res) => {
        //console.log(res);
        this.products = res;

        console.log(this.products);
        this.totalProducts = this.products.length;
      },
    });
  }

  getTotalRevenue(orders: any[]): number {
    let total = 0;
    orders.forEach((order: any) => {
      let productTotal = order.quantity * order.price;
      total += productTotal;
    });

    return total;
  }
}
