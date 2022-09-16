using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Borrow
    {
        public string Wid { get; set; } = null!;
        public string Borrowdate { get; set; } = null!;
        public string Borrowname { get; set; } = null!;
        public string Borrowdept { get; set; } = null!;
        public string Willreturndate { get; set; } = null!;
        public string Returndate { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string Bdsc { get; set; } = null!;
        public string Remark { get; set; } = null!;
        public string Itemno { get; set; } = null!;
    }
}
