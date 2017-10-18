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
                userMonthPaymentsResponse.endDate = payment.endDate;

            }
            return userMonthPaymentsResponse;
        }

        public UserMonthPaymentsResponse InsertUserMonthPayment(UserMonthPaymentsRequest objUserMonthPaymentsRequest)
        {

            UserMonthPayment userMonthPayment = db.UserMonthPayments.SingleOrDefault(userMonth => userMonth.numberIdentification == objUserMonthPaymentsRequest.idUser);
             if (userMonthPayment == null)
            {
                userMonthPayment.numberIdentification = objUserMonthPaymentsRequest.idUser;
                userMonthPayment.name = objUserMonthPaymentsRequest.name;
                userMonthPayment.state = true;
                userMonthPayment.idParkCells = objUserMonthPaymentsRequest.parkCell;
                db.UserMonthPayments.Add(userMonthPayment);
                db.SaveChanges();

            }
            
            return null;
        }
    }
}