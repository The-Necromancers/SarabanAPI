using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentInproc
{
    public class RqHeader
    {
        [Required(ErrorMessage = "AppId is required")]
        public string AppId { get; set; }
    }
}
