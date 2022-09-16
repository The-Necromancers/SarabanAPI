using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class ViewNewdocument
    {
        public string Wid { get; set; } = null!;
        public string PriorityCode { get; set; } = null!;
        public string Expr1 { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string RegisterNo { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public string Viewstatus { get; set; } = null!;
        public string ReceiveDate { get; set; } = null!;
        public string ReceiveTime { get; set; } = null!;
        public string Docuname { get; set; } = null!;
    }
}
