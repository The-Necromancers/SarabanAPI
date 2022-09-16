using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Doccir
{
    public partial class Regbook
    {
        public string Id { get; set; }
        public string Entrydate { get; set; }
        public string Grp { get; set; }
        public string Dsc { get; set; }
        public string Dept { get; set; }
        public string Originator { get; set; }
        public string Times { get; set; }
    }
}
