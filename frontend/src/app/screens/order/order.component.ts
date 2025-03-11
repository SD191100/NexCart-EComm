
import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { OrderDetailComponent } from '../../components/order-detail/order-detail.component';

@Component({
  selector: 'app-order',
  imports: [CommonModule, FormsModule, OrderDetailComponent],
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent implements OnInit {
  orders: any[] = []; // Array to hold all orders

  constructor(private orderService: OrderService, private productService: ProductService) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getOrdersByUserId().subscribe({
      next: (orders) => {
       this.orders = orders.sort((a: any, b: any) => {
        return new Date(b.orderDate).getTime() - new Date(a.orderDate).getTime();
      });
      console.log('Orders:', orders);
      },
      error: (err) => {
        console.error('Error fetching orders:', err);
      },
    });
  }

  calculateOrderTotal(orderDetails: any[]): number {
    return orderDetails.reduce((total, item) => total + item.quantity * item.price, 0);
  }

  getProductName(id: number): string {
    let returnValue = "";
    this.productService.getProductById(id).subscribe(
      {
        next: (product) => {
          returnValue = product.name;
        },
        error: (err) => {
        }

      })
    return returnValue
  }
}

