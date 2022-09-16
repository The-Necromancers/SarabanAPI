using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class EformDataVer
    {
        public long EvId { get; set; }
        public long EdId { get; set; }
        public string Wid { get; set; } = null!;
        public string EformId { get; set; } = null!;
        public string EdData { get; set; } = null!;
        public string? EformXml { get; set; }
        public string? Pdffile { get; set; }
        public string? EvDate { get; set; }
        public string? EvTime { get; set; }
    }
}
