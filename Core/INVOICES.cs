//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppDB.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class INVOICES
    {
        public int INVOICE_ID { get; set; }
        public Nullable<System.DateTime> DATE_OF_INVOICE { get; set; }
        public Nullable<int> PRODUCT_ID { get; set; }
        public Nullable<int> PURVEYOR_ID { get; set; }
        public Nullable<int> FORWARDER_ID { get; set; }
        public Nullable<int> SUPPLY_TYPE_ID { get; set; }
        public Nullable<int> DELIVERY_TONNAGE { get; set; }
        public Nullable<int> DELIVERY_COST { get; set; }
    
        public virtual FORWARDERS FORWARDERS { get; set; }
        public virtual PRODUCTS PRODUCTS { get; set; }
        public virtual PURVEYORS PURVEYORS { get; set; }
        public virtual SUPPLY_TYPES SUPPLY_TYPES { get; set; }
    }
}