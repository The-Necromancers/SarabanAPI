using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Docfiling
    {
        public string FileId { get; set; } = null!;
        public string FileDesc { get; set; } = null!;
        public string YearStart { get; set; } = null!;
        public string YearEnd { get; set; } = null!;
        public string Prioritycode { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
