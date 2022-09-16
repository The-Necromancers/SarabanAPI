using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Ifmtemplate
    {
        public string TmpCode { get; set; } = null!;
        public string TmpWid { get; set; } = null!;
        public string TmpSubj { get; set; } = null!;
        public string TmpDsc { get; set; } = null!;
        public string TmpOrigin { get; set; } = null!;
        public string TmpOwner { get; set; } = null!;
        public string TmpRefwid { get; set; } = null!;
        public string TmpAttach { get; set; } = null!;
        public string TmpBid { get; set; } = null!;
        public string TmpUser { get; set; } = null!;
    }
}
