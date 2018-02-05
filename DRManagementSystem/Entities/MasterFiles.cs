using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRManagementSystem.Entities
{
    public class MstCustomer
    {
        public Int32 Id { get; set; }
        public String CustomerCode { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String CellNumber { get; set; }
        public String TellNumber { get; set; }
        public Decimal CreditLimit { get; set; }
    }

    public class MstSupplier
    {
        public Int32 Id { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String Address { get; set; }
        public String ContactPerson { get; set; }
        public String ContactNumber { get; set; }
        public String TellphoneNumber { get; set; }
    }

    public class MstUnit
    {
        public Int32 Id { get; set; }
        public String Unit { get; set; }
    }
    public class MstItem
    {
        public Int32 Id { get; set; }
        public String  ItemCode { get; set; }
        public String ManualCode { get; set; }
        public String Description { get; set; }
        public String Remarks { get; set; }
        public Int32 UnitId { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Int32 UpdatedBy { get; set; }
        public Int32 UpdatedDateTime { get; set; }
    }
    public class MstItemUnit
    {
        public Int32 Id { get; set; }
        public Int32 ItemId { get; set; }
        public Int32 UnitId { get; set; }
        public Decimal Multiplier { get; set; }
        public Decimal BaseCost { get; set; }
    }
}