import { Component, OnInit } from '@angular/core';
import {UserVehiclesComponent} from '../../vehicle/userVehiclesList/user-vehicles.component'
import {VehicleService} from '../../../services/vehicle.service'
import * as vehicleModels from '../../../models/vehicle.models';

@Component({
  selector: 'app-garage',
  templateUrl: './garage.component.html',
  styleUrls: ['./garage.component.css']
})
export class GarageComponent implements OnInit {
vehicles:vehicleModels.VehicleViewModel[];
  constructor(private _vehicleService : VehicleService) { }

  ngOnInit() {
    this._vehicleService.getUserVehicles()
    .subscribe(vehicles=>this.vehicles=vehicles)
  }

}
