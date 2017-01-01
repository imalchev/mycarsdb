import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Http } from '@angular/http';

import * as vehicleModels from '../../models/vehicle.models';
import { FuelModel } from '../../models/fuel.model';
import { VehicleService } from '../../services/vehicle.service';
import {Router} from '@angular/router'

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css'],
  providers: [VehicleService]
})
export class VehicleComponent implements OnInit {

  constructor(public _vehicleService: VehicleService, private router: Router) {
  }

    isCalendarShown = false;

    availableVehicleTypes: vehicleModels.VehicleTypeModel[] = null;
    availabaleFuelTypes: FuelModel[] = null;
    availableVehicleMakes: vehicleModels.VehicleTypeModel[] = null;
    availableVehicleModels: vehicleModels.VehicleModelModel[] = null;

    model: vehicleModels.VehicleModel = {
      manufactureDate: new Date(),
      power: null,
      exactModel: null,
      type: null,
      engineCapacity: null,
      regNumber: null,
      makeId: null,
      modelId: null
    };

    get manufactureDate() {
        return {
            day: this.model.manufactureDate.getDay(),
            month: this.model.manufactureDate.getMonth() + 1,
            year: this.model.manufactureDate.getFullYear()
        };
    };

    myDatePickerOptions = {
      showTodayBtn: false,
      dateFormat: 'yyyy-mm',
      firstDayOfWeek: 'mo',
      sunHighlight: false,
      customPlaceholderTxt: 'manufacture date',
      // height: '40px',
      width: '200px',
      inline: false,
      selectionTxtFontSize: '16px'
    };

    onDateChanged(event: any) {
        // TO DO: set first day of month
        this.model.manufactureDate = new Date(event.date.year, event.date.month - 1, event.date.day);

        console.log('onDateChanged(): ', event.date, ' - jsdate: ',
            new Date(event.jsdate).toLocaleDateString(), ' - formatted: ',
            event.formatted, ' - epoc timestamp: ', event.epoc);
    }

  addVehicle(vehicleForm: NgForm): void {
      this._vehicleService.addVehicle(this.model)
          .subscribe(data => this.router.navigate(['/garage']));
  }

  onSelect(makeId) {
      this._vehicleService.getModels(makeId)
          .subscribe(types => this.availableVehicleModels = types);
  }


  ngOnInit() {
      this._vehicleService.getVehicleTypes()
          .subscribe((types: vehicleModels.VehicleTypeModel[]) => this.availableVehicleTypes = types);

      this._vehicleService.getFuelTypes()
          .subscribe(types => this.availabaleFuelTypes = types);

      this._vehicleService.getMakes()
          .subscribe( types => this.availableVehicleMakes = types);
  }
}


