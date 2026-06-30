using BloodGroupApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml;
using System.Data; //temp
using System.Web;
using System.Security.Cryptography;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

using static BloodGroupApi.Models.Models;
using BloodGroupApi.Filter;



namespace BloodGroupApi.Controllers
{

    [JwtAuthentication]
    public class BGController : ApiController
    {
        Database DB = new Database();
        BusinessLogic BL = new BusinessLogic();
        Exceptions exp = new Exceptions();
        JwtToken jL = new JwtToken();
        Mail mail = new Mail();
        //PwdEncryptDecrypt Encryption = new PwdEncryptDecrypt();





        #region Service Account keys for getting accessToken to send the pushNotifications--Venu,suraj
        private const string JsonKeyString = @"
   {
  ""type"": ""service_account"",
  ""project_id"": ""bloodgroup-94ed5"",
  ""private_key_id"": ""c6401a1e84090f01cfb9d6bd4d8c07d15a587170"",
  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCIIycKOjMKy/oU\nKY9Gm7ugp79G4oM4mR4qxZ3HGw/TV3KdU6ksccV27WIHoufxYZqTwMdKnzgRhXLf\naHlNxZTJmR+6zgGFCbDt1gvfV1iMl8L5ANErh2uQ6gySAsrXi/NcjqHmISlzIjcN\nHzQv/CPNec0sKWTviLtCieo52jZ/oKlZu/7L5m2+SPmhmliiiXKYk6e6sGiD7Oms\n8q64jFPkpU0HvggJNag+VCYtr13uhkJaUkwPudNsL75wG9h9S+G0XDn9LLkvnh8Z\nqpRAjkz3FaLR1ySO9ic0KTq+SHTi4YtDhWT+zjs9rWbZPq4BXxd32EMu4tbkw2PV\n9x1HrdS5AgMBAAECggEAIITbGr2PuzzBuia6R4jEPypKzc0mQLMMqnVljK3JcDej\nlHKrRsdPfqrSH5ZZlsZ0lOY4BLp3O3I34CdV0I7JjYVy952lbcjEl29pixpTd9P5\nmh7ImEADIQzTX15k4yuWQI3+nNjfjJXH9i21ut+dEsRNTwb9Sh2Mc6a7iUG3gnE+\nSrrNDuU2I1l/Tn7R5uQxhyW+zMAZ1uqIcDPHLis79DpK5ZmUzyBtlvqEqQq0ClKT\n8zYJEDyDW7CYMQC6ZQqkQ0V9aCZGUcZwwAd1Qnp1fkIUdHvNpV+3O6cg7eu/6mlC\nVgeDTdqfnsgVPHDmX7MLWA0EQav+pZfTM9QtId3mHQKBgQC6wuicMNgEY/TA98Kw\nTEuhE4bzYRtbJMFEuEgshckgHMxWikbiC5qlrr5WtBlU2ggf/B1fMZTwTdVa1uHh\n+28wQCC5rnhK5Uoq58UkzkBoIRoEOdyK8BOJWEW8w1Up/ljJE6LYLWKTHzZbMl9e\nJ+O4ivZnXmBjIt2QZR/18MZyTQKBgQC6m6HHNI5kgNNUYKlJX1HhcRxroxHNfCRE\ne3SARxcsFlZUfgFjihfaNz7JUjoaN6llN8YOePe5HfQFplWlWg31mhfl6ecc6eDr\nU/wFqCJjNyaBaCeiPWM67BN+ngKQkMe4oM/oDHvy3H+SwCn0GJjd16mg3hUagZH0\nl/yyRttqHQKBgDYMLfD1ma47lxs5GFiY29IzqnxIf/gyfNP8WLa5yOjILMJmpfjI\nzupf1amV2TbbGjrXZVClMqsQe0wcn6Ycc6MNC+tweFOEY6BjDoSGQ9Fesv7CF/cF\nX0ICD7x+8uiCOQOH8TqKd9qHz904iKzg8l/3pzNm9pH6BxDAky1DjsqJAoGAEWuR\n8KzBVjSIhen4I7dYR3lts8anVM1v/UyhFZCNYzo6mOyiaI5tp5tcqyKX9faYQ7Cv\nNo6oYYmgTB65BBSiFPlrnbT2NEbJDm7qqgSGIUj2uUHl4UllyAxdzYYmqyQ44M1y\nWzE/KO7YofR6diUNsDf/x9qzkaPi4jiGCIaA2SkCgYA0SZMDGJwHa5DLbdCYtj8e\nRNx6KlPpEIy4yogmWR+2sWPyu1VCs0+I01ZU+lG75WOZ361QHqpw0hmV44fyk/bw\nGMNVDZxLwUKenNS/HXo+wUjvOA3KcfLznSFlsJcFllWf0UrbGr87EYfKF6Op+M60\n2hD9c4ay5/f2p2gpTx8CxA==\n-----END PRIVATE KEY-----\n"",
  ""client_email"": ""firebase-adminsdk-bzyo7@bloodgroup-94ed5.iam.gserviceaccount.com"",
  ""client_id"": ""113992212833094531171"",
  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
  ""token_uri"": ""https://oauth2.googleapis.com/token"",
  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-bzyo7%40bloodgroup-94ed5.iam.gserviceaccount.com"",
  ""universe_domain"": ""googleapis.com""
}";
        #endregion

        #region Bearer Token -- Rambabu, Naushad 
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Token/Gettoken")]
        public IHttpActionResult Gettoken()
        {
            var Client_id = "1";
            var data = JwtToken.GenerateToken(Client_id);

            return Ok(data);
        }
        #endregion


        #region  Get Exceptions -- srinivas k
        /// <summary>
        /// Get Top 100 Exceptions
        /// </summary>
        /// <returns>Exceptions List</returns>
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public dynamic getExceptions()
        {
            try
            {
                var data = BL.getExceptions();
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_AExceptions -->Upendra
        [HttpPost]
        [Authorize]
        public dynamic Get_AExceptions()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.Get_AExceptions(Param1);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_Exceptions---upendra

        [HttpPost]
        [Authorize]
        public dynamic Get_Exceptions()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var Param3 = System.Web.HttpContext.Current.Request.Form["Param3"];

                var MSG = BL.Get_Exceptions(Param1, Param2, Param3);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        //This code generate StatesMaster_crud

        #region   StatesMaster_crud     ------Deeksha
        [HttpPost]
        [Authorize]
        public dynamic StatesMaster_crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.StatesMaster_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   StatesMaster_crudCustomer     ------manohar
        [HttpPost]
        public dynamic StatesMaster_crudCustomer()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.StatesMaster_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region   DistrictMaster_crudCustomer     ----manohar
        [HttpPost]
        public dynamic DistrictMaster_crudCustomer()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.DistrictMaster_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region   CitiesMaster_Crudcustomer     ----manohar
        [HttpPost]
        public dynamic CitiesMaster_Crudcustomer()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.CitiesMaster_Crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   BloodGroupMaster_CRUDCustomer     ------manohar
        [HttpPost]
        public dynamic BloodGroupMaster_CRUDCustomer()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.BloodGroupMaster_CRUD(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   UnitsofBlood_CRUDCustomer     ----manohar
        [HttpPost]
        public dynamic UnitsofBlood_CRUDCustomer()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.UnitsofBlood_CRUD(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        //This code generate BloodGroupMaster_CRUD

        #region   BloodGroupMaster_CRUD     ------Deeksha
        [HttpPost]
        [Authorize]
        public dynamic BloodGroupMaster_CRUD()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.BloodGroupMaster_CRUD(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        //This code generate DistrictMaster_crud

        #region   DistrictMaster_crud     ----Amrutha
        [HttpPost]
        [Authorize]
        public dynamic DistrictMaster_crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.DistrictMaster_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion 

        //This code generate UnitsofBlood_CRUD

        #region   UnitsofBlood_CRUD     ----Amrutha
        [HttpPost]
        [Authorize]
        public dynamic UnitsofBlood_CRUD()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.UnitsofBlood_CRUD(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        //This code generate Oldagehome_CRUD

        #region Oldagehome_CRUD -----Amrutha
        [HttpPost]
        public dynamic Oldagehome_CRUD()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var data = BL.Oldagehome_CRUD(Param, Flag);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion 

        #region ShareYourService --deeksha
        [HttpPost]
        public dynamic ShareYourService()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.Get_ShareYourService(Param1);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Download_Brochures --deeksha
        [HttpGet]
        public dynamic Download_Brochures()
        {
            try
            {
                var data = BL.Download_Brochures();
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region HospitalDetails_CRUD  -----Amrutha,deeksha
        public dynamic HospitalDetails_CRUD()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var data = BL.HospitalDetails_CRUD(Param, Flag);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region BG_dashboardcount -- Deeksha,Amrutha

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [Authorize]
        public dynamic BG_dashboardcount()
        {
            try
            {
                var data = BL.BG_dashboardcount();
                return data;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region EnquiryMailTo_BloodCustomers -- Deeksha,Amrutha
        [HttpPost]
        public dynamic EnquiryMailTo_BloodCustomers()
        {
            try
            {
                var email = System.Web.HttpContext.Current.Request.Form["Email"];
                var data = mail.EnquiryMailTo_BloodCustomers(email);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }

        }
        #endregion

        #region MailTo_Admin --Amrutha 22/12/2025
        [HttpPost]
        public dynamic MailTo_Admin()
        {
            try
            {
                var email = System.Web.HttpContext.Current.Request.Form["Email"];
                var data = mail.MailTo_Admin(email);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }

        }
        #endregion




        #region EnquiryMailTo_Admin --Naushad
        [HttpPost]
        public dynamic EnquiryMailTo_Admin()
        {
            try
            {
                var email = System.Web.HttpContext.Current.Request.Form["Email"];
                var data = mail.EnquiryMailTo_Admin(email);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   Donorlist_crud     ----Naushad
        [HttpPost]
        [Authorize]
        public dynamic Donorlist_crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Donorlist_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   Recepient_crud     ----Naushad
        [HttpPost]
        [Authorize]
        public dynamic Recepient_crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Recepient_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region BG_Brochers_Crud -- manohar
        [HttpPost]
        public dynamic BG_Brochers_Crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.BG_Brochers_Crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }



        #endregion

        #region  Insert_Update_DonersForm --manohar
        [HttpPost]
        public dynamic Insert_Update_DonersForm()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Insert_Update_DonersForm(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region  Insert_Update_requestForm --manohar
        [HttpPost]
        public dynamic Insert_Update_requestForm()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Insert_Update_requestForm(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region BG_Admin_Login --Naushad, Rambabu
        [HttpPost]
        // [Authorize]
        public dynamic BG_Admin_Login()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var data = BL.BG_Admin_Login(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion



        #region BG_CustomersLoginByStatus---- Suraj
        [HttpPost]
        public dynamic BG_CustomersLoginByStatus()
        {
            try
            {


                var Param1 = System.Web.HttpContext.Current.Request.Form["Mobile"];
                var MSG = BL.BG_CustomersLoginByStatus(Param1);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                //BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region  Register_User_Curd --prashnth
        public dynamic Register_User_Curd()
        {
            try
            {

                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Register_User_Curd(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }

        }

        #endregion

        #region   CitiesMaster_Crud     ----Naushad
        [HttpPost]
        [Authorize]
        public dynamic CitiesMaster_Crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.CitiesMaster_Crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region upload the EmergencyType pdf --single image upolad --manu       
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public string UploadEmergencyTypeImage()
        {
            try
            {
                var filepath = "";
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var baseFolder = "Image\\Borchurepdf\\";
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder);
                    if (!Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["PostedFile"];
                    if (httpPostedFile != null)
                    {
                        var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Image/ContactInfo/"), Path.GetFileName(httpPostedFile.FileName));
                        httpPostedFile.SaveAs(fileSavePath);
                        filepath = "/Image/ContactInfo/" + Path.GetFileName(httpPostedFile.FileName);
                    }

                }
                return filepath;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                return "";
            }
        }
        #endregion





        #region upload Brochure Images ----> Amrutha
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public string UploadBrochureImages()
        {
            try
            {
                // Check if any file is uploaded
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Current.Request.Files["PostedFile"];
                    if (httpPostedFile != null && httpPostedFile.ContentLength > 0)
                    {
                        // Validate file extension
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
                        string ext = Path.GetExtension(httpPostedFile.FileName).ToLower();
                        if (!allowedExtensions.Contains(ext))
                            return "ERROR: Invalid file type";

                        // Define folder path
                        string folderPath = HttpContext.Current.Server.MapPath("~/Image/BrochureImages/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        // Get original filename (without extension)
                        string originalFileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);

                        // Add timestamp to make filename unique
                        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string newFileName = $"{originalFileName}_{timeStamp}{ext}";

                        // Full path to save the file
                        string savePath = Path.Combine(folderPath, newFileName);
                        httpPostedFile.SaveAs(savePath);

                        // Return relative path for Angular & DB
                        return "Image/BrochureImages/" + newFileName;
                    }
                    else
                    {
                        return "ERROR: No file selected";
                    }
                }
                return "ERROR: No files uploaded";
            }
            catch (Exception ex)
            {
                // Log exception if you have a handler
                return "ERROR: " + ex.Message;
            }
        }
        #endregion




        #region upload the BannerImages ---- Amrutha
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public string UploadBannersImages()
        {
            try
            {
                string filepath = "";
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var baseFolder = "Image\\BannersImages\\";
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder);

                    // Create folder if it doesn’t exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["PostedFile"];
                    if (httpPostedFile != null)
                    {
                        // Get original file name and extension
                        string originalFileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
                        string fileExtension = Path.GetExtension(httpPostedFile.FileName);

                        // Add timestamp to make filename unique
                        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string newFileName = $"{originalFileName}_{timeStamp}{fileExtension}";

                        // Save file to server
                        string savePath = HttpContext.Current.Server.MapPath("~/Image/BannersImages/" + newFileName);
                        httpPostedFile.SaveAs(savePath);

                        // Return relative path (to save in DB or show on frontend)
                        filepath = "Image/BannersImages/" + newFileName;
                    }
                }

                return filepath;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                return "";
            }
        }
        #endregion



        #region upload the Three Images   --->Amrutha 14-10-25
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public string UploadImages()
        {
            try
            {
                string filepath = "";
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var baseFolder = "Image\\ThreeImages\\";
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["PostedFile"];
                    if (httpPostedFile != null)
                    {
                        // Get the original filename and extension
                        string originalFileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
                        string fileExtension = Path.GetExtension(httpPostedFile.FileName);

                        // Create a unique filename using datetime
                        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string newFileName = $"{originalFileName}_{timeStamp}{fileExtension}";

                        // Full server path to save file
                        string savePath = HttpContext.Current.Server.MapPath("~/Image/ThreeImages/" + newFileName);
                        httpPostedFile.SaveAs(savePath);

                        // File path to return (for saving in DB or showing in frontend)
                        filepath = "Image/ThreeImages/" + newFileName;
                    }
                }
                return filepath;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                return "";
            }
        }
        #endregion




        #region BG_GeetExeption---upendra

        [HttpPost]
        [Authorize]
        public dynamic BG_GeetExeption()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var Param3 = System.Web.HttpContext.Current.Request.Form["Param3"];

                var MSG = BL.BG_GeetExeption(Param1, Param2, Param3);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        #region Orphanage_CRUD -----Amrutha
        [HttpPost]

        public dynamic Orphanage_CRUD()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var data = BL.Orphanage_CRUD(Param, Flag);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        


        #region send otp forlogin ---------> Upendra
        [System.Web.Http.HttpPost]
        public dynamic SendOtpToMobile()
        {
            try
            {
                var mobile = System.Web.HttpContext.Current.Request.Form["MobileNo"];
                var otp = System.Web.HttpContext.Current.Request.Form["OTP"];
                var SendSMS = "https://api.msg91.com/api/sendhttp.php?mobiles=" + mobile + "&authkey=462195AkG1ha2MQ6888a486P1&route=4&sender=LTSHPF&message=Use this OTP to log in to Let's Help:" + otp + " LTSHPF " + "&country=91&campaign=LTSHPF&DLT_TE_ID=1007345501694469517";

                WebClient client = new WebClient();
                client.OpenRead(SendSMS);
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Delete multiple ---upendra
        [HttpPost]
        [Authorize]
        public dynamic Delete_Exceptions()
        {
            try
            {
                var Id = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Delete_Exceptions(Id);
                return data;

            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   Gallery_Crud     -- Upendra
        [HttpPost]
        public dynamic Gallery_Crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Gallery_Crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region Crud_ThreeImages ---->Amrutha
        [HttpPost]
        public dynamic Crud_ThreeImages()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request["Param"];
                var flag = System.Web.HttpContext.Current.Request["Flag"];
                var data = BL.Crud_ThreeImages(param, flag);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Upload Galleyimages -- Upendra
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public string UploadGallery()
        {
            try
            {
                var filepath = "";
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var dateform = DateTime.Now.ToString("MMMM") + DateTime.Now.ToString("yyyy") + "//" + DateTime.Now.ToString("dd-MM-yyyy");
                    var dateform1 = DateTime.Now.ToString("MMMM") + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("dd-MM-yyyy");
                    var baseFolder = "Content\\Gallery\\" + dateform;
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder);
                    if (!Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Gallery/" + dateform1), httpPostedFile.FileName);
                        httpPostedFile.SaveAs(fileSavePath);
                        filepath = "Content/Gallery/" + dateform1 + "/" + httpPostedFile.FileName;
                    }
                }
                return filepath;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return "";
            }
        }
        #endregion

        #region GetMailsmtpdetails
        [HttpGet]
        public dynamic GetMailsmtpdetails()
        {
            try
            {
                var data = BL.GetMailsmtpdetails();
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   CRUD_MailSmtpDetails-->chandana
        [HttpPost]
        public dynamic Insert_MailSmtpDetails()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];

                var MSG = BL.Insert_MailSmtpDetails(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Approve_Requests -- Asif
        [HttpPost]
        [Authorize]
        public dynamic Approve_Requests()

        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.Approve_Requests(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        #region Check_Mobile---- Manohar
        [HttpPost]
        public dynamic checking_Mobile()
        {
            try
            {


                var Param1 = System.Web.HttpContext.Current.Request.Form["Mobile"];
                var MSG = BL.checking_Mobile(Param1);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                //BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_Requestform --manoahr
        [HttpPost]
        public dynamic Get_Requestform()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Get_Requestform(Param1);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                //BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion




        #region   Get Searchdetails    -- sv
        [HttpPost]
        public dynamic Get_Searchdetails()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var Param3 = System.Web.HttpContext.Current.Request.Form["Param3"];
                var Param4 = System.Web.HttpContext.Current.Request.Form["Param4"];
                var Param5 = System.Web.HttpContext.Current.Request.Form["Param5"];
                var Param6 = System.Web.HttpContext.Current.Request.Form["Param6"];

                var MSG = BL.Get_Searchdetails(Param1, Param2, Param3, Param4, Param5, Param6);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   Get Oldagehomedetails    -- sv
        [HttpPost]
        public dynamic Oldagehomedetails()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var Param3 = System.Web.HttpContext.Current.Request.Form["Param3"];
                var Param4 = System.Web.HttpContext.Current.Request.Form["Param4"];
                var Param5 = System.Web.HttpContext.Current.Request.Form["Param5"];
                var Param6 = System.Web.HttpContext.Current.Request.Form["Param6"];

                var MSG = BL.Oldagehomedetails(Param1, Param2, Param3, Param4, Param5, Param6);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   Get orphanDeatils    -- sv
        [HttpPost]
        public dynamic orphanDeatils()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var Param3 = System.Web.HttpContext.Current.Request.Form["Param3"];
                var Param4 = System.Web.HttpContext.Current.Request.Form["Param4"];
                var Param5 = System.Web.HttpContext.Current.Request.Form["Param5"];
                var Param6 = System.Web.HttpContext.Current.Request.Form["Param6"];

                var MSG = BL.orphanDeatils(Param1, Param2, Param3, Param4, Param5, Param6);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region    Get_Areass    -- sv
        [HttpGet]
        public dynamic Get_Areass()
        {
            try
            {
                var MSG = BL.Get_Areass();
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region    Get_oldAreass    -- sv
        [HttpGet]
        public dynamic Get_oldAreass()
        {
            try
            {
                var MSG = BL.Get_oldAreass();
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region    GetOrphan_Address    -- sv
        [HttpGet]
        public dynamic GetOrphan_Address()
        {
            try
            {
                var MSG = BL.GetOrphan_Address();
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_Requestbasedon_presonID --manoahr
        [HttpPost]
        public dynamic Get_Requestbasedon_presonID()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Get_Requestbasedon_presonID(param);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion


        #region Get_Requestbasedon_presonID --suraj
        [HttpPost]
        public dynamic Get_Requestbasedon_presonID_mobile()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Get_Requestbasedon_presonID_mobile(param);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion


        #region   GalleryCat_Crud     -- Upendra
        [HttpPost]
        public dynamic GalleryCat_Crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.GalleryCategory_Crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_gallerycatwise --Upendra
        [HttpPost]
        public dynamic Get_gallerycatwise()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.Get_gallerycatwise(Param1);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_mobilegallerycatagory --suraj(18-10-24)
        [HttpGet]
        public dynamic Get_mobilegallerycatagory()
        {
            try
            {
                //var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.Get_mobilegallerycatagory();
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region Get_BloodRequestsIDMobiles --Upendra
        [HttpPost]
        public dynamic Get_BloodRequestsIDMobiles()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.BG_GetBloodRequestsIDs(Param1);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Accept request --suraj(18-10-24)
        [HttpGet]
        public dynamic Acceptrequestby_mobile()
        {
            try
            {
                //var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.BG_Get_AcceptRequest_mobile();
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region InsertBrochures_Deatails --manohar
        public dynamic InsertBrochures_Deatails()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request["Param"];
                var data = BL.InsertBrochures_Deatails(Param);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region Get_gallery_Search --Upendra
        [HttpPost]
        public dynamic Get_gallery_Search()

        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.Get_Gallery_Search(Param1);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_Phonenumber --manoahr
        [HttpPost]
        public dynamic Get_Phonenumber()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Get_Phonenumber(param);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region  Get_Gallaryimages_bycategory --manohar
        [HttpGet]
        public dynamic Get_Gallaryimages_bycategory()
        {
            try
            {
                var data = BL.Get_Gallaryimages_bycategory();
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region  Get Get_AllcitiesPaged COMMENTED bY Upendra
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public dynamic Get_AllcitiesPaged()
        {
            try
            {
                var pageNumber = System.Web.HttpContext.Current.Request.Form["pageNumber"];
                var pageSize = System.Web.HttpContext.Current.Request.Form["pageSize"];
                var data = BL.Get_AllcitiesPaged(Convert.ToInt32(pageNumber), Convert.ToInt32(pageSize)); // Call the new method for paged data retrieval
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                return "";
            }
        }
        #endregion


        #region Gallery_View --Upendra
        [HttpPost]
        public dynamic Gallery_View()

        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.Gallery_view(Param1);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region  Get_GallaryImagedBasedOn_ID  --manohar
        [HttpPost]
        public dynamic Get_GallaryImagedBasedOn_ID()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Get_GallaryImagedBasedOn_ID(param);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_PincodeDropdown -- Manohar
        [HttpGet]
        public dynamic Get_PincodeDropdown()
        {
            try
            {
                var data = BL.Get_PincodeDropdown();
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region Forgot_Password Commented By -- manohar
        [HttpPost]
        public dynamic Forgot_Password()
        {
            try
            {
                var Password = System.Web.HttpContext.Current.Request.Form["Password"];
                var Mobile = System.Web.HttpContext.Current.Request.Form["Mobile"];
                var data = BL.Forgot_Password(Password, Mobile);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region   BG_Login     -- manohar
        [HttpPost]
        // [Authorize]
        public dynamic BG_Login()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var MSG = BL.BG_Login(Param);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region      Send OTP to mail when login      -- Suresh
        [HttpPost]
        public dynamic SendOTPtoMail()
        {
            try
            {
                var EmailID = System.Web.HttpContext.Current.Request.Form["EMailID"];
                var Content = System.Web.HttpContext.Current.Request.Form["Content"];
                var MSG = mail.SendOTPtoMail(EmailID, Content);
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_Hospital---- Upendra
        [HttpPost]
        public dynamic Get_Hospital()
        {
            try
            {


                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var MSG = BL.Get_Hospital(Param1);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                //BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion



        #region  Insert_Update_Requestpresent 
        [HttpPost]
        public dynamic Insert_Update_Requestpresent()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Insert_Update_Requestpresent(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region    BG_GetObservationReport    -- Amrutha
        [HttpGet]
        public dynamic BG_GetObservationReport()
        {
            try
            {
                var MSG = BL.BG_GetObservationReport();
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion



        #region  Insert_Update_Report 
        [HttpPost]
        public dynamic Insert_Update_Report()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Insert_Update_Report(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region   BG_Observation_Crud     ----Naushad
        [HttpPost]
        //[Authorize]
        public dynamic BG_Observation_Crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.BG_Observation_Crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion 

        #region   BG_Work_Place_Crud     ----Naushad
        [HttpPost]
        // [Authorize]
        public dynamic BG_Work_Place_Crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.BG_Work_Place_Crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_PresentationDetails ---->Amrutha
        [HttpGet]
        public dynamic Get_PresentationDetails()
        {
            try
            {
                var data = BL.Get_PresentationDetails();
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

       

        #region Get_RequestPresentation --Rambabu
        [HttpPost]
        public dynamic Get_RequestPresentation()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var Param3 = System.Web.HttpContext.Current.Request.Form["Param3"];
                var data = BL.Get_RequestPresentation(Param1, Param2, Param3);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region   Volunteerlist_crud     ----Naushad
        [HttpPost]
        [Authorize]
        public dynamic Volunteerlist_crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Volunteerlist_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region  Insert_Update_Addmembers --manohar
        [HttpPost]
        public dynamic Insert_Update_Addmembers()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Insert_Update_Addmembers(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion


        #region Get_DonorReports --Naushad,Rambabu
        [HttpPost]
        public dynamic Get_DonorReports()
        {
            try
            {
                var adminDetails = System.Web.HttpContext.Current.Request.Form["Param"];
                var MSG = BL.Get_DonorReports(adminDetails);
                return MSG;
            }
            catch (Exception ex)
            {

                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion


        #region PlaceOrderMail------------upendra
        [System.Web.Http.HttpPost]
        public dynamic MailPlaceOrder()
        {
            try
            {

                var CartData = System.Web.HttpContext.Current.Request.Form["Blood"];
                var baseurl = System.Web.HttpContext.Current.Request.Form["url"];
                List<Doners> Details1 = new JavaScriptSerializer().Deserialize<List<Doners>>(CartData);

                var MSG = mail.Sendorderdetailstouser1(Details1, baseurl);


                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }


        }
        #endregion

        #region PlaceOrderMail------------upendra
        [System.Web.Http.HttpPost]
        public dynamic MailPlaceOrdercerti()
        {
            try
            {

                var CartData = System.Web.HttpContext.Current.Request.Form["SPR"];
                var baseurl = System.Web.HttpContext.Current.Request.Form["url"];
                List<Doners> Details1 = new JavaScriptSerializer().Deserialize<List<Doners>>(CartData);

                var MSG = mail.SendCertificate(Details1, baseurl);


                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }


        }
        #endregion

        #region PlaceOrderMail------------upendra
        [System.Web.Http.HttpPost]
        public dynamic MailtoDonar()
        {
            try
            {

                var CartData = System.Web.HttpContext.Current.Request.Form["Blood"];
                var baseurl = System.Web.HttpContext.Current.Request.Form["url"];
                List<Doners> Details1 = new JavaScriptSerializer().Deserialize<List<Doners>>(CartData);

                var MSG = mail.send_observation(Details1, baseurl);


                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }


        }
        #endregion

        #region PlaceOrderMail------------prashnth
        [System.Web.Http.HttpPost]
        public dynamic MailPlaces()
        {
            try
            {

                var CartData = System.Web.HttpContext.Current.Request.Form["Blood"];
                var baseurl = System.Web.HttpContext.Current.Request.Form["url"];
                List<Doners> Details1 = new JavaScriptSerializer().Deserialize<List<Doners>>(CartData);

                var MSG = mail.Sendorderdetailstouser1(Details1, baseurl);


                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }


        }
        #endregion

        #region Get_Users_basedOn_Pincode --mano
        [HttpPost]
        public dynamic Get_Users_basedOn_Pincode()
        {
            try
            {
                var pincode = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Get_Users_basedOn_Pincode(pincode);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region RequestPresentationDetails --manohar
        [HttpPost]
        public dynamic RequestPresentationDetails()
        {
            try
            {
                var EMail = System.Web.HttpContext.Current.Request.Form["EMaildetails"];
                var data = mail.RequestPresentationDetails(EMail);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Customer Replyingmail --manohar
        [HttpGet]
        public dynamic CustomerReplyingMail(string EMaildetails)
        {
            try
            {
               
                var email = EMaildetails; // Extracting email from EMaildetails parameter
                var reply = ""; // You may extract reply if needed

                var data = mail.CustomerReplyingMail(email, reply);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region  LeadersMailfrom_admin  --manohar
        [HttpPost]
        public dynamic LeadersMailfrom_admin()
        {
            try
            {
                var admin = System.Web.HttpContext.Current.Request.Form["adminreply"];
                var data = mail.Volunteer_mail_admin(admin);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Get_image_uploadDetails --manohar
        [HttpPost]
        public dynamic Get_image_uploadDetails()
        {
            try
            {
                var regid = System.Web.HttpContext.Current.Request.Form["regid"];
                var catid = System.Web.HttpContext.Current.Request.Form["catid"];
                var data = BL.Get_image_uploadDetails(regid, catid);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region Upload Blood Donation Images    -- Suresh
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public string UploadBloodDonationImage()
        {
            try
            {
                var filepath = "";
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var dateform = DateTime.Now.ToString("MMMM") + DateTime.Now.ToString("yyyy") + "//" + DateTime.Now.ToString("dd-MM-yyyy");
                    var dateform1 = DateTime.Now.ToString("MMMM") + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("dd-MM-yyyy");
                    var baseFolder = "Content\\BloodDonations\\" + dateform;
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder);
                    if (!Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["UploadedImage"];
                    if (httpPostedFile != null)
                    {
                        var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/BloodDonations/" + dateform1), httpPostedFile.FileName);
                        httpPostedFile.SaveAs(fileSavePath);
                        filepath = "/Content/BloodDonations/" + dateform1 + "/" + httpPostedFile.FileName;
                    }
                }
                return filepath;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return "";
            }
        }
        #endregion

        #region Upload Blood Donation Image by Donor    -- Suresh
        [HttpPost]
        public dynamic UploadBloodDonationImagebyDonor()
        {
            try
            {
                var BloodDonationImage = System.Web.HttpContext.Current.Request.Form["BloodDonationImage"];
                var BloodRequestID = System.Web.HttpContext.Current.Request.Form["BloodRequestID"];
                var data = BL.UploadBloodDonationImage(BloodDonationImage, BloodRequestID);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Upload Blood Donation Image by Donor    -- Suraj
        [HttpPost]
        public dynamic UploadBloodDonationImagebyDonorwitroleid()
        {
            try
            {
                var BloodDonationImage = System.Web.HttpContext.Current.Request.Form["BloodDonationImage"];
                var BloodRequestID = System.Web.HttpContext.Current.Request.Form["BloodRequestID"];
                var RoleId = System.Web.HttpContext.Current.Request.Form["RoleId"];
                var data = BL.UploadBloodDonationImagebyDonorwitroleid(BloodDonationImage, BloodRequestID, RoleId);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Upload Blood Donation Image by Donor    -- Suraj
        [HttpPost]
        public dynamic UploadBloodDonationImagenorqstId()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var data = BL.UploadBloodDonationImagemobile(Param, Flag);
                // var data = BL.UploadBloodDonationImagemobile(BloodDonationImage,param2);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get Closed Blood Requests   -- Suresh
        [HttpPost]
        public dynamic Get_ClosedBloodRequests()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var data = BL.Get_ClosedBloodRequests(Param);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region   Crud_Notifications    ----Rk
        [HttpPost]

        public dynamic Crud_Notifications()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Crud_Notifications(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        


        #region BG_Getbloodrequest_notification  -- Rk
        [HttpPost]
        public dynamic BG_Getbloodrequest_notification()
        {
            try
            {

                var data = BL.BG_Getbloodrequest_notification();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Send Notification  -- chandana
        [HttpPost]
        public dynamic BG_GetNotificationForUsers()
        {
            try
            {
                var Pincode = System.Web.HttpContext.Current.Request.Form["Pincode"];
                var data = BL.BG_GetNotificationForUsers(Pincode);
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion


        #region Send Notification  -- suraj,venu
        public async Task<string> sendNotification()
        {
            // Create GoogleCredential from the static JSON key string
            GoogleCredential credential = GoogleCredential.FromJson(JsonKeyString)
                .CreateScoped(new[] { "https://www.googleapis.com/auth/firebase.messaging" });

            // Get the access token
            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            var deviceId = System.Web.HttpContext.Current.Request.Form["deviceId"];
            var message = System.Web.HttpContext.Current.Request.Form["message"];
            var senderName = System.Web.HttpContext.Current.Request.Form["senderName"];
            var path = System.Web.HttpContext.Current.Request.Form["path"];
            var Img = System.Web.HttpContext.Current.Request.Form["Img"];



            var data = BL.SendFCMNotification(deviceId, message, senderName, path, Img, token);
            return data;
        }


        #endregion


        

        #region Send Notification  -- Rk
        [HttpPost]
        public dynamic BG_Get_Referalcode_Notification()
        {
            try
            {
                var RegId = System.Web.HttpContext.Current.Request.Form["RegId"];
                var data = BL.BG_Get_Referalcode_Notification(RegId);
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion


        #region   MobileDashboardcount     -- suraj
        [HttpPost]
        public dynamic MobileDashboardcount()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var MSG = BL.MobileDashboard(Param1, Param2);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region DeleteAccount_Covert_as_Leader --manohar
        [HttpPost]
        public dynamic DeleteAccount_Covert_as_Leader()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request["Param2"];
                var data = BL.DeleteAccount_Covert_as_Leader(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region Get_Hospitaldetails_mobile --suraj
        [HttpGet]
        public dynamic Get_Hospitaldetails_mobile()
        {
            try
            {
                var data = BL.Get_Hospitaldetails_mobile();
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                //BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion


        #region Update Device Token
        [HttpPost]
        public dynamic UpdateDeviceToken()
        {
            try
            {
                string tokenDetails = System.Web.HttpContext.Current.Request.Form["tokenDetails"];
                var data = BL.UpdateDeviceToken(tokenDetails);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");

                throw ex;
            }
        }
        #endregion



        #region Bloodrequest Mail To Admin -- Rk
        [HttpPost]
        public dynamic EnquiryMailTo_Bloodrequest()
        {
            try
            {
                var email = System.Web.HttpContext.Current.Request.Form["Email"];
                var data = mail.EnquiryMailTo_Bloodrequest(email);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");

                throw ex;
            }
        }
        #endregion


        #region BDE_GET_EmployessforDayincentive --prshnth
        [HttpGet]
        public dynamic BDE_GET_EmployessforDayincentive()
        {
            try
            {
                var data = BL.BDE_GET_EmployessforDayincentive();
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_Notification_basedon_UserId -- manohar
        [HttpPost]
        public dynamic Get_Notification_basedon_UserId()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request["Param"];
                var data = BL.Get_Notification_basedon_UserId(param);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Get_USERrequest_Closedrequest_Idbased --manohar
        [HttpPost]
        public dynamic Get_USERrequest_Closedrequest_Idbased()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request["Param2"];
                var data = BL.Get_USERrequest_Closedrequest_Idbased(Param, Param2);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Crud_Banners --manohar
        [HttpPost]
        public dynamic Crud_Banners()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request["Param"];
                var flag = System.Web.HttpContext.Current.Request["Flag"];
                var data = BL.Crud_Banners(param, flag);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region customerRegconformmail --manohar
        [HttpPost]
        public dynamic customerRegconformmail()
        {
            try
            {
                var param = System.Web.HttpContext.Current.Request["Email"];
                var data = mail.customerRegconformmail(param);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Getbanners --venkat
        [HttpGet]
        public dynamic GetBanners()
        {
            try
            {

                var data = BL.GetBanners();
                return data;
            }
            catch (Exception ex)
            {

                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");

                throw ex;
            }
        }
        #endregion

        #region Update_ActivateAccount --manohar
        [HttpGet]
        public dynamic Update_ActivateAccount(string Email)
        {
            try
            {
                //  var Param = System.Web.HttpContext.Current.Request["Param"];
                var data = BL.Update_ActivateAccount(Email);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetUsers_basedonPincode_BloodreqCondition -- Manohar
        [HttpPost]
        public dynamic GetUsers_basedonPincode_BloodreqCondition()
        {
            try
            {
                var param1 = System.Web.HttpContext.Current.Request["Param1"];
                var param2 = System.Web.HttpContext.Current.Request["Param2"];
                var data = BL.GetUsers_basedonPincode_BloodreqCondition(param1, param2);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Update_Usercontrollers -- manohar
        [HttpPost]
        public dynamic Update_Usercontrollers()
        {
            try
            {
                var param1 = System.Web.HttpContext.Current.Request["Param1"];
                var param2 = System.Web.HttpContext.Current.Request["Param2"];
                var param3 = System.Web.HttpContext.Current.Request["Param3"];
                var param4 = System.Web.HttpContext.Current.Request["Param4"];
                var param5 = System.Web.HttpContext.Current.Request["Param5"];
                var param6 = System.Web.HttpContext.Current.Request["Param6"];
                var param7 = System.Web.HttpContext.Current.Request["Param7"];
                var data = BL.Update_Usercontrollers(param1, param2, param3, param4, param5, param6, param7);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region Update_request_donors -- suraj
        [HttpPost]
        public dynamic Update_request_donors()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request["Param2"];
                var Param3 = System.Web.HttpContext.Current.Request["Param3"];
                var data = BL.Update_request_donors(Param1, Param2, Param3);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

       


        #region BG_GetUserControllers 
        [HttpGet]
        public dynamic BG_GetUserControllers()
        {
            try
            {
                //var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.BG_GetUserControllers();
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion



        #region BG_UserControllersInsert ------Deeksha
        [HttpPost]
        public dynamic BG_UserControllersInsert()
        {
            try
            {
                var param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var param3 = System.Web.HttpContext.Current.Request.Form["Param3"];
                var param4 = System.Web.HttpContext.Current.Request.Form["Param4"];
                var param5 = System.Web.HttpContext.Current.Request.Form["Param5"];
                var param6 = System.Web.HttpContext.Current.Request.Form["Param6"];
                var param7 = System.Web.HttpContext.Current.Request.Form["Param7"];
                var param8 = System.Web.HttpContext.Current.Request.Form["Param8"];
                var param9 = System.Web.HttpContext.Current.Request.Form["Param9"];
                var MSG = BL.BG_UserControllersInsert(param1, param2, param3, param4, param5, param6, param7, param8, param9);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw;
            }
        }
        #endregion


        #region BG_UserControllersUpdate
        [HttpPost]
        public dynamic BG_UserControllersUpdate()
        {
            try
            {
                var param1 = System.Web.HttpContext.Current.Request["Param1"];
                var param2 = System.Web.HttpContext.Current.Request["Param2"];
                var Flag = System.Web.HttpContext.Current.Request["Flag"];
                var data = BL.BG_UserControllersUpdate(param1, param2, Flag);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get_RoleforRolechange --Upendra
        [HttpPost]
        public dynamic Get_RoleforRolechange()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request["Param2"];
                var data = BL.Get_RoleforRolechange(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region   Crud_UserCommunicationPreferences    ----Upendra
        [HttpPost]

        public dynamic Crud_UserCommunicationPreferences()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.Crud_UserCommunicationPreferences(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region   Get_CommunicationMaster    ----Upendra
        [HttpGet]
        public dynamic Get_CommunicationMaster()
        {
            try
            {
                var data = BL.Get_CommunicationMaster();
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion


        #region Get_userCommunicationPreferences --Upendra
        [HttpPost]
        public dynamic Get_UserCommunicationPref()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var data = BL.Get_userCommunicationPreferences(Param1);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region BG_UserControllersUpdate
        [HttpPost]
        public dynamic Update_CommunicationChannel()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request["Param2"];
                var data = BL.Update_CommunicationChannel(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region BloodRequestClosed -- Upendra
        [HttpPost]
        public dynamic BloodRequestClosed()
        {
            try
            {
                var param1 = System.Web.HttpContext.Current.Request["Param1"];
                var data = BL.BloodRequestClosed(param1);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AfterAdminApprovedNotificationtoAll --Upendra
        [HttpPost]
        public dynamic AfterAdminApprovedNotificationtoAll()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var data = BL.AfterAdminApprovedNotificationtoAll(Param1, Param2);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region Blood Request Mail to Donor -- Upendra
        [HttpPost]
        public dynamic BloodRequestMailtoDonor()
        {
            try
            {
                var email = System.Web.HttpContext.Current.Request.Form["Email"];
                var data = mail.BloodRequestMailtoDonor(email);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region DashBoardDetails---- Upendra
        [HttpPost]
        public dynamic DashBoardDetails()
        {
            try
            {


                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var MSG = BL.DashBoardDetails(Param1, Param2);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                //BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region upload the Contact attachment image with date time // Upendra     
        [System.Web.Http.HttpPost]
        public IHttpActionResult UploadContactAttachmentImage()
        {
            try
            {
                var filepath = "";
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var baseFolder = "Image\\ContactInfo\\";
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder);
                    if (!Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["attachment"];
                    if (httpPostedFile != null && httpPostedFile.ContentLength > 0)
                    {
                        string originalFileName = Path.GetFileName(httpPostedFile.FileName);
                        string fileExtension = Path.GetExtension(originalFileName);

                        string dateTimeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                        string newFileName = $"Contactinfo_{dateTimeStamp}{fileExtension}";

                        var fileSavePath = Path.Combine(
                            System.Web.HttpContext.Current.Server.MapPath("~/Image/ContactInfo/"),
                            newFileName
                        );

                        httpPostedFile.SaveAs(fileSavePath);

                        filepath = "/Image/ContactInfo/" + newFileName;
                    }
                }
                return Ok(new { path = filepath });
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                return InternalServerError(ex);
            }
        }

        #endregion


        #region EnquiryMailTo_BloodCustomer -- Upendra
        [HttpPost]
        public dynamic EnquiryMailTo_BloodCustomerr()
        {
            try
            {
                var email = System.Web.HttpContext.Current.Request.Form["EmailID"];
                var fullname = System.Web.HttpContext.Current.Request.Form["Fullname"];
                var phonenumber = System.Web.HttpContext.Current.Request.Form["Phonenumber"];
                var type = System.Web.HttpContext.Current.Request.Form["Type"];
                var comments = System.Web.HttpContext.Current.Request.Form["Comments"];
                var attachmentPath = System.Web.HttpContext.Current.Request.Form["AttachmentPath"];

                string subject = $"Enquiry from {fullname} - {type}";

                // Build email body dynamically
                var sb = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(fullname))
                    sb.AppendLine($"<p><b>Full Name:</b> {fullname}</p>");
                if (!string.IsNullOrWhiteSpace(phonenumber))
                    sb.AppendLine($"<p><b>Phone Number:</b> {phonenumber}</p>");

                if (!string.IsNullOrWhiteSpace(email))
                    sb.AppendLine($"<p><b>Email:</b> {email}</p>");

                if (!string.IsNullOrWhiteSpace(type))
                    sb.AppendLine($"<p><b>Reason:</b> {type}</p>");

                if (!string.IsNullOrWhiteSpace(comments))
                    sb.AppendLine($"<p><b>Comments:</b> {comments}</p>");



                // Send mail (with or without file attached)
                var result = new Mail().SendMail4(null, email, sb.ToString(), subject, attachmentPath);

                return new { status = result };
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw;
            }
        }
        #endregion

        [System.Web.Http.HttpPost]
        public IHttpActionResult SendSMSToDonors([FromBody] DonorSMSRequest request)
        {
            try
            {
                string mobile = request.MobileNo?.Trim();
                string donorName = request.DonorName?.Trim();
                string patientName = request.PatientName?.Trim();
                string bloodGroup = request.BloodGroup?.Trim();
                string cityName = request.CityName?.Trim();

                string message = "Dear " + donorName + ", " + patientName + " urgently need " + bloodGroup + " blood at " + cityName + ". Your help can save a life. LTSHPF";

                string encodedMessage = System.Web.HttpUtility.UrlEncode(message);

                var SendSMS = "https://api.msg91.com/api/sendhttp.php?mobiles=" + mobile
                            + "&authkey=462195AkG1ha2MQ6888a486P1"
                            + "&route=4"
                            + "&sender=LTSHPF"
                            + "&message=" + encodedMessage
                            + "&country=91"
                            + "&campaign=LTSHPF"
                            + "&DLT_TE_ID=1007968217554697111";

                using (WebClient client = new WebClient())
                {
                    client.OpenRead(SendSMS);
                }

                return Ok("SUCCESS");
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw;
            }
        }

        #region Update_RegisterApprovalStatus -- Venkat
        [HttpPost]
        // [Authorize]
        public dynamic Update_RegisterApprovalStatus()

        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var data = BL.Update_RegisterApprovalStatus(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        #region RejectMail------------Venkat
        [HttpPost]
        public dynamic MailRejection()
        {
            try
            {
                var CartData = System.Web.HttpContext.Current.Request.Form["SPR"];
                List<Doners> Details1 = new JavaScriptSerializer().Deserialize<List<Doners>>(CartData);

                var MSG = mail.SendRejectionMail(Details1);
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion



        #region GetLocation_Donationdate_basedonBrequestID -->Amrutha
        [HttpPost]
        public dynamic GetLocation_Donationdate_basedonBrequestID()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var data = BL.GetLocation_Donationdate_basedonBrequestID(Param1);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Update_SYSSubmitted_ByBloodRequestID --Upendra
        [HttpPost]
        public dynamic Update_SYSSubmitted_ByBloodRequestID()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var data = BL.Update_SYSSubmitted_ByBloodRequestID(Param1);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region InsertContactReport_MOBILE ----Amrutha
        public dynamic InsertContactReport_MOBILE()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request["Param"];
                var data = BL.InsertContactReport_MOBILE(Param);
                return data;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region    BG_GetContactReport  --- Amrutha
        [HttpGet]
        public dynamic BG_GetContactReport()
        {
            try
            {
                var MSG = BL.BG_GetContactReport();
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion



        #region upload Reciept Images ----> Asif
        [System.Web.Http.HttpPost]
        public string UploadRecieptImages()
        {
            try
            {
                // Check if any file is uploaded
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Current.Request.Files["PostedFile"];
                    if (httpPostedFile != null && httpPostedFile.ContentLength > 0)
                    {
                        // Validate file extension
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
                        string ext = Path.GetExtension(httpPostedFile.FileName).ToLower();
                        if (!allowedExtensions.Contains(ext))
                            return "ERROR: Invalid file type";

                        // Define folder path
                        string folderPath = HttpContext.Current.Server.MapPath("~/Content/RecieptImg/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        // Create filename with timestamp
                        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string newFileName = $"Filename_{timeStamp}{ext}";

                        // Full path to save the file
                        string savePath = Path.Combine(folderPath, newFileName);
                        httpPostedFile.SaveAs(savePath);

                        // Return relative path for Angular & DB
                        return "Content/RecieptImg/" + newFileName;
                    }
                    else

                    {
                        return "ERROR: No file selected";
                    }
                }
                return "ERROR: No files uploaded";
            }
            catch (Exception ex)
            {
                // Log exception if you have a handler
                return "ERROR: " + ex.Message;
            }
        }
        #endregion


        #region GetLatestLastDonationDate ----Amrutha
        [HttpPost]
        public dynamic GetLatestLastDonationDate()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request["Param2"];
                var data = BL.GetLatestLastDonationDate(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Check Any donor is accepted the blood Request -- Upendra
        [HttpPost]
        public dynamic CheckAnydonorisaccepted()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request["Param1"];
                var data = BL.CheckAnydonorisaccepted(Param1);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region ResetPasswordByEmail ---Asif
        [HttpPost]
        public dynamic ResetPasswordByEmail()
        {
            try
            {
                var email = HttpContext.Current.Request.Form["Param1"];
                var newPassword = HttpContext.Current.Request.Form["Param2"];

                var result = BL.BG_UpdatePassword_ByEmail(email, newPassword);
                return result;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Send Forgot Password OTP ---Asif
        [HttpPost]
        public dynamic SendForgotPasswordOTP()
        {
            try
            {
                var email = HttpContext.Current.Request.Form["Email"];
                var otp = HttpContext.Current.Request.Form["OTP"];
                var name = HttpContext.Current.Request.Form["Name"];

                Mail mail = new Mail();
                return mail.SendForgotPasswordOTP(email, otp, name);
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Send Email Change OTP ---Asif
        [HttpPost]
        public dynamic SendEmailChangeOTP()
        {
            try
            {
                var email = HttpContext.Current.Request.Form["Email"];
                var otp = HttpContext.Current.Request.Form["OTP"];
                var name = HttpContext.Current.Request.Form["Name"];

                Mail mail = new Mail();
                return mail.SendEmailChangeOTP(email, otp, name);
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        public dynamic UpdateNotificationstatus()
        {
            try
            {
                var Param1 = HttpContext.Current.Request.Form["Param1"];
                var Param2 = HttpContext.Current.Request.Form["Param2"];

                var result = BL.UpdateNotificationstatus(Param1, Param2);
                return result;
            }
            catch(Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #region Get_PresentationDetails ---->Upendra
        [HttpPost]
        public dynamic Get_PresentationAcceptedCount()
        {
            try
            {
                var Param1 = HttpContext.Current.Request.Form["Param1"];
                var Param2 = HttpContext.Current.Request.Form["Param2"];
                var data = BL.Get_PresentationAcceptedCount(Param1, Param2);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region Get_PresentationDetails ---->Upendra
        [HttpPost]
        public dynamic Get_PresentationAccetedDetails()
        {
            try
            {
                var Param1 = HttpContext.Current.Request.Form["Param1"];
                var data = BL.Get_PresentationAcceptedDetails(Param1);
                return data;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region   CastMaster_crud     ------  Upendra
        [HttpPost]
        public dynamic CastMaster_crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.CastMaster_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region ReasonMaster_crud     ------  Upendra
        [HttpPost]
        public dynamic ReasonMaster_crud()
        {
            try
            {
                var Param = System.Web.HttpContext.Current.Request.Form["Param"];
                var Flag = System.Web.HttpContext.Current.Request.Form["Flag"];
                var MSG = BL.ReasonMaster_crud(Param, Flag);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region BloodAcceptedUser ------  Upendra
        [HttpPost]
        public dynamic BloodAcceptedUser()
        {
            try
            {
                var Param1 = System.Web.HttpContext.Current.Request.Form["Param1"];
                var Param2 = System.Web.HttpContext.Current.Request.Form["Param2"];
                var MSG = BL.BloodAcceptedUser(Param1, Param2);
                return MSG;
            }
            catch (System.Exception ex)
            {
                exp.ExceptionHandler(ex);
                BL.saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion
    }
}
   







