using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class ViewNotworkinfo
    {
        public string Wid { get; set; } = null!;
        public string RegisterNo { get; set; } = null!;
        public string ReceiveDate { get; set; } = null!;
        public string? Workfowid { get; set; }
    }
}
