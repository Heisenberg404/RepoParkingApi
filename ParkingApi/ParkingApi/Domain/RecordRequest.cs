using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class RecordRequest
    {
        public String license { get; set; }
        public String numCell { get; set; }
    }
}
