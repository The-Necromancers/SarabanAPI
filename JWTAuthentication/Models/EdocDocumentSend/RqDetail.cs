using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentSend
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Wid is required")]
        public string WID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "SenderBasketID is required", AllowEmptyStrings = true)]
        public string SenderBasketID { get; set; }

        [Required(ErrorMessage = "ReceiverBasketID is required")]
        public string ReceiverBasketID { get; set; }
    }
}
