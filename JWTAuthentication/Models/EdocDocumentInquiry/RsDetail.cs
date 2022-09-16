namespace JWTAuthentication.Models.EdocDocumentInquiry
{
    public class RsDetail
    {
        public string WID { get; set; }
        public string RefNumber { get; set; }
        public string From { get; set; }
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string DocDate { get; set; }
        public string Priority { get; set; }
        public string SecretLevel { get; set; }
        public List<RsAttachmentDetail> AttachmentDetail { get; set; }
        public List<RsActionMessageDetail> ActionMessageDetail { get; set; }
    }
}
