using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_User
{
    public partial class GmemberDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte Seclev { get; set; }
        public string Dept { get; set; }
        public byte Viewlev { get; set; }
        public string Viewdoc { get; set; }
        public string Printdoc { get; set; }
        public string Scandoc { get; set; }
        public string Importdoc { get; set; }
        public string Updnote { get; set; }
        public string Delnote { get; set; }
        public string Grantdoc { get; set; }
        public string Printrep { get; set; }
        public string Movenoss { get; set; }
        public string Viewall { get; set; }
        public string Grantnew { get; set; }
        public string Otherdept { get; set; }
        public string Filingop { get; set; }
        public string Folderop { get; set; }
        public byte Loggedin { get; set; }
    }
}
