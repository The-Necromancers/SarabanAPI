using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class EcmsPriority
    {
        public string PriorityCode { get; set; } = null!;
        public string PriorityName { get; set; } = null!;
        public string MatchPriority { get; set; } = null!;
        public string Active { get; set; } = null!;
    }
}
