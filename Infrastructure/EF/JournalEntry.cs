//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class JournalEntry
    {
        public int EntryId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Description { get; set; }
        public string TotalDebit { get; set; }
        public string TotalCredit { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<int> ActionBy { get; set; }
    }
}