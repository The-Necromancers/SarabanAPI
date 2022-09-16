using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentCreateEForm
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "BasketID is required", AllowEmptyStrings = true)]
        public string BasketID { get; set; }

        [Required(ErrorMessage = "SendTo is required")]
        public string SendTo { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "DocDate is required", AllowEmptyStrings = true)]
        public string DocDate { get; set; }

        [Required(ErrorMessage = "FormID is required")]
        public string FormID { get; set; }

        [Required(ErrorMessage = "EformData is required")]
        public object EformData { get; set; }
    }
}
