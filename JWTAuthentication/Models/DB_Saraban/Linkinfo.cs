using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Linkinfo
    {
        public string Createdate { get; set; } = null!;
        public string Linkbid { get; set; } = null!;
        public string Sourcewid { get; set; } = null!;
        public string Linkwid { get; set; } = null!;
        public string Registerno { get; set; } = null!;
        public string Itemno { get; set; } = null!;
        public string Senderbid { get; set; } = null!;
        public string? Initdate { get; set; }
        public string? Inittime { get; set; }
    }
}
