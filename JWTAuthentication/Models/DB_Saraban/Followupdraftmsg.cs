using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Followupdraftmsg
    {
        public string Wid { get; set; } = null!;
        public string? Bid { get; set; }
        public string? Actionmsg { get; set; }
    }
}
