using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.DoccirCreation
{
    public class RqDetail
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "DocumentId is required")]
        public string DocumentId { get; set; }

        [Required(ErrorMessage = "DocDate is required")]
        public string DocDate { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Description is required", AllowEmptyStrings = true)]
        public string Description { get; set; }

        [Required(ErrorMessage = "FileData is required")]
        public string FileData { get; set; }

        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; }
    }
}
