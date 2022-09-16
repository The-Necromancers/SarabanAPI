using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class ListEform
    {
        public string ListId { get; set; } = null!;
        public string EformId { get; set; } = null!;
        public string Rid { get; set; } = null!;
        public string Active { get; set; } = null!;
    }
}
