using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class LinkRouteflow
    {
        public long Linkid { get; set; }
        public string Wid { get; set; } = null!;
        public string Refid { get; set; } = null!;
        public string Appid { get; set; } = null!;
        public string Actiondate { get; set; } = null!;
        public string Actiontime { get; set; } = null!;
    }
}
