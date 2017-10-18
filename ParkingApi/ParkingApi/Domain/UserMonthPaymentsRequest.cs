﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class UserMonthPaymentsRequest
    {
        public String idUser { get; set; }
        public String name { get; set; }
        public String license { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public String parkCell { get; set; }
    }
}