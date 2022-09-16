using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Clientinfo
    {
        public string ClientAlias { get; set; } = null!;
        public string ClientFullnameTh { get; set; } = null!;
        public string ClientFullnameEn { get; set; } = null!;
    }
}
