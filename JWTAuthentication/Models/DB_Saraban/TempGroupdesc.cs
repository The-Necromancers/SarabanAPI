using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class TempGroupdesc
    {
        public string ItemNo { get; set; } = null!;
        public string CenterId { get; set; } = null!;
        public string CenterName { get; set; } = null!;
        public string DeptId { get; set; } = null!;
        public string DeptName { get; set; } = null!;
        public string GIndent { get; set; } = null!;
        public string NextNode { get; set; } = null!;
        public string ItemSort { get; set; } = null!;
        public string ItemUsed { get; set; } = null!;
        public string? ParentId { get; set; }
    }
}
