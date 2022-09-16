using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class DocSign
    {
        public string Wid { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string Itemno { get; set; } = null!;
        public string Usrid { get; set; } = null!;
        public string Filepath { get; set; } = null!;
        public string Signature { get; set; } = null!;
    }
}
