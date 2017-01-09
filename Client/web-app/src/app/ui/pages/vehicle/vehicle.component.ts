import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router, Params } from '@angular/router';

import * as vehicleModels from '../../../models/vehicle.models';
import { FuelModel } from '../../../models/fuel.model';
import { VehicleService } from '../../../services/vehicle.service';

@Component({
    selector: 'app-vehicle',
    templateUrl: './vehicle.component.html',
    styleUrls: ['./vehicle.component.css']    
})
export class VehicleComponent implements OnInit {
    pageTitle = 'Add Vehicle';
    vehicleId: string = null;
    errorMessage: string;

    availableVehicleTypes: vehicleModels.VehicleTypeModel[] = null;
    availabaleFuelTypes: FuelModel[] = null;
    availableVehicleMakes: vehicleModels.VehicleTypeModel[] = null;
    availableVehicleModels: vehicleModels.VehicleModelModel[] = null;

    model: vehicleModels.VehicleModel = {
        manufactureDate: null,
        power: null,
        exactModel: null,
        type: null,
        engineCapacity: null,
        regNumber: null,
        makeId: null,
        modelId: null
    };

    constructor(private _vehicleService: VehicleService, private _router: Router, private _activatedRoute: ActivatedRoute) {
    }

    ngOnInit(): void {
        this._activatedRoute.params.subscribe((params: Params) => {
            this.vehicleId = params['id'];
            if (this.vehicleId) {
                this._vehicleService.getVehicleById(this.vehicleId)
                    .subscribe((vehicle: vehicleModels.VehicleModel) => {
                        this.model = vehicle;
                        this.onMakeSelect(vehicle.makeId);
                    },
                    (response: string) => this.handleResponse(response));

                this.pageTitle = 'Edit Vehicle';
            }
        });



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

    onMakeSelect(makeId): void {
        this._vehicleService.getModels(makeId)
            .subscribe((models: vehicleModels.VehicleModelModel[]) => this.availableVehicleModels = models,
            (response: string) => this.handleResponse(response));
    }

    save(vehicleForm: vehicleModels.VehicleModel): void {
        if (!this.vehicleId) {
            this._vehicleService.addVehicle(this.model)
                .subscribe(data => this._router.navigate(['/garage']),
                (response: string) => this.errorMessage = response);
        } else {
            this._vehicleService.editVehicle(this.vehicleId, this.model)
                .subscribe(data => this._router.navigate(['/garage']));
        }
    }

    handleResponse(response: string): void {
        this.errorMessage = response;
    }
}
