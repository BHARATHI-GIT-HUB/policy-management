import { user } from './../../../model/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  Validators,
} from '@angular/forms';
import { environment } from '../../../../environments/environment';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { json } from '@angular-devkit/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  validateForm: FormGroup<{
    userName: FormControl<string>;
    password: FormControl<string>;
    // remember: FormControl<boolean>;
  }> = this.fb.group({
    userName: ['', [Validators.required]],
    password: ['', [Validators.required]],
    // remember: [true],
  });

  submitForm(): void {
    if (this.validateForm.valid) {
      console.log('submit', this.validateForm.value);

      this.http
        .post<any>(
          `${environment.apiurl}api/auth/login`,
          this.validateForm.value,
          {
            headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
          }
        )
        .subscribe((response: any) => {
          console.log(response);
          console.log(response, 'login response');
          const token = response.token;

          if (token) {
            localStorage.clear();
            localStorage.setItem('token', token);

            const decodedToken = this.jwtHelper.decodeToken(token);
            const username =
              decodedToken[
                'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
              ];
            const role =
              decodedToken[
                'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
              ];
            const userId =
              decodedToken[
                'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
              ];

            // Print user details
            const user = {
              username: username,
              role: role,
              id: userId,
            };

            console.log('Username:', username);
            console.log('Role:', role);
            console.log('User ID:', userId);

            localStorage.setItem('user', JSON.stringify(user));
            if (role === 'Client') {
              this.router.navigate(['/home']);
            } else {
              console.log('admin ciew', JSON.stringify(user));
              this.router.navigate(['/']);
            }
          }
        });
    } else {
      Object.values(this.validateForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  constructor(
    private fb: NonNullableFormBuilder,
    private router: Router,
    private jwtHelper: JwtHelperService,
    private http: HttpClient
  ) {}
}
