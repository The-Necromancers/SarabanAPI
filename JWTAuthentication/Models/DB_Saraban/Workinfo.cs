using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Workinfo
    {
        public Workinfo()
        {
            QrcodeGens = new HashSet<QrcodeGen>();
        }

        public string Wid { get; set; } = null!;
        public string Wdate { get; set; } = null!;
        public string Wsubject { get; set; } = null!;
        public string Worigin { get; set; } = null!;
        public string WownerBdsc { get; set; } = null!;
        public string Wtype { get; set; } = null!;
        public string Refwid { get; set; } = null!;
        public string RegisterBid { get; set; } = null!;
        public string RegisterBdsc { get; set; } = null!;
        public string RegisterUid { get; set; } = null!;
        public string RegisterNo { get; set; } = null!;
        public string RegisterDate { get; set; } = null!;
        public string Registertime { get; set; } = null!;
        public string CompleteDate { get; set; } = null!;
        public string Completetime { get; set; } = null!;
        public string PriorityCode { get; set; } = null!;
        public string Secretlevcode { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public string DeptCode { get; set; } = null!;
        public string Docuname { get; set; } = null!;
        public short Pages { get; set; }
        public string Maxtime { get; set; } = null!;
        public string Dsc { get; set; } = null!;
        public string Wsubtype { get; set; } = null!;
        public string OtherAttach { get; set; } = null!;
        public string Agewid { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Autodelete { get; set; } = null!;

        public virtual ICollection<QrcodeGen> QrcodeGens { get; set; }
    }
}
