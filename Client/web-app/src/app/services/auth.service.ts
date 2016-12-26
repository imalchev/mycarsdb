import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';

// Statics
import 'rxjs/add/observable/throw';

// Operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
// import HTTP_STATUS_CODES from 'http-status-enum';

import { LocalStorageService } from './local-storage.service';

import { LoginResponseModel, SuccessLoginResponseModel, ErrorLoginResponseModel, RegisterUserModel } from '../models/auth.models';
import { BadRequest } from '../models/common.models';
import * as constants from '../common/constants';
import * as httpStatusCodes from '../common/httpStatusCodes';

const ACCESS_TOKEN_KEY = 'access_token';
const TOKEN_VALID_TO_KEY = 'token_valid_to';
const TOKEN_ISSUED_KEY = 'token_issued';
const USERNAME_KEY = 'username';
const REFRESH_TOKEN_KEY = 'refresh_token';
const REFRESH_TOKEN_VALID_TO_KEY = 'refresh_token_valid_to';

@Injectable()
export class AuthService {

  constructor(private _http: Http, private _localStorageService: LocalStorageService) { }

    /** login user
    *   @param {string} username - username
    *   @param {string} password - password 
    */
    public login(username: string, password: string): Observable<SuccessLoginResponseModel | string> {
        let url = `${constants.BASE_API_URI}/users/login`;
        let body = `username=${username}&password=${password}&grant_type=password`;
        let headers = new Headers([
            { 'Content-Type': 'application/x-www-form-urlencoded' },
            { 'Accept': 'application/json' }]);
        let options = new RequestOptions({ headers: headers });

        return this._http.post(url, body, options)
            .map((response: Response) => {
                let jsonData: SuccessLoginResponseModel = response.json();
                return this._onLoginSucceed(jsonData, username);
            })
            .catch(this._handleErrorOnLogin)
            .catch(error => this._onLoginRejected(error));
    }

    /** registers user 
     * @param {RegisterUserModel} registerUser - model of the user data
     */
    public register(registerUser: RegisterUserModel): Observable<void | BadRequest | string > {
        let url = `${constants.BASE_API_URI}/users/register`;
        let body = JSON.stringify(registerUser);

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Accept', 'application/json');
        let options = new RequestOptions({ headers: headers });

        return this._http.post(url, body, options)
            .catch(this._handleError)
            .catch(error => this._onRegisterRejected(error));
    }

    /** Check if user is logged in */
    public isLoggedIn(): boolean {
        if (!this._localStorageService.getItem(ACCESS_TOKEN_KEY)) {
            return false;
        }
        
        var tokenValidTo = this._localStorageService.getItem(TOKEN_VALID_TO_KEY);
        if (!tokenValidTo) {
            return false;
        }
        
        let tokenValidToDT = new Date(parseInt(tokenValidTo, 10));
        if (tokenValidToDT < new Date()) {
            return false;
        }
        
        return true;
    }

    /** Log out user */
    public logout(): void {
        this._localStorageService.removeItem(ACCESS_TOKEN_KEY);
        this._localStorageService.removeItem(TOKEN_VALID_TO_KEY);
        this._localStorageService.removeItem(TOKEN_ISSUED_KEY);
        this._localStorageService.removeItem(USERNAME_KEY);
    }

    /** Get username of logged user
    *   @return {string} - return username or null if user not logged in 
    */
    public getUsername(): string {
        if (!this.isLoggedIn()) {
            return null;
        }

        return this._localStorageService.getItem(USERNAME_KEY);
    }

    /** Get token
    *   @return {string} - return token or null if user not logged in 
    */
    public getToken(): string {
        if (!this.isLoggedIn()) {
            return null;
        }

        return this._localStorageService.getItem(ACCESS_TOKEN_KEY);
    }

    private _onLoginSucceed(loginResponse: SuccessLoginResponseModel, username: string): SuccessLoginResponseModel {
        let loginDT = new Date(); // asume that login date is now

        let tokenValidToDT = new Date(loginDT.setSeconds(loginDT.getSeconds() + loginResponse.expires_in));

        this._localStorageService.setItem(ACCESS_TOKEN_KEY, loginResponse.access_token);
        this._localStorageService.setItem(TOKEN_VALID_TO_KEY, tokenValidToDT.getTime().toString());
        this._localStorageService.setItem(TOKEN_ISSUED_KEY, loginDT.getSeconds().toString());
        this._localStorageService.setItem(USERNAME_KEY, username);

        return loginResponse;
    }

    private _onLoginRejected(reason: LoginResponseModel | string): Observable<string> {
        if (typeof reason === 'string') {
            return Observable.throw(reason);
        }

        let rejectLoginResponse: ErrorLoginResponseModel = reason;
        if (rejectLoginResponse.error !== undefined && rejectLoginResponse.error_description !== undefined) {
            if (rejectLoginResponse.error === 'invalid_grant') {
                return Observable.throw(rejectLoginResponse.error_description);
            }
        }

        return Observable.throw(constants.UNEXPECTED_RESPONSE);
    }

    private _handleErrorOnLogin(error: any): Observable<string | ErrorLoginResponseModel> {
        if (typeof error === 'string') {
            return  Observable.throw(error);
        }

        if (error instanceof Response) {
            let errorResponse: Response = error;

            if (errorResponse.status === 0) {
                return Observable.throw(constants.CHECK_INTERNET);
            } else if (errorResponse.status === httpStatusCodes.NOT_FOUND) {
                return Observable.throw(constants.INVALID_REQUEST_ADDRESS);
            }

            if (errorResponse.status ===  httpStatusCodes.BAD_REQUEST) {
                let rejectResponse: ErrorLoginResponseModel = errorResponse.json();
                if (rejectResponse.error || rejectResponse.error_description) {
                    return Observable.throw(rejectResponse);
                }
            }

            return Observable.throw(errorResponse.text());
        }

        return Observable.throw(constants.UNEXPECTED_RESPONSE);
    }

    /** tries to get error message as BadRequest or string */
    private _handleError(error: any): Observable<string | BadRequest> {
        if (typeof error === 'string') {
            return  Observable.throw(error);
        }

        if (error instanceof Response) {
            let errorResponse: Response = error;

            if (errorResponse.status === 0) {
                return Observable.throw(constants.CHECK_INTERNET);
            } else if (errorResponse.status === httpStatusCodes.NOT_FOUND) {
                return Observable.throw(constants.INVALID_REQUEST_ADDRESS);
            }

            let errorObj: any = errorResponse.json();

            if (errorResponse.status === httpStatusCodes.UNAUTHORIZED) {
                let errorText: string = errorObj.message;

                return Observable.throw(errorText || '');
            }

            if (errorResponse.status ===  httpStatusCodes.BAD_REQUEST) {
                let badRequest: BadRequest = errorObj;
                if (badRequest.message || badRequest.modelState) {
                    return Observable.throw(badRequest);
                }

                return Observable.throw(errorResponse.text());
            }

            if (errorObj && errorObj.message) {
                return Observable.throw(errorObj.message);
            }

            return Observable.throw(errorResponse.text() || constants.UNEXPECTED_RESPONSE);
        }

        return Observable.throw(constants.UNEXPECTED_RESPONSE);
    }

    private _onRegisterRejected(reason: BadRequest | string): Observable<string> {
        if (typeof reason === 'string') {
            return Observable.throw(reason);
        }

        let badRequest: BadRequest = reason;
        if (badRequest.modelState.errorMessages) {
            return Observable.throw(badRequest.modelState.errorMessages[0]);
        }

        return Observable.throw(badRequest.message);
    }
}
