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
    
    public partial class MCC_MessageInDirectory
    {
        public decimal Id { get; set; }
        public Nullable<System.DateTime> ReadDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<decimal> MessageId { get; set; }
        public Nullable<decimal> DirectoryId { get; set; }
    
        public virtual MCC_Directory MCC_Directory { get; set; }
        public virtual MCC_Message MCC_Message { get; set; }
    }
}
