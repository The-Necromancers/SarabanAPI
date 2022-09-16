using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Constdate
    {
        public string Tdate { get; set; } = null!;
        public string Ttime { get; set; } = null!;
        public short? Tno { get; set; }
    }
}
