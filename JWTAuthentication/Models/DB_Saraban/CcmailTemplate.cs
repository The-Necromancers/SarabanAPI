using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class CcmailTemplate
    {
        public int Itemno { get; set; }
        public string Bid { get; set; } = null!;
        public string Ccname { get; set; } = null!;
        public string Bidto { get; set; } = null!;
        public string Bidtonon { get; set; } = null!;
        public string Bidcc { get; set; } = null!;
        public string Bidccnon { get; set; } = null!;
    }
}
