export interface VehicleModel {
   power: number;
   exactModel: string;
   manufactureDate: Date;
   engineCapacity: number;
   type: string;
   regNumber:string,
   makeId: number;
   modelId: number;
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