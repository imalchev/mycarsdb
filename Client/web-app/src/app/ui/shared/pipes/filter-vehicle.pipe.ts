import { Pipe, PipeTransform } from '@angular/core';
import * as vehicleModels from '../../../models/vehicle.models';

@Pipe({
  name: 'filterVehicle'
})
export class FilterVehiclePipe implements PipeTransform {

transform(value: vehicleModels.VehicleViewModel[], filterBy: string): vehicleModels.VehicleViewModel[] {
    filterBy = filterBy ? filterBy.toLocaleLowerCase().trim() : null;
        return filterBy ? value.filter((vehicle: vehicleModels.VehicleViewModel) =>
            (
            vehicle.makeName.toLocaleLowerCase().indexOf(filterBy) !== -1)
            ||
            (vehicle.modelName.toLocaleLowerCase().indexOf(filterBy) !== -1)
            ||
            (vehicle.exactModel.toLocaleLowerCase().indexOf(filterBy) !== -1)
            ||
            ((vehicle.makeName+' '+vehicle.modelName+' '+vehicle.exactModel)
            .toLocaleLowerCase().indexOf(filterBy) !== -1)
             )
              : value 
}
}
