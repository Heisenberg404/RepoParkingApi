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
            IQueryable<Payment> payments = db.Payment.Where(payment => payment.idUserMonthPayment == idUserMonthPayment);

            return payments.OrderByDescending(payment => payment.id).FirstOrDefault();
                
        }
    }
}