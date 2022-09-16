using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class QrcodeGen
    {
        public string Qrcode { get; set; } = null!;
        public string Wid { get; set; } = null!;

        public virtual Workinfo WidNavigation { get; set; } = null!;
    }
}
