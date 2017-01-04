import { Component, OnInit } from '@angular/core';

import { VehicleListComponent } from '../../shared/vehicle-list/vehicle-list.component';
import { VehicleService } from '../../../services/vehicle.service';
import * as vehicleModels from '../../../models/vehicle.models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  vehicles: vehicleModels.VehicleViewModel[];
  vehicleFilter: string;
  
  constructor(private _vehicleService: VehicleService) { }

  ngOnInit() {
    this._vehicleService.getAllVehicles()
      .subscribe((vehicles: vehicleModels.VehicleViewModel[]) => this.vehicles = vehicles);
  }
}
