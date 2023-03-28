﻿using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentPeriod
{
    public class RqHeader
    {
        [Required(ErrorMessage = "AppId is required")]
        public string AppId { get; set; }
    }
}
