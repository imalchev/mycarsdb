import { Component, OnInit } from '@angular/core';
import {Vehicle} from './vehicle';
import {VehicleService} from '../services/vehicle.service'
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
  model = new Vehicle(25,"42",new Date());
  ngOnInit() {  
  }
}
