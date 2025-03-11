import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../services/cart.service';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'app-products',
  imports: [CommonModule, FormsModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent {
  productId: number = 0;
  userId: number = 0;
  productDetail = {
    category: null,
    categoryId: null,
    description: '',
    name: '',
    orderDetails: null,
    price: null,
    productId: null,
    productInventory: null,
    seller: null,
    sellerId: null,
    stock: null,
    mainImage: '',
    secondImage: '',
    thirdImage: '',
  };
  loading: boolean = true;
  currentImage: string = "";

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    // Get the category ID and name from the URL
    this.route.paramMap.subscribe((params) => {
      this.productId = Number(params.get('id'));
      this.fetchProductById();
    });
  }

  fetchProductById(): void {
    this.productService.getProductById(this.productId).subscribe({
      next: (product: any) => {
        this.productDetail = product;
        console.log(product);
        this.loading = false;
        this.currentImage = product.mainImage;
      },
      error: (err: any) => {
        console.error('Error fetching products:', err);
        this.loading = false;
      },
    });
  }

  onClick(): void {
    this.userId = this.authService.getUserId() || 0;
    console.log(this.userId);
    console.log('function entered');
    //this.router.navigate(['/cart']);
    if (this.authService.getToken() != null) {
      this.router.navigate(['/cart']); // Navigate after the request completes
    } else {
      this.router.navigate(['/login']);
    }

    this.cartService.addToCart(this.userId, this.productId, 1).subscribe({
      next: (response) => {
        console.log('Item added to cart successfully', response);
      },
      error: (err) => {
        console.error('Error adding item to cart', err);
      },
    });
  }

  setImage(imageUrl: string): void {
    this.currentImage = imageUrl;
  }
}
