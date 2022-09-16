using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Doccir
{
    public partial class Drawer
    {
        public string Id { get; set; }
        public string Dsc { get; set; }
        public string Dept { get; set; }
        public string Dbname { get; set; }
        public string Dbconnect { get; set; }
        public string Pathimage { get; set; }
        public byte? Seclev { get; set; }
        public byte? Nossclass { get; set; }
        public byte? Category { get; set; }
        public string GrantedTo { get; set; }
    }
}
