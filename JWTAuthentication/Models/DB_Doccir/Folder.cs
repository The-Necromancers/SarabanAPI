using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Doccir
{
    public partial class Folder
    {
        public string FoldCode { get; set; }
        public string Dsc { get; set; }
        public string ViewCode { get; set; }
        public byte? Seclev { get; set; }
    }
}
