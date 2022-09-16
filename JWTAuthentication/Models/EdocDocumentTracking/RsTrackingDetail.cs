namespace JWTAuthentication.Models.EdocDocumentTracking
{
    public class RsTrackingDetail
    {
        public string Wid { get; set; }
        public string SenderBasketID { get; set; }
        public string SenderBasketDsc { get; set; }
        public string SenderUsername { get; set; }
        public string SenderRegisterNo { get; set; }
        public string InitDate { get; set; }
        public string InitTime { get; set; }
        public string ReceiverBasketID { get; set; }
        public string ReceiverBasketDsc { get; set; }
        public string ReceiverUsername { get; set; }
        public string ReceiverRegisterNo { get; set; }
        public string ReceiveDate { get; set; }
        public string ReceiveTime { get; set; }
        public string CompleteDate { get; set; }
        public string CompleteTime { get; set; }
        public string StatusCode { get; set; }
        public string ActionMessage { get; set; }
    }
}
