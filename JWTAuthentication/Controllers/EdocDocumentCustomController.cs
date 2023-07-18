using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWTAuthentication.Models.DB_Saraban;
using JWTAuthentication.Models.DB_Doccir;
using JWTAuthentication.Models.DB_Cabinet;
using JWTAuthentication.Models.EdocDocumentActionMessage;
using JWTAuthentication.Models.EdocDocumentCreation;
using JWTAuthentication.Models.EdocDocumentCreateBasketInfo;
using JWTAuthentication.Models.EdocDocumentEdit;
using JWTAuthentication.Models.EdocDocumentInquiry;
using JWTAuthentication.Models.EdocDocumentIncoming;
using JWTAuthentication.Models.EdocDocumentInproc;
using JWTAuthentication.Models.EdocDocumentSend;
using JWTAuthentication.Models.EdocDocumentSendCustom;
using JWTAuthentication.Models.EdocDocumentFollowup;
using JWTAuthentication.Models.EdocDocumentTracking;
using JWTAuthentication.Models.EdocDocumentUpload;
using JWTAuthentication.Models.EdocDocumentUpdateUserInfo;
using JWTAuthentication.Models.EdocDocumentGetBasketInfo;
using JWTAuthentication.Models.EdocDocumentReceive;
using JWTAuthentication.Models.EdocDocumentInternal;
using JWTAuthentication.Models.DoccirCreation;
using JWTAuthentication.Models.DoccirDetail;
using JWTAuthentication.Models.ResponseMsg;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;
using System.Net;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JWTAuthentication.Models.EdocDocumentPeriod;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdocDocumentCustomController : ControllerBase
    {
        private readonly RwSaraban64Context _context;
        private readonly RwCabinetContext _context_cabinet;
        private readonly IConfiguration _configuration;
        private string msgLog;

        public EdocDocumentCustomController(RwSaraban64Context context, RwCabinetContext context_cabinet, IConfiguration configuration)
        {
            _context = context;
            _context_cabinet = context_cabinet;
            _configuration = configuration;
        }

        //Saraban Custom Version//

        #region

        // SSS //

        [Authorize]
        [HttpGet]
        [Route("DocumentInquiryforSSS")]
        public ActionResult<EdocDocDetailRs> DocumentInquiryforSSS(string wid, [Required(ErrorMessage = "usrID is required")] string usrID, string basketID, string year, string regNumber, string workType, string bookGroup, [Required(ErrorMessage = "appID is required")] string appID)
        {
            string method = "DocumentInquiryforSSS";
            string userBid = string.Empty;
            try
            {
                //********** check userid **********//
                if (!string.IsNullOrWhiteSpace(year))
                {
                    _context.Database.GetDbConnection().ConnectionString = SetConnectionString(year);
                }
                else
                {
                    _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                }

                var dataUser = _context.Userinfos.Where(a => a.Usrid == usrID && a.Bid == basketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == usrID && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, appID);

                        return StatusCode(400, responseMsg);
                    }
                    else
                    {
                        userBid = userMainBid.Bid;
                    }

                }
                else
                {
                    userBid = dataUser.Bid;
                }
                //*********************************//

                //********** check data **********//
                string workID = CheckInputParams(wid, userBid, year, regNumber, workType, bookGroup);

                if (workID == "0")
                {
                    var responseMsg = CheckData(method, appID, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == workID).FirstOrDefault();
                    var docAttms = _context.Docattaches.Where(a => a.Wid == workID).ToList();
                    var attmDetails = new List<RsAttachmentDetail>();
                    var getFileExt = Path.GetExtension(dataWorkInfo.Docuname);

                    if (!string.IsNullOrWhiteSpace(dataWorkInfo.Docuname) && !string.IsNullOrWhiteSpace(getFileExt))
                    {
                        attmDetails.Add(new RsAttachmentDetail
                        {
                            AttachDate = dataWorkInfo.RegisterDate + " " + dataWorkInfo.Registertime,
                            Detail = "เอกสารแนบหลัก",
                            URL = CreateURL(dataWorkInfo.Docuname)
                        });
                    }

                    foreach (Docattach docAttm in docAttms)
                    {
                        var strExt = Path.GetExtension(docAttm.Attachname);
                        if (!string.IsNullOrWhiteSpace(strExt))
                        {
                            attmDetails.Add(new RsAttachmentDetail
                            {
                                AttachDate = docAttm.Attachdate + " " + docAttm.Attachtime,
                                Detail = docAttm.Contextattach,
                                URL = CreateURL(docAttm.Attachname)
                            });
                        }
                    }

                    var actionMsgs = _context.ActionMessages.Where(a => a.Wid == workID).ToList();
                    var actionMsgDetail = new List<RsActionMessageDetail>();

                    foreach (ActionMessage actionMsg in actionMsgs)
                    {
                        if (actionMsg != null)
                        {
                            actionMsgDetail.Add(new RsActionMessageDetail
                            {
                                BasketID = actionMsg.Bid,
                                BasketDsc = BasketDescription(actionMsg.Bid),
                                Username = actionMsg.Usrid,
                                ActionDate = actionMsg.Actiondate + " " + actionMsg.Actiontime,
                                CommandCode = actionMsg.Commandcode,
                                Presentto = actionMsg.Presentto,
                                ActionMsg = actionMsg.Actionmsg,
                                SignImage = actionMsg.Imagefile
                            });
                        }
                    }

                    var ret = new Models.EdocDocumentInquiry.RsDetail
                    {
                        WID = dataWorkInfo.Wid,
                        DocDate = dataWorkInfo.Wdate,
                        From = dataWorkInfo.Worigin,
                        SendTo = dataWorkInfo.WownerBdsc,
                        Priority = dataWorkInfo.PriorityCode,
                        Subject = dataWorkInfo.Wsubject,
                        SecretLevel = dataWorkInfo.Secretlevcode,
                        RefNumber = dataWorkInfo.Refwid,
                        AttachmentDetail = attmDetails,
                        ActionMessageDetail = actionMsgDetail

                    };
                    var res = new EdocDocDetailRs
                    {
                        RsHeader = new Models.EdocDocumentInquiry.RsHeader
                        {
                            AppId = appID,
                            Status = new Models.EdocDocumentInquiry.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", appID);

                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocDetailRs
                {
                    RsHeader = new Models.EdocDocumentInquiry.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.EdocDocumentInquiry.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, appID);

                return StatusCode(400, res);
            }
        }

        //RSU//

        [Authorize]
        [HttpPost]
        [Route("DocumentCreateBasketInfoforRSU")]
        public ActionResult<EdocDocCreateBasketInfoRs> DocumentCreateBasketInfoforRSU(EdocDocCreateBasketInfoRq edocDocCreateBasketInfoRq)
        {
            string method = "DocumentCreateBasketInfoforRSU";
            try
            {
                var rawData = edocDocCreateBasketInfoRq.RqDetail;
                List<string> bidSuccess = new List<string>();
                List<string> bidUnsuccess = new List<string>();

                //********** check bid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();

                string json = rawData.BasketInfo.ToString();
                JArray jArray = JArray.Parse(json);
                var jObjects = jArray.ToObject<List<JObject>>();

                foreach (var obj in jObjects)
                {
                    var bid = obj.GetValue("BasketID").ToString();
                    var bdsc = obj.GetValue("BasketDescription").ToString();
                    var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == bid).FirstOrDefault();

                    if (dataBasketInfo == null)
                    {
                        CreateBasket(bid, bdsc);
                        bidSuccess.Add(bid);
                    }
                    else
                    {
                        bidUnsuccess.Add(bid);
                    }
                }

                if (bidSuccess.Count != 0)
                {
                    var ret = new Models.EdocDocumentCreateBasketInfo.RsDetail
                    {
                        Success = bidSuccess,
                        Unsuccess = bidUnsuccess
                    };
                    var res = new EdocDocCreateBasketInfoRs
                    {
                        RsHeader = new Models.EdocDocumentCreateBasketInfo.RsHeader
                        {
                            AppId = edocDocCreateBasketInfoRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreateBasketInfo.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", edocDocCreateBasketInfoRq.RqHeader.AppId);

                    return StatusCode(201, res);
                }
                else
                {
                    var ret = new Models.EdocDocumentCreateBasketInfo.RsDetail
                    {
                        Unsuccess = bidUnsuccess
                    };
                    var res = new EdocDocCreateBasketInfoRs
                    {
                        RsHeader = new Models.EdocDocumentCreateBasketInfo.RsHeader
                        {
                            AppId = edocDocCreateBasketInfoRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreateBasketInfo.RsHeaderStatus
                            {
                                OrgStatusCode = "Error004",
                                OrgStatusDesc = "data already exists",
                                StatusCode = "-1"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Error004 - data already exists", edocDocCreateBasketInfoRq.RqHeader.AppId);

                    return StatusCode(400, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocCreateBasketInfoRs
                {
                    RsHeader = new Models.EdocDocumentCreateBasketInfo.RsHeader
                    {
                        AppId = edocDocCreateBasketInfoRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentCreateBasketInfo.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocCreateBasketInfoRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentUpdateBasketInfoforRSU")]
        public ActionResult<EdocDocCreateBasketInfoRs> DocumentUpdateBasketInfoforRSU(EdocDocCreateBasketInfoRq edocDocCreateBasketInfoRq)
        {
            string method = "DocumentUpdateBasketInfoforRSU";
            try
            {
                var rawData = edocDocCreateBasketInfoRq.RqDetail;
                List<string> bidSuccess = new List<string>();
                List<string> bidUnsuccess = new List<string>();

                //********** check bid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();

                string json = rawData.BasketInfo.ToString();
                JArray jArray = JArray.Parse(json);
                var jObjects = jArray.ToObject<List<JObject>>();

                foreach (var obj in jObjects)
                {
                    var bid = obj.GetValue("BasketID").ToString();
                    var bdsc = obj.GetValue("BasketDescription").ToString();
                    var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == bid).FirstOrDefault();

                    if (dataBasketInfo != null)
                    {
                        dataBasketInfo.Bdsc = bdsc;

                        _context.SaveChanges();

                        bidSuccess.Add(bid);
                    }
                    else
                    {
                        bidUnsuccess.Add(bid);
                    }
                }

                if (bidSuccess.Count != 0)
                {
                    var ret = new Models.EdocDocumentCreateBasketInfo.RsDetail
                    {
                        Success = bidSuccess,
                        Unsuccess = bidUnsuccess
                    };
                    var res = new EdocDocCreateBasketInfoRs
                    {
                        RsHeader = new Models.EdocDocumentCreateBasketInfo.RsHeader
                        {
                            AppId = edocDocCreateBasketInfoRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreateBasketInfo.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", edocDocCreateBasketInfoRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
                else
                {
                    var ret = new Models.EdocDocumentCreateBasketInfo.RsDetail
                    {
                        Unsuccess = bidUnsuccess
                    };
                    var res = new EdocDocCreateBasketInfoRs
                    {
                        RsHeader = new Models.EdocDocumentCreateBasketInfo.RsHeader
                        {
                            AppId = edocDocCreateBasketInfoRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreateBasketInfo.RsHeaderStatus
                            {
                                OrgStatusCode = "Error001",
                                OrgStatusDesc = "data not found",
                                StatusCode = "-1"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Error001 - data not found", edocDocCreateBasketInfoRq.RqHeader.AppId);

                    return StatusCode(400, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocCreateBasketInfoRs
                {
                    RsHeader = new Models.EdocDocumentCreateBasketInfo.RsHeader
                    {
                        AppId = edocDocCreateBasketInfoRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentCreateBasketInfo.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocCreateBasketInfoRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentUpdateUserInfoforRSU")]
        public ActionResult<EdocDocUpdateUserInfoRs> DocumentUpdateUserInfoforRSU(EdocDocUpdateUserInfoRq edocDocUpdateUserInfoRq)
        {
            string method = "DocumentUpdateUserInfoforRSU";
            try
            {
                var rawData = edocDocUpdateUserInfoRq.RqDetail;
                List<string> uidSuccess = new();
                List<string> uidUnsuccess = new();

                //********** check bid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();

                string json = rawData.Username.ToString();
                JArray jArray = JArray.Parse(json);
                var jObjects = jArray.ToObject<List<JObject>>();

                foreach (var obj in jObjects)
                {
                    var uid = obj.GetValue("UserID").ToString();
                    var dataUserInfo = _context.Userinfos.Where(a => a.Usrid == uid.ToUpper()).ToList();

                    if (dataUserInfo.Count != 0)
                    {
                        dataUserInfo.All(a => { a.StateCode = "01"; return true; });

                        _context.SaveChanges();

                        uidSuccess.Add(uid.ToUpper());
                    }
                    else
                    {
                        uidUnsuccess.Add(uid.ToUpper());
                    }
                }

                if (uidSuccess.Count != 0)
                {
                    var ret = new Models.EdocDocumentUpdateUserInfo.RsDetail
                    {
                        Success = uidSuccess,
                        Unsuccess = uidUnsuccess
                    };
                    var res = new EdocDocUpdateUserInfoRs
                    {
                        RsHeader = new Models.EdocDocumentUpdateUserInfo.RsHeader
                        {
                            AppId = edocDocUpdateUserInfoRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentUpdateUserInfo.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", edocDocUpdateUserInfoRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
                else
                {
                    var ret = new Models.EdocDocumentUpdateUserInfo.RsDetail
                    {
                        Unsuccess = uidUnsuccess
                    };
                    var res = new EdocDocUpdateUserInfoRs
                    {
                        RsHeader = new Models.EdocDocumentUpdateUserInfo.RsHeader
                        {
                            AppId = edocDocUpdateUserInfoRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentUpdateUserInfo.RsHeaderStatus
                            {
                                OrgStatusCode = "Error001",
                                OrgStatusDesc = "data not found",
                                StatusCode = "-1"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Error001 - data not found", edocDocUpdateUserInfoRq.RqHeader.AppId);

                    return StatusCode(400, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocUpdateUserInfoRs
                {
                    RsHeader = new Models.EdocDocumentUpdateUserInfo.RsHeader
                    {
                        AppId = edocDocUpdateUserInfoRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentUpdateUserInfo.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocUpdateUserInfoRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        //IPST//

        [Authorize]
        [HttpGet]
        [Route("GetBasketInfoforIPST")]
        public ActionResult<EdocDocGetBasketInfoRs> GetBasketInfoforIPST(EdocDocGetBasketInfoRq edocDocGetBasketInfoRq)
        {
            string method = "GetBasketInfoforIPST";
            try
            {
                var rawData = edocDocGetBasketInfoRq.RqDetail;
                string userBid = string.Empty;
                string fullName = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username).OrderBy(a => a.Mainbid).Select(a => a.Bid).ToList();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, edocDocGetBasketInfoRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }
                //*********************************//

                //********** check data **********//

                var listUserBid = new List<object>();

                for (int i = 0; i < dataUser.Count; i++)
                {
                    var data = _context.Basketinfos.Where(a => a.Bid == dataUser[i]).FirstOrDefault();
                    listUserBid.Add(data);
                }

                var basketInfoDetail = new List<RsBasketInfoDetail>();

                foreach (Basketinfo basketinfo in listUserBid)
                {
                    basketInfoDetail.Add(new RsBasketInfoDetail
                    {
                        BasketID = basketinfo.Bid,
                        Detail = basketinfo.Bdsc
                    });
                }

                var ret = new Models.EdocDocumentGetBasketInfo.RsDetail
                {
                    BasketInfoDetail = basketInfoDetail
                };
                var res = new EdocDocGetBasketInfoRs
                {
                    RsHeader = new Models.EdocDocumentGetBasketInfo.RsHeader
                    {
                        AppId = edocDocGetBasketInfoRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentGetBasketInfo.RsHeaderStatus
                        {
                            OrgStatusCode = "0",
                            OrgStatusDesc = "Success",
                            StatusCode = "0"
                        }

                    },
                    RsDetail = ret
                };

                CreateLog(method, "Success", edocDocGetBasketInfoRq.RqHeader.AppId);

                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var res = new EdocDocGetBasketInfoRs
                {
                    RsHeader = new Models.EdocDocumentGetBasketInfo.RsHeader
                    {
                        AppId = edocDocGetBasketInfoRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentGetBasketInfo.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocGetBasketInfoRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentSendforIPST")]
        public ActionResult<EdocDocSendRsCustom> DocumentSendforIPST(EdocDocSendRqCustom edocDocSendRqCustom)
        {
            string method = "DocumentSendforIPST";
            try
            {
                var rawData = edocDocSendRqCustom.RqDetail;
                string userBid = string.Empty;
                string fullName = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, edocDocSendRqCustom.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    userBid = dataUser.Bid;
                    fullName = dataUser.Username;
                }
                //*********************************//

                //********** check data **********//
                var data = _context.Workinfos.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                if (data == null)
                {
                    var responseMsg = CheckData(method, edocDocSendRqCustom.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var dataWorkinProc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).FirstOrDefault();

                    if (dataWorkinProc == null)
                    {
                        var responseMsg = CheckData(method, edocDocSendRqCustom.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                    else
                    {
                        var result = UpdateWorkinprocforIPST(rawData.WID, rawData.ReceiverBasketID, userBid, rawData.Username, fullName, rawData.Receiver);

                        if (result == "Unsuccess")
                        {
                            var responseMsg = CheckData(method, edocDocSendRqCustom.RqHeader.AppId, "1");

                            return StatusCode(400, responseMsg);
                        }

                        var res = new EdocDocSendRsCustom
                        {
                            RsHeader = new Models.EdocDocumentSendCustom.RsHeader
                            {
                                AppId = edocDocSendRqCustom.RqHeader.AppId,
                                Status = new Models.EdocDocumentSendCustom.RsHeaderStatus
                                {
                                    OrgStatusCode = "0",
                                    OrgStatusDesc = "Success",
                                    StatusCode = "0"
                                }
                            },
                        };

                        CreateLog(method, "Success", edocDocSendRqCustom.RqHeader.AppId);

                        return StatusCode(200, res);
                    }
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocSendRsCustom
                {
                    RsHeader = new Models.EdocDocumentSendCustom.RsHeader
                    {
                        AppId = edocDocSendRqCustom.RqHeader.AppId,
                        Status = new Models.EdocDocumentSendCustom.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocSendRqCustom.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentFollowupforIPST")]
        public ActionResult<EdocDocFollowupRs> DocumentFollowupforIPST(EdocDocFollowupRq edocDocFollowupRq)
        {
            string method = "DocumentFollowupforIPST";
            try
            {
                var rawData = edocDocFollowupRq.RqDetail;
                string userBid = string.Empty;
                string username = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, edocDocFollowupRq.RqHeader.AppId);

                        return StatusCode(400, responseMsg);
                    }
                    else
                    {
                        userBid = userMainBid.Bid;
                        username = userMainBid.Username;
                    }
                }
                else
                {
                    userBid = dataUser.Bid;
                    username = dataUser.Username;
                }
                //********************************//

                //********** check data **********//
                var data = _context.Workinfos.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                if (data == null)
                {
                    var responseMsg = CheckData(method, edocDocFollowupRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                //********************************//

                var dataFollowup = _context.Followups.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                var ret = new Models.EdocDocumentFollowup.RsDetail
                {
                    Wid = rawData.WID,
                    ActionMessage = dataFollowup.ActionMsg
                };
                var res = new EdocDocFollowupRs
                {
                    RsHeader = new Models.EdocDocumentFollowup.RsHeader
                    {
                        AppId = edocDocFollowupRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentFollowup.RsHeaderStatus
                        {
                            OrgStatusCode = "0",
                            OrgStatusDesc = "Success",
                            StatusCode = "0"
                        }
                    },
                    RsDetail = ret
                };

                CreateLog(method, "Success", edocDocFollowupRq.RqHeader.AppId);

                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var res = new EdocDocFollowupRs
                {
                    RsHeader = new Models.EdocDocumentFollowup.RsHeader
                    {
                        AppId = edocDocFollowupRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentFollowup.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocFollowupRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        //AOT//

        [Authorize]
        [HttpGet]
        [Route("GetBasketInfoforAOT")]
        public ActionResult<EdocDocGetBasketInfoRs> GetBasketInfoforAOT([Required] string appID)
        {
            string method = "GetBasketInfoforAOT";
            try
            {
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var data = _context.Basketinfos;
                var basketInfoDetail = new List<RsBasketInfoDetail>();

                foreach (Basketinfo basketinfo in data)
                {
                    basketInfoDetail.Add(new RsBasketInfoDetail
                    {
                        BasketID = basketinfo.Bid,
                        Detail = basketinfo.Bdsc
                    });
                }

                var ret = new Models.EdocDocumentGetBasketInfo.RsDetail
                {
                    BasketInfoDetail = basketInfoDetail
                };
                var res = new EdocDocGetBasketInfoRs
                {
                    RsHeader = new Models.EdocDocumentGetBasketInfo.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.EdocDocumentGetBasketInfo.RsHeaderStatus
                        {
                            OrgStatusCode = "0",
                            OrgStatusDesc = "Success",
                            StatusCode = "0"
                        }

                    },
                    RsDetail = ret
                };

                CreateLog(method, "Success", appID);

                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var res = new EdocDocGetBasketInfoRs
                {
                    RsHeader = new Models.EdocDocumentGetBasketInfo.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.EdocDocumentGetBasketInfo.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, appID);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentIncomingforAOT")]
        public ActionResult<EdocDocIncomingRsforAOT> DocumentIncomingforAOT(EdocDocIncomingRq edocDocIncomingRq)
        {
            string method = "DocumentIncomingforAOT";
            try
            {
                var rawData = edocDocIncomingRq.RqDetail;
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataBasketInfo == null)
                {
                    var responseMsg = CheckBasketinfo(method, edocDocIncomingRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                var paramBid = new SqlParameter("@bid", dataBasketInfo.Bid);
                var strSql = "SELECT t1.* FROM workinfo AS t1, workinprocess AS t2 WHERE (t1.wid = t2.wid) AND (t2.bid = @bid) AND " +
                             "(((t2.statecode < '03' OR t2.statecode = '12') AND (t2.registerno = '-')) OR (t2.registerno <> '-' AND " +
                             "t2.statecode = '00' AND t2.usrid = '-'))";

                var data = _context.Workinfos.FromSqlRaw(strSql, paramBid).Select(a => a.Wid).Count();

                if (data == 0)
                {
                    var responseMsg = CheckData(method, edocDocIncomingRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var ret = new Models.EdocDocumentIncoming.RsDetailforAOT
                    {
                        Total = data.ToString(),
                    };
                    var res = new EdocDocIncomingRsforAOT
                    {
                        RsHeader = new Models.EdocDocumentIncoming.RsHeader
                        {
                            AppId = edocDocIncomingRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentIncoming.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetailforAOT = ret
                    };

                    CreateLog(method, "Success", edocDocIncomingRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocIncomingRs
                {
                    RsHeader = new Models.EdocDocumentIncoming.RsHeader
                    {
                        AppId = edocDocIncomingRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentIncoming.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocIncomingRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentPendingforAOT")]
        public ActionResult<EdocDocInprocRsforAOT> DocumentPendingforAOT(EdocDocInprocRq edocDocInprocRq)
        {
            string method = "DocumentPendingforAOT";
            try
            {
                var rawData = edocDocInprocRq.RqDetail;
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataBasketInfo == null)
                {
                    var responseMsg = CheckBasketinfo(method, edocDocInprocRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                string searchDate = GetDaypast();

                var paramBid = new SqlParameter("@bid", dataBasketInfo.Bid);
                var paramBackDate = new SqlParameter("@backdate", searchDate);
                var strSql = "SELECT t1.* FROM workinprocess AS t2, workinfo AS t1 WHERE (t1.wid = t2.wid) AND (t2.bid = @bid) AND " +
                             "((t2.statecode < '03') AND (t2.statecode <> '00') AND (t2.registerno <> '-') AND (t2.viewstatus <> '0')) " +
                             "AND (t1.wtype = '01' or t1.wtype = '00' or (t1.wtype = '02' and t2.bid = @bid and t1.registerbid <> @bid)) AND (t2.initdate >= @backdate)";

                //********** check data **********//
                var data = _context.Workinfos.FromSqlRaw(strSql, paramBid, paramBackDate).Select(a => a.Wid).Count();

                if (data == 0)
                {
                    var responseMsg = CheckData(method, edocDocInprocRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var ret = new Models.EdocDocumentInproc.RsDetailforAOT
                    {
                        Total = data.ToString(),
                    };
                    var res = new EdocDocInprocRsforAOT
                    {
                        RsHeader = new Models.EdocDocumentInproc.RsHeader
                        {
                            AppId = edocDocInprocRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentInproc.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetailforAOT = ret
                    };

                    CreateLog(method, "Success", edocDocInprocRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocInprocRs
                {
                    RsHeader = new Models.EdocDocumentInproc.RsHeader
                    {
                        AppId = edocDocInprocRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInproc.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocInprocRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentReceiveforAOT")]
        public ActionResult<EdocDocReceiveRsforAOT> DocumentReceiveforAOT(EdocDocReceiveRq edocDocReceiveRq)
        {
            string method = "DocumentReceiveforAOT";
            try
            {
                var rawData = edocDocReceiveRq.RqDetail;
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataBasketInfo == null)
                {
                    var responseMsg = CheckBasketinfo(method, edocDocReceiveRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                string searchDate = GetDaypast();

                var paramBid = new SqlParameter("@bid", dataBasketInfo.Bid);
                var paramBackDate = new SqlParameter("@backdate", searchDate);
                var strSql = "SELECT t2.* FROM workinfo AS t1,workinprocess AS t2 WHERE ((t2.bid = @bid AND t1.wtype = '01') OR " +
                             "(t1.wtype = '00' AND t2.bid = @bid AND t1.registerbid <> @bid) OR (t1.wtype = '02' AND t2.bid = @bid AND t1.registerbid <> @bid)) AND " +
                             "t2.bid = @bid AND t1.wid = t2.wid and t2.initdate >= @backdate AND t2.registerno <> '-'";

                //********** check data **********//
                var data = _context.Workinfos.FromSqlRaw(strSql, paramBid, paramBackDate).Select(a => a.Wid).Count();

                if (data == 0)
                {
                    var responseMsg = CheckData(method, edocDocReceiveRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var ret = new Models.EdocDocumentReceive.RsDetailforAOT
                    {
                        Total = data.ToString(),
                    };
                    var res = new EdocDocReceiveRsforAOT
                    {
                        RsHeader = new Models.EdocDocumentReceive.RsHeader
                        {
                            AppId = edocDocReceiveRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentReceive.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetailforAOT = ret
                    };

                    CreateLog(method, "Success", edocDocReceiveRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocReceiveRsforAOT
                {
                    RsHeader = new Models.EdocDocumentReceive.RsHeader
                    {
                        AppId = edocDocReceiveRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentReceive.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetailforAOT = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocReceiveRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentInternalforAOT")]
        public ActionResult<EdocDocInternalRsforAOT> DocumentInternalforAOT(EdocDocInternalRq edocDocInternalRq)
        {
            string method = "DocumentInternalforAOT";
            try
            {
                var rawData = edocDocInternalRq.RqDetail;
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataBasketInfo == null)
                {
                    var responseMsg = CheckBasketinfo(method, edocDocInternalRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                string searchDate = GetDaypast();

                var paramBid = new SqlParameter("@bid", dataBasketInfo.Bid);
                var paramBackDate = new SqlParameter("@backdate", searchDate);
                var strSql = "SELECT t2.* FROM workinfo AS t1, workinprocess AS t2 WHERE (t1.wid = t2.wid) AND (t2.bid = @bid) AND (t1.wtype = '00') AND t1.registerbid = @bid and t2.initdate >= @backdate";

                //********** check data **********//
                var data = _context.Workinfos.FromSqlRaw(strSql, paramBid, paramBackDate).Select(a => a.Wid).Count();

                if (data == 0)
                {
                    var responseMsg = CheckData(method, edocDocInternalRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var ret = new Models.EdocDocumentInternal.RsDetailforAOT
                    {
                        Total = data.ToString(),
                    };
                    var res = new EdocDocInternalRsforAOT
                    {
                        RsHeader = new Models.EdocDocumentInternal.RsHeader
                        {
                            AppId = edocDocInternalRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentInternal.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetailforAOT = ret
                    };

                    CreateLog(method, "Success", edocDocInternalRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocInternalRsforAOT
                {
                    RsHeader = new Models.EdocDocumentInternal.RsHeader
                    {
                        AppId = edocDocInternalRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInternal.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetailforAOT = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocInternalRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentPeriodforAOT")]
        public ActionResult<EdocDocPeriodRsforAOT> DocumentPeriodforAOT(EdocDocPeriodRq edocDocPeriodRq)
        {
            string method = "DocumentPeriodforAOT";
            try
            {
                var rawData = edocDocPeriodRq.RqDetail;
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataBasketInfo == null)
                {
                    var responseMsg = CheckBasketinfo(method, edocDocPeriodRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                string searchDate = GetDaypast();

                var paramBid = new SqlParameter("@bid", dataBasketInfo.Bid);
                var paramBackDate = new SqlParameter("@backdate", searchDate);
                var strSql = "SELECT top 50 t1.* FROM workinprocess AS t2, workinfo AS t1 WHERE (t1.wid = t2.wid) AND (t2.bid = @bid) AND " +
                             "((t2.statecode < '03') AND (t2.statecode <> '00') AND (t2.registerno <> '-') AND (t2.viewstatus <> '0')) " +
                             "AND (t1.wtype = '01' or t1.wtype = '00' or (t1.wtype = '02' and t2.bid = @bid and t1.registerbid <> @bid)) AND (t2.initdate >= @backdate)";

                //********** check data **********//
                var data = _context.Workinfos.FromSqlRaw(strSql, paramBid, paramBackDate).ToList();

                if (data == null)
                {
                    var responseMsg = CheckData(method, edocDocPeriodRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var setDetail = new List<RsDocumentDetail>();

                    foreach(var item in data)
                    {
                        setDetail.Add(new RsDocumentDetail()
                        {
                            BasketID = rawData.BasketID,
                            WID = item.Wid,
                            Receiver = item.RegisterUid,
                            ReceiveDate = item.RegisterDate,
                            Duration = GetPeriod(item.RegisterDate),
                            Status = "ระหว่างดำเนินการ"
                        });
                    }

                    var ret = new Models.EdocDocumentPeriod.RsDetailforAOT
                    {
                        rsDocumentDetails = setDetail
                    };
                    var res = new EdocDocPeriodRsforAOT
                    {
                        RsHeader = new Models.EdocDocumentPeriod.RsHeader
                        {
                            AppId = edocDocPeriodRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentPeriod.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetailforAOT = ret
                    };

                    CreateLog(method, "Success", edocDocPeriodRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocInprocRs
                {
                    RsHeader = new Models.EdocDocumentInproc.RsHeader
                    {
                        AppId = edocDocPeriodRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInproc.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocPeriodRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        #endregion

        //Method for API//

        #region

        public object CheckData(string method, string appID, string caseNo)
        {
            try
            {
                if (caseNo == "1")
                {
                    var detailRs = new ResponseMsgRs
                    {
                        RsHeader = new Models.ResponseMsg.RsHeader
                        {
                            AppId = appID,
                            Status = new Models.ResponseMsg.RsHeaderStatus
                            {
                                OrgStatusCode = "Error001",
                                OrgStatusDesc = "data not found",
                                StatusCode = "-1"
                            }
                        },
                        RsDetail = null
                    };

                    CreateLog(method, "Error001 - data not found", appID);

                    return detailRs;
                }

                if (caseNo == "2")
                {
                    var detailRs = new ResponseMsgRs
                    {
                        RsHeader = new Models.ResponseMsg.RsHeader
                        {
                            AppId = appID,
                            Status = new Models.ResponseMsg.RsHeaderStatus
                            {
                                OrgStatusCode = "Error004",
                                OrgStatusDesc = "data already exists",
                                StatusCode = "-1"
                            }
                        },
                        RsDetail = null
                    };

                    CreateLog(method, "Error004 - data already exists", appID);

                    return detailRs;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public object CheckInputData(string method, string appID)
        {
            try
            {
                var detailRs = new ResponseMsgRs
                {
                    RsHeader = new Models.ResponseMsg.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.ResponseMsg.RsHeaderStatus
                        {
                            OrgStatusCode = "Error002",
                            OrgStatusDesc = "invalid input format",
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error002 - invalid input format", appID);

                return detailRs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public object CheckUser(string method, string appID)
        {
            try
            {
                var detailRs = new ResponseMsgRs
                {
                    RsHeader = new Models.ResponseMsg.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.ResponseMsg.RsHeaderStatus
                        {
                            OrgStatusCode = "Error003",
                            OrgStatusDesc = "invalid username",
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error003 - invalid username", appID);

                return detailRs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public object CheckBasketinfo(string method, string appID)
        {
            try
            {
                var detailRs = new ResponseMsgRs
                {
                    RsHeader = new Models.ResponseMsg.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.ResponseMsg.RsHeaderStatus
                        {
                            OrgStatusCode = "Error005",
                            OrgStatusDesc = "basketid not found",
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error005 - basketid not found", appID);

                return detailRs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetServerTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        public string GetServerDate()
        {
            string yy, mm, dd;

            yy = DateTime.Now.Year.ToString();
            mm = DateTime.Now.Month.ToString("D2");
            dd = DateTime.Now.Day.ToString("D2");

            if (int.Parse(yy) < 2500)
            {
                yy = (int.Parse(yy) + 543).ToString();
            }

            return yy + "/" + mm + "/" + dd;
        }

        public string GetServDateNormalFormat()
        {
            string yy, mm, dd;

            yy = DateTime.Now.Year.ToString();
            mm = DateTime.Now.Month.ToString("D2");
            dd = DateTime.Now.Day.ToString("D2");

            if (int.Parse(yy) < 2500)
            {
                yy = (int.Parse(yy) + 543).ToString();
            }

            return dd + "/" + mm + "/" + yy;
        }

        public Boolean CreateAttachment(EdocDocUploadRq edocDocUploadRq, string userBid, string username)
        {
            var rawData = edocDocUploadRq.RqDetail;
            var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == rawData.WID).FirstOrDefault();
            string curBid = userBid;
            var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == curBid).FirstOrDefault();
            string strSourcePath = "";
            string strExt = "";
            string strHomeDir = "";
            string strDocuname = "";
            string newDocuname = "";
            string strDestPath = "";
            string strNewActionMsg = "";
            int count = 0;
            string ipWebServer = _configuration.GetSection("MySettings").GetSection("IPWebServer").Value;
            string flowData = _configuration.GetSection("MySettings").GetSection("Flowdata").Value;
            string fileTemp = SaveAttach(rawData.FileName, rawData.FileExtension, rawData.FileData);
            string RegisterDate = GetServerDate();
            string RegisterTime = GetServerTime();
            string RegisterDateNewFormat = GetServDateNormalFormat();
            strNewActionMsg = "วันที่ " + RegisterDateNewFormat + "  " + RegisterTime + "  " + dataWorkInfo.RegisterBdsc + " : " + username + " (เพิ่ม)" + "\n";
            strSourcePath = fileTemp;
            strDocuname = dataBasketInfo.DocuName;
            newDocuname = (int.Parse(strDocuname) + 1).ToString("D8");
            dataBasketInfo.DocuName = newDocuname;
            _context.SaveChanges();

            if (fileTemp != "")
            {
                strExt = Path.GetExtension(strSourcePath);
                strHomeDir = dataBasketInfo.HomeDir;
                string flowPath = strHomeDir.ToUpper().Replace("\\\\" + ipWebServer.ToUpper(), flowData);
                strDestPath = flowPath + "\\" + strDocuname + strExt;
                strHomeDir = strHomeDir + "\\" + strDocuname + strExt;

                if (System.IO.File.Exists(strDestPath))
                {
                    System.IO.File.Delete(strDestPath);
                }
                System.IO.File.Copy(strSourcePath, strDestPath);
            }
            else
            {
                strHomeDir = dataBasketInfo.HomeDir;
                strHomeDir = strHomeDir + "\\" + strDocuname;
            }

            var dataDocAttm = _context.Docattaches.Where(a => a.Wid == rawData.WID && a.Bid == curBid).OrderByDescending(x => x.Itemno).FirstOrDefault();
            if (dataDocAttm != null)
            {
                count = int.Parse(dataDocAttm.Itemno) + 1;
            }
            else
            {
                count = 1;
            }

            var addAttm = new Docattach
            {
                Wid = rawData.WID,
                Bid = curBid,
                Attachdate = RegisterDate,
                Attachtime = RegisterTime,
                Attachname = strHomeDir,
                Userattach = username + " : " + dataWorkInfo.RegisterBdsc,
                Contextattach = rawData.Detail,
                Itemno = count.ToString(),
                Actionmsg = strNewActionMsg,
                Linkwid = "",
                Allowupdate = "Y"
            };
            _context.Docattaches.Add(addAttm);
            _context.SaveChanges();

            return true;
        }

        public string SaveAttach(string fileName, string fileExtension, string fileData)
        {
            string iwebTemp = _configuration.GetSection("MySettings").GetSection("iwebtemp").Value;
            try
            {
                if (fileData == "")
                {
                    return "";
                }
                else
                {
                    var bytes = Convert.FromBase64String(fileData);
                    string fullPath = iwebTemp + fileName + "." + fileExtension;

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);

                    }

                    var fs = new FileStream(fullPath, FileMode.CreateNew);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();

                    return fullPath;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GenQrcode()
        {
            Random generator = new Random();
            string r = generator.Next(0, 999999999).ToString("D14"); //AOT010100010000000000012342019002
            r = "AOT01010001" + r + DateTime.Now.Year.ToString() + "002";

            var item = _context.QrcodeGens.Where(a => a.Qrcode == r).FirstOrDefault();

            if (item == null)
            {
                return r;
            }
            else
            {
                return GenQrcode();
            }
        }

        public Boolean InsertQrCode(string wid)
        {
            var genCode = GenQrcode();
            var additem = new QrcodeGen
            {
                Wid = wid,
                Qrcode = genCode
            };
            _context.QrcodeGens.Add(additem);
            _context.SaveChanges();

            return true;
        }

        public string GetURL(string docuname, string wid, string subject, string usr)
        {
            string fullName = docuname.Trim();
            fullName = fullName.Replace("\"", "//");
            string fn = IEncrypt2(fullName);
            string strURL = _configuration.GetSection("MySettings").GetSection("IwebflowSharename").Value + "/content/viewext.asp?fn=" + WebUtility.UrlEncode(fn) + "&ds=" + subject + "&ws=" + wid + "&usr=" + usr + "&wtm=true";

            return strURL;
        }

        public string IEncrypt2(string encryptedstring)
        {
            if (encryptedstring.Trim().Length < 1 || encryptedstring == null)
            {
                return "";
            }
            string x = "";
            string tmp = "";

            for (int i = 0; i < encryptedstring.Trim().Length; i++)
            {
                x = encryptedstring.Substring(i, 1);
                tmp += Strings.ChrW(Strings.AscW(x) + 1);
            }

            tmp = Reverse(tmp);

            return tmp;
        }

        public string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        public string GetFullIfmid(string prefixIfmid)
        {
            string retString = string.Empty;
            string strSQL = "select  max(doccir_ifmid)  from doccir_detail  where  doccir_ifmid  like  '" + prefixIfmid + "%'";
            string connectionString = _configuration.GetConnectionString("DoccirSqlConnect");
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlTransaction Transaction = null;
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.SelectCommand.Transaction = Transaction;
            DataSet ds = new DataSet();
            da.Fill(ds, "doccirdet");
            var dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                var rsmdl = dt.Rows[0];
                var maxval = rsmdl[0].ToString();

                if (maxval != "")
                {
                    int intLastFolderNo = int.Parse(maxval.Substring(maxval.Length - 3)) + 1;
                    string lastFolderNo = intLastFolderNo.ToString();
                    var formatLastfolderNo = ("000").Substring(1, 3 - (lastFolderNo.Length)) + "" + lastFolderNo;
                    retString = prefixIfmid + "/" + formatLastfolderNo;
                }
                else
                {
                    retString = prefixIfmid + "/001";
                }
            }
            else
            {
                retString = prefixIfmid + "/001";
            }
            Connection.Close();

            return retString;
        }

        public string CreateDocumentOutside(EdocDocCreateRq edocDocCreateRq, string userBid)
        {
            var rawData = edocDocCreateRq.RqDetail;
            var rqDetail = rawData;
            string regNo = "-";
            string ConstWsubtypeIndoc = "02";
            string typeDoc = rqDetail.Wstype;

            if (string.IsNullOrWhiteSpace(typeDoc))
            {
                typeDoc = "02";
            }

            string strRegMode = "0";

            if (string.IsNullOrWhiteSpace(rawData.DocDate))
            {
                rawData.DocDate = DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("th-TH"));
            }

            rawData.DocDate = rawData.DocDate.Replace("/", "-");

            if (int.Parse(rawData.DocDate.Split("-")[0]) <= 2500)
            {
                rawData.DocDate = rawData.DocDate.Replace(rawData.DocDate.Split("-")[0], (Int32.Parse(rawData.DocDate.Split("-")[0]) + 543).ToString());
            }

            rawData.DocDate = rawData.DocDate.Replace("-", "/");

            string curBid = userBid;
            var BasketInfo = _context.Basketinfos.Where(a => a.Bid == curBid).FirstOrDefault();
            string strPrefixIn = BasketInfo.Wfid; //WebservicePrefixIN
            string curDir = BasketInfo.HomeDir;
            string curBdsc = BasketInfo.Bdsc;
            string curUsrname = rawData.Username.ToUpper();
            string curDeptcode = BasketInfo.Deptcode;
            string ownerWsubType = curBid;

            var strSQL = "ifmflow_sp_ControlBasketinfo '1','" + ownerWsubType + "','-','" + curDeptcode + "','" + (ConstWsubtypeIndoc).Trim() + "','01','" + typeDoc + "','" + strRegMode + "'";
            string connectionString = SetSQLConnectionString();
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlTransaction Transaction = null;
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.SelectCommand.Transaction = Transaction;
            DataSet ds = new DataSet();
            da.Fill(ds, "ctrbasket");
            var dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Count == 1)
                {
                    throw new Exception(dt.Rows[0][0].ToString());
                }
                else
                {
                    var dr = dt.Rows[0];
                    regNo = dr[0].ToString().Trim();
                    string sendNo = dr[1].ToString().Trim();
                    string strRegNo = decimal.Parse(sendNo).ToString();

                    if (strRegNo.Substring(strRegNo.Length - 3, 3) == ".00")
                    {
                        strRegNo = double.Parse(strRegNo).ToString();
                    }

                    if (strRegNo.IndexOf(".") > 0)
                    {
                        if (strRegNo.Substring(strRegNo.Length - 2, strRegNo.Length).IndexOf(".") > 0)
                        {
                            strRegNo = strRegNo + "0";
                        }
                    }

                    string itemNo = dr[2].ToString().Trim();
                    string docuname = dr[3].ToString().Trim();
                    string wid = rawData.WID;
                    string wtype = "01";
                    string strDateAddDoc = rawData.DocDate;
                    string strTimeAddDoc = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + ":" + DateTime.Now.Second.ToString("D2");
                    string refWid = rqDetail.RefNumber;
                    string strTmpFrom = rqDetail.From;
                    string strTmpTo = rqDetail.SendTo;
                    string strTmpSubject = rqDetail.Subject;
                    string detail = rqDetail.Description; ;
                    string priority = rqDetail.Priority;

                    if (string.IsNullOrWhiteSpace(rqDetail.Priority))
                    {
                        rqDetail.Priority = "00";
                    }

                    string secLev = rqDetail.SecretLevel;

                    if (string.IsNullOrWhiteSpace(rqDetail.SecretLevel))
                    {
                        rqDetail.SecretLevel = "0";
                    }

                    string userAction = "-";
                    string strUserDate = "-";
                    string remark = rqDetail.Remark;
                    string attach = "-";
                    string selectActionInfo = "00";
                    string strBookGroup = "00";
                    string strAgeWid = "-";
                    string strLocation = "-";
                    string strAutoDelete = "0";
                    string strInternalAction = "00";
                    string strSelectReceivedoc = rqDetail.ReceiveDoc;

                    if (string.IsNullOrWhiteSpace(strSelectReceivedoc))
                    {
                        strSelectReceivedoc = "01";
                    }

                    strSQL = "ifmflow_sp_CreateWork '" + wtype + "','" + curDeptcode + "','" + curBid + "','";
                    strSQL = strSQL + curBdsc + "','" + curDir + "','" + curUsrname + "','" + regNo + "','";
                    strSQL = strSQL + sendNo + "','" + itemNo + "','" + docuname + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + (strTimeAddDoc) + "','" + wid + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + refWid + "','" + typeDoc + "','" + strTmpFrom + "','" + strTmpTo + "','";
                    strSQL = strSQL + strTmpSubject + "','" + detail + "','" + priority + "','" + secLev + "','" + userAction + "','";
                    strSQL = strSQL + strUserDate + "','0','" + remark + "','" + ConvertRegNotoNumber(regNo) + "','";
                    strSQL = strSQL + attach + "','" + selectActionInfo + "','" + strBookGroup + "','" + strAgeWid + "','" + strLocation + "','" + strAutoDelete + "','" + strInternalAction + "','" + rqDetail.Username.ToUpper() + "','" + strSelectReceivedoc + "'";     //wtype=00

                    SqlCommand Command = null;
                    Command = Connection.CreateCommand();
                    Command.Transaction = Transaction;
                    Command.CommandText = strSQL;
                    Command.ExecuteNonQuery();
                    Connection.Close();

                    //insert bar code
                    InsertQrCode(wid);

                    return wid;
                }
            }
            else
            {
                return "Unsuccess";
            }
        }

        public string CreateDocument(EdocDocCreateRq edocDocCreateRq, string userBid)
        {
            var rawData = edocDocCreateRq.RqDetail;
            var rqDetail = rawData;
            string regNo = "-";
            string ConstWsubtypeIndoc = "01";
            string typeDoc = rqDetail.Wstype;

            if (string.IsNullOrWhiteSpace(typeDoc))
            {
                typeDoc = "01";
            }

            string strRegMode = "0";

            if (string.IsNullOrWhiteSpace(rawData.DocDate))
            {
                rawData.DocDate = DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("th-TH"));
            }

            rawData.DocDate = rawData.DocDate.Replace("/", "-");

            if (int.Parse(rawData.DocDate.Split("-")[0]) <= 2500)
            {
                rawData.DocDate = rawData.DocDate.Replace(rawData.DocDate.Split("-")[0], (Int32.Parse(rawData.DocDate.Split("-")[0]) + 543).ToString());
            }

            rawData.DocDate = rawData.DocDate.Replace("-", "/");

            string curBid = userBid;
            var BasketInfo = _context.Basketinfos.Where(a => a.Bid == curBid).FirstOrDefault();
            string strPrefixIn = BasketInfo.Wfid; //WebservicePrefixIN
            string curDir = BasketInfo.HomeDir;
            string curBdsc = BasketInfo.Bdsc;
            string curUsrname = rawData.Username.ToUpper();
            string curDeptcode = BasketInfo.Deptcode;
            string ownerWsubType = curBid;

            var wsubType = _context.Wsubtypes.Where(a => a.Code == typeDoc).FirstOrDefault();
            string strTmpWid = wsubType.Granted;

            if (typeDoc != "01" && wsubType.Category == "0")
            {
                ownerWsubType = wsubType.Bid;
            }

            var strSQL = "ifmflow_sp_ControlBasketinfo '5','" + ownerWsubType + "','-','" + curDeptcode + "','" + (ConstWsubtypeIndoc).Trim() + "','00','" + typeDoc + "','" + strRegMode + "'";
            string connectionString = SetSQLConnectionString();
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlTransaction Transaction = null;
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.SelectCommand.Transaction = Transaction;
            DataSet ds = new DataSet();
            da.Fill(ds, "ctrbasket");
            var dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Count == 1)
                {
                    throw new Exception(dt.Rows[0][0].ToString());
                }
                else
                {
                    var dr = dt.Rows[0];
                    regNo = dr[0].ToString().Trim();
                    string sendNo = dr[1].ToString().Trim();
                    string strRegNo = decimal.Parse(sendNo).ToString();

                    if (strRegNo.Substring(strRegNo.Length - 3, 3) == ".00")
                    {
                        strRegNo = double.Parse(strRegNo).ToString();
                    }

                    if (strRegNo.IndexOf(".") > 0)
                    {
                        if (strRegNo.Substring(strRegNo.Length - 2, strRegNo.Length).IndexOf(".") > 0)
                        {
                            strRegNo = strRegNo + "0";
                        }
                    }

                    var strDate = DateTime.Now.Year + 543;
                    string itemNo = dr[2].ToString().Trim();
                    string docuname = dr[3].ToString().Trim();
                    string wid = string.Empty;

                    if (typeDoc == "01" && strPrefixIn == "ไม่อนุญาตออกเลข")
                    {
                        throw new Exception("ไม่อนุญาตให้ออกเลขที่หนังสือ");
                    }

                    if (typeDoc == "01" && strPrefixIn != "ไม่อนุญาตออกเลข")
                    {
                        wid = strPrefixIn + strRegNo + "/" + strDate.ToString();
                    }

                    if (typeDoc != "01" && wsubType.Category == "0")
                    {
                        wid = strTmpWid + strRegNo + "/" + strDate.ToString();
                    }

                    if (typeDoc != "01" && wsubType.Category == "1" && wsubType.Bid == curBid)
                    {
                        wid = strTmpWid + strRegNo + "/" + strDate.ToString();
                    }

                    string wtype = "00";
                    string strDateAddDoc = rawData.DocDate;
                    string strTimeAddDoc = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + ":" + DateTime.Now.Second.ToString("D2");
                    string refWid = rqDetail.RefNumber;
                    string strTmpFrom = rqDetail.From;
                    string strTmpTo = rqDetail.SendTo;
                    string strTmpSubject = rqDetail.Subject;
                    string detail = rqDetail.Description; ;
                    string priority = rqDetail.Priority;

                    if (string.IsNullOrWhiteSpace(rqDetail.Priority))
                    {
                        rqDetail.Priority = "00";
                    }

                    string secLev = rqDetail.SecretLevel;

                    if (string.IsNullOrWhiteSpace(rqDetail.SecretLevel))
                    {
                        rqDetail.SecretLevel = "0";
                    }

                    string userAction = "-";
                    string strUserDate = "-";
                    string remark = rqDetail.Remark;
                    string attach = "-";
                    string selectActionInfo = "00";
                    string strBookGroup = "00";
                    string strAgeWid = "-";
                    string strLocation = "-";
                    string strAutoDelete = "0";
                    string strInternalAction = "00";
                    string strSelectReceivedoc = rqDetail.ReceiveDoc;

                    if (string.IsNullOrWhiteSpace(strSelectReceivedoc))
                    {
                        strSelectReceivedoc = "01";
                    }

                    strSQL = "ifmflow_sp_CreateWork '" + wtype + "','" + curDeptcode + "','" + curBid + "','";
                    strSQL = strSQL + curBdsc + "','" + curDir + "','" + curUsrname + "','" + sendNo + "','";
                    strSQL = strSQL + sendNo + "','" + itemNo + "','" + docuname + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + (strTimeAddDoc) + "','" + wid + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + refWid + "','" + typeDoc + "','" + strTmpFrom + "','" + strTmpTo + "','";
                    strSQL = strSQL + strTmpSubject + "','" + detail + "','" + priority + "','" + secLev + "','" + userAction + "','";
                    strSQL = strSQL + strUserDate + "','0','" + remark + "','" + ConvertRegNotoNumber(sendNo) + "','";
                    strSQL = strSQL + attach + "','" + selectActionInfo + "','" + strBookGroup + "','" + strAgeWid + "','" + strLocation + "','" + strAutoDelete + "','" + strInternalAction + "','" + rqDetail.Username + "','" + strSelectReceivedoc + "'";

                    SqlCommand Command = null;
                    Command = Connection.CreateCommand();
                    Command.Transaction = Transaction;
                    Command.CommandText = strSQL;
                    Command.ExecuteNonQuery();
                    Connection.Close();

                    //insert bar code
                    InsertQrCode(wid);

                    return wid;
                }
            }
            else
            {
                return "Unsuccess";
            }
        }

        public string CreateDocumentExt(EdocDocCreateRq edocDocCreateRq, string userBid)
        {
            var rawData = edocDocCreateRq.RqDetail;
            var rqDetail = rawData;
            string regNo = "-";
            string ConstWsubtypeIndoc = "03";
            string typeDoc = "03";
            string strRegMode = "0";

            if (string.IsNullOrWhiteSpace(rawData.DocDate))
            {
                rawData.DocDate = DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("th-TH"));
            }

            rawData.DocDate = rawData.DocDate.Replace("/", "-");

            if (int.Parse(rawData.DocDate.Split("-")[0]) <= 2500)
            {
                rawData.DocDate = rawData.DocDate.Replace(rawData.DocDate.Split("-")[0], (Int32.Parse(rawData.DocDate.Split("-")[0]) + 543).ToString());
            }

            rawData.DocDate = rawData.DocDate.Replace("-", "/");

            string curBid = userBid;
            var BasketInfo = _context.Basketinfos.Where(a => a.Bid == curBid).FirstOrDefault();
            string strPrefixIn = BasketInfo.Wfid; //WebservicePrefixIN
            string curDir = BasketInfo.HomeDir;
            string curBdsc = BasketInfo.Bdsc;
            string curUsrname = rawData.Username.ToUpper();
            string curDeptcode = BasketInfo.Deptcode;
            string ownerWsubType = curBid;

            var ifmFlowDept = _context.IfmflowDepartments.Where(a => a.Deptcode == curDeptcode).FirstOrDefault();
            strPrefixIn = ifmFlowDept.Prefixdept;

            var strSQL = "ifmflow_sp_ControlBasketinfo '6','" + ownerWsubType + "','-','" + curDeptcode + "','" + (ConstWsubtypeIndoc).Trim() + "','02','" + typeDoc + "','" + strRegMode + "'";
            string connectionString = SetSQLConnectionString();
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlTransaction Transaction = null;
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.SelectCommand.Transaction = Transaction;
            DataSet ds = new DataSet();
            da.Fill(ds, "ctrbasket");
            var dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Count == 1)
                {
                    throw new Exception(dt.Rows[0][0].ToString());
                }
                else
                {
                    var dr = dt.Rows[0];
                    regNo = dr[0].ToString().Trim();
                    string sendNo = dr[1].ToString().Trim();
                    string strRegNo = decimal.Parse(regNo).ToString();

                    if (strRegNo.Substring(strRegNo.Length - 3, 3) == ".00")
                    {
                        strRegNo = double.Parse(strRegNo).ToString();
                    }

                    if (strRegNo.IndexOf(".") > 0)
                    {
                        if (strRegNo.Substring(strRegNo.Length - 2, strRegNo.Length).IndexOf(".") > 0)
                        {
                            strRegNo = strRegNo + "0";
                        }
                    }

                    var strDate = DateTime.Now.Year + 543;
                    string itemNo = dr[2].ToString().Trim();
                    string docuname = dr[3].ToString().Trim();
                    string wid = strPrefixIn + strRegNo + "/" + strDate.ToString();
                    string wtype = "02";
                    string strDateAddDoc = rawData.DocDate;
                    string strTimeAddDoc = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + ":" + DateTime.Now.Second.ToString("D2");
                    string refWid = rqDetail.RefNumber;
                    string strTmpFrom = rqDetail.From;
                    string strTmpTo = rqDetail.SendTo;
                    string strTmpSubject = rqDetail.Subject;
                    string detail = rqDetail.Description; ;
                    string priority = rqDetail.Priority;

                    if (string.IsNullOrWhiteSpace(rqDetail.Priority))
                    {
                        rqDetail.Priority = "00";
                    }

                    string secLev = rqDetail.SecretLevel;

                    if (string.IsNullOrWhiteSpace(rqDetail.SecretLevel))
                    {
                        rqDetail.SecretLevel = "0";
                    }

                    string userAction = "-";
                    string strUserDate = "-";
                    string remark = rqDetail.Remark;
                    string attach = "-";
                    string selectActionInfo = "00";
                    string strBookGroup = "00";
                    string strAgeWid = "-";
                    string strLocation = "-";
                    string strAutoDelete = "0";
                    string strInternalAction = "00";
                    string strSelectReceivedoc = "01";

                    strSQL = "ifmflow_sp_CreateWork '" + wtype + "','" + curDeptcode + "','" + curBid + "','";
                    strSQL = strSQL + curBdsc + "','" + curDir + "','" + curUsrname + "','" + regNo + "','";
                    strSQL = strSQL + sendNo + "','" + itemNo + "','" + docuname + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + (strTimeAddDoc) + "','" + wid + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + refWid + "','" + typeDoc + "','" + strTmpFrom + "','" + strTmpTo + "','";
                    strSQL = strSQL + strTmpSubject + "','" + detail + "','" + priority + "','" + secLev + "','" + userAction + "','";
                    strSQL = strSQL + strUserDate + "','0','" + remark + "','" + ConvertRegNotoNumber(regNo) + "','";
                    strSQL = strSQL + attach + "','" + selectActionInfo + "','" + strBookGroup + "','" + strAgeWid + "','" + strLocation + "','" + strAutoDelete + "','" + strInternalAction + "','" + rqDetail.Username + "','" + strSelectReceivedoc + "'";

                    SqlCommand Command = null;
                    Command = Connection.CreateCommand();
                    Command.Transaction = Transaction;
                    Command.CommandText = strSQL;
                    Command.ExecuteNonQuery();
                    Connection.Close();

                    //insert bar code
                    InsertQrCode(wid);

                    return wid;
                }
            }
            else
            {
                return "Unsuccess";
            }
        }

        public Boolean UpdateDoc(EdocDocEditRq edocDocEditRq, string userBid)
        {
            msgLog = string.Empty;
            var rawData = edocDocEditRq.RqDetail;
            string refNumber, from, sendto, subject, priority, secretLevel, wdate;
            var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == rawData.WID && a.RegisterBid == userBid).FirstOrDefault();
            if (dataWorkInfo != null)
            {
                refNumber = dataWorkInfo.Refwid;
                from = dataWorkInfo.Worigin;
                sendto = dataWorkInfo.WownerBdsc;
                subject = dataWorkInfo.Wsubject;
                wdate = dataWorkInfo.Wdate;
                priority = dataWorkInfo.PriorityCode;
                secretLevel = dataWorkInfo.Secretlevcode;

                if (rawData.RefNumber.Trim() != "")
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขเลขที่หนังสืออ้างอิง จาก " + refNumber + " เป็น " + rawData.RefNumber;
                    refNumber = rawData.RefNumber;
                }

                if (rawData.From.Trim() != "")
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขส่งจาก จาก " + from + " เป็น " + rawData.From;
                    from = rawData.From;
                }

                if (rawData.SendTo.Trim() != "")
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขส่งถึง จาก " + sendto + " เป็น " + rawData.SendTo;
                    sendto = rawData.SendTo;
                }

                if (rawData.Subject.Trim() != "")
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขเรื่อง จาก " + subject + " เป็น " + rawData.Subject;
                    subject = rawData.Subject;
                }

                if (rawData.DocDate != null)
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขวันที่เอกสาร จาก " + wdate + " เป็น " + rawData.DocDate.Replace("-", "/");
                    wdate = rawData.DocDate;
                    wdate = wdate.Replace("/", "-");

                    if (int.Parse(wdate.Split("-")[0]) <= 2500)
                    {
                        wdate = wdate.Replace(wdate.Split("-")[0], (Int32.Parse(wdate.Split("-")[0]) + 543).ToString());

                    }

                    wdate = wdate.Replace("-", "/");
                }

                if (rawData.Priority.Trim() != "")
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขชั้นความเร็ว จาก " + priority + " เป็น " + rawData.Priority;
                    priority = rawData.Priority;
                }

                if (rawData.SecretLevel.Trim() != "")
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขชั้นความลับ จาก " + secretLevel + " เป็น " + rawData.SecretLevel;
                    secretLevel = rawData.SecretLevel;
                }

                if (msgLog != string.Empty)
                {
                    msgLog = "แก้ไขข้อมูลโดย " + edocDocEditRq.RqDetail.Username + msgLog;
                }

                dataWorkInfo.Refwid = refNumber;
                dataWorkInfo.Worigin = from;
                dataWorkInfo.WownerBdsc = sendto;
                dataWorkInfo.Wsubject = subject;
                dataWorkInfo.Wdate = wdate;
                dataWorkInfo.PriorityCode = priority;
                dataWorkInfo.Secretlevcode = secretLevel;
                dataWorkInfo.Wid = rawData.WID;

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CloseWip(string wid, string usrID, string userBid)
        {
            var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == userBid && a.RegisterNo != "-").FirstOrDefault();
            var strSQL = "ifmflow_sp_CloseWIP '" + userBid + "', '" + wid + "', '" + dataWorkInproc.RegisterNo + "', '" + dataWorkInproc.ItemNo + "', '" + userBid + "'";
            string connectionString = SetSQLConnectionString();
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlTransaction Transaction = null;
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.SelectCommand.Transaction = Transaction;
            DataSet ds = new DataSet();
            da.Fill(ds, "dsclosewip");
            var dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                string strCase = dr[0].ToString();
                switch (strCase)
                {
                    case "เอกสารได้ถูกยกเลิกออกจากตระกร้าไปแล้ว":
                        throw new Exception(strCase);
                    case "รายการนี้อยู่ระหว่างการตรวจสอบ ไม่สามารถปิดงานได้":
                        throw new Exception(strCase);
                    case "รายการนี้ปิดงานไปแล้ว":
                        throw new Exception(strCase);
                    case "ท่านต้องให้เลขทะเบียนก่อน":
                        throw new Exception(strCase);
                    case "รายการนี้ไม่ได้อยู่ในตระกร้ารับเข้าของท่าน ไม่สามารถปิดงานได้":
                        throw new Exception(strCase);
                    case "ดำเนินการปิดงานเรียบร้อยแล้ว":
                        UpdateFollowupMsg(dataWorkInproc.Bdsc, usrID, dataWorkInproc.RegisterNo, wid, "ปิดงาน");
                        return true;
                    default:
                        throw new Exception(strCase);
                }
            }
            else
            {
                throw new Exception("ไม่สามารถปิดงานได้");
            }
        }

        public Boolean UpdateFollowupMsg(string curBdsc, string curUsrname, string sbackRegno, string wid, string cmd)
        {
            var dataFollowUp = _context.Followups.Where(a => a.Wid == wid).FirstOrDefault();

            if (dataFollowUp != null)
            {
                var strDate = GetServerDate();
                var strTime = GetServerTime();
                var followMsg = "วันที่ " + strDate + " " + strTime + " " + curBdsc + ": " + curUsrname + System.Environment.NewLine + cmd + ": " + "ทะเบียน " + int.Parse(sbackRegno.Split(".")[0]);
                var lastActionMsg = dataFollowUp.ActionMsg + System.Environment.NewLine + followMsg + System.Environment.NewLine;

                dataFollowUp.ActionMsg = lastActionMsg;
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public string UpdateWorkinproc(string wid, string receiverBid, string senderBid, string usrID, string username)
        {
            try
            {
                var basketSender = _context.Basketinfos.Where(a => a.Bid == senderBid).FirstOrDefault();
                var basketReceive = _context.Basketinfos.Where(a => a.Bid == receiverBid).FirstOrDefault();
                var workinProc = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == senderBid && a.Usrid != "-").FirstOrDefault();

                if (workinProc == null)
                {
                    return "Unsuccess";
                }

                string userId = usrID.ToUpper();
                int tmpregNo = int.Parse(basketReceive.ItemNo) + 1;
                string regNo = "-";
                string itemNo = tmpregNo.ToString();
                string receiveBdsc = basketReceive.Bdsc;
                string receiveDateNormal = GetServDateNormalFormat();
                string receiveDate = GetServerDate();
                string receiveTime = GetServerTime();
                string senderBdsc = basketSender.Bdsc;
                string actionCode = "00";
                string priorityCode = "00";
                string secLevCode = "0";
                string attach1 = "00";
                string attach2 = "01";
                string senderMsg = "-";
                string actionMsg = "วันที่ " + receiveDateNormal + " " + receiveTime + " ส่งโดย " + senderBdsc + " : " + username + "  ";
                string senderRegno = workinProc.SenderRegisterNo;
                string bookGroup = "00";
                string realDoc = "0";
                string location = "-";
                string maxTime = "-";
                string actionFollowup = "0";

                string strSQL = "ifmflow_sp_SendDocument '" + userId + "','" + wid + "','" + regNo + "','" + itemNo + "','" + receiverBid + "','" + receiveBdsc + "','" + receiveDate + "','";
                strSQL = strSQL + receiveTime + "','" + senderBid + "','" + senderBdsc + "','" + actionCode + "','" + priorityCode + "','" + secLevCode + "','";
                strSQL = strSQL + attach1 + "','" + attach2 + "','" + actionMsg + "','" + senderRegno + "','" + bookGroup + "','" + realDoc + "','" + location + "','";
                strSQL = strSQL + maxTime + "','" + senderMsg + "','" + actionFollowup + "'";

                string connectionString = SetSQLConnectionString();
                SqlConnection Connection = new SqlConnection(connectionString);
                Connection.Open();
                SqlTransaction Transaction = null;
                SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.SelectCommand.Transaction = Transaction;

                SqlCommand Command = null;
                Command = Connection.CreateCommand();
                Command.Transaction = Transaction;
                Command.CommandText = strSQL;
                Command.ExecuteNonQuery();
                Connection.Close();

                basketReceive.ItemNo = itemNo;
                workinProc.StateCode = "03";
                workinProc.CompleteDate = receiveDate;
                workinProc.CompleteTime = receiveTime;
                _context.SaveChanges();

                return "Success";
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string UpdateWorkinprocforIPST(string wid, string receiverBid, string senderBid, string usrID, string username, string receiver)
        {
            try
            {
                var basketSender = _context.Basketinfos.Where(a => a.Bid == senderBid).FirstOrDefault();
                var basketReceive = _context.Basketinfos.Where(a => a.Bid == receiverBid).FirstOrDefault();
                var workinProc = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == senderBid && a.Usrid != "-").FirstOrDefault();

                if (workinProc == null)
                {
                    return "Unsuccess";
                }

                string userId = usrID.ToUpper();
                int tmpregNo = int.Parse(basketReceive.ItemNo) + 1;
                string regNo = "-";
                string itemNo = tmpregNo.ToString();
                string receiveBdsc = basketReceive.Bdsc;
                string receiveDateNormal = GetServDateNormalFormat();
                string receiveDate = GetServerDate();
                string receiveTime = GetServerTime();
                string senderBdsc = basketSender.Bdsc;
                string actionCode = "00";
                string priorityCode = "00";
                string secLevCode = "0";
                string attach1 = "00";
                string attach2 = "01";
                string senderMsg = "-";
                string actionMsg = "วันที่ " + receiveDateNormal + " " + receiveTime + " ส่งโดย " + senderBdsc + " : " + username + "  ";
                string senderRegno = workinProc.SenderRegisterNo;
                string bookGroup = "00";
                string realDoc = "0";
                string location = "-";
                string maxTime = "-";
                string actionFollowup = "0";

                string strSQL = "ifmflow_sp_SendDocument '" + userId + "','" + wid + "','" + regNo + "','" + itemNo + "','" + receiverBid + "','" + receiveBdsc + "','" + receiveDate + "','";
                strSQL = strSQL + receiveTime + "','" + senderBid + "','" + senderBdsc + "','" + actionCode + "','" + priorityCode + "','" + secLevCode + "','";
                strSQL = strSQL + attach1 + "','" + attach2 + "','" + actionMsg + "','" + senderRegno + "','" + bookGroup + "','" + realDoc + "','" + location + "','";
                strSQL = strSQL + maxTime + "','" + senderMsg + "','" + actionFollowup + "'";

                string connectionString = SetSQLConnectionString();
                SqlConnection Connection = new SqlConnection(connectionString);
                Connection.Open();
                SqlTransaction Transaction = null;
                SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.SelectCommand.Transaction = Transaction;

                SqlCommand Command = null;
                Command = Connection.CreateCommand();
                Command.Transaction = Transaction;
                Command.CommandText = strSQL;
                Command.ExecuteNonQuery();
                Connection.Close();

                basketReceive.ItemNo = itemNo;
                workinProc.StateCode = "03";
                workinProc.CompleteDate = receiveDate;
                workinProc.CompleteTime = receiveTime;
                _context.SaveChanges();

                var workinProcess = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == receiverBid && a.Usrid == "-").FirstOrDefault();
                var workinProcessDup = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == receiverBid).FirstOrDefault();
                var userInfo = _context.Userinfos.Where(a => a.Bid == receiverBid && a.Usrid == receiver).FirstOrDefault();

                if (userInfo == null)
                {
                    return "Unsuccess";
                }

                string currentRegNo = ConvertRegNotoNumber(basketReceive.RegisterNo);
                currentRegNo = (int.Parse(currentRegNo) + 1).ToString();
                string newRegNo = ConvertNumbertoRegNo(currentRegNo);
                string updateActionMsg = workinProcess.ActionMsg + "วันที่ " + receiveDateNormal + " " + receiveTime + " " + receiveBdsc + " : " + userInfo.Username + "  " + "รับเอกสารต้นฉบับ : เลขทะเบียน " + currentRegNo + "  ";

                if (workinProcessDup.RegisterNo != "-")
                {
                    currentRegNo = ConvertRegNotoNumber(workinProcessDup.RegisterNo);
                    workinProcess.RegisterNo = workinProcessDup.RegisterNo;
                    workinProcess.Usrid = receiver.ToUpper();
                    workinProcess.StateCode = "02";
                    workinProcess.Viewstatus = "1";
                    workinProcess.ActionMsg = actionMsg + "วันที่ " + receiveDateNormal + " " + receiveTime + " " + receiveBdsc + " : " + userInfo.Username + System.Environment.NewLine + "เอกสารเดิมกลับมา : เลขทะเบียน " + currentRegNo + "  ";
                    workinProcess.SenderRegisterNo = workinProc.RegisterNo;
                    _context.SaveChanges();

                    return "Success";
                }
                else
                {
                    workinProcess.RegisterNo = newRegNo;
                    workinProcess.Usrid = receiver.ToUpper();
                    workinProcess.StateCode = "02";
                    workinProcess.Viewstatus = "1";
                    workinProcess.ActionMsg = updateActionMsg;
                    _context.SaveChanges();

                    UpdateRegNoBasketinfo(currentRegNo, receiverBid);

                    return "Success";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string GetConnectionString()
        {
            try
            {
                var connStr = _configuration.GetConnectionString("SarabanDatabase");
                int svStart = connStr.IndexOf("Server=") + "Server=".Length;
                int svEnd = connStr.IndexOf(";Database") - svStart;
                string ip = connStr.Substring(svStart, svEnd);
                string dbName = GetCurrentDB();
                int uidStart = connStr.IndexOf("user id=") + "user id=".Length;
                int uidEnd = connStr.IndexOf(";Password") - uidStart;
                string uid = connStr.Substring(uidStart, uidEnd);
                int pwdStart = connStr.IndexOf("Password=") + "Password=".Length;
                int pwdEnd = connStr.IndexOf(";ConnectRetryCount") - pwdStart;
                string pwd = connStr.Substring(pwdStart, pwdEnd);
                string result = "Server=" + ip + ";Database=" + dbName + ";user id=" + uid + ";Password=" + pwd + ";ConnectRetryCount=0";

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string SetConnectionString(string year)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(year))
                {
                    var connStr = _configuration.GetConnectionString("SarabanDatabase");
                    int svStart = connStr.IndexOf("Server=") + "Server=".Length;
                    int svEnd = connStr.IndexOf(";Database") - svStart;
                    string ip = connStr.Substring(svStart, svEnd);
                    string dbName = GetCurrentDB();
                    int uidStart = connStr.IndexOf("user id=") + "user id=".Length;
                    int uidEnd = connStr.IndexOf(";Password") - uidStart;
                    string uid = connStr.Substring(uidStart, uidEnd);
                    int pwdStart = connStr.IndexOf("Password=") + "Password=".Length;
                    int pwdEnd = connStr.IndexOf(";ConnectRetryCount") - pwdStart;
                    string pwd = connStr.Substring(pwdStart, pwdEnd);
                    string result = "Server=" + ip + ";Database=" + dbName + ";user id=" + uid + ";Password=" + pwd + ";ConnectRetryCount=0";

                    return result;
                }
                else
                {
                    var connStr = _configuration.GetConnectionString("SarabanDatabase");
                    int svStart = connStr.IndexOf("Server=") + "Server=".Length;
                    int svEnd = connStr.IndexOf(";Database") - svStart;
                    string ip = connStr.Substring(svStart, svEnd);
                    int dbstart = connStr.IndexOf("Database=") + "Database=".Length;
                    int dbend = connStr.IndexOf(";user id") - dbstart;
                    string dbName = connStr.Substring(dbstart, dbend);
                    int uidStart = connStr.IndexOf("user id=") + "user id=".Length;
                    int uidEnd = connStr.IndexOf(";Password") - uidStart;
                    string uid = connStr.Substring(uidStart, uidEnd);
                    int pwdStart = connStr.IndexOf("Password=") + "Password=".Length;
                    int pwdEnd = connStr.IndexOf(";ConnectRetryCount") - pwdStart;
                    string pwd = connStr.Substring(pwdStart, pwdEnd);
                    string result = "Server=" + ip + ";Database=" + dbName + year + ";user id=" + uid + ";Password=" + pwd + ";ConnectRetryCount=0";

                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string ListConnectionString(string dbName)
        {
            try
            {
                var connStr = _configuration.GetConnectionString("SarabanDatabase");
                int svStart = connStr.IndexOf("Server=") + "Server=".Length;
                int svEnd = connStr.IndexOf(";Database") - svStart;
                string ip = connStr.Substring(svStart, svEnd);
                int uidStart = connStr.IndexOf("user id=") + "user id=".Length;
                int uidEnd = connStr.IndexOf(";Password") - uidStart;
                string uid = connStr.Substring(uidStart, uidEnd);
                int pwdStart = connStr.IndexOf("Password=") + "Password=".Length;
                int pwdEnd = connStr.IndexOf(";ConnectRetryCount") - pwdStart;
                string pwd = connStr.Substring(pwdStart, pwdEnd);
                string result = "Server=" + ip + ";Database=" + dbName + ";user id=" + uid + ";Password=" + pwd + ";ConnectRetryCount=0";

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<string> ListFlowCabinet()
        {
            try
            {
                var data = _context_cabinet.FlwCabinets.Where(a => a.RdbmsStatus == "01").OrderByDescending(a => a.CabName).Select(a => a.RdbmsDatabasename).ToList();

                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetCurrentDB()
        {
            try
            {
                var data = _context_cabinet.FlwCabinets.Where(a => a.RdbmsStatus == "01" && (!a.RdbmsDatabasename.Contains("sec") && !a.CabName.Contains("ลับ"))).OrderByDescending(a => a.CabName).FirstOrDefault();
                var result = data.RdbmsDatabasename;

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string SetSQLConnectionString()
        {
            try
            {
                var connStr = _configuration.GetConnectionString("SarabanDatabase");
                int svStart = connStr.IndexOf("Server=") + "Server=".Length;
                int svEnd = connStr.IndexOf(";Database") - svStart;
                string ip = connStr.Substring(svStart, svEnd);
                string dbName = GetCurrentDB();
                int uidStart = connStr.IndexOf("user id=") + "user id=".Length;
                int uidEnd = connStr.IndexOf(";Password") - uidStart;
                string uid = connStr.Substring(uidStart, uidEnd);
                int pwdStart = connStr.IndexOf("Password=") + "Password=".Length;
                int pwdEnd = connStr.IndexOf(";ConnectRetryCount") - pwdStart;
                string pwd = connStr.Substring(pwdStart, pwdEnd);
                string result = "data source=" + ip + ";initial catalog=" + dbName + ";password=" + pwd + ";user id=" + uid + ";";

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string ConvertRegNotoNumber(string regNo)
        {
            try
            {
                if (regNo == "" || regNo == "-")
                {
                    return "-";
                }
                else
                {
                    string[] item = regNo.Split(".");

                    if (item[0] == "0000000")
                    {
                        return "0";
                    }
                    else
                    {
                        string result = item[0].TrimStart('0');

                        return result;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string ConvertNumbertoRegNo(string regNo)
        {
            try
            {
                if (regNo.Contains('/'))
                {
                    string[] item = regNo.Split("/");
                    int suffix = int.Parse(item[1]);
                    string tmpSuffix = suffix.ToString("D9");
                    string result = item[0] + "/" + tmpSuffix + ".00";

                    return result;
                }
                else
                {
                    int item = int.Parse(regNo);
                    string tmpRegNo = item.ToString("D12");
                    string result = tmpRegNo + ".00";

                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string BasketDescription(string bid)
        {
            try
            {
                var basketInfo = _context.Basketinfos.Where(a => a.Bid == bid).FirstOrDefault();
                string result = basketInfo.Bdsc;

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool CreateLog(string method, string errorMsg, string appID)
        {
            try
            {
                if (!Directory.Exists("c:\\WebAPILog"))
                {
                    Directory.CreateDirectory("c:\\WebAPILog");
                }
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                var filename = "c:\\WebAPILog\\Saraban_api_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH")) + ".txt";
                System.IO.File.AppendAllText(filename, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("th-TH")) + " " + method + " " + "Request From appID =" + " " + "" + appID + "" + " " + "" + remoteIpAddress + "" + " " + "" + errorMsg + "" + Environment.NewLine);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CreateURL(string docuname)
        {
            string tmpDocuname = docuname.Trim();
            string[] pathArray = tmpDocuname.Split('\\');
            string newDocuname = _configuration.GetSection("MySettings").GetSection("Flowdata").Value + "\\" + pathArray[4] + "\\" + pathArray[5] + "\\" + pathArray[6];
            string token = EncryptText(newDocuname, "inf0m@ECL62");
            string strURL = _configuration.GetSection("MySettings").GetSection("APIReader").Value + "/Verify.aspx?token=" + token + "";

            return strURL;
        }

        public static string EncryptText(string input, string password)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (Aes AES = Aes.Create())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public string CheckInputParams(string wid, string userBid, string year, string regNumber, string workType, string bookGroup)
        {
            string result = string.Empty;
            string regNo = string.Empty;

            if (!string.IsNullOrWhiteSpace(year))
            {
                _context.Database.GetDbConnection().ConnectionString = SetConnectionString(year);
            }
            else
            {
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
            }

            if (!string.IsNullOrWhiteSpace(wid) && string.IsNullOrWhiteSpace(regNumber) && string.IsNullOrWhiteSpace(workType) && string.IsNullOrWhiteSpace(bookGroup))
            {
                var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == wid && a.RegisterBid == userBid).FirstOrDefault();

                if (dataWorkInfo == null)
                {
                    var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == userBid).FirstOrDefault();

                    if (dataWorkInproc == null)
                    {
                        result = "0";
                    }
                    else
                    {
                        result = dataWorkInproc.Wid;
                    }
                }
                else
                {
                    result = dataWorkInfo.Wid;
                }
            }

            if (!string.IsNullOrWhiteSpace(wid) && (!string.IsNullOrWhiteSpace(regNumber) || !string.IsNullOrWhiteSpace(workType) || !string.IsNullOrWhiteSpace(bookGroup)))
            {
                result = "0";
            }

            if (!string.IsNullOrWhiteSpace(regNumber) && !string.IsNullOrWhiteSpace(workType) && string.IsNullOrWhiteSpace(bookGroup) && string.IsNullOrWhiteSpace(wid))
            {
                regNo = ConvertNumbertoRegNo(regNumber);

                if (workType == "00")
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.RegisterBid == userBid && a.Wtype == workType && a.RegisterNo == regNo).FirstOrDefault();

                    if (dataWorkInfo == null)
                    {
                        result = "0";
                    }
                    else
                    {
                        result = dataWorkInfo.Wid;
                    }
                }

                if (workType == "01")
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.RegisterBid == userBid && a.Wtype == workType && a.RegisterNo == regNo).FirstOrDefault();

                    if (dataWorkInfo == null)
                    {
                        var dataWorkInproc = _context.Workinprocesses.Where(a => a.Bid == userBid && a.RegisterNo == regNo).FirstOrDefault();

                        if (dataWorkInproc == null)
                        {
                            result = "0";
                        }
                        else
                        {
                            result = dataWorkInproc.Wid;
                        }
                    }
                    else
                    {
                        var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == dataWorkInfo.Wid).FirstOrDefault();

                        result = dataWorkInproc.Wid;
                    }
                }

                if (workType == "02")
                {
                    result = "0";
                }
            }

            if (!string.IsNullOrWhiteSpace(regNumber) && !string.IsNullOrWhiteSpace(workType) && !string.IsNullOrWhiteSpace(bookGroup) && string.IsNullOrWhiteSpace(wid))
            {
                regNo = ConvertNumbertoRegNo(regNumber);

                if (workType == "00")
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.RegisterBid == userBid && a.Wtype == workType && a.RegisterNo == regNo).FirstOrDefault();

                    if (dataWorkInfo == null)
                    {
                        result = "0";
                    }
                    else
                    {
                        result = dataWorkInfo.Wid;
                    }
                }

                if (workType == "01")
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.RegisterBid == userBid && a.Wtype == workType && a.RegisterNo == regNo).FirstOrDefault();

                    if (dataWorkInfo == null)
                    {
                        var dataWorkInproc = _context.Workinprocesses.Where(a => a.Bid == userBid && a.RegisterNo == regNo && a.Bookgroup == bookGroup).FirstOrDefault();

                        if (dataWorkInproc == null)
                        {
                            result = "0";
                        }
                        else
                        {
                            result = dataWorkInproc.Wid;
                        }
                    }
                    else
                    {
                        var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == dataWorkInfo.Wid && a.Bookgroup == bookGroup).FirstOrDefault();

                        if (dataWorkInproc == null)
                        {
                            result = "0";
                        }
                        else
                        {
                            result = dataWorkInproc.Wid;
                        }
                    }
                }

                if (workType == "02")
                {
                    result = "0";
                }
            }

            return result;
        }

        public Boolean CreateBasket(string bid, string bdsc)
        {
            try
            {
                string currentDB = GetCurrentDB();
                string ipServer = _configuration.GetSection("MySettings").GetSection("IPWebServer").Value;
                string setHomedir = "\\\\" + ipServer + "\\FLOWDATA\\" + currentDB + "\\" + bid;
                var data = new Basketinfo
                {
                    Bid = bid,
                    Bdsc = bdsc,
                    Deptcode = "100",
                    Wfid = bid + "/",
                    Wfdsc = "-",
                    StateCode = "00",
                    RegisterNo = "0000000.00",
                    SendNo = "0000000.00",
                    ItemNo = "0",
                    DocuName = "00000000",
                    HomeDir = setHomedir.ToUpper(),
                    Class = "-",
                    Password = "123"
                };

                _context.Basketinfos.Add(data);

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean UpdateRegNoBasketinfo(string regNo, string bid)
        {
            try
            {
                var dataBasket = _context.Basketinfos.Where(a => a.Bid == bid).FirstOrDefault();

                int item = int.Parse(regNo);
                string tmpRegNo = item.ToString("D7");
                string result = tmpRegNo + ".00";
                dataBasket.RegisterNo = result;

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetDaypast()
        {
            var daysPast = int.Parse(_configuration.GetSection("MySettings").GetSection("DaysPast").Value);
            var curDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime startDate = DateTime.Parse(curDate);

            var backDate = startDate.AddDays(-daysPast);
            var stry = backDate.ToString("yyyy-MM-dd").Substring(0, 4);
            var strm = backDate.ToString("yyyy-MM-dd").Substring(5, 2);
            var strd = backDate.ToString("yyyy-MM-dd").Substring(8, 2);

            if (int.Parse(stry) < 2500)
            {
                stry = (int.Parse(stry) + 543).ToString();
            }
            if (strm.Length == 1)
            {
                strm = "0" + strm;
            }
            if (strd.Length == 1)
            {
                strd = "0" + strm;
            }

            string result = stry + "/" + strm + "/" + strd;

            return result;
        }

        public string GetPeriod(string date)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH"));
            DateTime curDate = DateTime.Parse(strDate);
            DateTime pastDate = DateTime.Parse(date);
            var totalDate = (curDate - pastDate).TotalDays;
            string result = totalDate.ToString();

            return result;
        }

        #endregion
    }
}
