using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_User
{
    public partial class IfmsysDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Dsn { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public string AuditL { get; set; }
        public string AuditV { get; set; }
        public string AuditP { get; set; }
        public string AuditA { get; set; }
        public string AuditE { get; set; }
        public string AuditD { get; set; }
        public string AuditG { get; set; }
        public string Status { get; set; }
        public int? Dbsize { get; set; }
    }
}
