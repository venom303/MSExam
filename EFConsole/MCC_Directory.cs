//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFConsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class MCC_Directory
    {
        public MCC_Directory()
        {
            this.MCC_MessageInDirectory = new HashSet<MCC_MessageInDirectory>();
        }
    
        public decimal Id { get; set; }
        public string Name { get; set; }
        public decimal Type { get; set; }
        public Nullable<decimal> OwnerId { get; set; }
    
        public virtual MCC_User MCC_User { get; set; }
        public virtual ICollection<MCC_MessageInDirectory> MCC_MessageInDirectory { get; set; }
    }
}