import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

import { VehicleService } from '../../../services/vehicle.service';
import * as vehicleModels from '../../../models/vehicle.models';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  @Input() vehicles: vehicleModels.VehicleViewModel[];
  @Input() vehicleFilter: string;

  private location: string = '';

  constructor(private _router: Router) {
  }

  ngOnInit(): void {
    this.location = this._router.url;
  }

  onSelect(vehicleId: string): void {
    this._router.navigate(['/vehicle', vehicleId]);
  }
}
