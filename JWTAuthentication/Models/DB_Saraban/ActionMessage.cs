using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class ActionMessage
    {
        public string Wid { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string Usrid { get; set; } = null!;
        public string Actiondate { get; set; } = null!;
        public string Actiontime { get; set; } = null!;
        public string Actionmsg { get; set; }
        public string Presentto { get; set; }
        public string Commandcode { get; set; }
        public string Signature { get; set; }
        public string Imagefile { get; set; }
    }
}
