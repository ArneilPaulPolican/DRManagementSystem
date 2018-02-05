using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRManagementSystem.Entities
{
    public class TrnRecievingReceipt
    {
        public Int32 Id { get; set; }
        public String RRNumber { get; set; }
        public Int32 SupplierId { get; set; }
        public String Remarks { get; set; }
        public String Particulars { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Int32 UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
    public class TrnRecievingReceiptLine
    {
        public Int32 Id { get; set; }
        public Int32 RRId { get; set; }
        public Int32 ItemId { get; set; }
        public Int32 UnitId { get; set; }
        public Decimal Qty { get; set; }
        public Decimal Cost { get; set; }
        public Decimal Amount { get; set; }

    }
}