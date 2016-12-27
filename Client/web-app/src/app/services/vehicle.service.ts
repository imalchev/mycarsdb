import {Injectable} from '@angular/core'
import {VehicleModel} from '../models/vehicle.models'
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import {Inject} from '@angular/core'
import 'rxjs/Rx'
// Operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';


Injectable();
export class VehicleService{
constructor(@Inject(Http) private _http:Http){

}

addVehicle(vehicle:VehicleModel):Observable<any>{
 let body = JSON.stringify(vehicle);
 let headers = new Headers({'Content-Type':'application/json'});
 let options = new RequestOptions({headers:headers});
 console.log(body);

 return this._http.post('http://localhost:52120/api/Vehicles/Add',body,options)
 .map((response: Response) => {
                let jsonData: any = response.json();
                return jsonData;
            });
}
getVehicleTypes():Observable<string[]>{
return this._http.get('http://localhost:52120/api/Vehicles/GetVehicleTypes')
.map((response: Response) => <string[]>response.json())
}

getFuelTypes():Observable<string[]>{
return this._http.get('http://localhost:52120/api/Vehicles/GetFuelTypes')
.map((response: Response) => <string[]>response.json())
}


}
