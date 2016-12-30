import { Component, OnInit } from '@angular/core';
import * as vehicleModels from '../../models/vehicle.models';
import { FuelModel } from '../../models/fuel.model';
import {VehicleService} from '../../services/vehicle.service'
import {NgForm} from '@angular/forms';
import {Http} from '@angular/http';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
  providers: [VehicleService]
})
export class VehicleComponent implements OnInit {
isCalendarShown = false;

 availableVehicleTypes: vehicleModels.VehicleTypeModel[] = null;
 availabaleFuelTypes: FuelModel[] = null;
 availableVehicleMakes: vehicleModels.VehicleTypeModel[] = null;
 availableVehicleModels: vehicleModels.VehicleModelModel[] = null;

 model: vehicleModels.VehicleModel = 
{
  manufactureDate : new Date(),
  power: null,
  exactModel:null,
  type:null,
 engineCapacity: null,
 regNumber:null,
 makeId: null,
 modelId: null
};
  constructor(public _vehicleService: VehicleService) {
  }
  addVehicle(vehicleForm:NgForm): void {
this._vehicleService.addVehicle(vehicleForm.value)
.subscribe(data => '');//TODO redirect to garage
 }

 onSelect(makeId) {
     this._vehicleService.getModels(makeId)
                .subscribe(types=>this.availableVehicleModels = types)
 }


  ngOnInit() {
      this._vehicleService.getVehicleTypes()
    .subscribe(types=>this.availableVehicleTypes= types)

     this._vehicleService.getFuelTypes()
    .subscribe(types=>this.availabaleFuelTypes= types);

     this._vehicleService.getMakes()  
    .subscribe(types=>this.availableVehicleMakes=types);
  }
}


