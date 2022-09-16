using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Docfulltext
    {
        public string Wid { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string Attachname { get; set; } = null!;
        public string Itemno { get; set; } = null!;
        public string Actionmsg { get; set; } = null!;
    }
}
