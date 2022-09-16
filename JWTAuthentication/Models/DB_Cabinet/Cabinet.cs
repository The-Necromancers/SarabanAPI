using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Cabinet
{
    public partial class Cabinet
    {
        public string? RdbmsDesc { get; set; }
        public string? RdbmsMainconnect { get; set; }
        public string RdbmsDatasource { get; set; } = null!;
        public string? RdbmsDatabasename { get; set; }
        public string? RdbmsUsername { get; set; }
        public string? RdbmsPassword { get; set; }
        public string? NossDesc { get; set; }
        public string? NossServername { get; set; }
        public string? CabName { get; set; }
    }
}
