using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class ActionCommand
    {
        public string Wid { get; set; } = null!;
        public string Registerno { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string? Bdsc { get; set; }
        public string Registerdte { get; set; } = null!;
        public string Registertime { get; set; } = null!;
        public string? Commanddate { get; set; }
        public string? CommandBy { get; set; }
        public string? Commandmessage { get; set; }
        public string? Commandtowho { get; set; }
    }
}
