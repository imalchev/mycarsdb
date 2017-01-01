import { Component, OnInit,Input } from '@angular/core';
import { VehicleService } from '../../../services/vehicle.service';
import { Http } from '@angular/http';
import * as vehicleModels from '../../../models/vehicle.models';

@Component({
  selector: 'user-vehicles',
  templateUrl: './user-vehicles.component.html',
  styleUrls: ['./user-vehicles.component.css']
})
export class UserVehiclesComponent implements OnInit {
@Input() vehicles:vehicleModels.VehicleViewModel[];
  constructor() { }

  ngOnInit() {

  }
}
