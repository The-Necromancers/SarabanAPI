using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentUpload
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "BasketID is required", AllowEmptyStrings = true)]
        public string BasketID { get; set; }

        [Required(ErrorMessage = "WID is required")]
        public string WID { get; set; }

        [Required(ErrorMessage = "LinkWID is required", AllowEmptyStrings = true)]
        public string LinkWID { get; set; }

        [Required(ErrorMessage = "FileName is required", AllowEmptyStrings = true)]
        public string FileName { get; set; }

        [Required(ErrorMessage = "FileExtension is required", AllowEmptyStrings = true)]
        public string FileExtension { get; set; }

        [Required(ErrorMessage = "FileData is required", AllowEmptyStrings = true)]
        public string FileData { get; set; }

        [Required(ErrorMessage = "Detail is required", AllowEmptyStrings = true)]
        public string Detail { get; set; }
    }
}
