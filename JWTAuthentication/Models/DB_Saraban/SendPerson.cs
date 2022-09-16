using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class SendPerson
    {
        public int SpId { get; set; }
        public string Wid { get; set; } = null!;
        public string SendDate { get; set; } = null!;
        public string SendTime { get; set; } = null!;
        public string SenderUid { get; set; } = null!;
        public string SenderBid { get; set; } = null!;
        public string ReciverUid { get; set; } = null!;
        public string ReciverBid { get; set; } = null!;
        public string FlagUpdate { get; set; } = null!;
        public string Msg { get; set; } = null!;
    }
}
