using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class EcmsStatus
    {
        public string StatusCode { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public string MatchStatus { get; set; } = null!;
        public string Active { get; set; } = null!;
    }
}
