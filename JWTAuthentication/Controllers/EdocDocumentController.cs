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
using JWTAuthentication.Models.EdocDocumentInquiry;
using JWTAuthentication.Models.EdocDocumentIncoming;
using JWTAuthentication.Models.EdocDocumentInproc;
using JWTAuthentication.Models.EdocDocumentCreation;
using JWTAuthentication.Models.EdocDocumentEdit;
using JWTAuthentication.Models.EdocDocumentUpload;
using JWTAuthentication.Models.EdocDocumentTracking;
using JWTAuthentication.Models.EdocDocumentActionMessage;
using JWTAuthentication.Models.EdocDocumentSend;
using JWTAuthentication.Models.EdocDocumentClose;
using JWTAuthentication.Models.EdocDocumentCancel;
using JWTAuthentication.Models.EdocDocumentFollowup;
using JWTAuthentication.Models.EdocDocumentAttachActionMessage;
using JWTAuthentication.Models.EdocDocumentCreateEForm;
using JWTAuthentication.Models.EdocDocumentGetBasketInfo;
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
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdocDocumentController : ControllerBase
    {
        private readonly RwSaraban64Context _context;
        private readonly AotDoccirContext _context_doccir;
        private readonly RwCabinetContext _context_cabinet;
        private readonly IConfiguration _configuration;
        private string msgLog;

        public EdocDocumentController(RwSaraban64Context context, AotDoccirContext context_doccir, RwCabinetContext context_cabinet, IConfiguration configuration)
        {
            _context = context;
            _context_doccir = context_doccir;
            _context_cabinet = context_cabinet;
            _configuration = configuration;
        }

        //Saraban Standard Version//

        #region

        [Authorize]
        [HttpGet]
        [Route("DocumentInquiry")]
        public ActionResult<EdocDocDetailRs> DocumentInquiry(EdocDocDetailRq edocDocDetailRq)
        {
            string method = "DocumentInquiry";
            try
            {
                var rawData = edocDocDetailRq.RqDetail;
                string userBid = string.Empty;
                string userID = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, edocDocDetailRq.RqHeader.AppId);

                        return StatusCode(400, responseMsg);
                    }
                    else
                    {
                        userBid = userMainBid.Bid;
                        userID = userMainBid.Usrid;
                    }
                }
                else
                {
                    userBid = dataUser.Bid;
                    userID = dataUser.Usrid;
                }
                //********************************//

                //********** check data **********//
                var data = _context.Workinfos.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                if (data != null)
                {
                    var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).FirstOrDefault();

                    if (dataWorkInproc == null)
                    {
                        var responseMsg = CheckData(method, edocDocDetailRq.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocDetailRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                //********************************//

                var docAttms = _context.Docattaches.Where(a => a.Wid == rawData.WID).ToList();
                var attmDetails = new List<RsAttachmentDetail>();
                var getFileExt = Path.GetExtension(data.Docuname);

                if (!string.IsNullOrWhiteSpace(data.Docuname) && !string.IsNullOrWhiteSpace(getFileExt))
                {
                    attmDetails.Add(new RsAttachmentDetail
                    {
                        AttachDate = data.RegisterDate + " " + data.Registertime,
                        Detail = "เอกสารแนบหลัก",
                        URL = GetURL(data.Docuname, data.Wid, data.Wsubject, userID)
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
                            URL = GetURL(docAttm.Attachname, data.Wid, data.Wsubject, userID)
                        });
                    }
                }

                var actionMsgs = _context.ActionMessages.Where(a => a.Wid == rawData.WID).ToList();
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
                            //SignImage = actionMsg.Imagefile
                        });
                    }
                }

                var ret = new Models.EdocDocumentInquiry.RsDetail
                {
                    WID = data.Wid,
                    DocDate = data.Wdate,
                    From = data.Worigin,
                    SendTo = data.WownerBdsc,
                    Priority = data.PriorityCode,
                    Subject = data.Wsubject,
                    SecretLevel = data.Secretlevcode,
                    RefNumber = data.Refwid,
                    AttachmentDetail = attmDetails,
                    ActionMessageDetail = actionMsgDetail

                };
                var res = new EdocDocDetailRs
                {
                    RsHeader = new Models.EdocDocumentInquiry.RsHeader
                    {
                        AppId = edocDocDetailRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInquiry.RsHeaderStatus
                        {
                            OrgStatusCode = "0",
                            OrgStatusDesc = "Success",
                            StatusCode = "0"
                        }
                    },
                    RsDetail = ret
                };

                CreateLog(method, "Success", edocDocDetailRq.RqHeader.AppId);

                return StatusCode(200, res);

            }
            catch (Exception ex)
            {
                var res = new EdocDocDetailRs
                {
                    RsHeader = new Models.EdocDocumentInquiry.RsHeader
                    {
                        AppId = edocDocDetailRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInquiry.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocDetailRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentIncoming")]
        public ActionResult<EdocDocIncomingRs> DocumentIncoming([Required] string usrID, [Required] string appID)
        {
            string method = "DocumentIncoming";
            try
            {
                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == usrID && a.Mainbid == "0").FirstOrDefault();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, appID);

                    return StatusCode(400, responseMsg);
                }
                //*********************************//

                var paramBid = new SqlParameter("@bid", dataUser.Bid);
                var strSql = "SELECT t1.*";
                strSql += " FROM workinfo AS t1,workinprocess AS t2 ";
                strSql += " WHERE (t1.wid = t2.wid) AND (t2.bid = @bid) AND (((t2.statecode<'03' OR t2.statecode='12') ";
                strSql += " AND (t2.registerno='-')) OR (t2.registerno<>'-' AND t2.statecode='00' AND t2.usrid='-'))";

                var data = _context.Workinfos.FromSqlRaw(strSql, paramBid).Select(a => a.Wid).ToList();

                if (data == null)
                {
                    var responseMsg = CheckData(method, appID, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var ret = new Models.EdocDocumentIncoming.RsDetail
                    {
                        Wid = data,
                    };
                    var res = new EdocDocIncomingRs
                    {
                        RsHeader = new Models.EdocDocumentIncoming.RsHeader
                        {
                            AppId = appID,
                            Status = new Models.EdocDocumentIncoming.RsHeaderStatus
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
                var res = new EdocDocIncomingRs
                {
                    RsHeader = new Models.EdocDocumentIncoming.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.EdocDocumentIncoming.RsHeaderStatus
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
        [Route("DocumentPending")]
        public ActionResult<EdocDocInprocRs> DocumentPending([Required] string usrID, [Required] string appID)
        {
            string method = "DocumentPending";
            try
            {
                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == usrID && a.Mainbid == "0").FirstOrDefault();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, appID);

                    return StatusCode(400, responseMsg);
                }
                //*********************************//

                var daysPast = int.Parse(_configuration.GetSection("MySettings").GetSection("DaysPast").Value);
                var curDate = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime startDate = DateTime.Parse(curDate);
                DateTime expiryDate = startDate.AddDays(-daysPast);

                var backDate = startDate.AddDays(-daysPast); //DateSerial(Year(curdate), Month(curdate), Day(curdate) - dayspast);
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

                string strSearchDate = stry + "/" + strm + "/" + strd;

                var paramBid = new SqlParameter("@bid", dataUser.Bid);
                var paramBackDate = new SqlParameter("@backdate", strSearchDate);
                var strSql = "SELECT t1.*";
                strSql += " FROM workinfo AS t1,workinprocess AS t2 ";
                strSql += " WHERE (t1.wid = t2.wid) AND (t2.bid = @bid) AND (t2.statecode<'03') AND (t2.registerno<>'-') ";
                strSql += " AND (t2.viewstatus<>'0') AND (t1.wtype='01' or t1.wtype='00' or ( t1.wtype='02' and t2.bid= @bid and t1.registerbid <> @bid ))";
                strSql += " AND (t2.initdate >= @backdate)";

                //********** check data **********//
                var data = _context.Workinfos.FromSqlRaw(strSql, paramBid, paramBackDate).Select(a => a.Wid).ToList();

                if (data == null)
                {
                    var responseMsg = CheckData(method, appID, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var ret = new Models.EdocDocumentInproc.RsDetail
                    {
                        Wid = data,
                    };
                    var res = new EdocDocInprocRs
                    {
                        RsHeader = new Models.EdocDocumentInproc.RsHeader
                        {
                            AppId = appID,
                            Status = new Models.EdocDocumentInproc.RsHeaderStatus
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
                var res = new EdocDocInprocRs
                {
                    RsHeader = new Models.EdocDocumentInproc.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.EdocDocumentInproc.RsHeaderStatus
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
        [HttpPost]
        [Route("DocumentCreateOutside")]
        public ActionResult<EdocDocCreateRs> DocumentCreateOutside(EdocDocCreateRq edocDocCreateRq)
        {
            string method = "DocumentCreateOutside";
            try
            {
                var rawData = edocDocCreateRq.RqDetail;
                string userBid = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, edocDocCreateRq.RqHeader.AppId);

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
                //********************************//

                //********** check data **********//
                if (string.IsNullOrWhiteSpace(rawData.WID))
                {
                    rawData.WID = rawData.From + "/" + DateTime.Now.ToString("ddMMyyyy", new CultureInfo("th-TH")) + "/" + DateTime.Now.ToString("HHmmss", new CultureInfo("th-TH"));
                }

                var data = _context.Workinfos.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                if (data != null)
                {
                    var responseMsg = CheckData(method, edocDocCreateRq.RqHeader.AppId, "2");

                    return StatusCode(400, responseMsg);
                }
                //*******************************//

                if (string.IsNullOrWhiteSpace(rawData.From) || string.IsNullOrWhiteSpace(rawData.SendTo) || string.IsNullOrWhiteSpace(rawData.Subject))
                {
                    var responseMsg = CheckInputData(method, edocDocCreateRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                var wid = CreateDocumentOutside(edocDocCreateRq, userBid);

                if (wid != null)
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == wid).FirstOrDefault();
                    string registerNo = dataWorkInfo.RegisterNo.ToString().Split(".")[0];
                    int regNo = int.Parse(registerNo);

                    var ret = new Models.EdocDocumentCreation.RsDetail
                    {
                        WID = wid,
                        RegisterNo = string.Format(regNo.ToString(), "000000000000"),
                        Wdate = dataWorkInfo.RegisterDate + " " + dataWorkInfo.Registertime
                    };
                    var res = new EdocDocCreateRs
                    {
                        RsHeader = new Models.EdocDocumentCreation.RsHeader
                        {
                            AppId = edocDocCreateRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreation.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", edocDocCreateRq.RqHeader.AppId);

                    return StatusCode(201, res);
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocCreateRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocInprocRs
                {
                    RsHeader = new Models.EdocDocumentInproc.RsHeader
                    {
                        AppId = edocDocCreateRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInproc.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocCreateRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentCreate")]
        public ActionResult<EdocDocCreateRs> DocumentCreate(EdocDocCreateRq edocDocCreateRq)
        {
            string method = "DocumentCreate";
            try
            {
                var rawData = edocDocCreateRq.RqDetail;
                string userBid = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, edocDocCreateRq.RqHeader.AppId);

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
                //********************************//

                if (string.IsNullOrWhiteSpace(rawData.From))
                {
                    var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == userBid).FirstOrDefault();
                    rawData.From = dataBasketInfo.Bdsc;
                }

                if (string.IsNullOrWhiteSpace(rawData.SendTo) || string.IsNullOrWhiteSpace(rawData.Subject))
                {
                    var responseMsg = CheckInputData(method, edocDocCreateRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                var wid = CreateDocument(edocDocCreateRq, userBid);

                if (wid != null)
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == wid).FirstOrDefault();
                    string registerNo = dataWorkInfo.RegisterNo.ToString().Split(".")[0];
                    int regNo = int.Parse(registerNo);

                    var ret = new Models.EdocDocumentCreation.RsDetail
                    {
                        WID = wid,
                        RegisterNo = string.Format(regNo.ToString(), "000000000000"),
                        Wdate = dataWorkInfo.RegisterDate + " " + dataWorkInfo.Registertime
                    };
                    var res = new EdocDocCreateRs
                    {
                        RsHeader = new Models.EdocDocumentCreation.RsHeader
                        {
                            AppId = edocDocCreateRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreation.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", edocDocCreateRq.RqHeader.AppId);

                    return StatusCode(201, res);
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocCreateRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocInprocRs
                {
                    RsHeader = new Models.EdocDocumentInproc.RsHeader
                    {
                        AppId = edocDocCreateRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInproc.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocCreateRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentCreateExt")]
        public ActionResult<EdocDocCreateRs> DocumentCreateExt(EdocDocCreateRq edocDocCreateRq)
        {
            string method = "DocumentCreateExt";
            try
            {
                var rawData = edocDocCreateRq.RqDetail;
                string userBid = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, edocDocCreateRq.RqHeader.AppId);

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
                //********************************//

                if (string.IsNullOrWhiteSpace(rawData.From))
                {
                    var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == userBid).FirstOrDefault();
                    rawData.From = dataBasketInfo.Bdsc;
                }

                if (string.IsNullOrWhiteSpace(rawData.SendTo) || string.IsNullOrWhiteSpace(rawData.Subject))
                {
                    var responseMsg = CheckInputData(method, edocDocCreateRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                var wid = CreateDocumentExt(edocDocCreateRq, userBid);

                if (wid != null)
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == wid).FirstOrDefault();
                    string registerNo = dataWorkInfo.RegisterNo.ToString().Split(".")[0];
                    int regNo = int.Parse(registerNo);

                    var ret = new Models.EdocDocumentCreation.RsDetail
                    {
                        WID = wid,
                        RegisterNo = string.Format(regNo.ToString(), "000000000000"),
                        Wdate = dataWorkInfo.RegisterDate + " " + dataWorkInfo.Registertime
                    };
                    var res = new EdocDocCreateRs
                    {
                        RsHeader = new Models.EdocDocumentCreation.RsHeader
                        {
                            AppId = edocDocCreateRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreation.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", edocDocCreateRq.RqHeader.AppId);

                    return StatusCode(201, res);
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocCreateRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocInprocRs
                {
                    RsHeader = new Models.EdocDocumentInproc.RsHeader
                    {
                        AppId = edocDocCreateRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentInproc.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocCreateRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentCreateEForm")]
        public ActionResult<EdocDocCreateEFormRs> DocumentCreateEForm(EdocDocCreateEFormRq EdocDocCreateEFormRq)
        {
            string method = "DocumentCreateEForm";
            try
            {
                var rawData = EdocDocCreateEFormRq.RqDetail;
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
                        var responseMsg = CheckUser(method, EdocDocCreateEFormRq.RqHeader.AppId);

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
                //********************************//

                if (string.IsNullOrWhiteSpace(rawData.SendTo) || string.IsNullOrWhiteSpace(rawData.Subject))
                {
                    var responseMsg = CheckInputData(method, EdocDocCreateEFormRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }

                var wid = CreateEForm(EdocDocCreateEFormRq, userBid);

                if (wid != null)
                {
                    var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == wid).FirstOrDefault();
                    string registerNo = dataWorkInfo.RegisterNo.ToString().Split(".")[0];
                    int regNo = int.Parse(registerNo);

                    var ret = new Models.EdocDocumentCreateEForm.RsDetail
                    {
                        WID = wid,
                        RegisterNo = string.Format(regNo.ToString(), "000000000000"),
                        Wdate = dataWorkInfo.RegisterDate + " " + dataWorkInfo.Registertime
                    };
                    var res = new EdocDocCreateEFormRs
                    {
                        RsHeader = new Models.EdocDocumentCreateEForm.RsHeader
                        {
                            AppId = EdocDocCreateEFormRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentCreateEForm.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = ret
                    };

                    CreateLog(method, "Success", EdocDocCreateEFormRq.RqHeader.AppId);

                    return StatusCode(201, res);
                }
                else
                {
                    var responseMsg = CheckData(method, EdocDocCreateEFormRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocCreateEFormRs
                {
                    RsHeader = new Models.EdocDocumentCreateEForm.RsHeader
                    {
                        AppId = EdocDocCreateEFormRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentCreateEForm.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, EdocDocCreateEFormRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("DocumentEdit")]
        public ActionResult<EdocDocEditRs> DocumentEdit(EdocDocEditRq edocDocEditRq)
        {
            string method = "DocumentEdit";
            try
            {
                var rawData = edocDocEditRq.RqDetail;
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
                        var responseMsg = CheckUser(method, edocDocEditRq.RqHeader.AppId);

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

                if (UpdateDoc(edocDocEditRq, userBid, username))
                {
                    CreateLog(method, msgLog, edocDocEditRq.RqHeader.AppId);

                    if (rawData.ActionCode == "1")
                    {
                        CloseWip(rawData.WID, rawData.Username, dataUser.Bid);
                    };

                    var res = new EdocDocEditRs
                    {
                        RsHeader = new Models.EdocDocumentEdit.RsHeader
                        {
                            AppId = edocDocEditRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentEdit.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        }
                    };

                    CreateLog(method, "Success", edocDocEditRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocEditRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocEditRs
                {
                    RsHeader = new Models.EdocDocumentEdit.RsHeader
                    {
                        AppId = edocDocEditRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentEdit.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocEditRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentUpload")]
        public ActionResult<EdocDocUploadRs> DocumentUpload(EdocDocUploadRq edocDocUploadRq)
        {
            string method = "DocumentUpload";
            try
            {
                var rawData = edocDocUploadRq.RqDetail;
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
                        var responseMsg = CheckUser(method, edocDocUploadRq.RqHeader.AppId);

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

                if (data != null)
                {
                    var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).FirstOrDefault();

                    if (dataWorkInproc == null)
                    {
                        var responseMsg = CheckData(method, edocDocUploadRq.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocUploadRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                //********************************//

                //********** check datalinkwid **********//
                if (!string.IsNullOrWhiteSpace(rawData.LinkWID))
                {
                    var datalinkwid = _context.Workinfos.Where(a => a.Wid == rawData.LinkWID).FirstOrDefault();

                    if (datalinkwid != null)
                    {
                        var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.LinkWID && a.Bid == userBid).FirstOrDefault();

                        if (dataWorkInproc == null)
                        {
                            var responseMsg = CheckData(method, edocDocUploadRq.RqHeader.AppId, "1");

                            return StatusCode(400, responseMsg);
                        }
                    }
                    else
                    {
                        var responseMsg = CheckData(method, edocDocUploadRq.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                }
                //********************************//

                if (!string.IsNullOrWhiteSpace(rawData.FileData))
                {
                    if (!Directory.Exists("c:\\WebAPILog"))
                    {
                        Directory.CreateDirectory("c:\\WebAPILog");
                    }
                    var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                    var filename = "c:\\WebAPILog\\Saraban_api_filebase64_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH")) + ".txt";
                    System.IO.File.AppendAllText(filename, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("th-TH")) + " " + "Request From appID =" + " " + "" + edocDocUploadRq.RqHeader.AppId + "" + " " + "" + remoteIpAddress + "" + " " + "Wid =" + " " + "" + rawData.WID + "" + " " + "Filename =" + " " + "" + rawData.FileName + "" + " " + "Filedata =" + " " + "" + rawData.FileData + "" + Environment.NewLine + Environment.NewLine);
                }

                if (CreateAttachment(edocDocUploadRq, userBid, username))
                {
                    var res = new EdocDocUploadRs
                    {
                        RsHeader = new Models.EdocDocumentUpload.RsHeader
                        {
                            AppId = edocDocUploadRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentUpload.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        },
                        RsDetail = null
                    };

                    CreateLog(method, "Success", edocDocUploadRq.RqHeader.AppId);

                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(400, "upload failed");
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocUploadRs
                {
                    RsHeader = new Models.EdocDocumentUpload.RsHeader
                    {
                        AppId = edocDocUploadRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentUpload.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocUploadRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentTracking")]
        public ActionResult<EdocDocTrackingRs> DocumentTracking([Required] string wid, [Required] string usrID, [Required] string appID)
        {
            string method = "DocumentTracking";
            try
            {
                string userBid = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == usrID && a.Mainbid == "0").FirstOrDefault();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, appID);

                    return StatusCode(400, responseMsg);
                }
                //*********************************//

                userBid = dataUser.Bid;

                //********** check data **********//
                var data = _context.Workinfos.Where(a => a.Wid == wid).FirstOrDefault();
                var workInproc = _context.Workinprocesses.Where(a => a.Wid == wid && (a.Bid == userBid || a.SenderBid == userBid)).FirstOrDefault();

                if (data == null)
                {
                    var responseMsg = CheckData(method, appID, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var workInprocess = _context.Workinprocesses.Where(a => a.Wid == wid).OrderBy(a => a.ReceiveDate).ThenBy(a => a.ReceiveTime).ToList();
                    var workList = new List<RsTrackingDetail>();

                    foreach (Workinprocess workinproc in workInprocess)
                    {
                        if (workInprocess != null)
                        {
                            workList.Add(new RsTrackingDetail
                            {
                                Wid = workinproc.Wid,
                                SenderBasketID = workinproc.SenderBid,
                                SenderBasketDsc = workinproc.SenderBdsc,
                                SenderUsername = workinproc.SenderUid,
                                SenderRegisterNo = ConvertRegNotoNumber(workinproc.SenderRegisterNo),
                                InitDate = workinproc.InitDate,
                                InitTime = workinproc.InitTime,
                                ReceiverBasketID = workinproc.Bid,
                                ReceiverBasketDsc = workinproc.Bdsc,
                                ReceiverUsername = workinproc.Usrid,
                                ReceiverRegisterNo = ConvertRegNotoNumber(workinproc.RegisterNo),
                                ReceiveDate = workinproc.ReceiveDate,
                                ReceiveTime = workinproc.ReceiveTime,
                                CompleteDate = workinproc.CompleteDate,
                                CompleteTime = workinproc.CompleteTime,
                                StatusCode = workinproc.StateCode,
                                ActionMessage = workinproc.ActionMsg
                            });
                        }
                    }

                    var ret = new Models.EdocDocumentTracking.RsDetail
                    {
                        RsTrackingDetails = workList
                    };
                    var res = new EdocDocTrackingRs
                    {
                        RsHeader = new Models.EdocDocumentTracking.RsHeader
                        {
                            AppId = appID,
                            Status = new Models.EdocDocumentTracking.RsHeaderStatus
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
                var res = new EdocDocTrackingRs
                {
                    RsHeader = new Models.EdocDocumentTracking.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.EdocDocumentTracking.RsHeaderStatus
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
        [Route("DocumentActionMessage")]
        public ActionResult<EdocDocActionMessageRs> DocumentActionMessage([Required] string wid, [Required] string usrID, [Required] string appID)
        {
            string method = "DocumentActionMessage";
            try
            {
                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == usrID && a.Mainbid == "0").FirstOrDefault();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, appID);

                    return StatusCode(400, responseMsg);
                }
                //*********************************//

                string userBid = dataUser.Bid;

                //********** check data **********//
                var data = _context.Workinfos.Where(a => a.Wid == wid).FirstOrDefault();
                var workInproc = _context.Workinprocesses.Where(a => a.Wid == wid && (a.Bid == userBid || a.SenderBid == userBid)).FirstOrDefault();

                if (data == null || workInproc == null)
                {
                    var responseMsg = CheckData(method, appID, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var actionMessage = _context.ActionMessages.Where(a => a.Wid == wid).OrderBy(a => a.Actiondate).ThenBy(a => a.Actiontime).ToList();
                    var actionMsgDetail = new List<RsActionDetail>();

                    foreach (ActionMessage actionMsg in actionMessage)
                    {
                        if (actionMessage != null)
                        {
                            actionMsgDetail.Add(new RsActionDetail
                            {
                                Wid = actionMsg.Wid,
                                BasketID = actionMsg.Bid,
                                Username = actionMsg.Usrid,
                                ActionDate = actionMsg.Actiondate,
                                ActionTime = actionMsg.Actiontime,
                                ActionMessage = actionMsg.Actionmsg,
                                PresentTo = actionMsg.Presentto,
                                Code = actionMsg.Commandcode,
                                SignImage = actionMsg.Imagefile
                            });
                        }
                    }

                    var ret = new Models.EdocDocumentActionMessage.RsDetail
                    {
                        RsActionDetail = actionMsgDetail
                    };
                    var res = new EdocDocActionMessageRs
                    {
                        RsHeader = new Models.EdocDocumentActionMessage.RsHeader
                        {
                            AppId = appID,
                            Status = new Models.EdocDocumentActionMessage.RsHeaderStatus
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
                var res = new EdocDocActionMessageRs
                {
                    RsHeader = new Models.EdocDocumentActionMessage.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.EdocDocumentActionMessage.RsHeaderStatus
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
        [HttpPost]
        [Route("DocumentSend")]
        public ActionResult<EdocDocSendRs> DocumentSend(EdocDocSendRq edocDocSendRq)
        {
            string method = "DocumentSend";
            try
            {
                var rawData = edocDocSendRq.RqDetail;
                string userBid = string.Empty;
                string username = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Bid == rawData.SenderBasketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, edocDocSendRq.RqHeader.AppId);

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
                //*********************************//

                //********** check data **********//
                var data = _context.Workinfos.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                if (data == null)
                {
                    var responseMsg = CheckData(method, edocDocSendRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                else
                {
                    var dataWorkinProc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.SenderBid == rawData.SenderBasketID).FirstOrDefault();

                    if (dataWorkinProc == null)
                    {
                        var responseMsg = CheckData(method, edocDocSendRq.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                    else
                    {
                        UpdateWorkinproc(rawData.WID, rawData.ReceiverBasketID, rawData.SenderBasketID, rawData.Username, username);

                        var res = new EdocDocSendRs
                        {
                            RsHeader = new Models.EdocDocumentSend.RsHeader
                            {
                                AppId = edocDocSendRq.RqHeader.AppId,
                                Status = new Models.EdocDocumentSend.RsHeaderStatus
                                {
                                    OrgStatusCode = "0",
                                    OrgStatusDesc = "Success",
                                    StatusCode = "0"
                                }
                            },
                        };

                        CreateLog(method, "Success", edocDocSendRq.RqHeader.AppId);

                        return StatusCode(200, res);
                    }
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocSendRs
                {
                    RsHeader = new Models.EdocDocumentSend.RsHeader
                    {
                        AppId = edocDocSendRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentSend.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocSendRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentClose")]
        public ActionResult<EdocDocCloseRs> DocumentClose(EdocDocCloseRq edocDocCloseRq)
        {
            string method = "DocumentClose";
            try
            {
                var rawData = edocDocCloseRq.RqDetail;
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
                        var responseMsg = CheckUser(method, edocDocCloseRq.RqHeader.AppId);

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
                if (data != null)
                {
                    var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).FirstOrDefault();
                    if (dataWorkInproc == null)
                    {
                        var responseMsg = CheckData(method, edocDocCloseRq.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocCloseRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                //********************************//

                if (CloseWip(rawData.WID, username, userBid))
                {
                    var res = new EdocDocCloseRs
                    {
                        RsHeader = new Models.EdocDocumentClose.RsHeader
                        {
                            AppId = edocDocCloseRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentClose.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        }
                    };

                    CreateLog(method, "Success", edocDocCloseRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
                else
                {
                    CreateLog(method, "Unsuccess", edocDocCloseRq.RqHeader.AppId);

                    return StatusCode(400, "Unsuccess");
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocCloseRs
                {
                    RsHeader = new Models.EdocDocumentClose.RsHeader
                    {
                        AppId = edocDocCloseRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentClose.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocCloseRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DocumentFollowup")]
        public ActionResult<EdocDocFollowupRs> DocumentFollowup(EdocDocFollowupRq edocDocFollowupRq)
        {
            string method = "DocumentFollowup";
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

                if (data != null)
                {
                    var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).FirstOrDefault();

                    if (dataWorkInproc == null)
                    {
                        var responseMsg = CheckData(method, edocDocFollowupRq.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                }
                else
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

        [Authorize]
        [HttpPost]
        [Route("DocumentAttachActionMessage")]
        public ActionResult<EdocDocAttachActionMsgRs> DocumentAttachActionMessage(EdocDocAttachActionMsgRq edocDocAttachActionMsg)
        {
            string method = "DocumentAttachActionMessage";
            try
            {
                var rawData = edocDocAttachActionMsg.RqDetail;
                string userBid = string.Empty;
                string username = string.Empty;
                string ImageBase64 = string.Empty;
                string useSignature = string.Empty;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Bid == rawData.BasketID).FirstOrDefault();

                if (dataUser == null)
                {
                    var userMainBid = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                    if (userMainBid == null)
                    {
                        var responseMsg = CheckUser(method, edocDocAttachActionMsg.RqHeader.AppId);

                        return StatusCode(400, responseMsg);
                    }
                    else
                    {
                        userBid = userMainBid.Bid;
                        username = userMainBid.Username;
                        useSignature = userMainBid.SecureSignature;
                    }
                }
                else
                {
                    userBid = dataUser.Bid;
                    username = dataUser.Username;
                    useSignature = dataUser.SecureSignature;
                }
                //********************************//

                //********** check data **********//
                var data = _context.Workinfos.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                if (data != null)
                {
                    var dataWorkInprocess = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).FirstOrDefault();

                    if (dataWorkInprocess == null)
                    {
                        var responseMsg = CheckData(method, edocDocAttachActionMsg.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocAttachActionMsg.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                //********************************//

                var getItemNo = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).Max(a => a.ItemNo);
                var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid && a.ItemNo == getItemNo).FirstOrDefault();

                var cspParameters = new CspParameters();
                cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
                RSACryptoServiceProvider.UseMachineKeyStore = true;

                var rsa = new RSACryptoServiceProvider(1024, cspParameters);
                var dataKeyStore = _context.Keystores.Where(a => a.Usrid == rawData.Username).FirstOrDefault();
                string signedHashFollowupText = string.Empty;

                if (dataKeyStore == null)
                {
                    dataKeyStore = CreateUserKeyStore(rawData.Username);
                }

                string currentDate = GetServerDate();
                string currentDateNormal = GetServDateNormalFormat();
                string currentTime = GetServerTime();

                string curBdsc = _context.Basketinfos.Where(a => a.Bid == userBid).Select(a => a.Bdsc).FirstOrDefault();
                string actionMsg = "วันที่ " + currentDateNormal + " " + currentTime + "  " + curBdsc + ": " + username + System.Environment.NewLine + "นำเสนอ/ผู้ปฏิบัติ : " + rawData.PresentTo + System.Environment.NewLine + "บันทึกงาน: " + rawData.ActionMessage + System.Environment.NewLine + "หมายเหตุ : " + rawData.Remark;

                if (!string.IsNullOrWhiteSpace(dataKeyStore.Privatekey))
                {
                    rsa.FromXmlString(dataKeyStore.Privatekey);
                    byte[] hashValueFollowup = HashTextToByte(actionMsg);
                    byte[] signedHashFollowup = RSASignHash(hashValueFollowup, rsa.ExportParameters(true), "SHA1");
                    signedHashFollowupText = Convert.ToBase64String(signedHashFollowup);
                }
                else
                {
                    signedHashFollowupText = "";
                }

                var dataSignature = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.SignaturePath != "" && a.SignaturePath != "-").FirstOrDefault();

                if (dataSignature != null)
                {
                    var lastIndex = dataSignature.SignaturePath.ToUpper().Substring(dataSignature.SignaturePath.ToUpper().LastIndexOf("FLOWDATA"));
                    string flowData = _configuration.GetSection("MySettings").GetSection("Flowdata").Value.ToUpper();
                    string signedPathImage = flowData + "\\" + lastIndex.Replace("/", "\\");

                    if (System.IO.File.Exists(signedPathImage))
                    {
                        string strExt = Path.GetExtension(signedPathImage).ToLower().Replace(".", "");
                        ImageBase64 = " data:image/" + strExt + ";base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(signedPathImage));
                    }
                }

                string strSQL = "insert into ActionMessage values('" + rawData.WID + "','" + userBid + "','" + rawData.Username.ToUpper() + "','" + currentDate + "','" + currentTime + "','" + actionMsg + "','" + rawData.PresentTo + "','01','" + signedHashFollowupText + "','" + ImageBase64 + "')";

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

                if (dataWorkInproc != null)
                {
                    var newMsg = dataWorkInproc.ActionMsg.Trim() + System.Environment.NewLine + actionMsg;
                    var remark = rawData.PresentTo.Trim();

                    if (dataWorkInproc.StateCode == "00" || dataWorkInproc.StateCode == "02")
                    {
                        dataWorkInproc.ActionMsg = newMsg;
                        dataWorkInproc.TakeActionname = remark;
                        dataWorkInproc.StateCode = "02";
                        dataWorkInproc.Viewstatus = "1";

                        _context.SaveChanges();
                    }
                    else
                    {
                        dataWorkInproc.ActionMsg = newMsg;
                        dataWorkInproc.TakeActionname = remark;
                        dataWorkInproc.Viewstatus = "1";

                        _context.SaveChanges();
                    }
                }

                var dataFollowup = _context.Followups.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                if (dataFollowup != null)
                {
                    var strReplace = System.Environment.NewLine + System.Environment.NewLine;
                    actionMsg = actionMsg.Replace(strReplace, System.Environment.NewLine);
                    var newMsg = dataFollowup.ActionMsg + System.Environment.NewLine + actionMsg + System.Environment.NewLine;

                    dataFollowup.ActionMsg = newMsg;

                    _context.SaveChanges();
                }

                var res = new EdocDocAttachActionMsgRs
                {
                    RsHeader = new Models.EdocDocumentAttachActionMessage.RsHeader
                    {
                        AppId = edocDocAttachActionMsg.RqHeader.AppId,
                        Status = new Models.EdocDocumentAttachActionMessage.RsHeaderStatus
                        {
                            OrgStatusCode = "0",
                            OrgStatusDesc = "Success",
                            StatusCode = "0"
                        }
                    }
                };

                CreateLog(method, "Success", edocDocAttachActionMsg.RqHeader.AppId);

                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var res = new EdocDocAttachActionMsgRs
                {
                    RsHeader = new Models.EdocDocumentAttachActionMessage.RsHeader
                    {
                        AppId = edocDocAttachActionMsg.RqHeader.AppId,
                        Status = new Models.EdocDocumentAttachActionMessage.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                    RsDetail = null
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocAttachActionMsg.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DocumentCancel")]
        public ActionResult<EdocDocCancelRs> DocumentCancel(EdocDocCancelRq edocDocCancelRq)
        {
            string method = "DocumentCancel";
            try
            {
                var rawData = edocDocCancelRq.RqDetail;
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
                        var responseMsg = CheckUser(method, edocDocCancelRq.RqHeader.AppId);

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
                if (data != null)
                {
                    var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).FirstOrDefault();
                    if (dataWorkInproc == null)
                    {
                        var responseMsg = CheckData(method, edocDocCancelRq.RqHeader.AppId, "1");

                        return StatusCode(400, responseMsg);
                    }
                }
                else
                {
                    var responseMsg = CheckData(method, edocDocCancelRq.RqHeader.AppId, "1");

                    return StatusCode(400, responseMsg);
                }
                //********************************//

                if (CancelWip(rawData.WID, username, userBid))
                {
                    var res = new EdocDocCloseRs
                    {
                        RsHeader = new Models.EdocDocumentClose.RsHeader
                        {
                            AppId = edocDocCancelRq.RqHeader.AppId,
                            Status = new Models.EdocDocumentClose.RsHeaderStatus
                            {
                                OrgStatusCode = "0",
                                OrgStatusDesc = "Success",
                                StatusCode = "0"
                            }
                        }
                    };

                    CreateLog(method, "Success", edocDocCancelRq.RqHeader.AppId);

                    return StatusCode(200, res);
                }
                else
                {
                    CreateLog(method, "Unsuccess", edocDocCancelRq.RqHeader.AppId);

                    return StatusCode(400, "Unsuccess");
                }
            }
            catch (Exception ex)
            {
                var res = new EdocDocCloseRs
                {
                    RsHeader = new Models.EdocDocumentClose.RsHeader
                    {
                        AppId = edocDocCancelRq.RqHeader.AppId,
                        Status = new Models.EdocDocumentClose.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                };

                CreateLog(method, "Error999 - " + ex.Message, edocDocCancelRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetBasketInfo")]
        public ActionResult<EdocDocGetBasketInfoRs> GetBasketInfo([Required] string usrID, [Required] string appID)
        {
            string method = "GetBasketInfo";
            try
            {
                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == usrID && a.Mainbid == "0").FirstOrDefault();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, appID);

                    return StatusCode(400, responseMsg);
                }
                //*********************************//

                //********** check data **********//
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

        //Doccir//

        [Authorize]
        [HttpGet]
        [Route("DoccirCreate")]
        public ActionResult<DoccirCreateRq> DoccirCreate(DoccirCreateRq doccirCreateRq)
        {
            string method = "DoccirCreate";
            try
            {
                var rawData = doccirCreateRq.RqDetail;

                //********** check userid **********//
                _context.Database.GetDbConnection().ConnectionString = GetConnectionString();
                var dataUser = _context.Userinfos.Where(a => a.Usrid == rawData.Username && a.Mainbid == "0").FirstOrDefault();

                if (dataUser == null)
                {
                    var responseMsg = CheckUser(method, doccirCreateRq.RqHeader.AppId);

                    return StatusCode(400, responseMsg);
                }
                //*********************************//
                string curBid = dataUser.Bid;
                var basketInfoData = _context.Basketinfos.Where(a => a.Bid == curBid).FirstOrDefault();
                string curBdsc = basketInfoData.Bdsc;
                string curDate = GetServerDate();
                curDate = curDate.Replace("/", "-");
                string strDocDate = rawData.DocDate;
                strDocDate = strDocDate.Replace("/", "-");

                string DoccirWid = GetFullIfmid("INT" + curDate.Substring(2, 2) + curDate.Split("-")[1] + curDate.Split("-")[2]);
                string doccirFolder = "00100000";
                string strSQL = "insert into doccir_detail values('" + DoccirWid + "','" + curDate + "','" + rawData.DocumentId + "','" + curBdsc + "','','" + strDocDate + "','',' ','','" + rawData.Subject + "','" + rawData.Description + "','1 ','" + curDate + "','','','','','1','0','0','0','" + curBid + "' ,'' )";
                string connectionString = _configuration.GetConnectionString("DoccirSqlConnect");
                SqlConnection Connection = new SqlConnection(connectionString);
                Connection.Open();
                SqlTransaction Transaction = null;
                SqlCommand Command = null;
                Command = Connection.CreateCommand();
                Command.Transaction = Transaction;
                Command.CommandText = strSQL;
                Command.ExecuteNonQuery();

                string fileTemp = SaveAttach(rawData.FileName, Path.GetExtension(rawData.FileName), rawData.FileData);

                var curGenID = DoccirWid;
                var tmpExtension = Path.GetExtension(rawData.FileName).Replace(".", "");
                strSQL = "exec IfmFile_sp_UsrDoc 'A','001','" + curGenID + "','" + curGenID + "','" + doccirFolder + "','-','0','" + tmpExtension + "','1','','" + curDate + "','" + (rawData.Username) + "','" + rawData.Subject.Replace("'", "''") + "','" + curBid + "','0' ";

                Command.CommandText = strSQL;
                Command.ExecuteNonQuery();

                strSQL = "update document set storage='-' where id='" + curGenID + "' ";
                Command.CommandText = strSQL;
                Command.ExecuteNonQuery();

                string tmpSQLStmt = "select w.pathimage, d.docuname from document as d, drawer as w where substring(d.fold_code,1,3) = w.id and d.id = '" + curGenID + "'";
                SqlDataAdapter da = new SqlDataAdapter(tmpSQLStmt, Connection);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.SelectCommand.Transaction = Transaction;
                DataSet ds = new DataSet();
                da.Fill(ds, "doccirdocu");
                var dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    var rsz = dt.Rows[0];
                    var dstPath = rsz[0] + "DATA" + rsz[1].ToString().Substring(1, 4);
                    var dstFile = dstPath + "\\" + rsz[1] + "." + tmpExtension;

                    if (!Directory.Exists(dstPath))
                    {
                        Directory.CreateDirectory(dstPath);
                    }
                    System.IO.File.Copy(fileTemp, dstFile);
                }

                Connection.Close();

                //success
                var viewAttachLink = _configuration.GetSection("MySettings").GetSection("ViewAttachLink").Value;
                var link = viewAttachLink + "?mode=2&ifmid=" + DoccirWid;

                //DoccirWid
                var res = new DoccirCreateRs
                {
                    RsHeader = new Models.DoccirCreation.RsHeader
                    {
                        AppId = doccirCreateRq.RqHeader.AppId,
                        Status = new Models.DoccirCreation.RsHeaderStatus
                        {
                            OrgStatusCode = "0",
                            OrgStatusDesc = "Success|" + link,
                            StatusCode = "0"
                        }
                    }
                };

                CreateLog(method, "Success", doccirCreateRq.RqHeader.AppId);

                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var res = new DoccirCreateRs
                {
                    RsHeader = new Models.DoccirCreation.RsHeader
                    {
                        AppId = doccirCreateRq.RqHeader.AppId,
                        Status = new Models.DoccirCreation.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                };

                CreateLog(method, "Error999 - " + ex.Message, doccirCreateRq.RqHeader.AppId);

                return StatusCode(400, res);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("DoccirFeed")]
        public ActionResult<DoccirDetailRs> DoccirFeed(string appID)
        {
            string method = "DoccirFeed";
            try
            {
                var lastDateItem = _context_doccir.DoccirDetails.Where(a => EF.Functions.Like(a.DoccirIfmid, "__T%") && (a.DoccirCategory == "1" && a.DoccirSecret == "0")).OrderByDescending(b => b.DoccirInput).FirstOrDefault();
                string lastDate = lastDateItem.DoccirInput;
                var strYear = int.Parse(lastDate.Substring(0, 4));
                var strMonth = int.Parse(lastDate.Substring(5, 2));
                var strDay = int.Parse(lastDate.Substring(8, 2));

                DateTime toDate = new DateTime(strYear - 543, strMonth, strDay);
                DateTime fromDate = toDate.AddDays(-15);

                string strToDate = toDate.ToString("yyyy-MM-dd");
                string strFromDate = fromDate.ToString("yyyy-MM-dd");
                strToDate = (int.Parse(strToDate.Substring(0, 4)) + 543).ToString() + "" + strToDate.Substring(4, strToDate.Length - 4);
                strFromDate = (int.Parse(strFromDate.Substring(0, 4)) + 543).ToString() + "" + strFromDate.Substring(4, strFromDate.Length - 4);

                var param1 = new SqlParameter("@fromdate", strFromDate);
                var param2 = new SqlParameter("@todate", strToDate);

                var doccirItem = _context_doccir.DoccirDetails.FromSqlRaw("select * from doccir_detail where doccir_ifmid like '__T%' and (doccir_category ='1' and doccir_secret='0') and doccir_input between @fromdate and @todate", param1, param2).OrderByDescending(b => b.DoccirInput).Select(c => new { c.DoccirSubject, c.DoccirDocid, c.DoccirOwner, c.DoccirIfmid, c.DoccirInput, c.DoccirDocdate }).ToList();
                var listDoccirDetail = new List<Models.DoccirDetail.DoccirDetail>();

                var ViewAttachLink = _configuration.GetSection("MySettings").GetSection("ViewAttachLink").Value;

                for (int i = 0; i <= doccirItem.Count() - 1; i++)
                {
                    listDoccirDetail.Add(new Models.DoccirDetail.DoccirDetail
                    {
                        Title = doccirItem[i].DoccirSubject,
                        Description = "เลขที่เอกสาร: " + doccirItem[i].DoccirDocid,
                        Author = doccirItem[i].DoccirOwner,
                        InputDate = doccirItem[i].DoccirInput.Replace("-", "/"),
                        PubDate = doccirItem[i].DoccirDocdate.Replace("-", "/"),
                        Link = ViewAttachLink + "?mode=2&ifmid=" + doccirItem[i].DoccirIfmid

                    });
                }

                var ret = new Models.DoccirDetail.RsDetail
                {
                    DoccirDetail = listDoccirDetail
                };
                var res = new DoccirDetailRs
                {
                    RsHeader = new Models.DoccirDetail.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.DoccirDetail.RsHeaderStatus
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
                var res = new DoccirDetailRs
                {
                    RsHeader = new Models.DoccirDetail.RsHeader
                    {
                        AppId = appID,
                        Status = new Models.DoccirDetail.RsHeaderStatus
                        {
                            OrgStatusCode = "Error999",
                            OrgStatusDesc = ex.Message,
                            StatusCode = "-1"
                        }
                    },
                };

                CreateLog(method, "Error999 - " + ex.Message, appID);

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
            var dataFollowup = _context.Followups.Where(a => a.Wid == rawData.WID).FirstOrDefault();
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
                if (fileTemp == "notfound")
                {
                    return false;
                }
                else
                {
                    strExt = Path.GetExtension(strSourcePath);
                    strHomeDir = dataBasketInfo.HomeDir;
                    string flowPath = strHomeDir.ToUpper().Replace("\\\\" + ipWebServer.ToUpper(), flowData);
                    strDestPath = flowPath + "\\" + newDocuname + strExt;
                    strHomeDir = strHomeDir + "\\" + newDocuname + strExt;

                    if (System.IO.File.Exists(strDestPath))
                    {
                        System.IO.File.Delete(strDestPath);
                    }
                    System.IO.File.Copy(strSourcePath, strDestPath);
                }
            }
            else
            {
                strHomeDir = dataBasketInfo.HomeDir;
                strHomeDir = strHomeDir + "\\" + newDocuname;
            }

            var dataDocAttm = _context.Docattaches.Where(a => a.Wid == rawData.WID && a.Bid == curBid).OrderByDescending(x => Convert.ToInt32(x.Itemno)).FirstOrDefault();

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
                Contextattach = rawData.FileName + strExt,
                Itemno = count.ToString(),
                Actionmsg = strNewActionMsg,
                Linkwid = rawData.LinkWID,
                Allowupdate = "Y"
            };
            _context.Docattaches.Add(addAttm);

            dataFollowup.Wserial = "-";
            dataFollowup.Wid = rawData.WID;
            dataFollowup.ActionMsg = dataFollowup.ActionMsg + "\r" + "\n" + "วันที่ " + RegisterDateNewFormat + "  " + RegisterTime + "  " + dataWorkInfo.RegisterBdsc + " : " + username + "\r" + "\n" + " เพิ่มเอกสารแนบ" + "\r" + "\n";

            _context.SaveChanges();

            if (!string.IsNullOrWhiteSpace(rawData.LinkWID))
            {
                CreateLinkWID(userBid, username, rawData.WID, rawData.LinkWID, RegisterDate, RegisterTime, dataWorkInfo.RegisterBdsc);
            }

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

                    if (System.IO.File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                    else
                    {
                        return "notfound";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Boolean CreateLinkWID(string userBid, string username, string wid, string linkWID, string RegisterDate, string RegisterTime, string RegisterBdsc)
        {
            string curBid = userBid;
            var dataBasketInfo = _context.Basketinfos.Where(a => a.Bid == curBid).FirstOrDefault();
            string strHomeDir = dataBasketInfo.HomeDir;
            string strDocuname = dataBasketInfo.DocuName;
            strHomeDir = strHomeDir + "\\" + strDocuname;
            var dataFollowup = _context.Followups.Where(a => a.Wid == wid).FirstOrDefault();
            int count = 0;
            string RegisterDateNewFormat = GetServDateNormalFormat();
            string strNewActionMsg = "วันที่ " + RegisterDateNewFormat + "  " + RegisterTime + "  " + RegisterBdsc + " : " + username + " (เพิ่ม)" + "\n" + "    วันที่ " + RegisterDateNewFormat + "  " + RegisterTime + "  " + RegisterBdsc + " : " + username + " (แก้ไข)" + "\n";

            var dataDocAttm = _context.Docattaches.Where(a => a.Wid == linkWID && a.Bid == curBid).OrderByDescending(x => Convert.ToInt32(x.Itemno)).FirstOrDefault();
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
                Wid = linkWID,
                Bid = curBid,
                Attachdate = RegisterDate,
                Attachtime = RegisterTime,
                Attachname = strHomeDir,
                Userattach = username + " : " + RegisterBdsc,
                Contextattach = "เชื่อมโยง เอกสารเลขที่ : ",
                Itemno = count.ToString(),
                Actionmsg = strNewActionMsg,
                Linkwid = wid,
                Allowupdate = "N"
            };
            _context.Docattaches.Add(addAttm);
            _context.SaveChanges();

            return true;
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
            string curDB = GetCurrentDB();
            string encryptDB = AES_EncryptDatabase(curDB, "inf0m@ECL62");
            string strURL = _configuration.GetSection("MySettings").GetSection("IwebflowSharename").Value + "/viewextx.aspx?d=" + WebUtility.UrlEncode(encryptDB) + "&fn=" + WebUtility.UrlEncode(fn) + "&ds=" + subject + "&ws=" + wid + "&usr=" + usr + "&wtm=true";

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
                    strSQL = strSQL + strUserDate + "','1','" + remark + "','" + ConvertRegNotoNumber(regNo) + "','";
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
                        if (rawData.WID == "")
                        {
                            wid = strPrefixIn + strRegNo + "/" + strDate.ToString();
                        }
                        else
                        {
                            wid = strPrefixIn + rawData.WID + strRegNo + "/" + strDate.ToString();
                        }
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
                        priority = "00";
                    }

                    string secLev = rqDetail.SecretLevel;

                    if (string.IsNullOrWhiteSpace(rqDetail.SecretLevel))
                    {
                        secLev = "0";
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
                    strSQL = strSQL + strUserDate + "','1','" + remark + "','" + ConvertRegNotoNumber(sendNo) + "','";
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
            string strPrefixOut = BasketInfo.Wfdsc;
            string curDir = BasketInfo.HomeDir;
            string curBdsc = BasketInfo.Bdsc;
            string curUsrname = rawData.Username.ToUpper();
            string curDeptcode = BasketInfo.Deptcode;
            string ownerWsubType = curBid;

            var ifmFlowDept = _context.IfmflowDepartments.Where(a => a.Deptcode == curDeptcode).FirstOrDefault();

            if (strPrefixOut == "-" || strPrefixOut == "")
            {
                if (rawData.WID != "")
                {
                    strPrefixIn = ifmFlowDept.Prefixdept + rawData.WID;
                }
                else
                {
                    strPrefixIn = ifmFlowDept.Prefixdept;
                }
            }
            else
            {
                if (rawData.WID != "")
                {
                    strPrefixIn = ifmFlowDept.Prefixdept + strPrefixOut + rawData.WID;
                }
                else
                {
                    strPrefixIn = ifmFlowDept.Prefixdept + strPrefixOut;
                }
            }

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
                    strSQL = strSQL + strUserDate + "','1','" + remark + "','" + ConvertRegNotoNumber(regNo) + "','";
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

        public string CreateEForm(EdocDocCreateEFormRq EdocDocCreateEFormRq, string userBid)
        {
            var rawData = EdocDocCreateEFormRq.RqDetail;
            var rqDetail = rawData;
            string regNo = "-";
            string ConstWsubtypeIndoc = "01";
            string typeDoc = "01";
            string strRegMode = "0";

            if (string.IsNullOrWhiteSpace(rawData.DocDate))
            {
                rawData.DocDate = DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("th-TH"));
            }

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
            string strDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

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

                    int curYear = DateTime.Now.Year + 543;
                    string itemNo = dr[2].ToString().Trim();
                    string docuname = dr[3].ToString().Trim();
                    string wid = string.Empty;

                    if (typeDoc == "01" && strPrefixIn == "ไม่อนุญาตออกเลข")
                    {
                        throw new Exception("ไม่อนุญาตให้ออกเลขที่หนังสือ");
                    }

                    if (typeDoc == "01" && strPrefixIn != "ไม่อนุญาตออกเลข")
                    {
                        wid = strPrefixIn + strRegNo + "/" + curYear.ToString();
                    }

                    string wtype = "00";
                    string strDateAddDoc = rawData.DocDate;
                    string strTimeAddDoc = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + ":" + DateTime.Now.Second.ToString("D2");
                    string refWid = "";
                    string strTmpFrom = curBdsc;
                    string strTmpTo = rawData.SendTo;
                    string strTmpSubject = rawData.Subject;
                    string detail = "";
                    string priority = "00";
                    string secLev = "0";
                    string userAction = "-";
                    string strUserDate = "-";
                    string remark = "";
                    string attach = "-";
                    string selectActionInfo = "00";
                    string strBookGroup = "00";
                    string strAgeWid = "-";
                    string strLocation = "-";
                    string strAutoDelete = "0";
                    string strInternalAction = "00";
                    string strSelectReceivedoc = "01";

                    strSQL = "ifmflow_sp_CreateWork '" + wtype + "','" + curDeptcode + "','" + curBid + "','";
                    strSQL = strSQL + curBdsc + "','" + curDir + "','" + curUsrname + "','" + sendNo + "','";
                    strSQL = strSQL + sendNo + "','" + itemNo + "','" + docuname + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + (strTimeAddDoc) + "','" + wid + "','" + (strDateAddDoc) + "','";
                    strSQL = strSQL + refWid + "','" + typeDoc + "','" + strTmpFrom + "','" + strTmpTo + "','";
                    strSQL = strSQL + strTmpSubject + "','" + detail + "','" + priority + "','" + secLev + "','" + userAction + "','";
                    strSQL = strSQL + strUserDate + "','1','" + remark + "','" + ConvertRegNotoNumber(sendNo) + "','";
                    strSQL = strSQL + attach + "','" + selectActionInfo + "','" + strBookGroup + "','" + strAgeWid + "','" + strLocation + "','" + strAutoDelete + "','" + strInternalAction + "','" + rqDetail.Username + "','" + strSelectReceivedoc + "'";

                    SqlCommand Command = null;
                    Command = Connection.CreateCommand();
                    Command.Transaction = Transaction;
                    Command.CommandText = strSQL;
                    Command.ExecuteNonQuery();
                    Connection.Close();

                    var data = _context.Workinfos.Where(a => a.Wid == wid).FirstOrDefault();
                    data.Docuname = data.Docuname + ".eform";

                    _context.SaveChanges();

                    var dataFormXml = _context.Eforms.Where(a => a.EformId == rawData.FormID).Select(a => a.Xml).FirstOrDefault();

                    var newData = UpdateData(rawData.EformData, strPrefixIn, wid, strDate, strTmpFrom, strTmpTo, strTmpSubject);

                    var eformData = new EformData
                    {
                        Wid = wid,
                        EformId = rawData.FormID,
                        EdData = newData,
                        EformXml = dataFormXml,
                        Pdffile = ""
                    };

                    _context.EformDatas.Add(eformData);

                    _context.SaveChanges();

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

        public string UpdateData(object eformData, string strPrefixIn, string wid, string date, string from, string sendTo, string subject)
        {
            string json = eformData.ToString();
            JArray jArray = JArray.Parse(json);
            var jObjects = jArray.ToObject<List<JObject>>();

            foreach (var obj in jObjects)
            {
                foreach (var prop in obj.Properties())
                {
                    if (prop.Name == "wid")
                        obj["wid"] = wid;
                }
            }

            jObjects[0]["value"] = strPrefixIn;
            jObjects[1]["value"] = date;
            jObjects[2]["value"] = from;
            jObjects[3]["value"] = sendTo;
            jObjects[4]["value"] = subject;

            JArray result = JArray.FromObject(jObjects);

            return result.ToString(Formatting.None);
        }

        public Boolean UpdateDoc(EdocDocEditRq edocDocEditRq, string userBid, string username)
        {
            msgLog = string.Empty;
            var rawData = edocDocEditRq.RqDetail;
            string refNumber, from, sendto, subject, priority, secretLevel, wdate, description;
            var dataWorkInfo = _context.Workinfos.Where(a => a.Wid == rawData.WID && a.RegisterBid == userBid).FirstOrDefault();
            var getItemNo = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).Max(a => a.ItemNo);
            var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid && a.ItemNo == getItemNo).FirstOrDefault();
            var index = new List<string>();
            if (dataWorkInfo != null)
            {
                refNumber = dataWorkInfo.Refwid;
                from = dataWorkInfo.Worigin;
                sendto = dataWorkInfo.WownerBdsc;
                subject = dataWorkInfo.Wsubject;
                wdate = dataWorkInfo.Wdate;
                priority = dataWorkInproc.PriorityCode;
                secretLevel = dataWorkInproc.SecretLevCode;
                description = dataWorkInfo.Dsc;

                if (rawData.RefNumber.Trim() != "" && (refNumber != rawData.RefNumber.Trim()))
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขเลขที่หนังสืออ้างอิง จาก " + refNumber + " เป็น " + rawData.RefNumber;
                    string strRefNumber = "อ้างถึง:จาก " + refNumber + " เป็น " + rawData.RefNumber + "";
                    index.Add(strRefNumber);
                    refNumber = rawData.RefNumber;
                }

                if (rawData.From.Trim() != "" && (from != rawData.From.Trim()))
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขส่งจาก จาก " + from + " เป็น " + rawData.From;
                    string strFrom = "จาก:จาก " + from + " เป็น " + rawData.From + "";
                    index.Add(strFrom);
                    from = rawData.From;
                }

                if (rawData.SendTo.Trim() != "" && (sendto != rawData.SendTo.Trim()))
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขส่งถึง จาก " + sendto + " เป็น " + rawData.SendTo;
                    string strSendTo = "ถึง:จาก " + sendto + " เป็น " + rawData.SendTo + "";
                    index.Add(strSendTo);
                    sendto = rawData.SendTo;
                }

                if (rawData.Subject.Trim() != "" && (subject != rawData.Subject.Trim()))
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขเรื่อง จาก " + subject + " เป็น " + rawData.Subject;
                    string strSubject = "เรื่อง:จาก " + subject + " เป็น " + rawData.Subject + "";
                    index.Add(strSubject);
                    subject = rawData.Subject;
                }

                if (rawData.DocDate.Trim() != "" && (wdate != rawData.DocDate.Trim().Replace("-", "/")))
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขวันที่เอกสาร จาก " + wdate + " เป็น " + rawData.DocDate.Replace("-", "/");
                    string oldDate = wdate;
                    string[] convertOldDate = oldDate.Split("/");
                    oldDate = convertOldDate[2] + "/" + convertOldDate[1] + "/" + convertOldDate[0];
                    wdate = rawData.DocDate;
                    wdate = wdate.Replace("/", "-");

                    if (int.Parse(wdate.Split("-")[0]) <= 2500)
                    {
                        wdate = wdate.Replace(wdate.Split("-")[0], (Int32.Parse(wdate.Split("-")[0]) + 543).ToString());
                    }

                    wdate = wdate.Replace("-", "/");
                    string[] convertWdate = wdate.Split("/");
                    wdate = convertWdate[2] + "/" + convertWdate[1] + "/" + convertWdate[0];
                    string strDate = "ลงวันที่:จาก " + oldDate + " เป็น " + wdate + "";
                    index.Add(strDate);
                }

                if (rawData.Priority.Trim() != "" && (priority != rawData.Priority.Trim()))
                {
                    var priorityText = new Dictionary<string, string>()
                    {
                        { "00", "ปกติ"},
                        { "01", "ด่วน"},
                        { "02", "ด่วนมาก"},
                        { "03", "ด่วนที่สุด"}
                    };

                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขชั้นความเร็ว จาก " + priorityText["" + priority + ""] + " เป็น " + priorityText["" + rawData.Priority + ""];
                    string strPriority = "ชั้นความเร็ว :จาก " + priorityText["" + priority + ""] + " เป็น " + priorityText["" + rawData.Priority + ""] + "";
                    index.Add(strPriority);
                    priority = rawData.Priority;
                }

                if (rawData.SecretLevel.Trim() != "" && (secretLevel != rawData.SecretLevel.Trim()))
                {
                    var secretText = new Dictionary<string, string>()
                    {
                        { "0", "ปกติ"},
                        { "2", "ลับ"},
                        { "3", "ลับมาก"},
                        { "4", "ลับที่สุด"}
                    };

                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขชั้นความลับ จาก " + secretText["" + secretLevel + ""] + " เป็น " + secretText["" + rawData.SecretLevel + ""];
                    string strSecretLevel = "ชั้นความลับ :จาก " + secretText["" + secretLevel + ""] + " เป็น " + secretText["" + rawData.SecretLevel + ""] + "";
                    index.Add(strSecretLevel);
                    secretLevel = rawData.SecretLevel;
                }

                if (rawData.Description.Trim() != "" && (description != rawData.Description.Trim()))
                {
                    msgLog = msgLog + System.Environment.NewLine + "แก้ไขรายละเอียด จาก " + description + " เป็น " + rawData.Description;
                    description = rawData.Description;
                }

                if (msgLog != string.Empty)
                {
                    msgLog = "แก้ไขข้อมูลโดย " + edocDocEditRq.RqDetail.Username + msgLog;
                }

                dataWorkInfo.Refwid = refNumber;
                dataWorkInfo.Worigin = from;
                dataWorkInfo.WownerBdsc = sendto;
                dataWorkInfo.Wsubject = subject;
                dataWorkInfo.Wdate = rawData.DocDate.Replace("-", "/");
                dataWorkInfo.Wid = rawData.WID;
                dataWorkInfo.Dsc = description;

                //var getItemNo = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid).Max(a => a.ItemNo);
                //var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == rawData.WID && a.Bid == userBid && a.ItemNo == getItemNo).FirstOrDefault();

                dataWorkInproc.PriorityCode = priority;
                dataWorkInproc.SecretLevCode = secretLevel;

                string currentDate = GetServDateNormalFormat();
                string currentTime = GetServerTime();
                string curBdsc = dataWorkInproc.Bdsc;
                string actionMsg = "วันที่ " + currentDate + " " + currentTime + "  " + curBdsc + ": " + username + System.Environment.NewLine + "แก้ไขข้อมูลเอกสาร เลขที่เอกสาร :" + rawData.WID + System.Environment.NewLine;
                string readMsg = string.Empty;

                foreach (var item in index)
                {
                    readMsg = readMsg + item + System.Environment.NewLine;
                }

                var dataFollowup = _context.Followups.Where(a => a.Wid == rawData.WID).FirstOrDefault();

                dataFollowup.ActionMsg = dataFollowup.ActionMsg + System.Environment.NewLine + actionMsg + readMsg + System.Environment.NewLine;

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
                var strDate = GetServDateNormalFormat();
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

        public Boolean CancelWip(string wid, string usrID, string userBid)
        {
            var dataWorkInproc = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == userBid && a.RegisterNo != "-").FirstOrDefault();
            var strSQL = "ifmflow_sp_CancelWIP '" + wid + "', '" + dataWorkInproc.RegisterNo + "', '" + dataWorkInproc.ItemNo + "', '" + userBid + "'";
            string connectionString = SetSQLConnectionString();
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlTransaction Transaction = null;
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.SelectCommand.Transaction = Transaction;
            DataSet ds = new DataSet();
            da.Fill(ds, "dscancelwip");
            var dt = ds.Tables[0];
            
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                string strCase = dr[0].ToString();
                switch (strCase)
                {
                    case "เอกสารได้ถูกยกเลิกไปแล้ว":
                        throw new Exception(strCase);
                    case "รายการนี้อยู่ระหว่างการตรวจสอบ ไม่สามารถยกเลิกได้":
                        throw new Exception(strCase);
                    case "ท่านไม่ใช่เจ้าของ ไม่สามารถยกเลิกเอกสารได้":
                        throw new Exception(strCase);
                    case "ดำเนินการยกเลิกเอกสารเรียบร้อยแล้ว":
                        UpdateFollowupMsg(dataWorkInproc.Bdsc, usrID, dataWorkInproc.RegisterNo, wid, "ยกเลิก");
                        return true;
                    default:
                        throw new Exception(strCase);
                }
            }
            else
            {
                throw new Exception("ไม่สามารถยกเลิกได้");
            }
        }

        public string UpdateWorkinproc(string wid, string receiverBid, string senderBid, string usrID, string username)
        {
            try
            {
                var basketSender = _context.Basketinfos.Where(a => a.Bid == senderBid).FirstOrDefault();
                var basketReceive = _context.Basketinfos.Where(a => a.Bid == receiverBid).FirstOrDefault();
                var workinProc = _context.Workinprocesses.Where(a => a.Wid == wid && a.Bid == senderBid && a.SenderBid == senderBid).FirstOrDefault();

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

                string currentRegNo = ConvertRegNotoNumber(basketReceive.RegisterNo);
                currentRegNo = (int.Parse(currentRegNo) + 1).ToString();

                UpdateRegNoBasketinfo(currentRegNo, receiverBid);

                return "Success";
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
            string newDocuname = _configuration.GetSection("MySettings").GetSection("Flowdata").Value + "\\" + pathArray[3] + "\\" + pathArray[4] + "\\" + pathArray[5] + "\\" + pathArray[6];
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

        public static string AES_EncryptDatabase(string curdb, string password)
        {
            string result = string.Empty;

            using (Aes AES = Aes.Create())
            {
                using (var md5 = MD5.Create())
                {
                    byte[] hash = new byte[32];
                    byte[] temp = md5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password));
                    Array.Copy(temp, 0, hash, 0, 16);
                    Array.Copy(temp, 0, hash, 15, 16);
                    AES.Key = hash;
                    AES.Mode = CipherMode.ECB;
                    System.Security.Cryptography.ICryptoTransform encrypter = AES.CreateEncryptor();
                    byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(curdb);
                    result = Convert.ToBase64String(encrypter.TransformFinalBlock(buffer, 0, buffer.Length));
                }

            }

            return result;
        }

        public static byte[] HashTextToByte(string Message)
        {
            byte[] Data = Encoding.ASCII.GetBytes(Message);
            var sha = System.Security.Cryptography.SHA1.Create();
            byte[] encryptData = sha.ComputeHash(Data);

            return encryptData;
        }

        public static byte[] RSASignHash(byte[] HashToSign, RSAParameters RSAKeyInfo, string HashAlg)
        {
            try
            {
                var rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(RSAKeyInfo);
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm(HashAlg);

                return rsaFormatter.CreateSignature(HashToSign);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Keystore CreateUserKeyStore(string usrID)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    string privateKey = rsa.ToXmlString(true);
                    string publicKey = rsa.ToXmlString(false);
                    string strDate = GetServerDate();
                    string strExpireDate = DateTime.Now.AddYears(1).ToString("yyyy/MM/dd", new CultureInfo("th-TH"));
                    string strSQL = "insert into KEYSTORE values('" + usrID.ToUpper() + "','" + privateKey + "','" + publicKey + "','" + strDate + "','" + strDate + "','" + strExpireDate + "',NULL,NULL,NULL,NULL,NULL)";

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

                    var result = _context.Keystores.Where(a => a.Usrid == usrID).FirstOrDefault();

                    return result;
                }
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

        #endregion
    }
}
