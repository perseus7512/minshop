//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace minshop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ship
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ship()
        {
            this.saleInvoice = new HashSet<saleInvoice>();
            this.status = new HashSet<status>();
        }
    
        public string shipId { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string name { get; set; }
        public string pickingListId { get; set; }
        public string memberId { get; set; }
    
        public virtual pickingList pickingList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saleInvoice> saleInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<status> status { get; set; }
    }
}
