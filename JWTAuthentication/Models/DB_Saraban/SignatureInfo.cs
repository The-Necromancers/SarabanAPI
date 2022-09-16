using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class SignatureInfo
    {
        public string Wid { get; set; } = null!;
        public string Usrid { get; set; } = null!;
        public string Filepath { get; set; } = null!;
        public string Signature { get; set; } = null!;
        public string Imagesfile { get; set; } = null!;
    }
}
