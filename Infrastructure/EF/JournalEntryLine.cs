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
    
    public partial class JournalEntryLine
    {
        public int JLineId { get; set; }
        public int EntryId { get; set; }
        public int AccountId { get; set; }
        public decimal DebitAmount { get; set; }
        public Nullable<decimal> CreditAmount { get; set; }
        public string Description { get; set; }
        public int JID { get; set; }
    }
}