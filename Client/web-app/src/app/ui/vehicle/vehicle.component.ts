import { Component, OnInit } from '@angular/core';
import {VehicleModel} from '../../models/vehicle.models';
import {VehicleService} from '../../services/vehicle.service'
import {NgForm} from '@angular/forms'
import {Http} from '@angular/http'

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
  providers:[VehicleService]
})
export class VehicleComponent implements OnInit {

  constructor(public _vehicleService:VehicleService) {
  }
  addVehicle(vehicleForm:NgForm):any{
this._vehicleService.addVehicle(vehicleForm.value)
.subscribe(data => "")
  }
  model : VehicleModel = {manufactureDate : new Date()
    ,power:null,exactModel:null, vehicleTypes:null,type:null,availableFuelTypes:null,fuelTypes:null,engineCapacity=null};

  ngOnInit() {  
    var types = this._vehicleService.getVehicleTypes()
    .subscribe(types=>this.model.vehicleTypes= types)

    var fuelTypes = this._vehicleService.getFuelTypes()
    .subscribe(types=>this.model.availableFuelTypes= types)
  }
}

