import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Product } from '../../models/product/product';

@Component({
  selector: 'app-featured',
  imports: [CommonModule, RouterLink],
  templateUrl: './featured.component.html',
})
export class FeaturedComponent implements OnInit {
  redirectLink = "['/products', product.id]";
  featuredProducts: Product[] = [];
  loading: boolean = true;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getFeaturedProducts().subscribe({
      next: (products: any) => {
        this.featuredProducts = products;
        this.loading = false;
      },
      error: (err: any) => {
        console.error('Error fetching featured products:', err);
        this.loading = false;
      }
    });
  }
}
