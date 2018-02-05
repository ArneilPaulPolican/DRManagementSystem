using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using DRManagementSystem.Entities;

namespace DRManagementSystem.Api_Controllers
{
    [RoutePrefix("api/DR")]
    public class TrnDRController : ApiController
    {
        private Data.DataClasses1DataContext db = new Data.DataClasses1DataContext();

        [HttpGet, Route("List")]
        public List<TrnDeliveryReceipt> GetDRList()
        {
            var DR = from d in db.TrnDeliveries
                     select new TrnDeliveryReceipt
                     {
                         Id = d.Id,
                         DRNumber = d.DRNumber,
                         ManualDRNumber = d.ManualDRNumber,
                         CustomerId = d.CustomerId,
                         Remarks = d.Remarks,
                         CreatedBy = d.CreatedBy,
                         UpdatedBy = d.UpdatedBy,
                         CreatedDateTime = d.CreatedDateTime,
                         UpdatedDateTime = d.UpdatedDateTime
                     };
            return DR.ToList();
        }

        [HttpGet, Route("Detail")]
        public TrnDeliveryReceipt GetDRId(string id)
        {
            var DRId = from d in db.TrnDeliveries
                       where d.Id == Convert.ToInt32(id)
                       select new TrnDeliveryReceipt
                       {
                           Id = d.Id,
                           DRNumber = d.DRNumber,
                           ManualDRNumber = d.ManualDRNumber,
                           CustomerId = d.CustomerId,
                           Remarks = d.Remarks,
                           CreatedBy = d.CreatedBy,
                           UpdatedBy = d.UpdatedBy,
                           CreatedDateTime = d.CreatedDateTime,
                           UpdatedDateTime = d.UpdatedDateTime
                       };
            return (TrnDeliveryReceipt)DRId.FirstOrDefault();
        }

        [HttpPost, Route("Add")]
        public Int32 AddDR(TrnDeliveryReceipt NewDelieveryReceipt)
        {
            try
            {
                Data.TrnDelivery newDR = new Data.TrnDelivery()
                {
                    Id = NewDelieveryReceipt.Id,
                    DRNumber = NewDelieveryReceipt.DRNumber,
                    ManualDRNumber = NewDelieveryReceipt.ManualDRNumber,
                    CustomerId = NewDelieveryReceipt.CustomerId,
                    Remarks = NewDelieveryReceipt.Remarks,
                    CreatedBy = NewDelieveryReceipt.CreatedBy,
                    UpdatedBy = NewDelieveryReceipt.UpdatedBy,
                    CreatedDateTime = NewDelieveryReceipt.CreatedDateTime,
                    UpdatedDateTime = NewDelieveryReceipt.UpdatedDateTime
                };
                db.TrnDeliveries.InsertOnSubmit(newDR);
                db.SubmitChanges();

                return newDR.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        [HttpDelete, Route("Delete")]
        public HttpResponseMessage DeleteDR(string id)
        {
            try
            {
                var DRId = from d in db.TrnDeliveries
                           where d.Id == Convert.ToInt32(id)
                           select d;
                if (DRId.Any())
                {
                    db.TrnDeliveries.DeleteOnSubmit(DRId.First());
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

        [HttpPut, Route("Update")]
        public HttpResponseMessage UpdateDR(string id, TrnDeliveryReceipt UpdateDeliveryReceipt)
        {
            try
            {
                var DRId = from d in db.TrnDeliveries
                           where d.Id == Convert.ToInt32(id)
                           select d;
                if (DRId.Any())
                {
                    var UpdateDRData = DRId.FirstOrDefault();

                    UpdateDRData.DRNumber = UpdateDeliveryReceipt.DRNumber;
                    UpdateDRData.ManualDRNumber = UpdateDeliveryReceipt.ManualDRNumber;
                    UpdateDRData.CustomerId = UpdateDeliveryReceipt.CustomerId;
                    UpdateDRData.Remarks = UpdateDeliveryReceipt.Remarks;
                    UpdateDRData.UpdatedBy = UpdateDeliveryReceipt.UpdatedBy;
                    UpdateDRData.UpdatedDateTime = DateTime.Now;

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

        //====================================================================
        //====================================================================
        //==========================  LINE  ==================================
        //====================================================================
        //====================================================================

        [HttpGet, Route("Line/List")]
        public List<TrnDeliveryReceiptLine> GetDRLineList(string id)
        {
            var DRLine = from d in db.TrnDeliveryLines
                         where d.DRId == Convert.ToInt32(id)
                         select new TrnDeliveryReceiptLine
                         {
                             Id = d.Id,
                             DRId = d.DRId,
                             ItemId = d.ItemId,
                             Qty = d.Qty,
                             UnitId = d.UnitId,
                             Cost = d.Cost,
                             Price = d.Price,
                             Amount = d.Amount
                         };
            return DRLine.ToList();
        }

        [HttpGet, Route("Line/Detail")]
        public TrnDeliveryReceiptLine GetDRLineId(string Id)
        {
            var DRLineId = from d in db.TrnDeliveryLines
                           where d.Id == Convert.ToInt32(Id)
                           select new TrnDeliveryReceiptLine
                           {
                               Id = d.Id,
                               DRId = d.DRId,
                               ItemId = d.ItemId,
                               Qty = d.Qty,
                               UnitId = d.UnitId,
                               Cost = d.Cost,
                               Price = d.Price,
                           };
            return (TrnDeliveryReceiptLine)DRLineId.FirstOrDefault();
        }

        [HttpPost,Route("Line/Add")]
        public Int32 AddDRLine(TrnDeliveryReceiptLine NewDelieveryReceiptLine)
        {
            try
            {
                Data.TrnDeliveryLine NewDRLineId = new Data.TrnDeliveryLine()
                {
                    Id = NewDelieveryReceiptLine.Id,
                    DRId = NewDelieveryReceiptLine.DRId,
                    ItemId = NewDelieveryReceiptLine.ItemId,
                    Qty = NewDelieveryReceiptLine.Qty,
                    UnitId = NewDelieveryReceiptLine.UnitId,
                    Cost = NewDelieveryReceiptLine.Cost,
                    Price = NewDelieveryReceiptLine.Price,
                };
                db.TrnDeliveryLines.InsertOnSubmit(NewDRLineId);
                db.SubmitChanges();

                return NewDRLineId.Id;
            }catch(Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        [HttpDelete,Route("Line/Delete")]
        public HttpResponseMessage DeleteDRLineId(string id)
        {
            try
            {
                var DRLineId = from d in db.TrnDeliveryLines
                               where d.Id == Convert.ToInt32(id)
                               select d;
                if (DRLineId.Any())
                {
                    db.TrnDeliveryLines.DeleteOnSubmit(DRLineId.First());
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

        [HttpPut,Route("Line/Update")]
        public HttpResponseMessage UpdateDRLine(string id,TrnDeliveryReceiptLine UpdateDeliveryLine)
        {
            try
            {
                var DRLIneId = from d in db.TrnDeliveryLines
                               where d.Id == Convert.ToInt32(id)
                               select d;
                if (DRLIneId.Any())
                {
                    var UpdateDRLineData = DRLIneId.FirstOrDefault();

                    UpdateDRLineData.ItemId = UpdateDeliveryLine.ItemId;
                    UpdateDRLineData.Qty = UpdateDeliveryLine.Qty;
                    UpdateDRLineData.UnitId = UpdateDeliveryLine.UnitId;
                    UpdateDRLineData.Cost = UpdateDeliveryLine.Cost;
                    UpdateDRLineData.Price = UpdateDeliveryLine.Price;
                    UpdateDRLineData.Amount = UpdateDeliveryLine.Amount;

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
