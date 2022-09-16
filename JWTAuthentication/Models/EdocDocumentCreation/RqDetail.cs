using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentCreation
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "BasketID is required", AllowEmptyStrings = true)]
        public string BasketID { get; set; }

        [Required(ErrorMessage = "WID is required", AllowEmptyStrings = true)]
        public string WID { get; set; }

        [Required(ErrorMessage = "RefNumber is required", AllowEmptyStrings = true)]
        public string RefNumber { get; set; }

        [Required(ErrorMessage = "From is required")]
        public string From { get; set; }

        [Required(ErrorMessage = "SendTo is required")]
        public string SendTo { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "DocDate is required", AllowEmptyStrings = true)]
        public string DocDate { get; set; }

        [Required(ErrorMessage = "Priority is required", AllowEmptyStrings = true)]
        public string Priority { get; set; }

        [Required(ErrorMessage = "SecretLevel is required", AllowEmptyStrings = true)]
        public string SecretLevel { get; set; }

        [Required(ErrorMessage = "Description is required", AllowEmptyStrings = true)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Remark is required", AllowEmptyStrings = true)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Wstype is required", AllowEmptyStrings = true)]
        public string Wstype { get; set; }

        [Required(ErrorMessage = "ReceiveDoc is required", AllowEmptyStrings = true)]
        public string ReceiveDoc { get; set; }
    }
}
