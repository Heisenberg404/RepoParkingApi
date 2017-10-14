using ParkingApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingApi.Models
{
    public class UserMonthPaymentsModel
    {
        private ParkingEntities db = new ParkingEntities();
        PaymentsModel paymentsModel = new PaymentsModel();
        UserMonthPaymentsResponse userMonthPaymentsResponse = new UserMonthPaymentsResponse();
        public UserMonthPaymentsResponse GetById(int id)
        {
            UserMonthPayment userMonthPayment = db.UserMonthPayments.SingleOrDefault(userMonth => userMonth.idParkCells == id);
            if (userMonthPayment != null)
            {
                Payment payment = paymentsModel.GetByIdUserMonthPayment(userMonthPayment.id);
                userMonthPaymentsResponse.idUser = payment.UserMonthPayment.numberIdentification;
                userMonthPaymentsResponse.name = payment.UserMonthPayment.name;
                userMonthPaymentsResponse.license = payment.UserMonthPayment.ParkCell.license;
                userMonthPaymentsResponse.startDate = payment.startDate;
                userMonthPaymentsResponse.startDate = payment.endDate;

            }
            return userMonthPaymentsResponse;
        } 

    }
}