import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user-profile.service'; // Adjust path as needed
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
  imports: [CommonModule, ReactiveFormsModule, FormsModule]
})
export class UserProfileComponent implements OnInit {
  userForm: FormGroup;
  addressForm: FormGroup;
  userId: number | null = null;
  addresses: any[] = [];

  constructor(private fb: FormBuilder, private userService: UserService, private authService: AuthService, private router: Router) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      contactNumber: ['', Validators.required],
    });

    this.addressForm = this.fb.group({
      street: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      postalCode: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    // Get user ID from the JWT token (this could be dynamic or fetched from storage)
    this.userId = this.authService.getUserId();
    console.log(this.userId);
    if (this.userId !== null) {
      // Load user profile
      this.userService.getUserProfile(this.userId).subscribe(userData => {
        this.userForm.patchValue(userData); // Populate the form with user data
      });


      // Load user addresses
      this.userService.getUserAddresses(this.userId).subscribe(addresses => {
        this.addresses = addresses; // Set the addresses in the component
      });
    }
  }

  // Handle the user profile update
  onSubmit(): void {
    if (this.userForm.valid && this.userId !== null) {
      console.log(this.userForm.value);
      this.userService.updateUserProfile(this.userId, this.userForm.value).subscribe(
        (next) => alert('Profile updated successfully!'),
        (error) => alert('Error updating profile: ' + error)
      );
    }
  }

  // Handle adding new address
  addAddress(): void {
    if (this.addressForm.valid && this.userId !== null) {
      const addressData = { ...this.addressForm.value, userId: this.userId};
      this.userService.addAddress(addressData).subscribe(
        (response) => {
          alert('Address added successfully!');
          this.addresses.push(response); // Add new address to the list
          this.addressForm.reset(); // Reset the address form
        },
        (error) => alert('Error adding address: ' + error)
      );
    }
  }

  // Handle address deletion
  deleteAddress(addressId: number): void {
    if (confirm('Are you sure you want to delete this address?')) {
      this.userService.deleteAddress(addressId).subscribe(
        () => {
          alert('Address deleted successfully!');
          this.addresses = this.addresses.filter(address => address.id !== addressId); // Remove the deleted address
        },
        (error) => alert('Error deleting address: ' + error)
      );
    }
  }

  // Handle account deletion
  //deleteAccount(): void {
  //  if (this.userId !== null) {
  //    if (confirm('Are you sure you want to delete your account?')) {
  //      this.userService.deleteUserAccount(this.userId).subscribe(
  //        () => alert('Account deleted successfully!'),
  //        (error) => alert('Error deleting account: ' + error)
  //      );
  //    }
  //  }
  //}

  // Handle logout
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/home'])
    this.router.navigate(['/home'])
  }
}
