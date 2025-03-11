// admin-profile.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminProfileService, UserProfile } from '../../services/admin-profile.service';
import { AuthService } from '../../services/auth-service.service';

// import { AdminProfileService, UserProfile } from './admin-profile.service';

@Component({
  selector: 'app-admin-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-profile.component.html',
  styleUrls: ['./admin-profile.component.css']
})
export class AdminProfileComponent implements OnInit {
  userProfile: UserProfile | null = null;
  profileForm: FormGroup;
  isLoading = true;
  isEditing = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private adminProfileService: AdminProfileService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService : AuthService,
  ) {
    // Initialize the form with validators
    this.profileForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      contactNumber: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.isLoading = true;
    this.errorMessage = '';

    // Assuming user ID is known or retrieved from auth service
    const userId = this.authService.getUserId()!;
    this.adminProfileService.getUserProfile(userId).subscribe({
      next: (profile) => {
        this.userProfile = profile;
        this.populateForm(profile);
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load profile. Please try again.';
        this.isLoading = false;
        console.error('Profile load error', error);
      }
    });
  }

  populateForm(profile: UserProfile): void {
    this.profileForm.patchValue({
      firstName: profile.firstName,
      lastName: profile.lastName,
      email: profile.email,
      contactNumber: profile.contactNumber
    });
  }

  toggleEditMode(): void {
    this.isEditing = !this.isEditing;
    this.errorMessage = '';
    this.successMessage = '';

    if (!this.isEditing) {
      // Reset form to original values when canceling edit
      this.populateForm(this.userProfile!);
    }
  }

  onSubmit(): void {
    if (this.profileForm.invalid) {
      this.errorMessage = 'Please correct the errors in the form.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const updatedProfile: Partial<UserProfile> = {
      ...this.profileForm.value
    };

    this.adminProfileService.updateUserProfile(this.userProfile!.userId, updatedProfile)
      .subscribe({
        next: (updatedProfile) => {
          this.userProfile = updatedProfile;
          this.successMessage = 'Profile updated successfully!';
          this.isEditing = false;
          this.isLoading = false;
        },
        error: (error) => {
          this.errorMessage = 'Failed to update profile. Please try again.';
          this.isLoading = false;
          console.error('Profile update error', error);
        }
      });
  }

  logout(): void {
    this.adminProfileService.logout();
    // Navigate to login page
    this.router.navigate(['/login']);
  }
}