//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MinShop_frontdesk.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class shoppingDetail
    {
        public string cartId { get; set; }
        public string productId { get; set; }
        public string memberId { get; set; }
        public Nullable<System.DateTime> putDate { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual shoppingCart shoppingCart { get; set; }
        public virtual stock stock { get; set; }
    }
}
