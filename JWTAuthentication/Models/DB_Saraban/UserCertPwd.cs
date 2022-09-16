using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class UserCertPwd
    {
        public string Usrid { get; set; } = null!;
        public string CertPwd { get; set; } = null!;
    }
}
