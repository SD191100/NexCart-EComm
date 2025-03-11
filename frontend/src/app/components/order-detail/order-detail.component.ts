import { Component, Input } from '@angular/core';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-order-detail',
  imports: [],
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.css'
})
export class OrderDetailComponent {
 @Input() item: any; // Input property to receive order detail
  productName: string = ''; // Store the product name

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    if (this.item?.productId) {
      this.fetchProductName(this.item.productId);
    }
  }

  fetchProductName(productId: number): void {
    this.productService.getProductById(productId).subscribe({
      next: (product) => {
        this.productName = product?.name || 'Unknown Product';
      },
      error: (err) => {
        console.error(`Error fetching product (ID: ${productId}):`, err);
        this.productName = 'Product not found';
      },
    });
  }
}
