import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Http } from '@angular/http';

import * as vehicleModels from '../../models/vehicle.models';
import { FuelModel } from '../../models/fuel.model';
import { VehicleService } from '../../services/vehicle.service';
import { Router } from '@angular/router';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'app-vehicle',
    templateUrl: './vehicle.component.html',
    styleUrls: ['./vehicle.component.css'],
    providers: [VehicleService]
})
export class VehicleComponent implements OnInit {
    pageTitle = 'Add Vehicle';
    vehicleExists: boolean = false;
    errorMessage: string;

    availableVehicleTypes: vehicleModels.VehicleTypeModel[] = null;
    availabaleFuelTypes: FuelModel[] = null;
    availableVehicleMakes: vehicleModels.VehicleTypeModel[] = null;
    availableVehicleModels: vehicleModels.VehicleModelModel[] = null;

    model: vehicleModels.VehicleModel = {
        vehicleId: null,
        manufactureDate: '',
        power: null,
        exactModel: null,
        type: null,
        engineCapacity: null,
        regNumber: null,
        makeId: null,
        modelId: null
    };

    myDatePickerOptions = {
        showTodayBtn: false,
        dateFormat: 'yyyy-mm',
        firstDayOfWeek: 'mo',
        sunHighlight: false,
        customPlaceholderTxt: 'Manufacture date',
        // height: '40px',
        width: '200px',
        inline: false,
        selectionTxtFontSize: '16px',
        showClearDateBtn: false,
    };
    constructor(public _vehicleService: VehicleService, private router: Router, private activatedRoute: ActivatedRoute) {
    }

    get manufactureDate() {
        return this.model.manufactureDate;
    };


    onDateChanged(event: any) {
        // TO DO: set first day of month
        this.model.manufactureDate = event.formatted;

        console.log('onDateChanged(): ', event.date, ' - jsdate: ',
            new Date(event.jsdate).toLocaleDateString(), ' - formatted: ',
            event.formatted, ' - epoc timestamp: ', event.epoc);
    }

    addVehicle(vehicleForm: NgForm): void {
        if (!this.vehicleExists) {
            this._vehicleService.addVehicle(this.model)
                .subscribe(data => this.router.navigate(['/garage']),
                (response: string) => this.errorMessage = response);
        } else {
            this._vehicleService.editVehicle(this.model)
                .subscribe(data => this.router.navigate(['/garage']));
        }
    }

    onSelect(makeId) {
        this._vehicleService.getModels(makeId)
            .subscribe((models: vehicleModels.VehicleModelModel[]) => this.availableVehicleModels = models,
            (response: string) => this.handleResponse(response));
    }

    ngOnInit() {
        let id;
        this.activatedRoute.params.subscribe((params: Params) => id = params['id']);
        if (id) {
            this._vehicleService.getVehicleById(id)
                .subscribe((types: vehicleModels.VehicleModel) => {this.model = types,this.onSelect(types.makeId) },
                (response: string) => this.handleResponse(response));
            this.pageTitle = 'Edit Vehicle';
            this.vehicleExists = true;
        }

        this._vehicleService.getVehicleTypes()
            .subscribe((types: vehicleModels.VehicleTypeModel[]) => this.availableVehicleTypes = types,
            (response: string) => this.handleResponse(response));

        this._vehicleService.getFuelTypes()
            .subscribe((types: FuelModel[]) => this.availabaleFuelTypes = types,
            (response: string) => this.handleResponse(response));

        this._vehicleService.getMakes()
            .subscribe((makes: vehicleModels.VehicleMakeModel[]) => this.availableVehicleMakes = makes,
            (response: string) => this.handleResponse(response));
    }

    handleResponse(response: string): void {
        this.errorMessage = response;
    }
}


