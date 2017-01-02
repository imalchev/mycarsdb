export interface VehicleModel {
   vehicleId:number,
   power: number;
   exactModel: string;
   manufactureDate: string;
   engineCapacity: number;
   type: string;
   regNumber:string,
   makeId: number;
   modelId: number;
}

export interface VehicleViewModel extends VehicleModel{
    encodedId:string,
    modelName:string;
    makeName:string;
    fuelTypesStr:string[];  
}

export interface VehicleMakeModel {
    id: number;
    name: string;
}

export interface VehicleModelModel {
    id: number;
    name: string;
}

export interface VehicleTypeModel {
    id: number;
    name: string;
}
