using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ParkingApi;
using ParkingApi.Domain;
using System.Data.Entity.Validation;

namespace ParkingApi.Models
{
    public class RecordsModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje = "OK";

        //Funcion para obtener todos los usuarios registrados.
        public IQueryable<Record> SelectAll()
        {
            return db.Record;
        }

        /*public RecordRequest getCellsActive() {

            var result = from r in db.Record
                         select new RecordRequest()
                         {
                             license = r.license,
                             numCell = r.idParkCell.
                             //Where(x => x.status == 1).Select(x => x.numCell)

                         };
            return null
        }*/


        public Record InsertRecord (RecordRequest rq)
        {
            Record recordReturn = new Record();

            try
            {                
                Record record = new Record();
                record.idParkCell = rq.idCell;
                record.license = rq.license;
                record.timeEntry = DateTime.UtcNow.ToLocalTime();
                record.idPrice = rq.idprice;                
                db.Record.Add(record);
                db.SaveChanges();
                UpdateStateParkCells(rq);
                mensaje = "OK";                
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al insertar vehicculo" + e;
            }
            return recordReturn;
        }


        public void UpdateStateParkCells(RecordRequest record)
        {
            // ALQU DIS
           ParkCells parkcell = db.ParkCells.Find(record.idCell);
           parkcell.state = "OCUP";
           parkcell.license = record.license;
           db.Entry(parkcell).State = EntityState.Modified;
           db.SaveChanges();

        }

        public void UpdateStateParkCellsQuit(RecordRequest record, int id)
        {
            // ALQU DIS
            ParkCells parkcell = db.ParkCells.Find(record.idCell);
            Record rq = db.Record.Find(id);

            rq.timeOut = DateTime.UtcNow.ToLocalTime();
            parkcell.state = "DISP";
            parkcell.license = null;
            db.Entry(parkcell).State = EntityState.Modified;
            db.Entry(rq).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Invoice QuitRecord(RecordRequest rq)
        {
            Record recordReturn = new Record();
            Invoice invoice = new Invoice();

            try
            {

                int id = idrecordwithplaca(rq);
                Record record = db.Record.Find(id);
                Price price = db.Price.Find(rq.idprice);
                record.timeEntry = record.timeEntry;
                record.timeOut = DateTime.UtcNow.ToLocalTime();

                decimal valor = 0;
                int diferenciadias = record.timeOut.Value.Day - record.timeEntry.Day;
                int diferenciaHoras = record.timeOut.Value.Hour - record.timeEntry.Hour;
                int diferenciaminutos = record.timeOut.Value.Minute - record.timeEntry.Minute;
                diferenciaHoras = diferenciaHoras * 60;

                if (diferenciadias == 0)
                {
                    decimal value = price.valueMinute;
                    valor = Convert.ToDecimal(diferenciaminutos + diferenciaHoras) * value;
                }
                else if (record.timeOut > record.timeEntry)
                {
                    diferenciadias = (24 * 60) * diferenciadias;
                    decimal value = price.valueMinute;
                    valor = Convert.ToDecimal(diferenciaminutos + diferenciaHoras) * value;
                }
                else {

                    diferenciaHoras = record.timeEntry.Hour - record.timeOut.Value.Hour ;
                    diferenciaminutos = record.timeEntry.Minute - record.timeOut.Value.Minute ;
                    diferenciadias = (24 * 60) * diferenciadias;
                    decimal value = price.valueMinute;
                    valor = Convert.ToDecimal(diferenciaminutos + diferenciaHoras) * value;
                }
                
                invoice.ValorPago = valor;
                invoice.HoraEntrada = record.timeEntry;
                invoice.HoraSalida = DateTime.UtcNow.ToLocalTime();
                
                UpdateStateParkCellsQuit(rq, id);                               

            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error retirar vehiculo" + e;
            }
            return invoice;
        }

        public int idrecordwithplaca (RecordRequest rq)
        {
            var record = db.Record.Where(p => p.license == rq.license).ToList();
            int id = -1;
            DateTime lastdate = new DateTime(1990, 1, 1);


            foreach (Record rec in record)
            {
                if (rec.timeEntry > lastdate)
                {
                    id = rec.Id;
                    lastdate = rec.timeEntry;
                }

            }

            return id;
        }

        public User InsertUser(UserRequest userRequest)
        {
            User userReturn = new User();
            try
            {
                userReturn = this.GetByUsernameUser(userRequest.username);
                if (userReturn == null)
                {
                    User user = new User();
                    user.username = userRequest.username;
                    user.pass = userRequest.pass;
                    db.User.Add(user);
                    db.SaveChanges();
                    mensaje = "OK";
                    userReturn = this.GetByUsernameUser(user.username);
                }
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al crear un usuario" + e;
            }
            return userReturn;

        }

        public String UpdateUser(int id, User user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    mensaje = "NOT_FOUND";
                }

            }

            return mensaje;
        }

        public User GetByIdUser(int id)
        {
            User user = db.User.Find(id);
            return user;
        }

        public User GetByUsernameUser(String username)
        {
            User myUser = db.User.SingleOrDefault(user => user.username == username);


            return myUser;
        }

        public User GetByUsernameAndPassUser(UserRequest userRequest)
        {
            IQueryable<User> u = db.User.Where(User => User.username == userRequest.username);
            User user = u.SingleOrDefault(User => User.pass == userRequest.pass);
            return user;
        }
        public String RemoveUser(User user)
        {
            try
            {
                db.User.Remove(user);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al eliminar un usuario" + e;
            }
            return mensaje;
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.id == id) > 0;
        }
    }
}
