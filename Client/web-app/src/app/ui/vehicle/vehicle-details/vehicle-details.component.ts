import { Component, OnInit,Input } from '@angular/core';
import * as vehicleModels from '../../../models/vehicle.models';
import {Router} from '@angular/router';

@Component({
  selector: '[app-vehicle-details]',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.css']
})
export class VehicleDetailsComponent implements OnInit {
@Input() vehicle:vehicleModels.VehicleViewModel
  constructor(private _router : Router)  {
    
   }
  public location = '' ;
get engineCapacity(): number {
        return this.vehicle.engineCapacity 
    }

    get type(): string {
        return  this.vehicle.type;
    }

    get manufatureDate(): string {
      var date = new Date(this.vehicle.manufactureDate);
     var month = ((date.getMonth()+1)>=10)? (date.getMonth()+1) : '0' + (date.getMonth()+1); 
        return month + '/'+ date.getFullYear();
    }

    get power(): string {
        return this.vehicle.power +" kw, ";
    }
    get modelName():string{
      return this.vehicle.modelName
   }

   get makeName():string{
     return this.vehicle.makeName
   }

   get fuelTypes():string{
     return this.vehicle.fuelTypesStr.join();
   }

   get exactModel():string{
     return this.vehicle.exactModel;
   }

   get encodedId():string{
     return this.vehicle.encodedId
   }

   ngOnInit() {
this.location = this._router.url;
   }
   onSelect(encodedId:string){
      this._router.navigate(['/editVehicle', encodedId]);
   }
}
