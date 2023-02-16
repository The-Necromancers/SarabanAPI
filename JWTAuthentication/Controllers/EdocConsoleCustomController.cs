using JWTAuthentication.Models.ConsoleCreateUser;
using JWTAuthentication.Models.DB_User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdocConsoleCustomController : ControllerBase
    {
        private readonly RwUserContext _context;
        private readonly IConfiguration _configuration;
        private string msgLog;

        public EdocConsoleCustomController(RwUserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Console Custom Version//

        #region

        // RedCross //

        [Authorize]
        [HttpPost]
        [Route("CreateUserforRedcross")]
        public ActionResult<ConsoleCreateUserRs> CreateUserforRedcross(ConsoleCreateUserRq consoleCreateUserRq)
        {
            string method = "CreateUserforRedcross";
            string secretkey = "nI1FVluWjbglxMPXk5ZIFETIXmqGx2KH";
            string vector = "h4BQ9Bi6DKJ24lMs";

            try
            {
                var rawData = consoleCreateUserRq;
                string appId = "1";
                string mode = rawData.reqType;
                string item = AesDecryptfromHex(rawData.requestParam, secretkey, vector);

                switch (mode)
                {
                    case "Add User":

                        CreateUserToConsole(item);

                        var addRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                        {
                            requestId = rawData.requestId,
                            responseCode = "0000",
                            responseMsg = "Success"
                        };

                        CreateLog(method, "Success", appId);

                        return StatusCode(200, addRes);

                    case "Edit User":
                        var editRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                        {
                            requestId = rawData.requestId,
                            responseCode = "0000",
                            responseMsg = "Success"
                        };

                        CreateLog(method, "Success", appId);

                        return StatusCode(200, editRes);

                    case "Delete User":
                        var deleteRes = new Models.ConsoleCreateUser.ConsoleCreateUserRs
                        {
                            requestId = rawData.requestId,
                            responseCode = "0000",
                            responseMsg = "Success"
                        };

                        CreateLog(method, "Success", appId);

                        return StatusCode(200, deleteRes);

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

        public bool CreateUserToConsole(string data)
        {
            var dataUser = _context.UserDetails.Where(a => a.Id != "99999").Max(a => a.Id);
            int newid = int.Parse(dataUser) + 1;
            string[] arr = data.Split("|");
            string strSQL = "insert into user_detail values('" + newid.ToString("D5") + "','" + arr[0].ToUpper() + "','KSJK','','" + arr[0] + "','" + arr[0] + "','" + arr[0].ToUpper() + "','";
            strSQL = strSQL + arr[0].ToUpper() + "','','','','','','','8888888888888','','','','','','','','','','','','','','','')";

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
