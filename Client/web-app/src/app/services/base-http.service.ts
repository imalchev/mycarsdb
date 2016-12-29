import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { BadRequest } from '../models/common.models';
import * as constants from '../common/constants';
import * as httpStatusCodes from '../common/httpStatusCodes';

export abstract class BaseHttpService {

    constructor(protected _http: Http) { }

    protected _getJsonHeaders(): Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Accept', 'application/json');
        return headers;
    }

    /** tries to get error message as BadRequest or string */
    protected _handleError(error: any): Observable<string | BadRequest> {
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
}
