using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Keystore
    {
        public string? Usrid { get; set; }
        public string? Privatekey { get; set; }
        public string? Publickey { get; set; }
        public string? RequestDate { get; set; }
        public string? EffectiveDate { get; set; }
        public string? ExpireDate { get; set; }
        public string? Version { get; set; }
        public string? SerialNumber { get; set; }
        public string? SignatureAlgorithm { get; set; }
        public string? Issuer { get; set; }
        public string? Subject { get; set; }
    }
}
