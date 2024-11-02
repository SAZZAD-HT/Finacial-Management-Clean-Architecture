using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTos
{
    public class JournalAddDto
    {
        public int EntryId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
        public int? ActionBy { get; set; }
        public List<JournalDetailAddDto> JournalDetailAddDtos { get; set; } 
    }
    public class JournalDetailAddDto
    {
        public int EntryDetailId { get; set; }
        public int EntryId { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
        public int? ActionBy { get; set; }
    }
}
