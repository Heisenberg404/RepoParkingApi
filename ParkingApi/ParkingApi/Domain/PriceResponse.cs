using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class PriceResponse
    {
        public int id { get; set; }
        public decimal valueMinute { get; set; }
        public int idVehicleType { get; set; }
        public decimal valueMonth { get; set; }
    }
}
