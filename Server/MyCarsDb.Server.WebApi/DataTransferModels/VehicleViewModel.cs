namespace MyCarsDb.Server.WebApi.DataTransferModels
{
    using System.Collections.Generic;

    public class VehicleViewModel : VehicleModel
    {
        public string Id { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public short FuelType { get; set; }
        public List<string> FuelTypesStr { get; set; }
    }
}