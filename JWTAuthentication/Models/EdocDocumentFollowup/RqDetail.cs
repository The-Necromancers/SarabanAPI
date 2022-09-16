using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentFollowup
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Wid is required")]
        public string WID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "BasketID is required", AllowEmptyStrings = true)]
        public string BasketID { get; set; }
    }
}
