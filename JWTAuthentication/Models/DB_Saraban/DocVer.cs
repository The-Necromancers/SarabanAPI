using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class DocVer
    {
        public long DocvId { get; set; }
        public string Wid { get; set; } = null!;
        public string DocvName { get; set; } = null!;
        public string DocvContenttype { get; set; } = null!;
        public byte[] DocvData { get; set; } = null!;
        public string DocvDate { get; set; } = null!;
        public string DocvTime { get; set; } = null!;
    }
}
