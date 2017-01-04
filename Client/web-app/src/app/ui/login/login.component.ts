import { Component, OnInit }    from '@angular/core';
import { Router }               from '@angular/router';
import { NgForm }               from '@angular/forms';

import { Observable }           from 'rxjs';

import { AuthService }          from '../../services/auth.service';
import { SuccessLoginResponseModel, ErrorLoginResponseModel } from '../../models/auth.models';
import * as constants           from '../../common/constants';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
   public errorMessage: string = '';

    public readonly emailValidationRegex: string = constants.SIMPLE_VALIDATION_EMAIL_PATTERN;

    public constructor(private _router: Router, private _authService: AuthService) {}

    public ngOnInit(): void {

    }

    public login(event: Event, email: string, password: string): void {
        event.preventDefault();

        if (!constants.VALIDATION_EMAIL_REGEX.test(email)) {
            this.errorMessage = 'Invalid email!';

            return;
        }

        this.errorMessage = '';

        this._authService.login(email, password)
            .subscribe((successLoginResult: SuccessLoginResponseModel) => this._router.navigate([''])
                , reason => this._onLoginRejected(reason));
    }

    public register(): void {
        this._router.navigate(['./register']);
    }

    private _onLoginSucceed(successLoginResult: SuccessLoginResponseModel): void {
        this._router.navigate(['']);
    }

    private _onLoginRejected(reason: string): void {
        this.errorMessage = reason;
    }
}
