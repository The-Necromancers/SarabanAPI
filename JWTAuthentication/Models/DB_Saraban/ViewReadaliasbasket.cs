using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class ViewReadaliasbasket
    {
        public string Bid { get; set; } = null!;
        public string Bdsc { get; set; } = null!;
        public string Wfid { get; set; } = null!;
        public string Wid { get; set; } = null!;
        public string Expr1 { get; set; } = null!;
        public string Expr2 { get; set; } = null!;
        public string SenderBdsc { get; set; } = null!;
        public string SenderBid { get; set; } = null!;
        public string ActionCode { get; set; } = null!;
        public string FlagDelete { get; set; } = null!;
    }
}
