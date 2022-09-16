using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_Saraban
{
    public partial class Workinprocess
    {
        public string Wid { get; set; } = null!;
        public string RegisterNo { get; set; } = null!;
        public string ItemNo { get; set; } = null!;
        public string Bid { get; set; } = null!;
        public string Bdsc { get; set; } = null!;
        public string Usrid { get; set; } = null!;
        public string ReceiveDate { get; set; } = null!;
        public string ReceiveTime { get; set; } = null!;
        public string SenderBid { get; set; } = null!;
        public string SenderBdsc { get; set; } = null!;
        public string SenderUid { get; set; } = null!;
        public string ActionCode { get; set; } = null!;
        public string PriorityCode { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public string SecretLevCode { get; set; } = null!;
        public string Viewstatus { get; set; } = null!;
        public string FlagDelete { get; set; } = null!;
        public string InitDate { get; set; } = null!;
        public string InitTime { get; set; } = null!;
        public string CompleteDate { get; set; } = null!;
        public string CompleteTime { get; set; } = null!;
        public string Attach1 { get; set; } = null!;
        public string Attach2 { get; set; } = null!;
        public string SenderMsg { get; set; } = null!;
        public string ActionMsg { get; set; } = null!;
        public string SenderRegisterNo { get; set; } = null!;
        public string? TakeActionname { get; set; }
        public string TakeRemark { get; set; } = null!;
        public string Bookgroup { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Maxtime { get; set; } = null!;
        public string? Actionfollowup { get; set; }
    }
}
