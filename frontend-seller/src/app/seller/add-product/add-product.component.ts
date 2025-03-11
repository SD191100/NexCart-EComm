import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';

import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { SellerService } from '../../services/seller.service';

@Component({
  selector: 'app-add-product',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, FormsModule],
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent implements OnInit {
  categories: any[] = [];
  newProduct = {
    name: '',
    description: '',
    price: 0,
    stock: 0,
    categoryId: 0,
    sellerId: 123, // Replace with dynamic seller ID if applicable
    mainImage: '',
    secondImage: '',
    thirdImage: '',
    details: '',
  };

  constructor(
    private productService: ProductService,
    private router: Router,
    private authService: AuthService,
    private sellerService: SellerService,
  ) { }

  ngOnInit(): void {
    this.fetchCategories();
    let userId = this.authService.getUserId();

    this.sellerService.getSellerByUserId(userId).subscribe({
      next: (res) => {
        console.log(res);
        this.newProduct.sellerId = res.sellerId;
      },
    });
  }

  fetchCategories() {
    this.productService.getCategories().subscribe(
      (data) => {
        this.categories = data;
        console.log(this.categories); // Populate the categories array
      },
      (error) => {
        console.error('Error fetching categories:', error);
      },
    );
  }
  addProduct() {
    console.log('Payload before submission:', this.newProduct);
    this.productService.addProduct(this.newProduct).subscribe(
      (response) => {
        console.log('Product added successfully', response);
        //this.resetForm();
      },
      (error) => {
        console.error('Error adding product', error);
      },
    );
  }

  cancel(): void {
    this.router.navigate(['/product-management']);
  }
}
