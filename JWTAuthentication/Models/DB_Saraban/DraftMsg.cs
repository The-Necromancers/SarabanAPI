using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class DraftMsg
    {
        public long DraftId { get; set; }
        public string? Bid { get; set; }
        public string? Wid { get; set; }
        public string? Usrid { get; set; }
        public string? CreateDate { get; set; }
        public string? CreateTime { get; set; }
        public string? Thread { get; set; }
        public string? Subject { get; set; }
        public string? Command { get; set; }
        public string? Action { get; set; }
        public string? Role { get; set; }
        public string? CommandDate { get; set; }
        public string? SignStatus { get; set; }
        public string? SugCommand { get; set; }
    }
}
