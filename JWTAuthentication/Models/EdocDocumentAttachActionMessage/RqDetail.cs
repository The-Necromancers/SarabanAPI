using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentAttachActionMessage
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Wid is required")]
        public string WID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "BasketID is required", AllowEmptyStrings = true)]
        public string BasketID { get; set; }

        [Required(ErrorMessage = "PresentTo is required", AllowEmptyStrings = true)]
        public string PresentTo { get; set; }

        [Required(ErrorMessage = "ActionMessage is required")]
        public string ActionMessage { get; set; }

        [Required(ErrorMessage = "Remark is required", AllowEmptyStrings = true)]
        public string Remark { get; set; }
    }
}
