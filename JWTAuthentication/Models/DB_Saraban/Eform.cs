using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Eform
    {
        public string EformId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Dsc { get; set; }
        public string? Xml { get; set; }
        public string? Wtype { get; set; }
        public string? Wsubtype { get; set; }
        public string? Granted { get; set; }
        public string? Pdffile { get; set; }
        public string? Draft { get; set; }
    }
}
