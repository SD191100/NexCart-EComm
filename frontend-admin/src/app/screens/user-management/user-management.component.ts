import { Component, OnInit } from '@angular/core';
import { UserManagementService } from '../../services/user-management.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminAnalyticsService } from '../../admin-analytics.service';

interface User {
  userId: number;
  firstName: string;
  email: string;
  role: 'User' | 'Seller' | 'Admin';
  isActive: boolean;
  contactNumber : number;
}


@Component({
  selector: 'app-user-management',
  imports: [CommonModule , FormsModule , ReactiveFormsModule],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent implements OnInit {
  users: User[] = [];
  loading: boolean = false;
  error: string | null = null;
  
  // Add filter for user type
  userType: 'User' | 'Seller' = 'User';

  constructor(private userService: UserManagementService) {}

  ngOnInit(): void {
    this.fetchUsers();
  }

  // Modified to filter users and sellers
  fetchUsers(): void {
    this.loading = true;
    this.userService.getAllUsers().subscribe(
      (data: User[]) => {
        // Filter only Customers and Sellers
        this.users = data.filter(user => 
          user.role === 'User' || user.role === 'Seller'
        );
        this.loading = false;
      },
      (error) => {
        this.error = 'Failed to load users.';
        this.loading = false;
      }
    );
  }

  // Method to switch between Customer and Seller views
  switchUserType(type: 'User' | 'Seller'): void {
    this.userType = type;
    this.fetchUsers();
  }

  // Filter users based on selected type
  get filteredUsers(): User[] {
    return this.users.filter(user => user.role === this.userType);
  }

  // toggleUserStatus(user: User): void {
  //   const updatedUserData = { ...user, isActive: !user.isActive };
  
  //   this.userService.updateUserProfile(user.userId, updatedUserData).subscribe(
  //     () => {
  //       // Update the local users array
  //       this.users = this.users.map((u) =>
  //         u.userId === user.userId ? { ...u, isActive: updatedUserData.isActive } : u
          
  //       );
  //       this.error = null;
  //     },
  //     (error) => {
  //       this.error = `Failed to update user status for ${user.firstName}.`;
  //       console.error('Error toggling user status:', error);
  //     }
  //   );
  // }

  toggleUserStatus(user: User): void {
    // Fetch the latest details of the user from the backend
    this.userService.getUserProfile(user.userId).subscribe(
      (fetchedUser) => {
        // Construct the minimal payload for the update request
        const updatedUserData = {
          firstName: fetchedUser.firstName,
          lastName: fetchedUser.lastName,
          email: fetchedUser.email,
          contactNumber: fetchedUser.contactNumber,
          isActive: !fetchedUser.isActive, // Toggle the active status
        };
  
        // Send the update request with the minimal payload
        this.userService.updateUserProfile(user.userId, updatedUserData).subscribe(
          () => {
            // Update the local users array with the new active status
            this.users = this.users.map((u) =>
              u.userId === user.userId ? { ...u, isActive: updatedUserData.isActive } : u
            );
            this.error = null;
          },
          (error) => {
            this.error = `Failed to update user status for ${user.firstName}.`;
            console.error('Error toggling user status:', error);
          }
        );
      },
      (error) => {
        this.error = `Failed to fetch user details for ${user.firstName}.`;
        console.error('Error fetching user details:', error);
      }
    );
  }
  
  
  deleteUser(userId: number): void {
    this.userService.deleteUserAccount(userId).subscribe(
      () => {
        this.users = this.users.filter((user) => user.userId !== userId);
      },
      (error) => {
        this.error = 'Failed to delete user.';
      }
    );
  }
}
