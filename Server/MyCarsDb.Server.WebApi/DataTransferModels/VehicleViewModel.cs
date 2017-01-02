using MyCarsDb.Server.WebApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCarsDb.Server.WebApi.DataTransferModels
{
    public class VehicleViewModel:VehicleModel
    {
        public string EncodedId { get
            {
 
                 return Helper.EncodeId(this.VehicleId);
            }
        }
        public string MakeName { get; set; }
        public string  ModelName { get; set; }
        public short FuelType { get; set; }
        public List<string> FuelTypesStr { get; set; }

    }
}