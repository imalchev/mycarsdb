import { Component } from '@angular/core';
import {VehicleService} from './vehicle/vehicle.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[VehicleService]
})
export class AppComponent {
  title = 'app works!';
}
