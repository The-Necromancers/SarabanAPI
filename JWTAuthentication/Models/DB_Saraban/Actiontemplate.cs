using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Actiontemplate
    {
        public int Tmpid { get; set; }
        public string TmpBid { get; set; } = null!;
        public string TmpDsc { get; set; } = null!;
        public string TmpUser { get; set; } = null!;
    }
}
