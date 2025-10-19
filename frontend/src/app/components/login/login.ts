import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiData } from '../../services/api-data';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {
  constructor(private fb: FormBuilder,
    private api: ApiData
  ) { }

  loginForm!: FormGroup;
  errorMessage: string = "";
  showPassword = false;
  
  //icons
  faEye = faEye;
  faEyeSlash = faEyeSlash;


  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    })
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  fnLoginClick() {
    this.loginForm.markAllAsTouched()
    if (this.loginForm.valid) {
      this.api.getTest().subscribe(res => console.log(res));
    }
  }
}
