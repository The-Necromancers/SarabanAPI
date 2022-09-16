using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class Positionpeadb
    {
        public string Bid { get; set; } = null!;
        public string Bdsc { get; set; } = null!;
        public string Ipserver { get; set; } = null!;
        public string Ipdatabase { get; set; } = null!;
        public string Pathimage { get; set; } = null!;
        public string Pathtemp { get; set; } = null!;
        public string Databasename { get; set; } = null!;
        public string Usrid { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string BidClass { get; set; } = null!;
    }
}
