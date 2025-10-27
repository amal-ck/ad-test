import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiData } from '../../services/api-data';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { Router, RouterModule } from '@angular/router';
import { catchError, filter, switchMap } from 'rxjs';
import { ErrorPopup } from '../shared/error-popup/error-popup';
import { MdlResponse } from '../../models/commonModels';
import { Loading } from '../shared/loading/loading';


@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule, RouterModule, ErrorPopup, Loading],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {
  constructor(private fb: FormBuilder,
    private api: ApiData,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) { }

  loginForm!: FormGroup;
  errorMessage: string = "";
  showPassword = false;
  errorData: MdlResponse | null = null;
  loading: boolean = false;

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
      this.loading = true;
      this.errorData = null;
      this.api.login(this.loginForm.getRawValue()).subscribe({
        next: (res: MdlResponse) => {
          console.log("login succes-> ", res);
          if (res.success == true) {
            this.loading = false;
            this.router.navigate([""])
          }
        },
        error: (err: any) => {
          this.loading = false;
          this.errorData = err.error
          console.log(this.errorData)
          this.cdr.detectChanges();
        }
      });
    }
  }

  closeErrorPopup() {
    this.errorData = null;
  }
}
