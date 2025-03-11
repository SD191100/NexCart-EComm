import { Component, OnInit } from '@angular/core';
import { SellerService } from '../../services/seller.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-order-management',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.css'],
})
export class OrderManagementComponent implements OnInit {
  orders: any[] = []; // Array to store order details
  selectedOrder: any = null; // Selected order for the modal
  sellerId = 1;
  constructor(
    private SellerService: SellerService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.getSellerId();
  }

  // Fetch orders from API
  getOrders(id: number): void {
    this.SellerService.getOrderDetailsBySellerId(id).subscribe(
      (data) => {
        this.orders = data; // Assign API response to orders array
      },
      (error) => {
        console.error('Error fetching orders:', error);
      },
    );
  }

  getSellerId(): void {
    let userId = this.authService.getUserId();
    this.SellerService.getSellerByUserId(userId).subscribe({
      next: (res) => {
        this.sellerId = res.sellerId;
        this.getOrders(res.sellerId);
      },
    });
  }

  // View Order Details in Modal
  viewOrderDetails(orderId: number): void {
    this.selectedOrder = this.orders.find((order) => order.orderId === orderId);
  }

  // Close the modal
  closeOrderDetails(): void {
    this.selectedOrder = null;
  }
}
