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
        public List<Report> SelectAll()
        {
            List<Report> returnReport = new List<Report>();

            IQueryable<Reporte_Result> reporte = db.Reporte1();
            var pa = reporte.ToList();
            pa.ForEach(x =>
            {
                Report obj = new Report();
                obj.id = (int)x.ID;
                obj.license = x.LINCESE;
                obj.vehicleType = x.VEHICLE_TYPE;
                obj.timeEntry = (System.DateTime)x.TIMEENTRY;
                obj.timeOut = (System.DateTime)x.TIMEOUT;
                obj.minuteInParking = (int)x.MINUTES_IN_PARKING;
                obj.minutePrice = (int)x.MINUTE_PRICE;
                obj.totalPrice = (int)x.TOTAL_PRICE;
                obj.cell = x.CELL;
                returnReport.Add(obj);
            });

            return returnReport;
        }

        public Invoice InsertRecord(RecordRequest rq)
        {
            Record recordReturn = new Record();
            Invoice invoice = new Invoice();

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
                invoice.mensaje = mensaje;
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al insertar vehicculo" + e;
            }
            return invoice;
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
                else
                {

                    diferenciaHoras = record.timeEntry.Hour - record.timeOut.Value.Hour;
                    diferenciaminutos = record.timeEntry.Minute - record.timeOut.Value.Minute;
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

        public int idrecordwithplaca(RecordRequest rq)
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

    }
}
