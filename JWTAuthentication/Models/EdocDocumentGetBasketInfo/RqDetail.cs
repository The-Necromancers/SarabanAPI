using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentGetBasketInfo
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
    }
}
