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
    [RoutePrefix("api/PO")]
    public class TrnPOController : ApiController
    {
        private Data.DataClasses1DataContext db = new Data.DataClasses1DataContext();

        //***************************************************
        //LIST
        //***************************************************
        [HttpGet, Route("List")]
        [Authorize]
        public List<TrnPurchaseOrder> GetPOList()
        {
            var PO = from d in db.TrnPOs
                     select new TrnPurchaseOrder
                     {
                         Id = d.Id,
                         PONumber = d.PONumber,
                         Remarks = d.Remarks,
                         Particulars = d.Particulars,
                         CreatedBy = d.CreatedBy,
                         CreatedDateTime = d.CreatedDateTime,
                         UpdatedBy = d.UpdatedBy,
                         UpdatedDateTime = d.UpdatedDateTime
                     };
            return PO.ToList();
        }

        //**********************************************
        //DETAIL
        //**********************************************
        [HttpGet, Route("Detail")]
        [Authorize]
        public TrnPurchaseOrder GetTrnPOId(string id)
        {
            var POId = from d in db.TrnPOs
                       where d.Id == Convert.ToInt32(id)
                       select new TrnPurchaseOrder
                       {
                           Id = d.Id,
                           PONumber = d.PONumber,
                           Remarks = d.Remarks,
                           Particulars = d.Particulars,
                           CreatedBy = d.CreatedBy,
                           CreatedDateTime = d.CreatedDateTime,
                           UpdatedBy = d.UpdatedBy,
                           UpdatedDateTime = d.UpdatedDateTime
                       };
            return (TrnPurchaseOrder)POId.FirstOrDefault();
        }

        //**************************************
        // ADD NEW
        //**************************************
        [HttpPost,Route("Add")]
        [Authorize]
        public Int32 AddPO(TrnPurchaseOrder NewPUrchaseOrder)
        {
            try
            {
                Data.TrnPO newPO = new Data.TrnPO()
                {
                    Id = NewPUrchaseOrder.Id,
                    PONumber = NewPUrchaseOrder.PONumber,
                    Remarks = NewPUrchaseOrder.Remarks,
                    Particulars = NewPUrchaseOrder.Particulars,
                    CreatedBy = NewPUrchaseOrder.CreatedBy,
                    CreatedDateTime = NewPUrchaseOrder.CreatedDateTime,
                    UpdatedBy = NewPUrchaseOrder.UpdatedBy,
                    UpdatedDateTime = NewPUrchaseOrder.UpdatedDateTime
                };
                db.TrnPOs.InsertOnSubmit(newPO);
                db.SubmitChanges();

                return newPO.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        //***************************************************
        // DELETE RECORD
        //***************************************************
        [HttpDelete,Route("Delete")]
        [Authorize]
        public HttpResponseMessage DeletePOId(string id)
        {
            try
            {
                var POData = from d in db.TrnPOs
                             where d.Id == Convert.ToInt32(id)
                             select d;
                if (POData.Any())
                {
                    db.TrnPOs.DeleteOnSubmit(POData.First());
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
        
        //*********************************************
        // UODATE
        //************************************************
        [HttpPut,Route("Update")]
        [Authorize]
        public HttpResponseMessage Update(string id,TrnPurchaseOrder UpdatePO)
        {
            try
            {
                var TrnPOData = from d in db.TrnPOs
                                where d.Id == Convert.ToInt32(id)
                                select d;
                if (TrnPOData.Any())
                {
                    var UpdatePOData = TrnPOData.FirstOrDefault();

                    UpdatePOData.PONumber = UpdatePO.PONumber;
                    UpdatePOData.Remarks = UpdatePO.Remarks;
                    UpdatePOData.Particulars = UpdatePO.Particulars;
                    UpdatePOData.UpdatedBy = UpdatePO.UpdatedBy;
                    UpdatePOData.UpdatedDateTime = UpdatePO.UpdatedDateTime;

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


        //=================================================================================
        //=================================================================================
        //=============================   LINE  ===========================================
        //=================================================================================
        //=================================================================================

        [HttpGet,Route("Line/List")]
        public List<TrnPurchaseOrderLine> GetPOLineList(string id)
        {
            var POLine = from d in db.TrnPOLines
                         where d.POId==Convert.ToInt32(id)
                         select new TrnPurchaseOrderLine
                         {
                             Id=d.Id,
                             POId=d.POId,
                             ItemId=d.ItemId,
                             UnitId=d.UnitId,
                             Qty=d.Qty,
                             Cost=d.Cost,
                             Amount=d.Amount
                         };
            return POLine.ToList();
        }

        [HttpGet,Route("Line/Detail")]
        public TrnPurchaseOrderLine GetPOLineId(string id)
        {
            var POLineId = from d in db.TrnPOLines
                           where d.Id == Convert.ToInt32(id)
                           select new TrnPurchaseOrderLine
                           {
                               Id = d.Id,
                               POId = d.POId,
                               ItemId = d.ItemId,
                               UnitId = d.UnitId,
                               Qty = d.Qty,
                               Cost = d.Cost,
                               Amount = d.Amount
                           };
            return (TrnPurchaseOrderLine)POLineId.FirstOrDefault();
        }

        [HttpPost,Route("Line/Add")]
        public Int32 AddPOLine(TrnPurchaseOrderLine NewPurchaeOrderLine)
        {
            try
            {
                Data.TrnPOLine newPOLine = new Data.TrnPOLine()
                {
                    Id = NewPurchaeOrderLine.Id,
                    POId=NewPurchaeOrderLine.POId,
                    ItemId=NewPurchaeOrderLine.ItemId,
                    UnitId=NewPurchaeOrderLine.UnitId,
                    Qty=NewPurchaeOrderLine.Qty,
                    Cost=NewPurchaeOrderLine.Cost,
                    Amount=NewPurchaeOrderLine.Amount

                };
                db.TrnPOLines.InsertOnSubmit(newPOLine);
                db.SubmitChanges();

                return newPOLine.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }

        [HttpDelete,Route("Line/Delete")]
        public HttpResponseMessage DeletePOLineId(string id)
        {
            try
            {
                var POLineData = from d in db.TrnPOLines
                             where d.Id == Convert.ToInt32(id)
                             select d;
                if (POLineData.Any())
                {
                    db.TrnPOLines.DeleteOnSubmit(POLineData.First());
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
        public HttpResponseMessage UpdateLine(string id, TrnPurchaseOrderLine UpdatePOLine)
        {
            try
            {
                var TrnPOLineData = from d in db.TrnPOLines
                                where d.Id == Convert.ToInt32(id)
                                select d;
                if (TrnPOLineData.Any())
                {
                    var UpdatePOLineData = TrnPOLineData.FirstOrDefault();

                    UpdatePOLineData.ItemId = UpdatePOLine.ItemId;
                    UpdatePOLineData.Qty = UpdatePOLine.Qty;
                    UpdatePOLineData.Cost = UpdatePOLine.Cost;
                    UpdatePOLineData.Amount = UpdatePOLine.Amount;

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
