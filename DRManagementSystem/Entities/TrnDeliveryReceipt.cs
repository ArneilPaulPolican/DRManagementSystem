using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRManagementSystem.Entities
{
    public class TrnDeliveryReceipt
    {
        public Int32 Id { get; set; }
        public String DRNumber { get; set; }
        public String ManualDRNumber { get; set; }
        public Int32 CustomerId { get; set; }
        public String Remarks { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Int32 UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
    public class TrnDeliveryReceiptLine
    {
        public Int32 Id { get; set; }
        public Int32 DRId { get; set; }
        public Int32 ItemId { get; set; }
        public Decimal Qty { get; set; }
        public Int32 UnitId { get; set; }
        public Decimal Cost { get; set; }
        public Decimal Price { get; set; }
        public Decimal Amount { get; set; }
    }
}