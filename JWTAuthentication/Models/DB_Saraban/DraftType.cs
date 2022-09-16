using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class DraftType
    {
        public string Wid { get; set; } = null!;
        public string Wtype { get; set; } = null!;
        public string Wsubtype { get; set; } = null!;
        public string? Granted { get; set; }
        public string? Prefix { get; set; }
    }
}
