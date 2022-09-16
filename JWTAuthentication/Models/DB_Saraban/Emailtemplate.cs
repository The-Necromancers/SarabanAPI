using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Emailtemplate
    {
        public string TmpCode { get; set; } = null!;
        public string Tmpname { get; set; } = null!;
        public string Tmpto { get; set; } = null!;
        public string Tmpcc { get; set; } = null!;
        public string Tmpbcc { get; set; } = null!;
        public string TmpBid { get; set; } = null!;
        public string TmpUser { get; set; } = null!;
    }
}
