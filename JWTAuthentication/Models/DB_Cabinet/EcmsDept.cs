using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class EcmsDept
    {
        public string DeptCode { get; set; } = null!;
        public string DeptName { get; set; } = null!;
        public string MatchBid { get; set; } = null!;
        public string Active { get; set; } = null!;
        public string? Status { get; set; }
        public string? Uri { get; set; }
    }
}
