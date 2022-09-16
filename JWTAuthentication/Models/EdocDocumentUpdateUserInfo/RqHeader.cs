using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentUpdateUserInfo
{
    public class RqHeader
    {
        [Required(ErrorMessage = "AppId is required")]
        public string AppId { get; set; }
    }
}
