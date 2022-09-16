using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Doccir
{
    public partial class Topic
    {
        public string Title { get; set; }
        public string Query { get; set; }
        public string Dept { get; set; }
        public byte? Seclev { get; set; }
        public string Granted { get; set; }
        public string FoldCode { get; set; }
        public string Entrydate { get; set; }
    }
}
