using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class Report
    {
        public int id { get; set; }
        public String license { get; set; }
        public String vehicleType { get; set; }
        public DateTime timeEntry { get; set; }
        public DateTime timeOut { get; set; }
        public int minuteInParking { get; set; }
        public int minutePrice { get; set; }
        public int totalPrice { get; set; }
        public String cell { get; set; }
    }
}