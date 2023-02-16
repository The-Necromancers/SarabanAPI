using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_User
{
    public partial class Viewsysall
    {
        public string Id { get; set; }
        public string Usrid { get; set; }
        public string Pname { get; set; }
        public string Fname { get; set; }
        public string Surname { get; set; }
        public string Fullname { get; set; }
        public string Sysmainconnect { get; set; }
        public string Sysname { get; set; }
        public string Sysdsn { get; set; }
        public string Sysuid { get; set; }
        public string Syspwd { get; set; }
        public string Sysstatus { get; set; }
        public string Lname { get; set; }
        public string Password { get; set; }
        public byte? Seclev { get; set; }
        public int? Viewlev { get; set; }
        public string Dept { get; set; }
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
        public byte? Loggedin { get; set; }
    }
}
