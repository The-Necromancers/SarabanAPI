using System;
using System.Collections.Generic;

namespace JWTAuthentication.Models.DB_User
{
    public partial class UserDetail
    {
        public string Id { get; set; }
        public string Lname { get; set; }
        public string Lpwd { get; set; }
        public string Pname { get; set; }
        public string Tname { get; set; }
        public string Tsurname { get; set; }
        public string Ename { get; set; }
        public string Esurname { get; set; }
        public string PosAdmin { get; set; }
        public string PosWork { get; set; }
        public string PosReserve { get; set; }
        public string PosLevel { get; set; }
        public string PosUnder { get; set; }
        public string MDept { get; set; }
        public string SDept { get; set; }
        public string UStatus { get; set; }
        public string UId1 { get; set; }
        public string UId2 { get; set; }
        public string UAddress { get; set; }
        public string UTel1 { get; set; }
        public string UTel2 { get; set; }
        public string UFax { get; set; }
        public string UEmail { get; set; }
        public string UTime { get; set; }
        public string UTrack { get; set; }
        public string UChgpwdnext { get; set; }
        public string UAlwchgpwd { get; set; }
        public string UPwdexpday { get; set; }
        public string UPwdexpdate { get; set; }
        public string UPause { get; set; }
    }
}
