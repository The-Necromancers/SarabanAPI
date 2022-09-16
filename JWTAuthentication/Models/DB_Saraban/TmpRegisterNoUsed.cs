using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class TmpRegisterNoUsed
    {
        public string Bid { get; set; } = null!;
        public string Wtype { get; set; } = null!;
        public string Wsubtype { get; set; } = null!;
        public string Registerno { get; set; } = null!;
        public string Wid { get; set; } = null!;
        public string Usrid { get; set; } = null!;
        public string? Createdate { get; set; }
        public string? Createtime { get; set; }
    }
}
