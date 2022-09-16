using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class TinyUrl
    {
        public string Tinyurl1 { get; set; } = null!;
        public string? Url { get; set; }
        public string? Ifmid { get; set; }
        public string? Cabinet { get; set; }
        public string? Createdate { get; set; }
        public string? Expiredate { get; set; }
        public string? Flag { get; set; }
    }
}
