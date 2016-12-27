import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService }  from '../../services/auth.service';
import { RegisterUserModel } from '../../models/auth.models';
import * as constants   from '../../common/constants';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public readonly emailValidationRegex: string = constants.SIMPLE_VALIDATION_EMAIL_PATTERN;
  public errorMessage = '';
  public model: RegisterUserModel = { email: '', name: '', password: '', confirmPassword: '', phoneNumber: '' };

  public constructor(private _router: Router, private _authService: AuthService) { }

  public ngOnInit() { }

  public register(event: Event): void {
    event.preventDefault();

    if (!this._isModelValid()) {
      return;
    }

    this.errorMessage = '';

    this._authService.register(this.model)
      .subscribe(() => this._onRegisterSucceed(),
        (response: string) => this.errorMessage = response);
  }

  private _isModelValid(): boolean {
        if (!constants.VALIDATION_EMAIL_REGEX.test(this.model.email)) {
            this.errorMessage = 'Invalid email!';
            return false;
        }

        if (this.model.password !== this.model.confirmPassword) {
            this.errorMessage = 'Password confirmation is not valid!';
            return false;
    }

    return true;
  }

  private _onRegisterSucceed() {
    this._router.navigate(['./']);
  }

  private _onRegisterRejected(reason: string): void {
    this.errorMessage = reason;
  }
}
