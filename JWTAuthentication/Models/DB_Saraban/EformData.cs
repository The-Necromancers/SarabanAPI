using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class EformData
    {
        public long EdId { get; set; }
        public string Wid { get; set; } = null!;
        public string EformId { get; set; } = null!;
        public string EdData { get; set; } = null!;
        public string? EformXml { get; set; }
        public string? Pdffile { get; set; }
    }
}
