using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class ViewUserinfo
    {
        public string Usrid { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public short SecLevCode { get; set; }
        public string Mainbid { get; set; } = null!;
        public string Expr1 { get; set; } = null!;
        public string Bdsc { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? SecretDsc { get; set; }
        public string Expr2 { get; set; } = null!;
        public string StatusDsc { get; set; } = null!;
        public string Deptcode { get; set; } = null!;
        public string? Expr3 { get; set; }
        public string? Deptname { get; set; }
        public string Username { get; set; } = null!;
        public string Wfid { get; set; } = null!;
        public string? Prefixdept { get; set; }
        public string Emailaddress { get; set; } = null!;
    }
}
