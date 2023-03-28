using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentReceive
{
    public class RqDetail
    {
        [Required(ErrorMessage = "BasketID is required")]
        [StringLength(13, MinimumLength = 13)]
        public string BasketID { get; set; }
    }
}
