using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Models
{
    public class PaymentsModel
    {
        private ParkingEntities db = new ParkingEntities();
        public Payment GetByIdUserMonthPayment(int idUserMonthPayment)
        {
            return db.Payment.SingleOrDefault(payment => payment.idUserMonthPayment == idUserMonthPayment);
        }
    }
}