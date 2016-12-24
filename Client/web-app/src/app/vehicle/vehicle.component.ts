import { Component, OnInit } from '@angular/core';
import {Vehicle} from './vehicle';
import {VehicleService} from './vehicle.service'

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
})
export class VehicleComponent implements OnInit {

  constructor(public _vehicleService:VehicleService) {
  }
  model = new Vehicle(25);
  ngOnInit() {  
  }
}
