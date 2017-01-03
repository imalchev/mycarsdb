import { Component, OnInit } from '@angular/core';
import {UserVehiclesComponent} from '../../vehicle/userVehiclesList/user-vehicles.component';
import {VehicleService} from '../../../services/vehicle.service';
import * as vehicleModels from '../../../models/vehicle.models';
import { slideInDownAnimation } from '../../shared/animations';


@Component({
  selector: 'app-garage',
  templateUrl: './garage.component.html',
  styleUrls: ['./garage.component.css'],
   animations: [ slideInDownAnimation ]
})
export class GarageComponent implements OnInit {
  isGarage: boolean = true;
  state: string = 'in';
vehicles: vehicleModels.VehicleViewModel[];
  constructor(private _vehicleService: VehicleService) { }

  ngOnInit() {
    this._vehicleService.getUserVehicles()
    .subscribe((vehicles: vehicleModels.VehicleViewModel[]) => this.vehicles = vehicles);
    this.state = (this.state === 'in' ? 'out' : 'in');
  }
}
