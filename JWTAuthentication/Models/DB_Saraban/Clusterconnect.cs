using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Clusterconnect
    {
        public string Wid { get; set; } = null!;
        public string Bidclass { get; set; } = null!;
        public string Senderclass { get; set; } = null!;
        public string Bidconnect { get; set; } = null!;
        public string Senderconnect { get; set; } = null!;
    }
}
