using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class EcmsSecret
    {
        public string SecretCode { get; set; } = null!;
        public string SecretName { get; set; } = null!;
        public string MatchSecret { get; set; } = null!;
        public string Active { get; set; } = null!;
    }
}
