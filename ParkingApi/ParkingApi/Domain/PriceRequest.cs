using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class PriceRequest
    {
        public decimal valueMinute { get; set; }
        public int idVehicleType { get; set; }
        public decimal valueMonth { get; set; }
    }
}
