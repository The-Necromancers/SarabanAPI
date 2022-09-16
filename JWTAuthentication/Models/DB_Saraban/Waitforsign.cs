using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Waitforsign
    {
        public string Wid { get; set; } = null!;
        public string? Status { get; set; }
    }
}
