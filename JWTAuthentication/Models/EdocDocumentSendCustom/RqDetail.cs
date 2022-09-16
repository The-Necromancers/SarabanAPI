using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentSendCustom
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Wid is required")]
        public string WID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "ReceiverBasketID is required")]
        public string ReceiverBasketID { get; set; }

        [Required(ErrorMessage = "Receiver is required")]
        public string Receiver { get; set; }
    }
}
