using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class FlwCabinet
    {
        public string? RdbmsDesc { get; set; }
        public string? RdbmsMainconnect { get; set; }
        public string? RdbmsDatasource { get; set; }
        public string? RdbmsDatabasename { get; set; }
        public string? RdbmsUsername { get; set; }
        public string? RdbmsPassword { get; set; }
        public string? CabName { get; set; }
        public string? RdbmsStatus { get; set; }
    }
}
