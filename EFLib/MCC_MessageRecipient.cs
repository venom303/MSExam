//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class MCC_MessageRecipient
    {
        public decimal Id { get; set; }
        public Nullable<decimal> RecipientId { get; set; }
        public Nullable<decimal> MessageId { get; set; }
    
        public virtual MCC_Message MCC_Message { get; set; }
        public virtual MCC_User MCC_User { get; set; }
    }
}
