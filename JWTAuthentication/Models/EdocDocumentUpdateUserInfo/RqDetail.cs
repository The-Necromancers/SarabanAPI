using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentUpdateUserInfo
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Username is required")]
        public object Username { get; set; }
    }
}
