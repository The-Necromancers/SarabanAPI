using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Actioninfo
    {
        public string Code { get; set; } = null!;
        public string Dsc { get; set; } = null!;
        public string OrderAction { get; set; } = null!;
    }
}
