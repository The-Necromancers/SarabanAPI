using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class DefineEform
    {
        public string Eformno { get; set; } = null!;
        public string Eformname { get; set; } = null!;
        public string Deptcode { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string TargetPath { get; set; } = null!;
        public string EformStatus { get; set; } = null!;
    }
}
