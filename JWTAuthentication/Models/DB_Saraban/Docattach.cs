using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Docattach
    {
        public string Wid { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string Attachdate { get; set; } = null!;
        public string Attachtime { get; set; } = null!;
        public string? Attachname { get; set; }
        public string Userattach { get; set; } = null!;
        public string Contextattach { get; set; } = null!;
        public string Itemno { get; set; } = null!;
        public string Actionmsg { get; set; } = null!;
        public string Linkwid { get; set; } = null!;
        public string Allowupdate { get; set; } = null!;
    }
}
