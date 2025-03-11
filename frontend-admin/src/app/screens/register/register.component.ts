import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'app-register',
  imports: [RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  signupForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
  ) {
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
    console.log(this.signupForm.value);
  }

  register() {
    if (this.signupForm.valid) {
      let user = {
        firstName: this.signupForm.value.firstName,
        lastName: this.signupForm.value.lastName,
        role: "Admin",
        email: this.signupForm.value.email,
        password: this.signupForm.value.password
      }
      this.authService.register(user).subscribe({
        next: (res) => {
          this.successMessage = 'Registration successful!' + res.successMessage;
          this.errorMessage = '';
          this.router.navigate(['/home']);
        },
        error: (err) => {
          this.errorMessage = 'Registration failed. Please try again.';
          this.successMessage = '';
        },
      });
    }
    else{
      console.log("not entered")
    }
  }
}
