import { Component, OnInit } from '@angular/core';

import { VehicleService } from '../../../services/vehicle.service';

import { VehicleListComponent } from '../../shared/vehicle-list/vehicle-list.component';
import * as vehicleModels from '../../../models/vehicle.models';
import { slideInDownAnimation } from '../../shared/animations';

@Component({
  selector: 'app-garage',
  templateUrl: './garage.component.html',
  styleUrls: ['./garage.component.css'],
  animations: [slideInDownAnimation]
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
