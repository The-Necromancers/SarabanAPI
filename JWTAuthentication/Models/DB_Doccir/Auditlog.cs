using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Doccir
{
    public partial class Auditlog
    {
        public string Auditdate { get; set; }
        public string Audittime { get; set; }
        public string Usrid { get; set; }
        public string Usrdept { get; set; }
        public string Usraction { get; set; }
        public string Usrtype { get; set; }
        public string Usrscaned { get; set; }
        public string Id { get; set; }
        public string FoldCode { get; set; }
        public string Grp { get; set; }
        public byte? Seclev { get; set; }
        public int? Nossobj { get; set; }
        public string Storage { get; set; }
        public string Origname { get; set; }
        public string Docuname { get; set; }
        public string Extension { get; set; }
        public int? Pages { get; set; }
        public byte? Filetype { get; set; }
        public string Entrydate { get; set; }
        public string Originator { get; set; }
        public string Dsc { get; set; }
        public string Dept { get; set; }
        public byte? Category { get; set; }
        public string Granted { get; set; }
    }
}
