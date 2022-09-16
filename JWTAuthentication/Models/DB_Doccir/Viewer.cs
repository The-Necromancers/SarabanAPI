using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Doccir
{
    public partial class Viewer
    {
        public byte? Filetype { get; set; }
        public string Ext { get; set; }
        public string Switches { get; set; }
        public string Progname { get; set; }
        public byte? Security { get; set; }
        public string Editor { get; set; }
    }
}
