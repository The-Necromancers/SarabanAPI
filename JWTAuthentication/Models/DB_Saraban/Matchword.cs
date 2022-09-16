using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Matchword
    {
        public int Code { get; set; }
        public string Mainwords { get; set; } = null!;
        public string? Detailwords { get; set; }
        public string? Bid { get; set; }
        public string? Category { get; set; }
    }
}
