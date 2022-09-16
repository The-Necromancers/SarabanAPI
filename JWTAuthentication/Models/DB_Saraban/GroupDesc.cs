using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class GroupDesc
    {
        public string Gorder { get; set; } = null!;
        public string GroupId { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public string SubLev { get; set; } = null!;
        public string Bdsc { get; set; } = null!;
        public short? Gindent { get; set; }
        public string PrevNode { get; set; } = null!;
        public string NextNode { get; set; } = null!;
        public string BeginNode { get; set; } = null!;
        public string? SubGroup { get; set; }
    }
}
