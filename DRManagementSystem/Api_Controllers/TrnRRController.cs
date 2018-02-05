using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DRManagementSystem.Entities;

namespace DRManagementSystem.Api_Controllers
{
    [RoutePrefix("api/RR")]
    public class TrnRRController : ApiController
    {
        private Data.DataClasses1DataContext db = new Data.DataClasses1DataContext();


        //**********************************
        //LIST
        //**********************************

        [HttpGet,Route("List")]
        [Authorize]
        public List<TrnRecievingReceipt> GetRRList()
        {
            var RR= from d in db.TrnRRs
                     select new TrnRecievingReceipt
                     {
                         Id=d.Id,
                         RRNumber=d.RRNumber,
                         SupplierId=d.SupplierId,
                         Remarks=d.Remarks,
                         Particulars=d.Particulars,
                         CreatedBy=d.CreatedBy,
                         CreatedDateTime=d.CreatedDateTime,
                         UpdatedBy=d.UpdatedBy,
                         UpdatedDateTime=d.UpdatedDateTime
                     };
            return RR.ToList();
        }

        //******************************************
        //DETAIL
        //******************************************
        [HttpGet,Route("Detail")]
        [Authorize]
        public TrnRecievingReceipt GetTrnRRId(string id)
        {
            var RRId = from d in db.TrnRRs
                       where d.Id == Convert.ToInt32(id)
                       select new TrnRecievingReceipt
                       {
                           Id = d.Id,
                           RRNumber = d.RRNumber,
                           SupplierId = d.SupplierId,
                           Remarks = d.Remarks,
                           Particulars = d.Particulars,
                           CreatedBy = d.CreatedBy,
                           CreatedDateTime = d.CreatedDateTime,
                           UpdatedBy = d.UpdatedBy,
                           UpdatedDateTime = d.UpdatedDateTime
                       };
            return (TrnRecievingReceipt)RRId.FirstOrDefault();
        }

        //***************************************
        //Add
        //***************************************
        [HttpPost,Route("Add")]
        [Authorize]
        public Int32 AddRR(TrnRecievingReceipt NewReceivingReceipt)
        {
            try
            {
                Data.TrnRR newRR = new Data.TrnRR()
                {
                    Id = NewReceivingReceipt.Id,
                    RRNumber = NewReceivingReceipt.RRNumber,
                    SupplierId = NewReceivingReceipt.SupplierId,
                    Remarks = NewReceivingReceipt.Remarks,
                    Particulars = NewReceivingReceipt.Particulars,
                    CreatedBy = NewReceivingReceipt.CreatedBy,
                    CreatedDateTime = NewReceivingReceipt.CreatedDateTime,
                    UpdatedBy = NewReceivingReceipt.UpdatedBy,
                    UpdatedDateTime = NewReceivingReceipt.UpdatedDateTime
                };
                db.TrnRRs.InsertOnSubmit(newRR);
                db.SubmitChanges();

                return newRR.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }


        //****************************************************
        //DELETE
        //****************************************************
        [HttpDelete,Route("Delete")]
        public HttpResponseMessage DeleteRR(string id)
        {
            try
            {
                var RRData = from d in db.TrnRRs
                             where d.Id == Convert.ToInt32(id)
                             select d;
                if (RRData.Any())
                {
                    db.TrnRRs.DeleteOnSubmit(RRData.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        //**************************************************
        //UPDATE
        //**************************************************
        [HttpPut,Route("Update")]
        public HttpResponseMessage Update(string id,TrnRecievingReceipt UpdateRR)
        {
            try
            {
                var RRData = from d in db.TrnRRs
                             where d.Id == Convert.ToInt32(id)
                             select d;
                if (RRData.Any())
                {
                    var UpdateRRData = RRData.FirstOrDefault();
                    UpdateRRData.RRNumber = UpdateRR.RRNumber;
                    UpdateRRData.SupplierId = UpdateRR.SupplierId;
                    UpdateRRData.Remarks = UpdateRR.Remarks;
                    UpdateRRData.Particulars = UpdateRR.Particulars;
                    UpdateRRData.UpdatedBy = UpdateRR.UpdatedBy;
                    UpdateRRData.UpdatedDateTime = UpdateRR.UpdatedDateTime;

                    db.SubmitChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        //=================================================================================
        //=================================================================================
        //=============================   LINE  ===========================================
        //=================================================================================
        //=================================================================================

        [HttpGet,Route("Line/List")]
        public List<TrnRecievingReceiptLine> GetRRLineList(string id)
        {
            var RRLineId = from d in db.TrnRRLines
                           where d.RRId == Convert.ToInt32(id)
                           select new TrnRecievingReceiptLine
                           {
                               Id = d.Id,
                               RRId = d.RRId,
                               ItemId = d.ItemId,
                               UnitId = d.UnitId,
                               Qty = d.Qty,
                               Cost = d.Cost,
                               Amount = d.Amount
                           };
            return RRLineId.ToList();
        }

        [HttpGet,Route("Line/Detail")]
        public TrnRecievingReceiptLine GetRRLineId(string id)
        {
            var RRLineId = from d in db.TrnRRLines
                           where d.Id == Convert.ToInt32(id)
                           select new TrnRecievingReceiptLine
                           {
                               Id = d.Id,
                               RRId = d.RRId,
                               ItemId = d.ItemId,
                               UnitId = d.UnitId,
                               Qty = d.Qty,
                               Cost = d.Cost,
                               Amount = d.Amount
                           };
            return (TrnRecievingReceiptLine)RRLineId.FirstOrDefault();
        }
        

        [HttpPut,Route("Line/Add")]
        public Int32 AddRRLine(TrnRecievingReceiptLine NewReceivingReceiptLine)
        {
            try
            {
                Data.TrnRRLine newRRLine = new Data.TrnRRLine()
                {
                    Id = NewReceivingReceiptLine.Id,
                    RRId = NewReceivingReceiptLine.RRId,
                    ItemId = NewReceivingReceiptLine.ItemId,
                    UnitId = NewReceivingReceiptLine.UnitId,
                    Qty = NewReceivingReceiptLine.Qty,
                    Cost = NewReceivingReceiptLine.Cost,
                    Amount = NewReceivingReceiptLine.Amount
                };
                db.TrnRRLines.InsertOnSubmit(newRRLine);
                db.SubmitChanges();

                return newRRLine.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }


        [HttpDelete,Route("Line/Delete")]
        public HttpResponseMessage DeleteRRLine(string id)
        {
            try
            {
                var RRLineData = from d in db.TrnRRLines
                             where d.Id == Convert.ToInt32(id)
                             select d;
                if (RRLineData.Any())
                {
                    db.TrnRRLines.DeleteOnSubmit(RRLineData.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut, Route("Line/Update")]
        public HttpResponseMessage UpdateLine(string id, TrnRecievingReceiptLine UpdateRRLine)
        {
            try
            {
                var RRLineData = from d in db.TrnRRLines
                             where d.Id == Convert.ToInt32(id)
                             select d;
                if (RRLineData.Any())
                {
                    var UpdateRRLineData = RRLineData.FirstOrDefault();

                    UpdateRRLineData.ItemId = UpdateRRLine.ItemId;
                    UpdateRRLineData.UnitId = UpdateRRLine.UnitId;
                    UpdateRRLineData.Qty = UpdateRRLine.Qty;
                    UpdateRRLineData.Cost = UpdateRRLine.Cost;
                    UpdateRRLineData.Amount = UpdateRRLine.Amount;


                    db.SubmitChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
