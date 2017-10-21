using ParkingApi.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                userMonthPaymentsResponse.id = payment.UserMonthPayment.id;

                var fecha = DateTime.Compare(payment.endDate, DateTime.Now);
                if (fecha > 0)
                {
                    userMonthPaymentsResponse.isPermited = true;
                }else{
                    userMonthPaymentsResponse.isPermited = false;
                }

                var tiempo = ((userMonthPaymentsResponse.startDate - userMonthPaymentsResponse.endDate).Days) / 30;

            }
            return userMonthPaymentsResponse;
        }

        public UserMonthPaymentsResponse InsertUserMonthPayment(UserMonthPaymentsRequest objUserMonthPaymentsRequest)
        {
            UserMonthPayment objuserMonthPayment = db.UserMonthPayments.SingleOrDefault(userMonth => userMonth.numberIdentification == objUserMonthPaymentsRequest.idUser);
             if (objuserMonthPayment == null)
            {
                UserMonthPayment userMonthPayment = new UserMonthPayment();
                userMonthPayment.numberIdentification = objUserMonthPaymentsRequest.idUser;
                userMonthPayment.name = objUserMonthPaymentsRequest.name;
                userMonthPayment.state = true;
                userMonthPayment.idParkCells = objUserMonthPaymentsRequest.parkCell;
                db.UserMonthPayments.Add(userMonthPayment);
                db.SaveChanges();

                UserMonthPayment ObjuserMonthPayment = db.UserMonthPayments.SingleOrDefault(userMonth => userMonth.numberIdentification == objUserMonthPaymentsRequest.idUser);
                Payment payment = new Payment();
                payment.paymentDate = System.DateTime.Now;
                payment.startDate = objUserMonthPaymentsRequest.startDate;
                payment.endDate = objUserMonthPaymentsRequest.endDate;
                payment.idUserMonthPayment = ObjuserMonthPayment.id;
                db.Payment.Add(payment);
                db.SaveChanges();

                ParkCells parkcell = db.ParkCells.Find(objUserMonthPaymentsRequest.parkCell);
                parkcell.state = "ALQU";
                parkcell.license = objUserMonthPaymentsRequest.license;
                db.Entry(parkcell).State = EntityState.Modified;
                db.SaveChanges();

                userMonthPaymentsResponse.id = ObjuserMonthPayment.id;
                userMonthPaymentsResponse.idUser = objUserMonthPaymentsRequest.idUser;
                userMonthPaymentsResponse.name = objUserMonthPaymentsRequest.name;
                userMonthPaymentsResponse.license = objUserMonthPaymentsRequest.license;
                userMonthPaymentsResponse.startDate = objUserMonthPaymentsRequest.startDate;
                userMonthPaymentsResponse.endDate = objUserMonthPaymentsRequest.endDate;
                userMonthPaymentsResponse.isPermited = true;

                Price Objprice = db.Price.SingleOrDefault(price => price.idVehicleType == objUserMonthPaymentsRequest.vehicleType);
                var tiempo = (((userMonthPaymentsResponse.endDate - userMonthPaymentsResponse.startDate).Days) / 30)*Objprice.valueMonth;
                userMonthPaymentsResponse.TotalPrice = (int)tiempo;
            } else
            {
                ParkCells parkCell = db.ParkCells.SingleOrDefault(cell => cell.id == objUserMonthPaymentsRequest.parkCell);
                userMonthPaymentsResponse.mensaje = "User exists in parkCell " + parkCell.numCell;
            }
            
            return userMonthPaymentsResponse;
        }
    }
}