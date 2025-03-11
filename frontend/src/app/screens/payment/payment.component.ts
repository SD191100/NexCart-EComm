import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { CheckoutDataService } from '../../services/checkout-data.service';
import { AuthService } from '../../services/auth-service.service';
import { OrderService } from '../../services/order.service';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-payment',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css',
})
export class PaymentComponent {
  paymentForm!: FormGroup; // FormGroup for the payment form
  amount: number = 0; // Total amount to pay
  cartItems: any[] = []; // Cart items from checkout service
  shippingAddress: {
    addressId: number;
    city: string;
    country: string;
    postalCode: string;
    state: string;
    street: string;
  } = {
      addressId: 0,
      city: '',
      country: '',
      postalCode: '',
      state: '',
      street: '',
    }; // Shipping address from checkout service

  paymentMethod: string = '';
  constructor(
    private fb: FormBuilder,
    private checkoutService: CheckoutDataService,
    private authService: AuthService,
    private orderService: OrderService,
    private router: Router,
    private cartService: CartService,
  ) { }

  ngOnInit(): void {
    // Initialize the form group
    this.paymentForm = this.fb.group({
      upi: ['', Validators.required],
    });

    // Fetch data from CheckoutService
    const checkoutData = this.checkoutService.getOrderData();
    if (checkoutData) {
      this.amount = checkoutData.total || 0;
      this.cartItems = checkoutData.cartItems || [];
      this.shippingAddress = checkoutData.selectedAddress || '';
      this.paymentMethod = checkoutData.paymentMethod || '';
    } else {
      console.error('No checkout data found. Redirecting to cart...');
      this.router.navigate(['/cart']);
    }
  }

  processPayment(): void {
    //if (this.paymentForm.invalid) {
    //  alert('Please select a payment method.');
    //  return;
    //}

    const paymentData = {
      paymentMethod: this.paymentMethod,
      amount: this.amount,
    };

    this.orderService.processPayment(paymentData).subscribe({
      next: (response: any) => {
        console.log('Payment successful:', response);
        const paymentID = response.paymentId;

        // Confirm the order with paymentID

        if (paymentID != 0) {
          const formattedAddress = `${this.shippingAddress.street}, ${this.shippingAddress.city}, ${this.shippingAddress.state}, ${this.shippingAddress.country}, ${this.shippingAddress.postalCode}`;
          console.log(formattedAddress, ' ', ' payment id', paymentID);
          const orderData = {
            userId: this.authService.getUserId(),
            cartItems: this.cartItems,
            shippingAddress: formattedAddress,
            totalAmount: this.amount,
            paymentID: paymentID,
          };
          console.log(orderData);

          this.orderService.confirmOrder(orderData).subscribe({
            next: () => {
              alert('Order placed successfully!');
              this.clearCart();
              this.router.navigate(['/orders']); // Redirect to orders page
            },
            error: (err: any) => {
              console.error('Order confirmation failed:', err);
              alert('Order confirmation failed. Please try again.');
            },
          });
        }
      },
      error: (err: any) => {
        console.error('Payment failed:', err);
        alert('Payment failed. Please try again.');
      },
    });
  }

  //confirmOrder(paymentID: number): void {
  //  const formattedAddress = `${this.shippingAddress.street}, ${this.shippingAddress.city}, ${this.shippingAddress.state}, ${this.shippingAddress.country}, ${this.shippingAddress.postalCode}`;
  //  console.log(formattedAddress);
  //  const orderData = {
  //    userId: this.authService.getUserId(),
  //    cartItems: this.cartItems,
  //    shippingAddress: formattedAddress,
  //    totalAmount: this.amount,
  //    paymentID: paymentID,
  //  };
  //
  //  this.orderService.confirmOrder(orderData).subscribe({
  //    next: () => {
  //      let userId = this.authService.getUserId();
  //      console.log('Order placed successfully!');
  //
  //      this.clearCart();
  //      this.router.navigate(['/orders']);
  //      // Redirect to orders page
  //    },
  //    error: (err: any) => {
  //      console.error('Order confirmation failed:', err);
  //      alert('Order confirmation failed. Please try again.');
  //    },
  //  });
  //}

  clearCart(): void {
    let userId = this.authService.getUserId();

    this.cartService.clearCart(userId).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err: any) => {
        console.error(err.message);
      },
    });
  }
}
