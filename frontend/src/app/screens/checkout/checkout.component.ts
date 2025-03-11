import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../services/user-profile.service';
import { AuthService } from '../../services/auth-service.service';
import { CheckoutDataService } from '../../services/checkout-data.service';
import { Product } from '../../models/product/product';
import { ProductService } from '../../services/product.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-checkout',
  imports: [CommonModule, FormsModule, RouterLink, ReactiveFormsModule],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  orderForm!: FormGroup; // FormGroup for the checkout form
  addresses: any[] = [];
  cartItems: any[] = [];
  userId: number | null = null;
  total: number = 0;
  product: Product | null = null;
  products: Product[] = [];
  combinedItems: any[] = [];
  price: number = 890;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private checkoutDataService: CheckoutDataService,
    private authService: AuthService,
    private productService: ProductService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    // Initialize the form group
    this.orderForm = this.fb.group({
      selectedAddress: ['', Validators.required],
      paymentMethod: ['', Validators.required],
    });
    this.userId = this.authService.getUserId();
    // Fetch user addresses
    this.userService.getUserAddresses(this.userId).subscribe({
      next: (addresses) => {
        this.addresses = addresses;
        if (this.addresses.length > 0) {
          this.orderForm.patchValue({ selectedAddress: this.addresses[0] });
        }
      },
      error: (err) => console.error('Error fetching addresses:', err),
    });

    // Retrieve cart data
    const checkoutData = this.checkoutDataService.getCheckoutData();
    if (checkoutData) {
      console.log('checkout Data in onInit', checkoutData);
      this.cartItems = checkoutData.cartItems || [];
      this.total = checkoutData.total || 0;

      console.log('Item in onInit', checkoutData);
      this.fetchProductPrice(this.cartItems);
    } else {
      console.error('No checkout data found. Redirecting to cart...');
      this.router.navigate(['/cart']);
    }

    //this.combinedItems = this.cartItems.map((cartItem) => {
    //  console.log(this.products, ' ', cartItem);
    //  const product = this.products.find(
    //    (p) => p.productId == cartItem.productId,
    //  );
    //  console.log(product);
    //  return { ...cartItem, ...product };
    //});
    //
    //console.log('combined Items', this.combinedItems);
  }

  fetchProductPrice(cartItems: any): void {
    //console.log(cartItems);
    //for (let i = 0; i < cartItems.length; i++) {
    //  let id: number = cartItems[i].productId;
    //  this.productService.getProductById(id).subscribe((product) => {
    //    this.product = product;
    //    console.log(product);
    //    this.products.push(product);
    //    console.log(this.products);
    //  });
    //}
    //this.cartItems[0].productId
    //
    const productRequests = this.cartItems.map((cartItem) =>
      this.productService.getProductById(cartItem.productId),
    );

    // Wait for all product requests to complete
    forkJoin(productRequests).subscribe({
      next: (products: Product[]) => {
        this.products = products; // Populate products array
        console.log('Products fetched:', this.products);

        this.combineCartAndProducts(); // Process combined items after all products are fetched
      },
      error: (err) => {
        console.error('Error fetching product prices:', err);
      },
    });
  }

  combineCartAndProducts(): void {
    this.combinedItems = this.cartItems.map((cartItem) => {
      const product = this.products.find(
        (p) => p.productId == cartItem.productId,
      );
      return { ...cartItem, ...product };
    });

    this.total = this.combinedItems.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0,
    );

    console.log('Combined Items:', this.combinedItems);
  }

  proceedToPayment(): void {
    if (this.orderForm.invalid) {
      alert('Please select an address and payment method.');
      return;
    }

    const orderData = {
      ...this.orderForm.value, // Get selected address and payment method
      cartItems: this.cartItems,
      total: this.total,
    };

    this.checkoutDataService.setOrderData(orderData);



    console.log('Proceeding to payment with order data:', orderData);
    // Implement API call or navigation to payment gateway
    this.router.navigate(['/payment']);
  }
}
