using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class ParkCellResponse
    {
        public int id { get; set; }
        public String numCell { get; set; }
        public String state { get; set; }
        public String license { get; set; }

    }
}