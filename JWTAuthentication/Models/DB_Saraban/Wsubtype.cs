using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Wsubtype
    {
        public string? Wtype { get; set; }
        public string? Code { get; set; }
        public string? Dsc { get; set; }
        public string? Category { get; set; }
        public string? Bid { get; set; }
        public string? Granted { get; set; }
        public string? SubWtypeNo { get; set; }
        public string? UsedCode { get; set; }
        public string? AliasPosition { get; set; }
    }
}
