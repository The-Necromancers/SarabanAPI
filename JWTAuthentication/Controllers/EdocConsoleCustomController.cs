using JWTAuthentication.Models.ConsoleCreateUser;
using JWTAuthentication.Models.DB_User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdocConsoleCustomController : ControllerBase
    {
        private readonly RwUserContext _context;
        private readonly IConfiguration _configuration;

        public EdocConsoleCustomController(RwUserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Console Custom Version//

        #region

        // RedCross //

        //[Authorize]
        [HttpPost]
        [Route("CreateUserforRedcross")]
        public ActionResult<ConsoleCreateUserRs> CreateUserforRedcross(ConsoleCreateUserRq consoleCreateUserRq)
        {
            // Change Authen Type to Basic //

            string authorize = Request.Headers["Authorization"];
            string username = _configuration.GetSection("BasicAuthen").GetSection("Username").Value;
            string password = _configuration.GetSection("BasicAuthen").GetSection("Password").Value;

            if (authorize != null)
            {
                string item = authorize.Replace("Basic ", "");
                byte[] data = Convert.FromBase64String(item);
                string decode = Encoding.UTF8.GetString(data);
                string[] arr = decode.Split(':');
                string user = arr[0];
                string pwd = arr[1];

                if (user == username && pwd == password)
                {
                    
                }
                else
                {
                    var Res = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                    {
                        requestId = consoleCreateUserRq.requestId,
                        responseCode = "1002",
                        responseMsg = "Unauthorized application"
                    };

                    return StatusCode(401, Res);
                }
            }
            else
            {
                var Res = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                {
                    requestId = consoleCreateUserRq.requestId,
                    responseCode = "1002",
                    responseMsg = "Unauthorized application"
                };

                return StatusCode(401, Res);
            }

            // Change Authen Type to Basic //

            string method = "CreateUserforRedcross";

            try
            {
                var rawData = consoleCreateUserRq;
                string appId = "1";
                string mode = rawData.reqType;
                string idcard = rawData.empProfile.IdentityCard;
                string fname = rawData.empProfile.FirstName;
                string lname = rawData.empProfile.LastName;
                string fnameEng = rawData.empProfile.FirstNameEng;
                string lnameEng = rawData.empProfile.LastNameEng;
                string email = rawData.empProfile.email;
                string empcode = rawData.empProfile.EmpCode;

                switch (mode)
                {
                    case "Add User":

                        bool resultCreate = CreateUserToConsole(idcard, fname, lname, fnameEng, lnameEng, email, empcode);

                        if (resultCreate == true)
                        {
                            var addRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                            {
                                requestId = rawData.requestId,
                                responseCode = "0000",
                                responseMsg = "Success"
                            };

                            CreateLog(method, "Success", appId);

                            return StatusCode(200, addRes);
                        }
                        else
                        {
                            var addRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                            {
                                requestId = rawData.requestId,
                                responseCode = "1001",
                                responseMsg = "Invalid data"
                            };

                            CreateLog(method, "Invalid data", appId);

                            return StatusCode(400, addRes);
                        }

                    case "Edit User":

                        var resultUpdate = UpdateUserToConsole(idcard, fname, lname, fnameEng, lnameEng, email, empcode);

                        if (resultUpdate.Item1 == true)
                        {
                            var editRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                            {
                                requestId = rawData.requestId,
                                responseCode = "0000",
                                responseMsg = "Success"
                            };

                            CreateLog(method, "Success + " + resultUpdate.Item2 + "", appId);

                            return StatusCode(200, editRes);
                        }
                        else
                        {
                            var editRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                            {
                                requestId = rawData.requestId,
                                responseCode = "1001",
                                responseMsg = "Invalid data"
                            };

                            CreateLog(method, "Invalid data", appId);

                            return StatusCode(400, editRes);
                        }

                    case "Delete User":

                        bool resultDelete = DeleteUserfromConsole(idcard);

                        if (resultDelete == true)
                        {
                            var deleteRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                            {
                                requestId = rawData.requestId,
                                responseCode = "0000",
                                responseMsg = "Success"
                            };

                            CreateLog(method, "Success", appId);

                            return StatusCode(200, deleteRes);
                        }
                        else
                        {
                            var deleteRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                            {
                                requestId = rawData.requestId,
                                responseCode = "1001",
                                responseMsg = "Invalid data"
                            };

                            CreateLog(method, "Invalid data", appId);

                            return StatusCode(400, deleteRes);
                        }

                    default:
                        var res = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                        {
                            requestId = rawData.requestId,
                            responseCode = "1001",
                            responseMsg = "Invalid data"
                        };

                        CreateLog(method, "Invalid data", appId);

                        return StatusCode(400, res);
                }
            }
            catch (Exception ex)
            {
                var appId = "1";
                var res = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                {
                    requestId = appId,
                    responseCode = "9999",
                    responseMsg = ex.Message
                };

                CreateLog(method, "9999" + ex.Message, appId);

                return StatusCode(500, res);
            }
        }

        #endregion

        //Method for API//

        #region

        public string AesEncryptToHex(string plaintext, string secretkey, string vector)
        {
            string result = "";

            using (MemoryStream ms = new MemoryStream())
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] data = Encoding.UTF8.GetBytes(plaintext);
                    byte[] key = Encoding.UTF8.GetBytes(secretkey);
                    byte[] iv = Encoding.UTF8.GetBytes(vector);
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }

                    var tmpresult = BitConverter.ToString(ms.ToArray());
                    result = tmpresult.Replace("-", "");
                }
            }

            return result;
        }

        public string AesDecryptfromHex(string plaintext, string secretkey, string vector)
        {
            string result = "";

            using (MemoryStream ms = new MemoryStream())
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] data = Convert.FromHexString(plaintext);
                    byte[] key = Encoding.UTF8.GetBytes(secretkey);
                    byte[] iv = Encoding.UTF8.GetBytes(vector);
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }

                    result = Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            return result;
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
                var filename = "c:\\WebAPILog\\Console_api_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH")) + ".txt";
                System.IO.File.AppendAllText(filename, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("th-TH")) + " " + method + " " + "Request From appID =" + " " + "" + appID + "" + " " + "" + remoteIpAddress + "" + " " + "" + errorMsg + "" + Environment.NewLine);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CreateUserToConsole(string idcard, string fname, string lname, string fnameEng, string lnameEng, string email, string empcode)
        {
            var dataUser = _context.UserDetails.Where(a => a.Id != "99999").Max(a => a.Id);
            int newid = int.Parse(dataUser) + 1;
            string checkData = CheckUsername(fname, lname, fnameEng, lnameEng);

            if (checkData == "already")
            {
                return false;
            }
            else
            {
                string strSQL = "insert into user_detail values('" + newid.ToString("D5") + "','" + checkData.ToUpper() + "','KSJK','','" + fname + "','" + lname + "','" + fnameEng.ToUpper() + "','";
                strSQL = strSQL + lnameEng.ToUpper() + "','','','','','','','8888888888888','','" + empcode + "','','','" + idcard + "','','','" + email + "','','','','','','','')";

                string connectionString = _configuration.GetConnectionString("UserDatabase");
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

                return true;
            }
        }

        public (bool, string) UpdateUserToConsole(string idcard, string fname, string lname, string fnameEng, string lnameEng, string email, string empcode)
        {
            var data = _context.UserDetails.Where(a => a.UTel1 == idcard).FirstOrDefault();
            string log = "";

            if (data != null)
            {
                if (fname.Contains("แก้ไข"))
                {
                    fname = fname.Replace("แก้ไข", "");
                    fname = fname.Trim();
                    log = "แก้ไขชื่อ : จาก " + data.Tname + " เป็น " + fname + " ";
                }

                if (lname.Contains("แก้ไข"))
                {
                    lname = lname.Replace("แก้ไข", "");
                    lname = lname.Trim();
                    log = log + "แก้ไขสกุล : จาก " + data.Tsurname + " เป็น " + lname + " ";
                }

                if (fnameEng.Contains("แก้ไข"))
                {
                    fnameEng = fnameEng.Replace("แก้ไข", "");
                    fnameEng = fnameEng.Trim();
                    log = log + "แก้ไขชื่ออังกฤษ : จาก " + data.Ename + " เป็น " + fnameEng + " ";
                }

                if (lnameEng.Contains("แก้ไข"))
                {
                    lnameEng = lnameEng.Replace("แก้ไข", "");
                    lnameEng = lnameEng.Trim();
                    log = log + "แก้ไขสกุลอังกฤษ : จาก " + data.Esurname + " เป็น " + lnameEng + " ";
                }

                if (email.Contains("แก้ไข"))
                {
                    email = email.Replace("แก้ไข", "");
                    email = email.Trim();
                    log = log + "แก้ไข email : จาก " + data.UEmail + " เป็น " + email + " ";
                }

                if (empcode.Contains("แก้ไข"))
                {
                    empcode = empcode.Replace("แก้ไข", "");
                    empcode = empcode.Trim();
                    log = log + "แก้ไขรหัสพนักงาน : จาก " + data.UId1 + " เป็น " + empcode + " ";
                }

                string strSQL = "update user_detail set tname = '" + fname + "', tsurname = '" + lname + "', ename = '" + fnameEng + "', esurname = '" + lnameEng + "', u_email = '";
                strSQL = strSQL + email + "', u_id1 = '" + empcode + "', u_address = '" + log + "' where u_tel1 = '" + idcard + "'";

                string connectionString = _configuration.GetConnectionString("UserDatabase");
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

                return (true, log);
            }
            else
            {
                return (false, "");
            }
        }

        public bool DeleteUserfromConsole(string idcard)
        {
            var data = _context.UserDetails.Where(a => a.UTel1 == idcard).FirstOrDefault();
            var status = _context.UstatusDetails.Where(a => a.Id == "000").FirstOrDefault();

            if (data != null && status != null)
            {
                string strSQL = "update user_detail set u_status = '" + status.Id + "' where u_tel1 = '" + idcard + "'";

                string connectionString = _configuration.GetConnectionString("UserDatabase");
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

                return true;
            }
            else
            {
                return false;
            }
        }

        public string CheckUsername(string fname, string lname, string fnameEng, string lnameEng)
        {
            string result = "";
            string tmpusrId = fnameEng + "." + lnameEng[0];
            var data = _context.UserDetails.Where(a => a.Lname == tmpusrId).FirstOrDefault();

            if (data != null)
            {
                var firstname = _context.UserDetails.Where(a => a.Tname == fname).FirstOrDefault();

                if (firstname != null)
                {
                    var lastname = _context.UserDetails.Where(a => a.Tsurname == lname).FirstOrDefault();

                    if (lastname != null)
                    {
                        result = "already";
                    }
                    else
                    {
                        string usrid2digit = fnameEng + "." + lnameEng.Substring(0, 2);
                        var dataUser = _context.UserDetails.Where(a => a.Lname == usrid2digit).FirstOrDefault();

                        if (dataUser != null)
                        {
                            string usrid3digit = fnameEng + "." + lnameEng.Substring(0, 3);
                            result = usrid3digit;
                        }
                        else
                        {
                            result = usrid2digit;
                        }
                    }
                }
                else
                {
                    string usrid2digit = fnameEng + "." + lnameEng.Substring(0, 2);
                    var dataUser = _context.UserDetails.Where(a => a.Lname == usrid2digit).FirstOrDefault();

                    if (dataUser != null)
                    {
                        string usrid3digit = fnameEng + "." + lnameEng.Substring(0, 3);
                        result = usrid3digit;
                    }
                    else
                    {
                        result = usrid2digit;
                    }
                }
            }
            else
            {
                result = tmpusrId;
            }
            
            return result;
        }

        public string SetSQLConnectionString()
        {
            try
            {
                var connStr = _configuration.GetConnectionString("UserDatabase");
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
                string result = "Server=" + ip + "Database=" + dbName + "user id=" + uid + "Password=" + pwd + "ConnectRetryCount=0";

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
