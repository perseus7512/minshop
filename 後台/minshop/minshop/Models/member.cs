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
    
    public partial class member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public member()
        {
            this.saleInvoice = new HashSet<saleInvoice>();
            this.shoppingCart = new HashSet<shoppingCart>();
        }
    
        public string memberId { get; set; }
        public string name { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string sex { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string companyNumbers { get; set; }
        public string company { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saleInvoice> saleInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shoppingCart> shoppingCart { get; set; }
    }
}
