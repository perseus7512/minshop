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
    
    public partial class level2Class
    {
        public string level1Class { get; set; }
        public string level2Class1 { get; set; }
        public string name { get; set; }
    
        public virtual level1Class level1Class1 { get; set; }
        public virtual stock stock { get; set; }
    }
}
