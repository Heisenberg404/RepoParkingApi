using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Domain
{
    public class Invoice
    {
        public decimal ValorPago { get; set; }
        public DateTime HoraSalida { get; set; }
        public DateTime HoraEntrada { get; set; }
        public String mensaje { get; set; }
    }
}