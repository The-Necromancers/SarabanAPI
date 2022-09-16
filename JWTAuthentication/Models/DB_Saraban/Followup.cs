using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Followup
    {
        public string Wserial { get; set; } = null!;
        public string Wid { get; set; } = null!;
        public string ActionMsg { get; set; } = null!;
    }
}
