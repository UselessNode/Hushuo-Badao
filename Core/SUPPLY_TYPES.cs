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
    
    public partial class SUPPLY_TYPES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUPPLY_TYPES()
        {
            this.INVOICES = new HashSet<INVOICES>();
        }
    
        public int SUPPLY_TYPE_ID { get; set; }
        public string SUPPLY_TYPE_NAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICES> INVOICES { get; set; }
    }
}