import { Component, OnInit } from '@angular/core';
import { Category, CategoryService } from '../../services/category.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-categories',
  imports: [CommonModule , FormsModule , ReactiveFormsModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})

// export class CategoryComponent implements OnInit {
//   categories: Category[] = [];
//   newCategory: Category = { name: '', description: '' };
//   categoryToEdit: Category | null = null;
//   currentCategory: Category = this.newCategory; // Variable for binding
//   editMode: boolean = false;

//   constructor(private categoryService: CategoryService ,  private cdr: ChangeDetectorRef) {}

//   ngOnInit(): void {
//     this.fetchCategories();
//   }

//   // Fetch all categories
//   fetchCategories(): void {
//     this.categoryService.getAllCategories().subscribe((data) => {
//       this.categories = data;
//       this.cdr.detectChanges();
//     });
//   }
//   confirmEdit(category: Category): void {
//     this.categoryToEdit = { ...category }; // Clone the object
//     this.currentCategory = this.categoryToEdit;
//   }
  
//   // Add a new category
//   addCategory(): void {
//     this.categoryService.addCategory(this.currentCategory).subscribe((category) => {
//       this.categories.push(category);
//       this.resetForm();
//       this.cdr.detectChanges();
//     });
//   }

//   // Set edit mode for updating a category
//   setEditCategory(category: Category): void {
//     this.categoryToEdit = { ...category }; // Clone the object
//     this.currentCategory = this.categoryToEdit;
//     this.editMode = true;
//   }

//   // Update category
//   updateCategory(): void {
//     if (this.categoryToEdit && this.categoryToEdit.categoryId) {
//       this.categoryService.updateCategory(this.categoryToEdit.categoryId, this.currentCategory).subscribe((updatedCategory) => {
//         const index = this.categories.findIndex((c) => c.categoryId === updatedCategory.categoryId);
//         if (index !== -1) {
//           this.categories[index] = updatedCategory;
//         }
//         this.resetForm();
//         this.cdr.detectChanges();
//       });
//     }
//   }

//   // Delete a category
//   deleteCategory(id: number): void {
//     this.categoryService.deleteCategory(id).subscribe(() => {
//       this.categories = this.categories.filter((c) => c.categoryId !== id);
//       this.cdr.detectChanges();
//     });
//   }

//   // Reset form
//   resetForm(): void {
//     this.newCategory = { name: '', description: '' };
//     this.currentCategory = this.newCategory; // Reset the binding variable
//     this.categoryToEdit = null;
//     this.editMode = false;
//   }
// } 

export class CategoryComponent implements OnInit {
  categories: Category[] = [];
  newCategory: Category = { name: '', description: '' };
  categoryToEdit: Category | null = null;
  currentCategory: Category = this.newCategory; // Variable for binding
  editMode: boolean = false;

  // Confirmation modal variables
  showConfirmation: boolean = false;
  confirmationAction: string = ''; // Action to display in the confirmation (Edit/Delete)
  confirmCallback: (() => void) | null = null; // Callback for confirmed actions

  constructor(private categoryService: CategoryService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.fetchCategories();
  }

  // Fetch all categories
  fetchCategories(): void {
    this.categoryService.getAllCategories().subscribe((data) => {
      this.categories = data;
      this.cdr.detectChanges();
    });
  }

  // Confirm edit action
  confirmEdit(category: Category): void {
    this.categoryToEdit = { ...category }; // Clone the object
    this.currentCategory = this.categoryToEdit;
    this.confirmationAction = 'update';
    this.showConfirmation = true;
    this.confirmCallback = this.updateCategory.bind(this);
  }

  // Confirm delete action
  confirmDelete(id: number): void {
    this.confirmationAction = 'delete';
    this.showConfirmation = true;
    this.confirmCallback = () => this.deleteCategory(id);
  }

  // Execute the confirmed action (Edit or Delete)
  executeAction(): void {
    if (this.confirmCallback) {
      this.confirmCallback(); // Execute the confirmed action
      this.resetConfirmation(); // Close the modal and reset
      this.fetchCategories(); // Refresh the categories
    }
  }

  // Add a new category
  addCategory(): void {
    this.categoryService.addCategory(this.currentCategory).subscribe((category) => {
      this.categories.push(category);
      this.resetForm();
      this.cdr.detectChanges();
    });
  }

  // Set edit mode for updating a category
  setEditCategory(category: Category): void {
    this.categoryToEdit = { ...category }; // Clone the object
    this.currentCategory = this.categoryToEdit; // Set the current category to the selected one
    this.editMode = true; // Switch to edit mode
  }
  // Update category
  updateCategory(): void {
    if (this.categoryToEdit && this.categoryToEdit.categoryId) {
      this.categoryService.updateCategory(this.categoryToEdit.categoryId, this.currentCategory).subscribe((updatedCategory) => {
        const index = this.categories.findIndex((c) => c.categoryId === updatedCategory.categoryId);
        if (index !== -1) {
          this.categories[index] = updatedCategory;
        }
        this.resetForm();
        this.cdr.detectChanges();
      });
    }
  }

  // Delete a category
  deleteCategory(id: number): void {
    this.categoryService.deleteCategory(id).subscribe(() => {
      this.categories = this.categories.filter((c) => c.categoryId !== id);
      this.cdr.detectChanges();
    });
  }

  // Reset form
  resetForm(): void {
    this.newCategory = { name: '', description: '' };
    this.currentCategory = this.newCategory; // Reset the binding variable
    this.categoryToEdit = null;
    this.editMode = false;
  }

  // Reset confirmation modal
  resetConfirmation(): void {
    this.showConfirmation = false;
    this.confirmationAction = '';
    this.confirmCallback = null;
  }
}
