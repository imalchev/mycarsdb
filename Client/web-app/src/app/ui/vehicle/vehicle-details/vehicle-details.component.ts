import { Component, OnInit,Input } from '@angular/core';
import * as vehicleModels from '../../../models/vehicle.models';

@Component({
  selector: '[app-vehicle-details]',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.css']
})
export class VehicleDetailsComponent{
@Input() vehicle:vehicleModels.VehicleViewModel
  constructor() { }
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
}
