using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentCreateBasketInfo
{
    public class RqDetail
    {
        [Required(ErrorMessage = "BasketInfo is required")]
        public object BasketInfo { get; set; }
    }
}
