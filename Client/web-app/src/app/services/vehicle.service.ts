import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';

import 'rxjs/Rx';

// Operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import * as vehicleModels from '../models/vehicle.models';
import { FuelModel } from '../models/fuel.model';
import { BadRequest } from '../models/common.models';

import { AuthService } from './auth.service';
import { BaseHttpService } from './base-http.service';

import * as constants from '../common/constants';

@Injectable()
export class VehicleService extends BaseHttpService{
    constructor(http: Http, private _authService: AuthService) {
        super(http);
    }

    addVehicle(vehicle: vehicleModels.VehicleModel): Observable<void | string>{
        let authToken = this._authService.getToken();
        let body = JSON.stringify(vehicle);
        let headers = this._getJsonHeaders();
        this._authService.setAuthorizationHeader(headers);
        let options = new RequestOptions({headers: headers});

        return this._http.post(`${constants.BASE_API_URI}/vehicles/add`, body, options)
            // .map((response: Response) => {
            //     let jsonData: any = response.json();
            //     return jsonData;
            //     })
            .catch(this._handleError);
    }

    getVehicleTypes(): Observable<vehicleModels.VehicleTypeModel[] | string> {
        let url = `${constants.BASE_API_URI}/vehicles/getVehicleTypes`;
        return this._http.get(url)
            .map((response: Response) => <vehicleModels.VehicleTypeModel[]>response.json())
            .catch(this._handleError);
    }

getFuelTypes(): Observable<FuelModel[]> {
let url = `${constants.BASE_API_URI}/vehicles/getFuelTypes`;
return this._http.get(url)
.map((response: Response) => <FuelModel[]>response.json());
}

 getMakes(): Observable<vehicleModels.VehicleMakeModel[]>{
     let url = `${constants.BASE_API_URI}/vehicles/getMakes`;
     return this._http.get(url)
.map((response: Response) => <vehicleModels.VehicleMakeModel[]>response.json());
 }

  getModels(id): Observable<vehicleModels.VehicleModelModel[]>{
     let url = `${constants.BASE_API_URI}/vehicles/getModelsByMakeId?id=${id}`;
     return this._http.get(url)
.map((response: Response) => <vehicleModels.VehicleModelModel[]>response.json());
 }

}
