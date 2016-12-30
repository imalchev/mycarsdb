import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';

// Statics
import 'rxjs/add/observable/throw';

// Operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { LocalStorageService } from './local-storage.service';
import { BaseHttpService } from './base-http.service';

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
export class AuthService extends BaseHttpService {

    private _authEvent: BehaviorSubject<boolean>;

    constructor(http: Http, private _localStorageService: LocalStorageService) {
        super(http);
        this._authEvent = new BehaviorSubject<boolean>(this.isLoggedIn());
    }

    public get authEvent(): Observable<boolean> {
        return this._authEvent.asObservable();
    }

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

                // emit successfull autentication
                this._authEvent.next(true);
                return this._onLoginSucceed(jsonData, username);
            })
            .catch(this._handleErrorOnLogin);
    }

    /** registers user 
     * @param {RegisterUserModel} registerUser - model of the user data
     */
    public register(registerUser: RegisterUserModel): Observable<void | BadRequest | string > {
        let url = `${constants.BASE_API_URI}/users/register`;
        let body = JSON.stringify(registerUser);

        let headers = this._getJsonHeaders();
        let options = new RequestOptions({ headers: headers });

        return this._http.post(url, body, options)
            .catch(this._handleError);
    }

    /** Check if user is logged in */
    public isLoggedIn(): boolean {
        if (!this._localStorageService.getItem(ACCESS_TOKEN_KEY)) {
            return false;
        }

        let tokenValidTo = this._localStorageService.getItem(TOKEN_VALID_TO_KEY);
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

        // emit successfull log out
        this._authEvent.next(false);
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

    /** generate Authorization header */
    public getAuthorizationHeader(): Headers {
        let authToken = this.getToken();

        let headers = new Headers();
        headers.append('Authorization', `Bearer ${authToken}`);

        return headers;
    }

    /** set authorization header and return the same headers */
    public setAuthorizationHeader(headers: Headers): Headers {
        let authToken = this.getToken();
        headers.append('Authorization', `Bearer ${authToken}`);
        return headers;
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

    private _handleErrorOnLogin(error: any): Observable<string> {
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
                if (rejectResponse.error === 'invalid_grant') {
                    return Observable.throw(rejectResponse);
                }
            }

            return Observable.throw(errorResponse.text());
        }

        return Observable.throw(constants.UNEXPECTED_RESPONSE);
    }
}
