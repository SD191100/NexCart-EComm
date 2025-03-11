import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ProductBrowsingService } from '../../services/product-browsing.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
})
export class ProductBrowsingComponent implements OnInit {
  products: any[] = [];
  searchQuery: string = '';
  sortOption: string = 'price_asc';
  minPrice: number = 0;
  maxPrice: number = 100000;
  currentPage: number = 1;
  pageSize: number = 10;
  categoryId: number | null = null;

  constructor(
    private productService: ProductBrowsingService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((queryParams) => {
      this.searchQuery = queryParams.get('searchQuery') || '';
      this.categoryId = Number(queryParams.get('categoryId')) || null;
      this.fetchProducts();
    });
    //this.route.paramMap.subscribe((params) => {
    //  this.categoryId = Number(params.get('id'));
    //  this.fetchProducts();
    //});
  }

  fetchProducts(): void {
    let queryParams = {
      searchQuery: this.searchQuery,
      sortOption: this.sortOption,
      minPrice: this.minPrice,
      maxPrice: this.maxPrice,
      page: this.currentPage,
      pageSize: this.pageSize,
      //categoryId: this.categoryId,
    };

    let reqQuery = {};

    if (!this.categoryId) {
      reqQuery = {
        ...queryParams,
      };
    } else {
      reqQuery = {
        ...queryParams,
        categoryId: this.categoryId,
      };
    }

    this.productService.getProducts(reqQuery).subscribe(
      (response) => {
        console.log(response);
        // console.log(this.products);
        this.products = response;
        console.log(this.products);
      },
      (error) => {
        console.error('Error fetching products:', error);
        this.products = [];
      },
    );
  }

  changePage(page: number): void {
    if (page < 1) return;
    this.currentPage = page;
    this.fetchProducts();
  }
}
