using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Bukeystore
    {
        public int Gencode { get; set; }
        public string DeptCode { get; set; } = null!;
        public string Startdate { get; set; } = null!;
        public string Enddate { get; set; } = null!;
        public string Pwdread { get; set; } = null!;
        public string Pwdencrypt { get; set; } = null!;
    }
}
