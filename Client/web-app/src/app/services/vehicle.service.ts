import {Injectable} from '@angular/core'
import {Vehicle} from '../vehicle/vehicle'
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

addVehicle(vehicle:Vehicle):Observable<any>{
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
}