import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit {
 categoryId: number = 0;
  categoryName: string = '';
  products: any[] = [];
  loading: boolean = true;

  constructor(private route: ActivatedRoute, private productService: ProductService) {}

  ngOnInit(): void {
    // Get the category ID and name from the URL
    this.route.paramMap.subscribe((params) => {
      this.categoryId = Number(params.get('id'));
      this.categoryName = params.get('name') || '';
      this.fetchProducts();
    });
  }

  fetchProducts(): void {
    this.productService.getProductsByCategoryId(this.categoryId).subscribe({
      next: (products: any) => {
        this.products = products;
        console.log(products);
        this.loading = false;
      },
      error: (err: any) => {
        console.error('Error fetching products:', err);
        this.loading = false;
      }
    });
  }
}
