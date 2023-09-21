using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.EdocDocumentCancel
{
    public class EdocDocCancelRq
    {
        public RqHeader RqHeader { get; set; }
        public RqDetail RqDetail { get; set; }
    }
}
