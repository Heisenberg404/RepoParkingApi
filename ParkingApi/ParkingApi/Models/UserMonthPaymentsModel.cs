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

        public Invoice UpdateUserMonthPayment (UserMonthPaymentsRequest userRequestPayment)
        {
            //var findUser = db.UserMonthPayments.Find(userRequestPayment.idPrice);

            // Encontramos el usuario filtrando por la cédula
            var findUser = db.UserMonthPayments.Where(x => x.numberIdentification == userRequestPayment.idUser).FirstOrDefault();

            Invoice invoice = new Invoice();

            if (findUser != null)
            {

                UpdateUserMonthPaymentRegister(userRequestPayment, findUser);
                UpdateParkCell(userRequestPayment, findUser);
                decimal value = newPayment(userRequestPayment, findUser);
                invoice.ValorPago = value;
                invoice.mensaje = "OK";    
                
            }
            else {

                invoice.mensaje = "No";
            }


            return invoice;

        }

        public void UpdateParkCell(UserMonthPaymentsRequest userRequestPayment, UserMonthPayment findUser)
        {
            var findParkCell = db.ParkCells.Find(findUser.idParkCells);

            if(findParkCell != null) {

                findParkCell.state = "ALQU";
                findParkCell.license = userRequestPayment.license;
                db.Entry(findParkCell).State = EntityState.Modified;
                db.SaveChanges();
            }
        
        }

        public void UpdateUserMonthPaymentRegister(UserMonthPaymentsRequest userRequestPayment, UserMonthPayment findUser)
        {
           
            if (findUser != null)
            {
                findUser.state = true;
                db.Entry(findUser).State = EntityState.Modified;
                db.SaveChanges();
            }
                                          
        }

        public decimal newPayment(UserMonthPaymentsRequest userRequestPayment, UserMonthPayment findUser)
        {
            //Encontramos el precio de la mensualidad 
            var findPrice = db.Price.Find(userRequestPayment.idPrice);

            //Método para calcular el valor de la mensualidad según la fecha ingresada
            decimal valuemonth = calculateValueMonth(findPrice, userRequestPayment);

            Payment payment = new Payment();
            //Obtenemos la fecha actual
            DateTime currentDate = DateTime.UtcNow.ToLocalTime();

            if (userRequestPayment.startDate.Year < currentDate.Year) {
                
                // Crear el nuevo registro
                payment.paymentDate = currentDate;
                payment.startDate = userRequestPayment.startDate;
                payment.idUserMonthPayment = findUser.id;
                payment.endDate = userRequestPayment.endDate;
                db.Payment.Add(payment);
                db.SaveChanges();
            }

            return valuemonth;
        }

        public decimal calculateValueMonth(Price findPrice, UserMonthPaymentsRequest userRequestPayment) {

            //Obtenemos el valor de la mensualidad según el id
            decimal valuemonth = findPrice.valueMonth;
            DateTime startDate = userRequestPayment.startDate;
            DateTime endDate = userRequestPayment.endDate;

            //Calculamos la diferencia entre los meses de las fechas ingresadas
            int diferenciaMeses = endDate.Month - startDate.Month;            

            if (diferenciaMeses > 1)
            {

                valuemonth = valuemonth * diferenciaMeses;
            }

            return valuemonth;
        }
    }
}