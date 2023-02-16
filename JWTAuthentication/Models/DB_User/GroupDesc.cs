using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_User
{
    public partial class GroupDesc
    {
        public string Gorder { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string SubLev { get; set; }
        public string Bdsc { get; set; }
        public short Gindent { get; set; }
        public string PrevNode { get; set; }
        public string NextNode { get; set; }
        public string BeginNode { get; set; }
        public string SubGroup { get; set; }
    }
}
