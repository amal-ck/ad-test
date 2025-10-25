import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiData } from '../../services/api-data';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { RouterModule } from '@angular/router';
import { ErrorPopup } from "../shared/error-popup/error-popup";
import { MdlResponse } from '../../models/commonModels';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule, RouterModule, ErrorPopup],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register implements OnInit {
  constructor(private fb: FormBuilder,
    private api: ApiData,
    private cdr: ChangeDetectorRef
  ) { }

  registrationForm!: FormGroup;
  errorMessage: string = "";
  showPassword = false;
  errorData: MdlResponse | null = null;

  //icons
  faEye = faEye;
  faEyeSlash = faEyeSlash;


  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      name: ['', [Validators.required]],
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.pattern('^(?=.*[a-z])(?=.*[A-Z]).+$')]],
      confirmPassword: ['', [Validators.required]],
    }, {
      validator: this.passwordMatchValidator
    });
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  fnRegisterClick() {
    this.registrationForm.markAllAsTouched()
    if (this.registrationForm.valid) {
      this.errorData = null;
      this.api.registration(this.registrationForm.getRawValue()).subscribe({
        next: (res: any) => {  },
        error: (err: any) => {
           this.errorData = err.error
           console.log(this.errorData)
           this.cdr.detectChanges();
        },
      })
    }
}

closeErrorPopup() {
    this.errorData = null;
  }
}
