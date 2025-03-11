import { CommonModule } from '@angular/common';
import { Component, effect, OnInit, signal, Signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';
import { CategoryService } from '../../services/category.service';
import { ProductBrowsingService } from '../../services/product-browsing.service';

@Component({
  selector: 'app-header',
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  products: any[] = [];
  searchQuery: string = '';
  sortOption: string = 'price_asc';
  minPrice: number = 0;
  maxPrice: number = 100000;
  currentPage: number = 1;
  pageSize: number = 10;
  //categoryId: number = 0;
  categories: any[] = [];
  buttons = [
    { id: 1, icon: 'assets/fav.svg', link: '/orders' },
    { id: 2, icon: 'assets/cart.svg', link: '/cart' },
    { id: 3, icon: 'assets/account.svg', link: '' },
  ];
  tokenAvailable: Signal<boolean>;
  constructor(
    private router: Router,
    private categoryService: CategoryService,
    private productService: ProductBrowsingService,
  ) {
    this.tokenAvailable = signal(!!localStorage.getItem('jwtToken'));
    //console.log(this.tokenAvailable);
    // Update the account button link dynamically based on token presence
    effect(() => {
      this.buttons[2].link = this.tokenAvailable() ? '/account' : '/login';
    });
  }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (categories: any) => {
        this.categories = categories;
        //console.log(categories);
      },
      error: (err: any) => {
        console.error('Error fetching featured products:', err);
      },
    });
  }

  fetchProducts(): void {
    const queryParams = {
      searchQuery: this.searchQuery,
      sortOption: this.sortOption,
      minPrice: this.minPrice,
      maxPrice: this.maxPrice,
      page: this.currentPage,
      pageSize: this.pageSize,
    };

    this.productService.getProducts(queryParams).subscribe(
      (response) => {
        //console.log(response);
        // console.log(this.products);
        this.products = response;
        //console.log(this.products);
      },
      (error) => {
        console.error('Error fetching products:', error);
        this.products = [];
      },
    );
    this.router.navigate(['/products'], {
      queryParams: { searchQuery: this.searchQuery },
    });
  }
}
