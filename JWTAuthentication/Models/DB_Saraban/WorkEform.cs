using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class WorkEform
    {
        public string Wid { get; set; } = null!;
        public string EformId { get; set; } = null!;
        public string EformData { get; set; } = null!;
    }
}
