import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service'; // assuming you already have a service for handling product API
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SellerService } from '../../services/seller.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-product-management',
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.css'],
})
export class ProductManagementComponent implements OnInit {
  products: any[] = [];
  sellerId = 0;
  showEditModal: boolean = false; // Controls modal visibility
  editableProduct: any = {}; // Stores the product being edited

  isAddModalOpen = false; // Control for Add Modal
  isEditModalOpen = false; // Control for Edit Modal
  newProduct: any = {}; // Model for new product
  selectedProduct: any = {}; // Model for the product being edited
  constructor(
    private authService: AuthService,
    private productService: ProductService,
    private sellerService: SellerService,
  ) { }

  ngOnInit(): void {
    this.getSellerId();
  }

  getSellerId(): void {
    let userId = this.authService.getUserId();
    this.sellerService.getSellerByUserId(userId).subscribe({
      next: (res) => {
        this.sellerId = res.sellerId;

        this.fetchProducts(this.sellerId);
      },
    });
  }

  fetchProducts(id: number): void {
    this.sellerService.getProductsBySellerId(id).subscribe(
      (data) => {
        this.products = data;
        console.log(this.products);
      },
      (error) => {
        console.error('Error fetching products', error);
      },
    );
  }

  //editProduct(productId: number): void {
  //  // Logic to navigate to the edit page or open a modal
  //  this.productService.updateProduct(productId, this.newProduct);
  //  console.log('Edit product', productId);
  //}

  deleteProduct(productId: number): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(productId).subscribe(() => {
        this.fetchProducts(this.sellerId); // Refresh the product list after deletion
      });
    }
  }

  openAddModal(): void {
    this.newProduct = {};
    this.isAddModalOpen = true;
  }

  closeAddModal(): void {
    this.isAddModalOpen = false;
  }

  openEditModal(product: any): void {
    this.selectedProduct = { ...product };
    this.isEditModalOpen = true;
  }

  closeEditModal(): void {
    this.isEditModalOpen = false;
  }

  addProduct(): void {
    // Call service to save the product
    console.log('Adding Product:', this.newProduct);
    this.closeAddModal();
  }

  editProduct(product: any) {
    // Open the modal and set the editable product
    this.showEditModal = true;
    this.editableProduct = { ...product }; // Create a copy to avoid direct mutation
  }

  saveChanges() {
    // Call the API to save changes and update the product list
    this.productService.updateProduct(this.editableProduct).subscribe(
      (response) => {
        // Ensure response matches the DTO structure
        const updatedProduct = {
          productId: response.id, // Map 'id' from response to 'productId'
          name: response.name,
          price: response.price,
          stock: response.stock,
          categoryId: response.categoryId,
        };

        // Update the product in the products array
        const index = this.products.findIndex(
          (p) => p.productId === this.editableProduct.productId,
        );
        if (index !== -1) {
          this.products[index] = updatedProduct; // Replace with the updated product
        }

        this.showEditModal = false; // Close the modal
        window.location.reload();
      },
      (error) => {
        console.error('Error updating product:', error);
      },
    );
  }

  cancelEdit() {
    // Close the modal without saving
    this.showEditModal = false;
  }
}
