import { Component } from '@angular/core';
import { Cart } from '../../models/cart/cart.model';
import { CartService } from '../../services/cart.service';
import { CartItemComponent } from '../../components/cart-item/cart-item.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';
import { OrderService } from '../../services/order.service';
import { CheckoutDataService } from '../../services/checkout-data.service';

@Component({
  selector: 'app-cart',
  imports: [CartItemComponent, CommonModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})

export class CartComponent {
  cart: Cart | null = null;
  userId = 0;
  cartItems: any[] = []; // Initialize as an empty array
  total: number = 0;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private orderService: OrderService,
    private router: Router,
    private checkoutDataService: CheckoutDataService
  ) {}

  ngOnInit(): void {
    this.getUserId();
  }

  getUserId(): void {
    const userId = this.authService.getUserId(); // Directly call the synchronous method
    if (userId !== null) {
      this.userId = userId; // Assign the userId
      this.loadCart(); // Load the cart after setting userId
    } else {
      console.error('User ID is null. Unable to load the cart.');
      // Handle the case where userId is null (e.g., redirect to login)
    }
  }

  loadCart(): void {
    this.cartService.loadCart(this.userId);
    this.cartService.cart$.subscribe((cart) => {
      this.cart = cart;
      console.log('cart: ', cart);
      this.cartItems = cart?.cartItems || []; // Ensure cartItems is never undefined
      console.log(this.cartItems);

      if (this.cartItems.length > 0) {
        console.log('running getTotal function...');
        this.getTotal(); // Call getTotal only when cartItems is populated
      } else {
        console.error('Cart is empty. Skipping total calculation.');
      }
    });
  }

  getTotal(): void {
    const body = {
      cartItems: this.cartItems, // Ensure this is populated
      shippingAddress: 'string',
      paymentMethod: 'string',
    };

    console.log('Cart Items:', this.cartItems);

    if (!this.cartItems || this.cartItems.length === 0) {
      console.error('CartItems is empty or invalid!');
      return;
    }

    console.log(body);

    this.orderService.getCheckout(body).subscribe({
      next: (res) => {
        console.log("printing res")
        console.log(res);
        this.total = res.totalAmount;
      },
      error: (err) => {
        console.error('Error during checkout:', err.error);
      },
    });
  }

   checkout(): void {
    if (!this.cartItems || this.cartItems.length === 0) {
      alert('Your cart is empty!');
      return;
    }

    //const checkoutData = {
    //  cartItems: this.cartItems,
    //  total: this.total,
    //};
    const checkoutData = {
      cartItems: this.cartItems,
      total: this.total,
    };

    this.checkoutDataService.setCheckoutData(checkoutData);

    console.log("entering the checkout page function")

    this.router.navigate(['/checkout']); // Redirect to checkout with data
  }

  clearCart(): void {
    this.cartService.clearCart(this.userId).subscribe(
      {
        next: (res) => {
          console.log(res)
        },
        error: (err: any) => {
          console.error(err.message)
        }
      }
    )
  }
}

