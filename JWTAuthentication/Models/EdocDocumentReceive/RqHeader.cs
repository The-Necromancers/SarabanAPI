using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentReceive
{
    public class RqHeader
    {
        [Required(ErrorMessage = "AppId is required")]
        public string AppId { get; set; }
    }
}
