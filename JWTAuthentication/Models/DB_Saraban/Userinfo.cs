using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Userinfo
    {
        public string Usrid { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string ExpireDate { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public short SecLevCode { get; set; }
        public string Username { get; set; } = null!;
        public string Icqaddress { get; set; } = null!;
        public string Emailaddress { get; set; } = null!;
        public string Computername { get; set; } = null!;
        public string Mainbid { get; set; } = null!;
        public string? Usedencrypt { get; set; }
        public string? SecureSignature { get; set; }
        public string? UsedInbound { get; set; }
        public string? UsedOutbound { get; set; }
        public string? SignaturePath { get; set; }
        public string? SecureIfmflowDepartment { get; set; }
        public string? SecureBasketinfo { get; set; }
        public string? StartDate { get; set; }
    }
}
