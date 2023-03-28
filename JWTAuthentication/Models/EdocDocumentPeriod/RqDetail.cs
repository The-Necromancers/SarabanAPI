using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentPeriod
{
    public class RqDetail
    {
        [Required(ErrorMessage = "BasketID is required")]
        [StringLength(13, MinimumLength = 13)]
        public string BasketID { get; set; }
    }
}
