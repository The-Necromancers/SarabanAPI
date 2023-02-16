using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_User
{
    public partial class GroupMember
    {
        public float Gitemno { get; set; }
        public string Mgroupid { get; set; }
        public string Mgroupname { get; set; }
        public string Bid { get; set; }
        public string Bdsc { get; set; }
        public int? Gindent { get; set; }
        public int? Prevnode { get; set; }
        public string NextNode { get; set; }
        public string FromBid { get; set; }
    }
}
