using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Deligation
    {
        public string Usrid { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string ToUsrid { get; set; } = null!;
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
