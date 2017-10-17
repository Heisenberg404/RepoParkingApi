using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class RecordRequest
    {
        public int id { get; set; }
        public String license { get; set; }
        public int idCell { get; set; }
        public String timeEntry { get; set; }
        public String timeOut { get; set; }
        public int idprice { get; set; }
    }
}
