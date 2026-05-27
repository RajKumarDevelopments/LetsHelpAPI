using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.Json;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Http;
using System.Web.UI;
using System.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Security.Cryptography;
using static BloodGroupApi.Models.Models;

namespace BloodGroupApi.Models
{
    public class BusinessLogic
    {

        DataSet ds = new DataSet();
        Database db = new Database();
        XmlProviders xml = new XmlProviders();
        CrosHandler CHandler = new CrosHandler();
        XmlProviders XMLConvert = new XmlProviders();
        SqlConnection con = new SqlConnection();
        StringBuilder sb = new StringBuilder();
        Exceptions exp = new Exceptions();
        Mail mail = new Mail();

        //  PwdEncryptDecrypt Encryption = new PwdEncryptDecrypt();
        public string MSG { get; private set; }


        //public BusinessLogic(TranslationContext context)
        //{
        //    _context = context;
        //}




        #region SAVE EXCEPTIONS IN THE DATABASE --Naushad
        public void saveExceptions(string ex, string UserID, string source)
        {
            db.GetDataWithThreeParameters(SPS.BG_Webapi_Exceptions.ToString(), UserID, ex, source);
            // mail.MailForExceptions(ex);
        }
        #endregion

        #region Get Top 100 Exceptions-->Commented By SRINIVAS
        public dynamic getExceptions()
        {
            try
            {
                List<allExceptions> allExceptions = new List<allExceptions>();
                ds = db.GetData(SPS.INSERT_EXCEPTIONS.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new allExceptions()
                    {
                        ExID = c.Field<Int64?>("ExID") ?? 0,
                        Ex_UserID = c.Field<Int64?>("Ex_UserID") ?? 0,
                        Ex_ErrLine = c.Field<string>("Ex_ErrLine") + "",
                        Ex_ErrMsg = c.Field<string>("Ex_ErrMsg") + "",
                        Ex_ErrProc = c.Field<string>("Ex_ErrProc") + "",
                        Ex_Source = c.Field<string>("Ex_Source") + "",
                        CreatedDate = c.Field<DateTime?>("CreatedDate") ?? null
                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_Exceptions-->Commented By Upendra
        public dynamic Get_AExceptions(string Param1)
        {
            try
            {
                List<Get_Exceptions> Exceptions = new JavaScriptSerializer().Deserialize<List<Get_Exceptions>>(Param1);
                var Excepts = XMLConvert.GetXmlArrayString<Get_Exceptions>(Exceptions);
                ds = db.SaveDataReturnSingleXML(SPS.BG_GetAllExeption.ToString(), Excepts);
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    Exceptions = ds.Tables[0].AsEnumerable().Select(c => new Get_Exceptions()
                    {
                        ExID = c.Field<Int64?>("ExID") ?? 0,
                        Ex_UserID = c.Field<Int64?>("Ex_UserID") ?? 0,
                        Ex_ErrLine = c.Field<string>("Ex_ErrLine") + "",
                        Ex_ErrMsg = c.Field<string>("Ex_ErrMsg") + "",
                        Ex_ErrProc = c.Field<string>("Ex_ErrProc") + "",
                        Ex_Source = c.Field<string>("Ex_Source") + "",
                        CreatedDate = c.Field<DateTime?>("CreatedDate") ?? null,
                        CreatedTime = c.Field<string>("CreatedTime") ?? null,
                        Ex_ErrNum = c.Field<string>("Ex_ErrNum") + "",
                        Ex_ErrSeverity = c.Field<string>("Ex_ErrSeverity") + "",
                        Ex_ErrState = c.Field<string>("Ex_ErrState") + "",


                    }).ToList();
                }
                return Exceptions;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region GetExceptions-->Commented By Upendra
        public dynamic Get_Exceptions(string Xml, string Param1, string Param2)
        {
            try
            {
                var MSG = "";

                List<Get_Exceptions> Detailslist = new JavaScriptSerializer().Deserialize<List<Get_Exceptions>>(Xml);

                var Data = XMLConvert.GetXmlArrayString<Get_Exceptions>(Detailslist);

                ds = db.GetDataReturnSingleXMLandthreeParam(SPS.BG_GetExeption.ToString(), Data, Param1, Param2);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var checkDb = db.CheckDatainDS(ds, 0);
                    if (checkDb == true)
                    {

                        Detailslist = ds.Tables[0].AsEnumerable().Select(
                            c => new Get_Exceptions()
                            {
                                ExID = c.Field<Int64?>("ExID") ?? 0,
                                Ex_UserID = c.Field<Int64?>("Ex_UserID") ?? 0,
                                Ex_ErrLine = c.Field<string>("Ex_ErrLine") + "",
                                Ex_ErrMsg = c.Field<string>("Ex_ErrMsg") + "",
                                Ex_ErrProc = c.Field<string>("Ex_ErrProc") + "",
                                Ex_Source = c.Field<string>("Ex_Source") + "",
                                CreatedDate = c.Field<DateTime?>("CreatedDate") ?? null,
                                CreatedTime = c.Field<string>("CreatedTime") + "",
                                Ex_ErrNum = c.Field<string>("Ex_ErrNum") + "",
                                Ex_ErrSeverity = c.Field<string>("Ex_ErrSeverity") + "",
                                Ex_ErrState = c.Field<string>("Ex_ErrState") + "",

                            }).ToList();
                        return Detailslist;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return " ";
            }
        }

        #endregion 

        #region   StatesMaster_crud     ----Deeksha
        public dynamic StatesMaster_crud(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<StatesMaster_crud> data = new JavaScriptSerializer().Deserialize<List<StatesMaster_crud>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<StatesMaster_crud>(data);
                ds = db.SaveDataReturn1(SPS.StatesMaster_crud.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new StatesMaster_crud()
                            {

                                StateId = A.Field<Int32?>("StateId") + 0,
                                StateName = A.Field<string>("StateName") + "",
                                CountryId = A.Field<Int32?>("CountryId") + 0,
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return data;
                        }
                    }
                    // ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region   BloodGroupMaster_CRUD     ----Deeksha
        public dynamic BloodGroupMaster_CRUD(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<BloodGroupMaster_CRUD> data = new JavaScriptSerializer().Deserialize<List<BloodGroupMaster_CRUD>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<BloodGroupMaster_CRUD>(data);
                ds = db.SaveDataReturn1(SPS.BloodGroupMaster_CRUD.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new BloodGroupMaster_CRUD()
                            {

                                BLGId = A.Field<Int32?>("BLGId") + 0,
                                BLGName = A.Field<string>("BLGName") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return data;
                        }
                    }
                    // ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region   DistrictMaster_crud     ----Amrutha
        public dynamic DistrictMaster_crud(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<DistrictMaster_crud> data = new JavaScriptSerializer().Deserialize<List<DistrictMaster_crud>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<DistrictMaster_crud>(data);
                ds = db.SaveDataReturn1(SPS.DistrictMaster_crud.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4" || flag == "5")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new DistrictMaster_crud()
                            {

                                DistrictID = A.Field<Int32?>("DistrictID") + 0,
                                DistrictName = A.Field<string>("DistrictName") + "",
                                StateName = A.Field<string>("StateName") + "",
                                StateId = A.Field<Int32?>("StateId") + 0,
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return data;
                        }
                    }
                    // ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region BG_dashboardcount --Deeksha, Amrutha
        public dynamic BG_dashboardcount()
        {
            try
            {
                List<BG_dashboardcount> allExceptions = new List<BG_dashboardcount>();
                ds = db.GetData(SPS.BG_dashboardcount.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(A =>
                    new BG_dashboardcount()
                    {
                        Donors = A.Field<Int32?>("Donors") ?? 0,
                        Leaders = A.Field<Int32?>("Leaders") ?? 0,
                        BrotureDownloaded = A.Field<Int32?>("BrotureDownloaded") ?? 0,
                        Approved = A.Field<Int32?>("Approved") ?? 0,
                        Pending = A.Field<Int32?>("Pending") ?? 0,
                        Requests = A.Field<Int32?>("Requests") ?? 0,
                        Aplus = A.Field<Int32?>("Aplus") ?? 0,
                        Aminus = A.Field<Int32?>("Aminus") ?? 0,
                        Bplus = A.Field<Int32?>("Bplus") ?? 0,
                        Bminus = A.Field<Int32?>("Bminus") ?? 0,
                        Oplus = A.Field<Int32?>("Oplus") ?? 0,
                        Ominus = A.Field<Int32?>("Ominus") ?? 0,
                        ABplus = A.Field<Int32?>("ABplus") ?? 0,
                        ABminus = A.Field<Int32?>("ABminus") ?? 0,
                        Bombay = A.Field<Int32?>("Bombay") ?? 0,
                        HsptCount = A.Field<Int32?>("HsptCount") ?? 0,
                        OldageHome = A.Field<Int32?>("OldageHome") ?? 0,
                        Orphanage = A.Field<Int32?>("Orphanage") ?? 0,
                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region   UnitsofBlood_CRUD     ----Amrutha
        public dynamic UnitsofBlood_CRUD(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<UnitsofBlood_CRUD> data = new JavaScriptSerializer().Deserialize<List<UnitsofBlood_CRUD>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<UnitsofBlood_CRUD>(data);
                ds = db.SaveDataReturn1(SPS.UnitsofBlood_CRUD.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new UnitsofBlood_CRUD()
                            {

                                UnitsofBloodId = A.Field<Int32?>("UnitsofBloodId") + 0,
                                UnitsofBlood = A.Field<string>("UnitsofBlood") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return data;
                        }
                    }
                    // ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Oldagehome_CRUD  ----Amrutha
        public dynamic Oldagehome_CRUD(string xmlParam, string flag)
        {
            try
            {

                var MSG = "";
                List<Oldagehome_CRUD> Data1 = new JavaScriptSerializer().Deserialize<List<Oldagehome_CRUD>>(xmlParam);
                string XML = XMLConvert.GetXmlArrayString<Oldagehome_CRUD>(Data1);
                ds = db.SaveDataReturn1(SPS.Oldagehome_CRUD.ToString(), XML, flag);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2" || flag == "4" || flag == "5")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                    else if (flag == "3")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(
                            A => new Oldagehome_CRUD()
                            {
                                Old_ID = A.Field<Int32?>("Old_ID") + 0,
                                HomeName = A.Field<string>("HomeName") + "",
                                Address = A.Field<string>("Address") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                                PhoneNumber = A.Field<string>("PhoneNumber") + "",
                                StateId = A.Field<Int32?>("StateId") + 0,
                                DistrictID = A.Field<Int32?>("DistrictID") + 0,
                                CityId = A.Field<Int32?>("CityId") + 0,
                                StateName = A.Field<string>("StateName") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                Latitude = A.Field<string>("Latitude") + "",
                                Longitude = A.Field<string>("Longitude") + "",
                                SiteURL = A.Field<string>("SiteURL") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                FullName = A.Field<string>("FullName") + "",
                                CreatedByMobile = A.Field<string>("CreatedByMobile") + "",
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,



                            }).ToList();
                        return Data1;

                    }
                }
                ds.Clear();
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region HospitalDetails_CRUD  ----Amrutha,deeksha
        public dynamic HospitalDetails_CRUD(string xmlParam, string flag)
        {
            try
            {

                var MSG = "";
                List<HospitalDetails_CRUD> Data1 = new JavaScriptSerializer().Deserialize<List<HospitalDetails_CRUD>>(xmlParam);
                string XML = XMLConvert.GetXmlArrayString<HospitalDetails_CRUD>(Data1);
                ds = db.SaveDataReturn1(SPS.HospitalDetails_CRUD.ToString(), XML, flag);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2" || flag == "3" || flag == "5" || flag == "7")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                    else if (flag == "4")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(
                            A => new HospitalDetails_CRUD()
                            {
                                HsptID = A.Field<Int32?>("HsptID") + 0,
                                HsptName = A.Field<string>("HsptName") + "",
                                HsptPhNum = A.Field<string>("HsptPhNum") + "",
                                HsptAddress = A.Field<string>("HsptAddress") + "",
                                DoctorName = A.Field<string>("DoctorName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                StateName = A.Field<string>("StateName") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                Latitude = A.Field<string>("Latitude") + "",
                                Longitude = A.Field<string>("Longitude") + "",
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                FullName = A.Field<string>("FullName") + "",
                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                ContactPerson = A.Field<string>("ContactPerson") + "",
                                ContactMobile = A.Field<string>("ContactMobile") + "",
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                POCName = A.Field<string>("POCName") + "",
                                POC_ContactNumber = A.Field<string>("POC_ContactNumber") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                                CityId = A.Field<Int32?>("CityId") + 0,
                                DistrictID = A.Field<Int32?>("DistrictID") + 0,
                                StateId = A.Field<Int32?>("StateId") + 0,
                                HStatus = A.Field<string>("HStatus") + "",
                                newStatename = A.Field<string>("newStatename") + "",
                                newDistrictname = A.Field<string>("newDistrictname") + "",
                                newCityname = A.Field<string>("newCityname") + "",
                            }).ToList();
                        return Data1;
                    }
                    else if (flag == "6")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(
                            A => new HospitalDetails_CRUD()
                            {
                                HsptName = A.Field<string>("HsptName") + "",
                                HsptID = A.Field<Int32?>("HsptID") + 0,

                            }).ToList();
                        return Data1;
                    }

                    else if (flag == "8")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(
                            A => new HospitalDetails_CRUD()
                            {
                                HsptID = A.Field<Int32?>("HsptID") + 0,
                                HsptName = A.Field<string>("HsptName") + "",
                                HsptPhNum = A.Field<string>("HsptPhNum") + "",
                                HsptAddress = A.Field<string>("HsptAddress") + "",
                                DoctorName = A.Field<string>("DoctorName") + "",
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                POCName = A.Field<string>("POCName") + "",
                                POC_ContactNumber = A.Field<string>("POC_ContactNumber") + "",
                                Pincode = A.Field<string>("Pincode") + "",

                            }).ToList();
                        return Data1;
                    }

                }

                ds.Clear();
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion


        #region Download_Brochures  --Deeksha,Amrutha
        public dynamic Download_Brochures()
        {
            try
            {
                List<Download_Brochures> userdata = new List<Download_Brochures>();
                ds = db.GetData(SPS.Download_Brochures.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Download_Brochures
                    {
                        Brochureid = A.Field<Int32?>("Brochureid") + 0,
                        StateName = A.Field<string>("StateName") + "",
                        DistrictName = A.Field<string>("DistrictName") + "",
                        CityName = A.Field<string>("CityName") + "",
                        newStatename = A.Field<string>("newStatename") + "",
                        newDistrictname = A.Field<string>("newDistrictname") + "",
                        newCityname = A.Field<string>("newCityname") + "",
                        Pincode = A.Field<string>("Pincode") + "",
                        Place = A.Field<string>("Place") + "",
                        Place_name = A.Field<string>("Place_name") + "",
                        Createdby = A.Field<Int32?>("Createdby") + 0,
                        FullName = A.Field<string>("FullName") + "",
                        Phonenumber = A.Field<string>("Phonenumber") + "",
                        Location = A.Field<string>("Location") + "",
                        Status = A.Field<Boolean?>("Status") ?? null,
                        Createddate = A.Field<DateTime?>("Createddate") ?? null,
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        public dynamic Get_ShareYourService(string Param1)
        {
            try
            {
                List<ShareYourService> services = new List<ShareYourService>();

                ds = db.GetDataWithSingleParam(SPS.ShareYourService.ToString(), Param1);

                bool hasData = db.CheckDatainDS(ds, 0);

                // If no data at all return empty list
                if (!hasData || ds.Tables[0].Rows.Count == 0)
                {
                    return new List<ShareYourService>();
                }

                // Check first column of first row for "Invalid User"
                var firstValue = ds.Tables[0].Rows[0][0]?.ToString();
                if (firstValue == "Invalid User")
                {
                    return "Invalid User"; // only in this case
                }

                // Map dataset rows into model
                services = ds.Tables[0].AsEnumerable().Select(A => new ShareYourService()
                {
                    FullName = A.Field<string>("FullName") ?? "",
                    GalleryID = A.Field<int>("GalleryID"),
                    //StateName = A.Field<string>("StateName") ?? "",
                    //DistrictName = A.Field<string>("DistrictName") ?? "",
                    //CityName = A.Field<string>("CityName") ?? "",
                    newStatename = A.Field<string>("newStatename") ?? "",
                    newDistrictname = A.Field<string>("newDistrictname") ?? "",
                    newCityname = A.Field<string>("newCityname") ?? "",
                    Institutionname = A.Field<string>("Institutionname") ?? "",
                    GalleryImages = A.Field<string>("GalleryImages") ?? "",
                    Message = A.Field<string>("Message") ?? "",
                    categoryname = A.Field<string>("categoryname") ?? "",
                    Email = A.Field<string>("Email") ?? "",
                    Dateofservice = A.Field<DateTime?>("Dateofservice"),
                    CreatedDate = A.Field<DateTime?>("CreatedDate"),
                }).ToList();

                return services;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw;
            }
        }



        // #region
        // public dynamic Get_ShareYourService(string Param1)
        // {
        //     try
        //     {
        //         var MSG = "";
        //         List<ShareYourService> login = new List<ShareYourService>();
        //         ds = db.GetDataWithSingleParam(SPS.ShareYourService.ToString(), Param1);
        //         bool checkdata = db.CheckDatainDS(ds, 0);
        //         MSG = ds.Tables[0].Rows[0][0].ToString();
        //         if (MSG == "Invalid User")
        //         {
        //             return MSG;
        //         }
        //         else
        //         {
        //             if (checkdata == true)
        //             {
        //                 login = ds.Tables[0].AsEnumerable().Select(A => new ShareYourService()
        //                 {
        //                     FullName = A.Field<string>("FullName") + "",
        //                     GalleryID = A.Field<Int32>("GalleryID") + 0,
        //                     StateName = A.Field<string>("StateName") + "",
        //                     DistrictName = A.Field<string>("DistrictName") + "",
        //                     CityName = A.Field<string>("CityName") + "",
        //                     Institutionname = A.Field<string>("Institutionname") + "",
        //                     GalleryImages = A.Field<string>("GalleryImages") + "",
        //                     Message = A.Field<string>("Message") + "",
        //                     categoryname = A.Field<string>("categoryname") + "",
        //                     Dateofservice = A.Field<DateTime?>("Dateofservice") ?? null,
        //                     CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
        //                 }).ToList();
        //             }
        //             return login;

        //         }

        //     }
        //     catch (Exception ex)
        //     {
        //         saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
        //         exp.ExceptionHandler(ex);
        //         throw ex;
        //     }
        // }
        //#endregion



        #region   Donorlist_crud     ----Naushad
        public dynamic Donorlist_crud(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<Donor> data = new JavaScriptSerializer().Deserialize<List<Donor>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<Donor>(data);
                ds = db.SaveDataReturn1(SPS.Donorlist_crud.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new Donor()
                            {

                                RegId = A.Field<Int32?>("RegId") + 0,
                                RoleId = A.Field<Int32?>("RoleId") + 0,
                                FullName = A.Field<string>("FullName") + "",
                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                Password = A.Field<string>("Password") + "",
                                Email = A.Field<string>("Email") + "",
                                Age = A.Field<string>("Age") + "",
                                Gender = A.Field<string>("Gender") + "",
                                TokenId = A.Field<Guid>("TokenId") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                BLGName = A.Field<string>("BLGName") + "",
                                StateName = A.Field<string>("StateName") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                UserAddress = A.Field<string>("UserAddress") + "",
                                Area = A.Field<string>("Area") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                                Addedby = A.Field<string>("Addedby") + "",

                            }).ToList();
                            return data;
                        }
                    }
                    ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region   Recepient_crud     ----Naushad
        public dynamic Recepient_crud(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<Recepient> data = new JavaScriptSerializer().Deserialize<List<Recepient>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<Recepient>(data);
                ds = db.SaveDataReturn1(SPS.Recepient_crud.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4" || flag == "5")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new Recepient()
                            {

                                RegId = A.Field<Int32?>("RegId") + 0,
                                UdId = A.Field<Int32?>("UdId") + 0,
                                RoleId = A.Field<Int32?>("RoleId") + 0,
                                FullName = A.Field<string>("FullName") + "",
                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                Password = A.Field<string>("Password") + "",
                                Email = A.Field<string>("Email") + "",
                                Age = A.Field<string>("Age") + "",
                                Devicetoken = A.Field<string>("Devicetoken") + "",
                                Gender = A.Field<string>("Gender") + "",
                                Creatername = A.Field<string>("Creatername") + "",
                                Acceptedby = A.Field<string>("Acceptedby") + "",
                                TokenId = A.Field<Guid>("TokenId") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,

                                ApprovedDate = A.Field<DateTime?>("ApprovedDate") ?? null,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                BLGName = A.Field<string>("BLGName") + "",
                                UnitsofBlood = A.Field<string>("UnitsofBlood") + "",
                                StateName = A.Field<string>("StateName") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                newStatename = A.Field<string>("newStatename") + "",
                                newDistrictname = A.Field<string>("newDistrictname") + "",
                                newCityname = A.Field<string>("newCityname") + "",
                                BloodRequestDate = A.Field<string>("BloodRequestDate") + "",
                                RequestTime = A.Field<string>("RequestTime") + "",
                                BloodDonationImage = A.Field<string>("BloodDonationImage") + "",
                                Receiptimage = A.Field<string>("Receiptimage") + "",
                                HospitalAddress = A.Field<string>("HospitalAddress") + "",
                                Area = A.Field<string>("Area") + "",
                                HospitalName = A.Field<string>("HospitalName") + "",
                                DoctorName = A.Field<string>("DoctorName") + "",
                                Purpose = A.Field<string>("Purpose") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                                ContactPerson = A.Field<string>("ContactPerson") + "",
                                ContactMobile = A.Field<string>("ContactMobile") + "",
                                ApprovalStatus = A.Field<Int32?>("ApprovalStatus") + 0,
                                userid = A.Field<Int32?>("userid") + 0,

                            }).ToList();
                            return data;
                        }
                    }
                    ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region  BG_Brochers_Crud  --manohar
        public dynamic BG_Brochers_Crud(string xmlparam, string flag)
        {
            try
            {
                var MSG = "";
                List<Brocheres> userdata = new JavaScriptSerializer().Deserialize<List<Brocheres>>(xmlparam);
                var data = XMLConvert.GetXmlArrayString<Brocheres>(userdata);
                ds = db.SaveDataReturn1(SPS.BG_Brochers_Crud.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdata = db.CheckDatainDS(ds, 0);
                    if (checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4" )
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new Brocheres()
                            {
                                BroID = A.Field<Int32?>("BroID") + 0,
                                BrochureName = A.Field<string>("BrochureName") + "",
                                BrochurePath = A.Field<string>("BrochurePath") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null
                            }).ToList();
                            return userdata;
                        }
                    }
                    //  ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Insert_Update_DonersForm -- manohar
        public dynamic Insert_Update_DonersForm(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<Doners> userdata = new JavaScriptSerializer().Deserialize<List<Doners>>(xml);
                var data = XMLConvert.GetXmlArrayString<Doners>(userdata);
                ds = db.SaveDataReturn1(SPS.Insert_Update_DonersForm.ToString(), data, flag);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2" || flag == "3" || flag == "4" || flag == "5" || flag == "6" || flag == "7")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Insert_Update_requestForm -- manohar
        public dynamic Insert_Update_requestForm(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<Doners> userdata = new JavaScriptSerializer().Deserialize<List<Doners>>(xml);
                var data = XMLConvert.GetXmlArrayString<Doners>(userdata);
                ds = db.SaveDataReturn1(SPS.Insert_Update_requestForm.ToString(), data, flag);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2" || flag == "3" || flag == "4" || flag == "5")

                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region BG_CustomersLoginByStatus
        public dynamic BG_CustomersLoginByStatus(string Param1)
        {
            try
            {
                var MSG = "";
                List<Logins> login = new List<Logins>();
                ds = db.GetDataWithSingleParam(SPS.CustomersLoginByStatus.ToString(), Param1);
                bool checkdata = db.CheckDatainDS(ds, 0);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                if (MSG == "Invalid User")
                {
                    return MSG;
                }
                else
                {
                    if (checkdata == true)
                    {
                        login = ds.Tables[0].AsEnumerable().Select(A => new Logins()
                        {
                            RegId = A.Field<Int32?>("RegId") + 0,
                            RoleId = A.Field<Int32?>("RoleId") + 0,
                            Weight = A.Field<Int32?>("Weight") + 0,
                            FullName = A.Field<string>("FullName") + "",
                            Phonenumber = A.Field<string>("Phonenumber") + "",
                            Password = A.Field<string>("Password") + "",
                            Email = A.Field<string>("Email") + "",
                            PastDonation = A.Field<string>("PastDonation") + "",
                            Age = A.Field<string>("Age") + "",
                            Gender = A.Field<string>("Gender") + "",
                            TokenId = A.Field<Guid>("TokenId") + "",
                            Availablestatus = A.Field<Boolean?>("Availablestatus") ?? null,
                            Status = A.Field<Boolean?>("Status") ?? null,
                            Statusphn = A.Field<Boolean?>("Statusphn") ?? null,
                            EmailStatus = A.Field<Int32?>("EmailStatus") ?? null,
                            WhatappStatus = A.Field<Int32?>("WhatappStatus") ?? null,
                            SMSStatus = A.Field<Int32?>("SMSStatus") ?? null,
                            CallStatus = A.Field<Int32?>("CallStatus") ?? null,
                            Rolestatus = A.Field<Boolean?>("Rolestatus") ?? null,
                            CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                            CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                            DOB = A.Field<string>("DOB") + "",
                            Lastdonatedate = A.Field<string>("Lastdonatedate") + "",
                            ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                            ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                            BLGName = A.Field<string>("BLGName") + "",
                            StateName = A.Field<string>("StateName") + "",
                            Pincode = A.Field<string>("Pincode") + "",
                            DistrictName = A.Field<string>("DistrictName") + "",
                            CityName = A.Field<string>("CityName") + "",
                            UserAddress = A.Field<string>("UserAddress") + "",
                            Area = A.Field<string>("Area") + "",
                            Messagetorequester = A.Field<string>("Messagetorequester") + "",
                            CityId = A.Field<Int32?>("CityId") + 0,
                            DistrictID = A.Field<Int32?>("DistrictID") + 0,
                            BLGId = A.Field<Int32?>("BLGId") + 0,
                            StateId = A.Field<Int32?>("StateId") + 0,
                            Categoryid = A.Field<Int32?>("Categoryid") + 0,
                            ReffererId = A.Field<Int32?>("ReffererId") + 0,
                            Refferpoints = A.Field<Int32?>("Refferpoints") + 0,
                            Reffercode = A.Field<string>("Reffercode") + "",
                            imguploaddate = A.Field<DateTime?>("imguploaddate") ?? null,


                            //Notificationstatus = A.Field<Boolean?>("Notificationstatus") ?? null,
                            Currentstatus = A.Field<Boolean?>("Currentstatus") ?? null,
                            ExpairyDate = A.Field<DateTime?>("ExpairyDate") ?? null
                        }).ToList();
                    }
                    return login;

                }

            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion


        #region BG_Admin_Login --Naushad
        public dynamic BG_Admin_Login(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                List<Logins> login = new List<Logins>();
                ds = db.GetDataWithTwoParameters(SPS.BG_Admin_Login.ToString(), Param1, Param2);
                bool checkdata = db.CheckDatainDS(ds, 0);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                if (MSG == "Invalid User")
                {
                    return MSG;
                }
                else
                {
                    if (checkdata == true)
                    {
                        login = ds.Tables[0].AsEnumerable().Select(A => new Logins()
                        {
                            RegId = A.Field<Int32?>("RegId") + 0,
                            RoleId = A.Field<Int32?>("RoleId") + 0,
                            Weight = A.Field<Int32?>("Weight") + 0,
                            FullName = A.Field<string>("FullName") + "",
                            Phonenumber = A.Field<string>("Phonenumber") + "",
                            Password = A.Field<string>("Password") + "",
                            Email = A.Field<string>("Email") + "",
                            PastDonation = A.Field<string>("PastDonation") + "",
                            Age = A.Field<string>("Age") + "",
                            Gender = A.Field<string>("Gender") + "",
                            TokenId = A.Field<Guid>("TokenId") + "",
                            Availablestatus = A.Field<Boolean?>("Availablestatus") ?? null,
                            Status = A.Field<Boolean?>("Status") ?? null,
                            Statusphn = A.Field<Boolean?>("Statusphn") ?? null,
                            EmailStatus = A.Field<Int32?>("EmailStatus") ?? null,
                            WhatappStatus = A.Field<Int32?>("WhatappStatus") ?? null,
                            Rolestatus = A.Field<Boolean?>("Rolestatus") ?? null,
                            CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                            CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                            DOB = A.Field<string>("DOB") + "",
                            Lastdonatedate = A.Field<string>("Lastdonatedate") + "",
                            ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                            ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                            BLGName = A.Field<string>("BLGName") + "",
                            StateName = A.Field<string>("StateName") + "",
                            Pincode = A.Field<string>("Pincode") + "",
                            DistrictName = A.Field<string>("DistrictName") + "",
                            CityName = A.Field<string>("CityName") + "",
                            UserAddress = A.Field<string>("UserAddress") + "",
                            Area = A.Field<string>("Area") + "",
                            Messagetorequester = A.Field<string>("Messagetorequester") + "",
                            CityId = A.Field<Int32?>("CityId") + 0,
                            DistrictID = A.Field<Int32?>("DistrictID") + 0,
                            BLGId = A.Field<Int32?>("BLGId") + 0,
                            StateId = A.Field<Int32?>("StateId") + 0,
                            Categoryid = A.Field<Int32?>("Categoryid") + 0,
                            ReffererId = A.Field<Int32?>("ReffererId") + 0,
                            Refferpoints = A.Field<Int32?>("Refferpoints") + 0,
                            Reffercode = A.Field<string>("Reffercode") + "",
                            newStatename = A.Field<string>("newStatename") + "",
                            newDistrictname = A.Field<string>("newDistrictname") + "",
                            newCityname = A.Field<string>("newCityname") + "",
                            imguploaddate = A.Field<DateTime?>("imguploaddate") ?? null,


                        }).ToList();
                    }
                    return login;

                }

            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Register_User_Curd  ----suraj
        public dynamic Register_User_Curd(string xmlParam, string flag)
        {
            try
            {

                var MSG = "";
                List<Doners> Data1 = new JavaScriptSerializer().Deserialize<List<Doners>>(xmlParam);
                string XML = XMLConvert.GetXmlArrayString<Doners>(Data1);
                ds = db.SaveDataReturn1(SPS.Register_User_Curd.ToString(), XML, flag);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2" || flag == "3")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                    else if (flag == "4")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(
                            A => new Doners()
                            {
                                RegId = A.Field<int?>("RegId") ?? 0,
                                RoleId = A.Field<int?>("RoleId") ?? 0,
                                Devicetoken = A.Field<string>("Devicetoken") ?? "",
                                FullName = A.Field<string>("FullName") ?? "",
                                //Pincode = A.Field<string>("Pincode") ?? "",

                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                Password = A.Field<string>("Password") + "",
                                Email = A.Field<string>("Email") + "",
                                Age = A.Field<string>("Age") + "",
                                Gender = A.Field<string>("Gender") + "",
                                TokenId = A.Field<Guid?>("TokenId")?.ToString() ?? "",
                                Rolestatus = A.Field<bool?>("Rolestatus")
                            }).ToList();
                        return Data1;
                    }

                    else if (flag == "6")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(
                            A => new Doners()
                            {
                                RegId = A.Field<int?>("RegId") ?? 0,
                                RoleId = A.Field<int?>("RoleId") ?? 0,
                                Devicetoken = A.Field<string>("Devicetoken") ?? "",
                                FullName = A.Field<string>("FullName") ?? "",
                                Pincode = A.Field<string>("Pincode") ?? "",

                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                Password = A.Field<string>("Password") + "",
                                Email = A.Field<string>("Email") + "",
                                Age = A.Field<string>("Age") + "",
                                Gender = A.Field<string>("Gender") + "",
                                TokenId = A.Field<Guid?>("TokenId")?.ToString() ?? "",
                                Rolestatus = A.Field<bool?>("Rolestatus")
                            }).ToList();
                        return Data1;
                    }
                    else if (flag == "5")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(A => new Doners()
                        {
                            RegId = A.Field<int?>("RegId") ?? 0,
                            Devicetoken = A.Field<string>("Devicetoken") ?? "",
                            FullName = A.Field<string>("FullName") ?? "",
                            ContactMobile = A.Field<string>("ContactMobile") ?? "",
                            Phonenumber = A.Field<string>("Phonenumber") ?? "",
                            HospitalAddress = A.Field<string>("HospitalAddress") + "",
                            UserAddress = A.Field<string>("UserAddress") + "",
                            HospitalName = A.Field<string>("HospitalName") + "",
                            newStatename = A.Field<string>("newStatename") + "",
                            newDistrictname = A.Field<string>("newDistrictname") + "",

                            Area = A.Field<string>("Area") + "",
                            //DistrictName = A.Field<string>("DistrictName") + "",
                            //CityName = A.Field<string>("CityName") + "",
                            CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                            Email = A.Field<string>("Email") + "",
                            Age = A.Field<string>("Age") + "",
                            Gender = A.Field<string>("Gender") + "",
                            newCityname = A.Field<string>("newCityname") + "",
                            Pincode = A.Field<string>("Pincode") + "",
                            TokenId = A.Field<Guid?>("TokenId")?.ToString() ?? "",
                            Rolestatus = A.Field<bool?>("Rolestatus")
                        }).ToList();
                        return Data1;
                    }



                }

                ds.Clear();
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion









        #region Register_User_Curd -- prashnth
        public dynamic Register_User_Curd1(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<Doners> userdata = new JavaScriptSerializer().Deserialize<List<Doners>>(xml);
                //foreach (var donar in data)
                //{
                //    donar.Password = Encryption.Encrypt(donar.Password);
                //}
                var data = XMLConvert.GetXmlArrayString<Doners>(userdata);
                ds = db.SaveDataReturn1(SPS.Register_User_Curd.ToString(), data, flag);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                    else if (flag == "4")
                    {
                        userdata = ds.Tables[0].AsEnumerable().Select(A => new Doners()
                        {
                            RegId = A.Field<int?>("RegId") ?? 0,
                            Devicetoken = A.Field<string>("Devicetoken") ?? "",
                            FullName = A.Field<string>("FullName") ?? "",
                            //Pincode = A.Field<string>("Pincode") ?? "",

                            TokenId = A.Field<Guid?>("TokenId")?.ToString() ?? "",
                            Rolestatus = A.Field<bool?>("Rolestatus")
                        }).ToList();
                        return data;
                    }
                    else if (flag == "5")
                    {
                        userdata = ds.Tables[0].AsEnumerable().Select(A => new Doners()
                        {
                            RegId = A.Field<int?>("RegId") ?? 0,
                            Devicetoken = A.Field<string>("Devicetoken") ?? "",
                            FullName = A.Field<string>("FullName") ?? "",
                            //Pincode = A.Field<string>("Pincode") ?? "",
                            HospitalAddress = A.Field<string>("HospitalAddress") + "",
                            HospitalName = A.Field<string>("HospitalName") + "",
                            newStatename = A.Field<string>("newStatename") + "",
                            newDistrictname = A.Field<string>("newDistrictname") + "",

                            newCityname = A.Field<string>("newCityname") + "",
                            Pincode = A.Field<string>("Pincode") + "",
                            TokenId = A.Field<Guid?>("TokenId")?.ToString() ?? "",
                            Rolestatus = A.Field<bool?>("Rolestatus"),
                            CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,

                        }).ToList();
                        return data;
                    }

                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region   CitiesMaster_Crud     ----Naushad
        public dynamic CitiesMaster_Crud(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<CitiesMaster> data = new JavaScriptSerializer().Deserialize<List<CitiesMaster>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<CitiesMaster>(data);
                ds = db.SaveDataReturn1(SPS.CitiesMaster_Crud.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4" || flag == "5")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new CitiesMaster()
                            {

                                CityId = A.Field<Int32?>("CityId") + 0,
                                DistrictName = A.Field<string>("DistrictName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                DistrictId = A.Field<Int32?>("DistrictId") + 0,
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return data;
                        }
                    }
                    // ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region BG_GeetExeption-->Commented By Upendra
        public dynamic BG_GeetExeption(string Xml, string Param1, string Param2)
        {
            try
            {
                var MSG = "";

                List<Get_Exceptions> Detailslist = new JavaScriptSerializer().Deserialize<List<Get_Exceptions>>(Xml);

                var Data = XMLConvert.GetXmlArrayString<Get_Exceptions>(Detailslist);

                ds = db.GetDataReturnSingleXMLandthreeParam(SPS.BG_GeetExeption.ToString(), Data, Param1, Param2);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var checkDb = db.CheckDatainDS(ds, 0);
                    if (checkDb == true)
                    {

                        Detailslist = ds.Tables[0].AsEnumerable().Select(
                            c => new Get_Exceptions()
                            {
                                ExID = c.Field<Int64?>("ExID") ?? 0,
                                Ex_UserID = c.Field<Int64?>("Ex_UserID") ?? 0,
                                Ex_ErrLine = c.Field<string>("Ex_ErrLine") + "",
                                Ex_ErrMsg = c.Field<string>("Ex_ErrMsg") + "",
                                Ex_ErrProc = c.Field<string>("Ex_ErrProc") + "",
                                Ex_Source = c.Field<string>("Ex_Source") + "",
                                CreatedDate = c.Field<DateTime?>("CreatedDate") ?? null,
                                CreatedTime = c.Field<string>("CreatedTime") + "",
                                Ex_ErrNum = c.Field<string>("Ex_ErrNum") + "",
                                Ex_ErrSeverity = c.Field<string>("Ex_ErrSeverity") + "",
                                Ex_ErrState = c.Field<string>("Ex_ErrState") + "",

                            }).ToList();
                        return Detailslist;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return " ";
            }
        }

        #endregion

        #region Orphanage_CRUD  ----Naushad
        public dynamic Orphanage_CRUD(string xmlParam, string flag)
        {
            try
            {

                var MSG = "";
                List<Orphanages> Data1 = new JavaScriptSerializer().Deserialize<List<Orphanages>>(xmlParam);
                string XML = xml.GetXmlArrayString<Orphanages>(Data1);
                ds = db.SaveDataReturn1(SPS.Orphanage_CRUD.ToString(), XML, flag);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                    else if (flag == "4")
                    {
                        Data1 = ds.Tables[0].AsEnumerable().Select(
                            A => new Orphanages()
                            {
                                Orph_ID = A.Field<Int32?>("Orph_ID") + 0,
                                Orphanage_Name = A.Field<string>("Orphanage_Name") + "",
                                Address = A.Field<string>("Address") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                                PhoneNumber = A.Field<string>("PhoneNumber") + "",
                                StateId = A.Field<Int32?>("StateId") + 0,
                                DistrictID = A.Field<Int32?>("DistrictID") + 0,
                                CityId = A.Field<Int32?>("CityId") + 0,
                                Latitude = A.Field<string>("Latitude") + "",
                                StateName = A.Field<string>("StateName") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                Longitude = A.Field<string>("Longitude") + "",
                                SiteURL = A.Field<string>("SiteURL") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                FullName = A.Field<string>("FullName") + "",
                                CreatedByMobile = A.Field<string>("CreatedByMobile") + "",
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,



                            }).ToList();
                        return Data1;

                    }
                }
                ds.Clear();
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region    Delete_Exception  --Naushad
        public dynamic Delete_Exceptions(string id)
        {
            try
            {
                var MSG = "";

                List<Get_Exceptions> registared = new JavaScriptSerializer().Deserialize<List<Get_Exceptions>>(id);

                var UserData = XMLConvert.GetXmlArrayString<Get_Exceptions>(registared);
                ds = db.SaveDataReturnSingleXML(SPS.Delete_Exceptions.ToString(), UserData);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;

            }
            catch (Exception ex)
            {

                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return " ";
            }
        }
        #endregion

        #region   Gallery_Crud     -- Upendra
        public dynamic Gallery_Crud(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<Gallery> data = new JavaScriptSerializer().Deserialize<List<Gallery>>(xmlParam);
                var UserData = xml.GetXmlArrayString<Gallery>(data);
                ds = db.SaveDataReturn1(SPS.Gallery_Crud.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {

                            data = ds.Tables[0].AsEnumerable().Select(
                                A => new Gallery()
                                {
                                    GalleryID = A.Field<Int32?>("GalleryID") + 0,
                                    GalleryImages = A.Field<string>("GalleryImages") + "",
                                    RegId = A.Field<Int32?>("RegId") + 0,
                                    Categoryid = A.Field<Int32?>("Categoryid") + 0,
                                    Dateofservice = A.Field<DateTime?>("Dateofservice") ?? null,
                                    State = A.Field<Int32?>("State") + 0,
                                    District = A.Field<Int32?>("District") + 0,
                                    City = A.Field<Int32?>("City") + 0,
                                    Institutionname = A.Field<string>("Institutionname") + "",
                                    Message = A.Field<string>("Message") + "",
                                    Status = A.Field<Boolean?>("Status") ?? null,
                                    CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                    CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                    ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                    ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                    CategoryName = A.Field<string>("CategoryName") + "",


                                }).ToList();
                            return data;
                        }
                        else if (flag == "5")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                                A => new Gallery()
                                {
                                    //GalleryID = A.Field<Int32?>("GalleryID") + 0,
                                    GalleryImages = A.Field<string>("GalleryImages") + "",
                                    RegId = A.Field<Int32?>("RegId") + 0,
                                    Email = A.Field<string>("Email") + "",

                                    Gender = A.Field<string>("Gender") + "",
                                    Phonenumber = A.Field<string>("Phonenumber") + "",

                                    Institutionname = A.Field<string>("Institutionname") + "",
                                    Age = A.Field<string>("Age") + "",
                                    //Status = A.Field<Boolean?>("Status") ?? null,
                                    CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                    CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                    //ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                    //ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                                    HospitalPhonenumber = A.Field<string>("HospitalPhonenumber") + "",
                                    FullName = A.Field<string>("FullName") + "",

                                    HospitalAddress = A.Field<string>("HospitalAddress") + "",
                                    HospitalName = A.Field<string>("HospitalName") + "",
                                    newStatename = A.Field<string>("newStatename") + "",
                                    newDistrictname = A.Field<string>("newDistrictname") + "",

                                    newCityname = A.Field<string>("newCityname") + "",
                                    Pincode = A.Field<string>("Pincode") + "",

                                }).ToList();
                            return data;
                        }
                        else if (flag == "6")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                                A => new Gallery()
                                {
                                    RoleId = A.Field<Int32?>("RoleId") + 0,
                                    GalleryImages = A.Field<string>("GalleryImages") + "",
                                    RegId = A.Field<Int32?>("RegId") + 0,
                                    //Email = A.Field<string>("Email") + "",

                                    //Gender = A.Field<string>("Gender") + "",
                                    //Phonenumber = A.Field<string>("Phonenumber") + "",

                                    //Institutionname = A.Field<string>("Institutionname") + "",
                                    //Age = A.Field<string>("Age") + "",
                                    ////Status = A.Field<Boolean?>("Status") ?? null,
                                    //CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                    CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                    ////ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                    ////ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                                    //HospitalPhonenumber = A.Field<string>("HospitalPhonenumber") + "",
                                    //FullName = A.Field<string>("FullName") + "",

                                    //HospitalAddress = A.Field<string>("HospitalAddress") + "",
                                    //HospitalName = A.Field<string>("HospitalName") + "",
                                    //newStatename = A.Field<string>("newStatename") + "",
                                    //newDistrictname = A.Field<string>("newDistrictname") + "",

                                    //newCityname = A.Field<string>("newCityname") + "",
                                    //Pincode = A.Field<string>("Pincode") + "",

                                }).ToList();
                            return data;
                        }
                    }
                    ds.Clear();
                }
                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region CRUD_MailSmtpDetails --Bhanu
        public dynamic Insert_MailSmtpDetails(string Xml, string flag)
        {
            try
            {
                var MSG = "";
                List<SmtpDetails> data = new JavaScriptSerializer().Deserialize<List<SmtpDetails>>(Xml);
                // flag = Encryption.Encrypt(flag);
                var UserData = XMLConvert.GetXmlArrayString<SmtpDetails>(data);

                ds = db.SaveDataReturnSingleXMLandSingleParam(SPS.BG_MailCrud.ToString(), UserData, flag);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {

                    MSG = db.GetMessage(ds);
                    return MSG;





                }
                ds.Clear();
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region GetMailsmtpdetails--->Bhanu
        public dynamic GetMailsmtpdetails()
        {
            try
            {
                List<SmtpDetails> Dashboard = new List<SmtpDetails>();
                ds = db.GetData(SPS.GetSmtpDetails.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    Dashboard = ds.Tables[0].AsEnumerable().Select(A => new SmtpDetails()
                    {
                        Id = A.Field<Int32?>("Id") + 0,
                        host = A.Field<string>("host") + "",
                        fromaddress = A.Field<string>("fromaddress") + "",
                        frompassword = A.Field<string>("frompassword") + "",
                        ToAddress = A.Field<string>("ToAddress") + "",
                        BccAddress = A.Field<string>("BccAddress") + "",
                        enableSsl = A.Field<Boolean?>("enableSsl") ?? null,
                        port = A.Field<int>("port") + 0,

                    }).ToList();
                }
                return Dashboard;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;

            }
        }
        #endregion

        #region Approve_Requests --Asif
        public dynamic Approve_Requests(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithTwoParameters(SPS.Approve_Requests.ToString(), Param1, Param2);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {

                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return " ";
            }
        }

        #endregion

        #region checking_Mobile --- deeksha,amrutha
        public dynamic checking_Mobile(string Param1)
        {
            try
            {
                var MSG = "";

                List<Donor> data = new List<Donor>();
                ds = db.GetDataWithSingleParam(SPS.Get_donor.ToString(), Param1);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                if (MSG == "NOTEXIST")
                {
                    return MSG;
                }
                else
                {
                    bool checkdb = db.CheckDatainDS(ds, 0);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (checkdb == true)
                        {
                            data = ds.Tables[0].AsEnumerable().Select(A => new Donor
                            {

                                RegId = A.Field<Int32?>("RegId") + 0,
                                RoleId = A.Field<Int32?>("RoleId") + 0,

                                Weight = A.Field<Int32?>("Weight") + 0,
                                FullName = A.Field<string>("FullName") + "",
                                Devicetoken = A.Field<string>("Devicetoken") + "",
                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                Password = A.Field<string>("Password") + "",
                                Email = A.Field<string>("Email") + "",
                                PastDonation = A.Field<string>("PastDonation") + "",
                                Age = A.Field<string>("Age") + "",
                                Gender = A.Field<string>("Gender") + "",
                                TokenId = A.Field<Guid>("TokenId") + "",
                                Availablestatus = A.Field<Boolean?>("Availablestatus") ?? null,
                                Status = A.Field<Boolean?>("Status") ?? null,
                                Statusphn = A.Field<Boolean?>("Statusphn") ?? null,
                                Rolestatus = A.Field<Boolean?>("Rolestatus") ?? null,
                                EmailStatus = A.Field<Int32?>("EmailStatus") + 0,
                                WhatappStatus = A.Field<Int32?>("WhatappStatus") + 0,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                DOB = A.Field<string>("DOB") + "",
                                Lastdonatedate = A.Field<string>("Lastdonatedate") + "",
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                BLGName = A.Field<string>("BLGName") + "",
                                StateName = A.Field<string>("StateName") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                UserAddress = A.Field<string>("UserAddress") + "",
                                Area = A.Field<string>("Area") + "",
                                Messagetorequester = A.Field<string>("Messagetorequester") + "",
                                CityId = A.Field<Int32?>("CityId") + 0,
                                DistrictID = A.Field<Int32?>("DistrictID") + 0,
                                BLGId = A.Field<Int32?>("BLGId") + 0,
                                StateId = A.Field<Int32?>("StateId") + 0,
                                Categoryid = A.Field<Int32?>("Categoryid") + 0,
                                ReffererId = A.Field<Int32?>("ReffererId") + 0,
                                Refferpoints = A.Field<Int32?>("Refferpoints") + 0,
                                Reffercode = A.Field<string>("Reffercode") + "",
                                newStatename = A.Field<string>("newStatename") + "",
                                newDistrictname = A.Field<string>("newDistrictname") + "",
                                newCityname = A.Field<string>("newCityname") + "",
                                imguploaddate = A.Field<DateTime?>("imguploaddate") ?? null,

                            }).ToList();
                            return data;
                        }
                    }
                    return MSG;
                }
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region Get_Requestform  --manohar
        public dynamic Get_Requestform(string Param1)
        {
            try
            {
                List<Request> userdata = new List<Request>();
                ds = db.GetDataWithSingleParam(SPS.Get_Requestform.ToString(), Param1);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Request
                    {
                        RegId = A.Field<Int32?>("RegId") + 0,
                        Requestedby = A.Field<Int32?>("Requestedby") + 0,
                        UdId = A.Field<Int32?>("UdId") + 0,
                        FullName = A.Field<string>("FullName") + "",
                        ContactMobile = A.Field<string>("ContactMobile") + "",
                        BLGName = A.Field<string>("BLGName") + "",
                        Gender = A.Field<string>("Gender") + "",
                        Typesofblood = A.Field<string>("Typesofblood") + "",
                        Purpose = A.Field<string>("Purpose") + "",
                        CityName = A.Field<string>("CityName") + "",
                        StateName = A.Field<string>("StateName") + "",
                        DistrictName = A.Field<string>("DistrictName") + "",
                        HospitalAddress = A.Field<string>("HospitalAddress") + "",
                        HospitalName = A.Field<string>("HospitalName") + "",
                        newStatename = A.Field<string>("newStatename") + "",
                        newDistrictname = A.Field<string>("newDistrictname") + "",

                        newCityname = A.Field<string>("newCityname") + "",
                        Pincode = A.Field<string>("Pincode") + "",
                        Latitude = A.Field<string>("Latitude") + "",
                        Longitude = A.Field<string>("Longitude") + "",
                        BloodRequestDate = A.Field<string>("BloodRequestDate") + "",
                        CreatedByEmail = A.Field<string>("CreatedByEmail") + "",

                        //RP
                        //CallStatus = A.Field<Boolean?>("CallStatus") ?? null,
                        //EmailStatus = A.Field<Boolean?>("EmailStatus") ?? null,
                        //NotificationStatus = A.Field<Boolean?>("NotificationStatus") ?? null,
                        //SMSStatus = A.Field<Boolean?>("SMSStatus") ?? null,
                        //WhatsappStatus = A.Field<Boolean?>("WhatsappStatus") ?? null,
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get Search user details-->Commented By SRINIVAS
        public dynamic Get_Searchdetails(string param1, string param2, string param3, string param4, string param5, string param6)
        {
            try
            {
                List<search> allExceptions = new List<search>();
                ds = db.GetDataWithFiveParam(SPS.User_SearchDeatils.ToString(), param1, param2, param3, param4, param5, param6);
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new search()
                    {

                        RegId = c.Field<Int32?>("RegId") + 0,
                        FullName = c.Field<string>("FullName") + "",
                        ApprovalStatus = c.Field<Int32?>("ApprovalStatus") + 0,
                        BLGName = c.Field<string>("BLGName") + "",
                        CityName = c.Field<string>("CityName") + "",
                        Email = c.Field<string>("Email") + "",
                        StateName = c.Field<string>("StateName") + "",
                        Area = c.Field<string>("Area") + "",
                        Pincode = c.Field<string>("Pincode") + "",
                        CountryName = c.Field<string>("CountryName") + "",
                        DoctorName = c.Field<string>("DoctorName") + "",
                        Gender = c.Field<string>("Gender") + "",
                        HospitalAddress = c.Field<string>("HospitalAddress") + "",
                        HospitalName = c.Field<string>("HospitalName") + "",
                        UserAddress = c.Field<string>("UserAddress") + "",
                        Phonenumber = c.Field<string>("Phonenumber") + "",
                        DistrictName = c.Field<string>("DistrictName") + "",
                        Messagetorequester = c.Field<string>("Messagetorequester") + "",
                        Age = c.Field<string>("Age") + "",
                        Devicetoken = c.Field<string>("Devicetoken") + "",

                        //RP
                        //CallStatus = c.Field<Boolean?>("CallStatus") ?? null,
                        //EmailStatus = c.Field<Boolean?>("EmailStatus") ?? null,
                        //NotificationStatus = c.Field<Boolean?>("NotificationStatus") ?? null,
                        // SMSStatus = c.Field<Boolean?>("SMSStatus") ?? null,
                        // WhatsappStatus = c.Field<Boolean?>("WhatsappStatus") ?? null,

                        //Upendra                        
                        PhoneCallNext = c.Field<DateTime?>("PhoneCallNext") ?? null,
                        WhatsAppNext = c.Field<DateTime?>("WhatsAppNext") ?? null,
                        NotificationNext = c.Field<DateTime?>("NotificationNext") ?? null,
                        SMSNext = c.Field<DateTime?>("SMSNext") ?? null,
                        Status = c.Field<Boolean?>("Status")?? null 
                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Oldagehomedetails-->Commented By SRINIVAS
        public dynamic Oldagehomedetails(string param1, string param2, string param3, string param4, string param5, string param6)
        {
            try
            {
                List<search> allExceptions = new List<search>();
                ds = db.GetDataWithFiveParam(SPS.Oldagehomedetails.ToString(), param1, param2, param3, param4, param5, param6);
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new search()
                    {


                        HomeName = c.Field<string>("HomeName") + "",
                        Address = c.Field<string>("Address") + "",
                        PhoneNumber = c.Field<string>("PhoneNumber") + "",
                        Pincode = c.Field<string>("Pincode") + "",


                        SiteURL = c.Field<string>("SiteURL") + "",
                        CityName = c.Field<string>("CityName") + "",
                        StateName = c.Field<string>("StateName") + "",

                        //  CountryName = c.Field<string>("CountryName") + "",

                        DistrictName = c.Field<string>("DistrictName") + "",

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region orphanDeatils-->Commented By SRINIVAS
        public dynamic orphanDeatils(string param1, string param2, string param3, string param4, string param5, string param6)
        {
            try
            {
                List<search> allExceptions = new List<search>();
                ds = db.GetDataWithFiveParam(SPS.orphanDeatils.ToString(), param1, param2, param3, param4, param5, param6);
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new search()
                    {


                        Orphanage_Name = c.Field<string>("Orphanage_Name") + "",
                        Address = c.Field<string>("Address") + "",
                        PhoneNumber = c.Field<string>("PhoneNumber") + "",
                        Pincode = c.Field<string>("Pincode") + "",


                        SiteURL = c.Field<string>("SiteURL") + "",
                        CityName = c.Field<string>("CityName") + "",
                        StateName = c.Field<string>("StateName") + "",

                        // CountryName = c.Field<string>("CountryName") + "",

                        DistrictName = c.Field<string>("DistrictName") + "",

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_Areas-->Commented By SRINIVAS
        public dynamic Get_Areass()
        {
            try
            {
                List<search> allExceptions = new List<search>();
                ds = db.GetData(SPS.Get_Areas.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new search()
                    {

                        Area = c.Field<string>("Area") + "",

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_oldAreass-->Commented By prash
        public dynamic Get_oldAreass()
        {
            try
            {
                List<search> allExceptions = new List<search>();
                ds = db.GetData(SPS.Get_Address.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new search()
                    {

                        Address = c.Field<string>("Address") + "",

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region GetOrphan_Address-->Commented By prash
        public dynamic GetOrphan_Address()
        {
            try
            {
                List<search> allExceptions = new List<search>();
                ds = db.GetData(SPS.GetOrphan_Address.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new search()
                    {

                        Address = c.Field<string>("Address") + "",

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region  Get_Requestbasedon_presonID --manohar
        public dynamic Get_Requestbasedon_presonID(string param)
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetDataWithSingleParam(SPS.Get_Requestbasedon_presonID.ToString(), param);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(c => new RequestForm()
                    {
                        RegId = c.Field<Int32?>("RegId") + 0,
                        UdId = c.Field<Int32?>("UdId") + 0,
                        ApprovalStatus = c.Field<Int32?>("ApprovalStatus") + 0,
                        FullName = c.Field<string>("FullName") + "",
                        BLGName = c.Field<string>("BLGName") + "",
                        CityName = c.Field<string>("CityName") + "",
                        StateName = c.Field<string>("StateName") + "",
                        Area = c.Field<string>("Area") + "",
                        DoctorName = c.Field<string>("DoctorName") + "",
                        Latitude = c.Field<string>("Latitude") + "",
                        Longitude = c.Field<string>("Longitude") + "",
                        BloodRequestID = c.Field<string>("BloodRequestID") + "",
                        //Devicetoken = c.Field<string>("Devicetoken") ?? "",

                        HospitalAddress = c.Field<string>("HospitalAddress") + "",
                        HospitalName = c.Field<string>("HospitalName") + "",
                        UserAddress = c.Field<string>("UserAddress") + "",
                        Phonenumber = c.Field<string>("Phonenumber") + "",
                        DistrictName = c.Field<string>("DistrictName") + "",
                        Gender = c.Field<string>("Gender") + "",
                        Age = c.Field<string>("Age") + "",
                        BloodRequestDate = c.Field<string>("BloodRequestDate") + "",
                        RequestTime = c.Field<string>("RequestTime") + "",
                        Purpose = c.Field<string>("Purpose") + "",
                        UnitsofBlood = c.Field<string>("UnitsofBlood") + "",
                        Typesofblood = c.Field<string>("Typesofblood") + "",
                        ContactPerson = c.Field<string>("ContactPerson") + "",
                        ContactMobile = c.Field<string>("ContactMobile") + "",
                        HospitalPhonenumber = c.Field<string>("HospitalPhonenumber") + "",

                        Pincode = c.Field<string>("Pincode") + "",
                        newStatename = c.Field<string>("newStatename") + "",
                        newDistrictname = c.Field<string>("newDistrictname") + "",
                        newCityname = c.Field<string>("newCityname") + "",
                        CreatedBy = c.Field<Int32?>("CreatedBy") + 0,
                        BloodGroupId = c.Field<Int32?>("BloodGroupId") + 0,
                        UnitsofBloodId = c.Field<Int32?>("UnitsofBloodId") + 0,
                        CityId = c.Field<Int32?>("CityId") + 0,
                        DistrictId = c.Field<Int32?>("DistrictId") + 0,
                        StateId = c.Field<Int32?>("StateId") + 0,


                    }).ToList();
                    return userdata;
                }
                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region  Get_Requestbasedon_presonID --suraj
        public dynamic Get_Requestbasedon_presonID_mobile(string param)
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetDataWithSingleParam(SPS.Get_Requestbasedon_presonID_mobile.ToString(), param);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(c => new RequestForm()
                    {
                        RegId = c.Field<Int32?>("RegId") + 0,
                        UdId = c.Field<Int32?>("UdId") + 0,
                        ApprovalStatus = c.Field<Int32?>("ApprovalStatus") + 0,
                        FullName = c.Field<string>("FullName") + "",
                        BLGName = c.Field<string>("BLGName") + "",
                        CityName = c.Field<string>("CityName") + "",
                        StateName = c.Field<string>("StateName") + "",
                        Area = c.Field<string>("Area") + "",
                        DoctorName = c.Field<string>("DoctorName") + "",
                        Latitude = c.Field<string>("Latitude") + "",
                        Longitude = c.Field<string>("Longitude") + "",
                        BloodRequestID = c.Field<string>("BloodRequestID") + "",
                        Devicetoken = c.Field<string>("Devicetoken") ?? "",
                        Email = c.Field<string>("Email") ?? "", //suraj

                        HospitalAddress = c.Field<string>("HospitalAddress") + "",
                        HospitalName = c.Field<string>("HospitalName") + "",
                        UserAddress = c.Field<string>("UserAddress") + "",
                        Phonenumber = c.Field<string>("Phonenumber") + "",
                        DistrictName = c.Field<string>("DistrictName") + "",
                        Gender = c.Field<string>("Gender") + "",
                        Age = c.Field<string>("Age") + "",
                        BloodRequestDate = c.Field<string>("BloodRequestDate") + "",
                        RequestTime = c.Field<string>("RequestTime") + "",
                        Purpose = c.Field<string>("Purpose") + "",
                        ContactPerson = c.Field<string>("ContactPerson") + "",
                        ContactMobile = c.Field<string>("ContactMobile") + "",
                        HospitalPhonenumber = c.Field<string>("HospitalPhonenumber") + "",
                        UnitsofBlood = c.Field<string>("UnitsofBlood") + "",
                        CreatedBy = c.Field<Int32?>("CreatedBy") + 0,
                        //RP
                        //CallStatus = c.Field<Boolean?>("CallStatus") ?? null,
                        // EmailStatus = c.Field<Boolean?>("EmailStatus") ?? null,
                        //NotificationStatus = c.Field<Boolean?>("NotificationStatus") ?? null,
                        //SMSStatus = c.Field<Boolean?>("SMSStatus") ?? null,
                        // WhatsappStatus = c.Field<Boolean?>("WhatsappStatus") ?? null,
                    }).ToList();
                    return userdata;
                }
                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region   Gallerycate_Crud     -- Upendra
        public dynamic GalleryCategory_Crud(string xmlParam, string flag)
        {
            try
            {
                var MSG = "";
                List<Gallerycategory> data = new JavaScriptSerializer().Deserialize<List<Gallerycategory>>(xmlParam);
                var UserData = xml.GetXmlArrayString<Gallerycategory>(data);
                ds = db.SaveDataReturn1(SPS.GalleryCategory_Crud.ToString(), UserData, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var Checkdata = db.CheckDatainDS(ds, 0);
                    if (Checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            data = ds.Tables[0].AsEnumerable().Select(
                                A => new Gallerycategory()
                                {
                                    Categoryid = A.Field<Int32?>("Categoryid") + 0,
                                    categoryname = A.Field<string>("categoryname") + "",
                                    Status = A.Field<Boolean?>("Status") ?? null,
                                    CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                    CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                    ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                    ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                }).ToList();
                            return data;
                        }
                    }
                    ds.Clear();
                }
                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region  Get_gallerycatwise --Upendra
        public dynamic Get_gallerycatwise(string param1)
        {
            try
            {
                var MSG = "";
                List<Gallery> userdata = new List<Gallery>();
                ds = db.GetDataWithSingleParam(SPS.Get_Gallerycatwise.ToString(), param1);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Gallery()
                    {
                        GalleryID = A.Field<Int32?>("GalleryID") + 0,
                        GalleryImages = A.Field<string>("GalleryImages") + "",
                        RegId = A.Field<Int32?>("RegId") + 0,
                        Categoryid = A.Field<Int32?>("Categoryid") + 0,
                        Dateofservice = A.Field<DateTime?>("Dateofservice") ?? null,
                        State = A.Field<Int32?>("State") + 0,
                        District = A.Field<Int32?>("District") + 0,
                        City = A.Field<Int32?>("City") + 0,
                        Institutionname = A.Field<string>("Institutionname") + "",
                        FullName = A.Field<string>("FullName") + "",
                        StateName = A.Field<string>("StateName") + "",


                    }).ToList();
                }
                return userdata;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }


        #endregion

        #region  Get_mobilegallerycatagory --suraj
        public dynamic Get_mobilegallerycatagory()
        {
            try
            {
                var MSG = "";
                List<Gallery> userdata = new List<Gallery>();
                ds = db.GetData(SPS.Get_Mobile_AllGallerycatagrise.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Gallery()
                    {
                        Requestedby = A.Field<Int32?>("Requestedby") + 0,
                        RoleId = A.Field<Int32?>("RoleId") + 0,
                        GalleryID = A.Field<Int32?>("GalleryID") + 0,
                        GalleryImages = A.Field<string>("GalleryImages") + "",
                        RegId = A.Field<Int32?>("RegId") + 0,
                        Categoryid = A.Field<Int32?>("Categoryid") + 0,
                        Dateofservice = A.Field<DateTime?>("Dateofservice") ?? null,
                        State = A.Field<Int32?>("State") + 0,
                        District = A.Field<Int32?>("District") + 0,
                        City = A.Field<Int32?>("City") + 0,
                        Institutionname = A.Field<string>("Institutionname") + "",
                        FullName = A.Field<string>("FullName") + "",
                        StateName = A.Field<string>("StateName") + "",
                        CreatedBy = A.Field<Int32?>("CreatedBy") + 0,



                    }).ToList();
                }
                return userdata;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region  BG_GetBloodRequestsIDs --suraj
        public dynamic BG_GetBloodRequestsIDs()
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetData(SPS.Get_Approve_Requests_byDonors_mobile.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new RequestForm()
                    {
                        FullName = A.Field<string>("FullName") + "",

                        BloodRequestID = A.Field<string>("BloodRequestID") + "",
                        ApprovalStatus = A.Field<Int32?>("ApprovalStatus") + 0,
                        CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                        ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                        AcceptedBy = A.Field<Int32?>("AcceptedBy") + 0,
                        SYSSubmitted = A.Field<Int32?>("SYSSubmitted") + 0,


                    }).ToList();
                }
                return userdata;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region  BG_GetBloodRequestsIDs --suraj
        public dynamic BG_Get_AcceptRequest_mobile()
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetData(SPS.BG_Get_AcceptRequest_mobile.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(c => new RequestForm()
                    {
                        RegId = c.Field<Int32?>("RegId") + 0,
                        //UdId = c.Field<Int32?>("UdId") + 0,
                        ApprovalStatus = c.Field<Int32?>("ApprovalStatus") + 0,
                        FullName = c.Field<string>("FullName") + "",
                        BLGName = c.Field<string>("BLGName") + "",
                        CityName = c.Field<string>("CityName") + "",
                        StateName = c.Field<string>("StateName") + "",
                        Area = c.Field<string>("Area") + "",
                        DoctorName = c.Field<string>("DoctorName") + "",
                        Latitude = c.Field<string>("Latitude") + "",
                        Longitude = c.Field<string>("Longitude") + "",
                        BloodRequestID = c.Field<string>("BloodRequestID") + "",
                        //Devicetoken = c.Field<string>("Devicetoken") ?? "",
                        Email = c.Field<string>("Email") ?? "", //suraj

                        HospitalAddress = c.Field<string>("HospitalAddress") + "",
                        HospitalName = c.Field<string>("HospitalName") + "",
                        UserAddress = c.Field<string>("UserAddress") + "",
                        Phonenumber = c.Field<string>("Phonenumber") + "",
                        DistrictName = c.Field<string>("DistrictName") + "",
                        Gender = c.Field<string>("Gender") + "",
                        Age = c.Field<string>("Age") + "",
                        BloodRequestDate = c.Field<string>("BloodRequestDate") + "",
                        Dateofservice = c.Field<DateTime?>("Dateofservice") ?? null,
                        RequestTime = c.Field<string>("RequestTime") + "",
                        Purpose = c.Field<string>("Purpose") + "",
                        ContactPerson = c.Field<string>("ContactPerson") + "",
                        ContactMobile = c.Field<string>("ContactMobile") + "",
                        HospitalPhonenumber = c.Field<string>("HospitalPhonenumber") + "",



                    }).ToList();
                }
                return userdata;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region  InsertBrochures_Deatails --manohar
        public dynamic InsertBrochures_Deatails(string xmlParam)
        {
            try
            {
                var MSG = "";
                List<Download> userdata = new JavaScriptSerializer().Deserialize<List<Download>>(xmlParam);
                var data = xml.GetXmlArrayString<Download>(userdata);
                ds = db.GetsinglexmlParam(SPS.InsertBrochures_Deatails.ToString(), data);

                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                    return MSG;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region  Get_Gallery_Search --Upendra
        public dynamic Get_Gallery_Search(string param1)
        {
            try
            {
                var MSG = "";
                List<Gallery> userdata = new List<Gallery>();
                ds = db.GetDataWithSingleParam(SPS.All_Gallery_Search.ToString(), param1);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Gallery()
                    {

                        GalleryImages = A.Field<string>("GalleryImages") + "",
                        Email = A.Field<string>("Email") + "",
                        Phonenumber = A.Field<string>("Phonenumber") + "",
                        FullName = A.Field<string>("FullName") + "",
                        StateName = A.Field<string>("StateName") + "",


                    }).ToList();
                }
                return userdata;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region  Get_Phonenumber --manohar
        public dynamic Get_Phonenumber(string param)
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetDataWithSingleParam(SPS.Get_Phonenumber.ToString(), param);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(c => new RequestForm()
                    {
                        RegId = c.Field<Int32>("RegId") + 0,
                        Phonenumber = c.Field<string>("Phonenumber") + "",
                        FullName = c.Field<string>("FullName") + "",
                        Email = c.Field<string>("Email") + "",
                        Messagetorequester = c.Field<string>("Messagetorequester") + "",

                    }).ToList();
                    return userdata;
                }
                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region  Get_Gallaryimages_bycategory  --manohar
        public dynamic Get_Gallaryimages_bycategory()
        {
            try
            {
                List<Gallerycategory> userdata = new List<Gallerycategory>();
                ds = db.GetData(SPS.Get_Gallaryimages_bycategory.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(c => new Gallerycategory
                    {
                        Categoryid = c.Field<Int32>("Categoryid") + 0,
                        categoryname = c.Field<string>("categoryname") + "",
                        GalleryImages = c.Field<string>("GalleryImages") + "",
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion
        public List<city> Get_AllcitiesPaged(int offset, int pageSize)
        {
            List<city> GetFarmerList = new List<city>();
            try
            {
                // Execute a SQL query with pagination using OFFSET and FETCH clauses
                ds = db.GetDataWithPagination(SPS.Get_cities.ToString(), offset, pageSize);

                bool getdata = db.CheckDatainDS(ds, 0);
                if (getdata == true)
                {
                    // Mapping data from DataSet to List of Farmerdetails
                    GetFarmerList = ds.Tables[0].AsEnumerable().Select(
                       u => new city()
                       {

                           CityName = u.Field<string>("CityName") + "",



                       }).ToList();
                }

                return GetFarmerList;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                return new List<city>();
            }
        }

        #region  gallery_View --Upendra
        public dynamic Gallery_view(string param1)
        {
            try
            {
                var MSG = "";
                List<Gallery> userdata = new List<Gallery>();
                ds = db.GetDataWithSingleParam(SPS.Gallery_View.ToString(), param1);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Gallery()
                    {

                        GalleryImages = A.Field<string>("GalleryImages") + "",
                        GalleryID = A.Field<Int32?>("GalleryID") + 0,


                    }).ToList();
                }
                return userdata;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Get_GallaryImagedBasedOn_ID  --manohar
        public dynamic Get_GallaryImagedBasedOn_ID(string gallary)
        {
            try
            {
                List<Gallerycategory> userdata = new List<Gallerycategory>();
                ds = db.GetDataWithSingleParam(SPS.Get_GallaryImagedBasedOn_ID.ToString(), gallary);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(G => new Gallerycategory
                    {
                        Categoryid = G.Field<Int32>("Categoryid") + 0,
                        GalleryImages = G.Field<string>("GalleryImages") + "",
                        categoryname = G.Field<string>("categoryname") + "",

                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region Get_PincodeDropdown --manohar
        public dynamic Get_PincodeDropdown()
        {
            try
            {
                List<pincode> userdata = new List<pincode>();
                ds = db.GetData(SPS.Get_PincodeDropdown.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(D => new pincode
                    {
                        Pincode = D.Field<string>("Pincode") + "",
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Forgot_Password By manohar
        public dynamic Forgot_Password(string Password, string Mobile)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithTwoParameters(SPS.BG_Forgot_Password.ToString(), Password, Mobile);
                bool checkData = db.CheckDatainDS(ds, 0);
                if (checkData == true)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region   BG_Login     -- manohar
        public dynamic BG_Login(string xmlParam)
        {
            try
            {
                var MSG = "";
                List<Users> data = new JavaScriptSerializer().Deserialize<List<Users>>(xmlParam);
                var UserData = XMLConvert.GetXmlArrayString<Users>(data);
                ds = db.SaveDataReturnSingleXML(SPS.BG_Login.ToString(), UserData);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    if (MSG == "Invalid User")
                    {
                        return MSG;
                    }
                    else
                    {
                        bool getdata = db.CheckDatainDS(ds, 0);
                        if (getdata == true)
                        {


                            data = ds.Tables[0].AsEnumerable().Select(
                            A => new Users()
                            {
                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                Password = A.Field<string>("Password") + "",
                                Email = A.Field<string>("Email") + "",


                            }).ToList();
                            return data;
                        }
                    }
                    // ds.Clear();
                    MSG = "No rows in the DataTable";
                    return MSG;
                }
                return null;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Get_Hospital --- Upendra
        public dynamic Get_Hospital(string Param1)
        {
            try
            {
                var MSG = "";

                List<Hospital> data = new List<Hospital>();
                ds = db.GetDataWithSingleParam(SPS.Get_Hospital.ToString(), Param1);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                if (MSG == "NOTEXIST")
                {
                    return MSG;
                }
                else
                {
                    bool checkdb = db.CheckDatainDS(ds, 0);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (checkdb == true)
                        {
                            data = ds.Tables[0].AsEnumerable().Select(A => new Hospital
                            {
                                HsptID = A.Field<Int32?>("HsptID") + 0,
                                CityId = A.Field<Int32?>("CityId") + 0,
                                DistrictID = A.Field<Int32?>("DistrictID") + 0,
                                StateId = A.Field<Int32?>("StateId") + 0,
                                HsptName = A.Field<string>("HsptName") + "",
                                HsptPhNum = A.Field<string>("HsptPhNum") + "",
                                HsptAddress = A.Field<string>("HsptAddress") + "",
                                DoctorName = A.Field<string>("DoctorName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                StateName = A.Field<string>("StateName") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                Latitude = A.Field<string>("Latitude") + "",
                                Longitude = A.Field<string>("Longitude") + "",
                                POCName = A.Field<string>("POCName") + "",
                                POC_ContactNumber = A.Field<string>("POC_ContactNumber") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                            }).ToList();
                            return data;
                        }
                    }
                    return MSG;
                }
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                throw ex;
            }
        }
        #endregion


        #region Insert_Update_Requestpresent
        public dynamic Insert_Update_Requestpresent(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<request> userdata = new JavaScriptSerializer().Deserialize<List<request>>(xml);
                var data = XMLConvert.GetXmlArrayString<request>(userdata);
                ds = db.SaveDataReturn1(SPS.Insert_Update_Requestpresent.ToString(), data, flag);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2" || flag == "3" || flag == "4" || flag == "5")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion



        #region BG_GetObservationReport-->Commented By Amrutha
        public dynamic BG_GetObservationReport()
        {
            try
            {
                List<report> allExceptions = new List<report>();
                ds = db.GetData(SPS.BG_GetObservationReport.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new report()
                    {
                        RegId = c.Field<Int32?>("RegId") + 0,
                        CreatedBy = c.Field<Int32?>("CreatedBy") + 0,
                        Observation = c.Field<string>("Observation") + "",
                        Comments = c.Field<string>("Comments") + "",
                        FullName = c.Field<string>("FullName") + "",
                        DonorName = c.Field<string>("DonorName") + "",
                        Phonenumber = c.Field<string>("Phonenumber") + "",
                        DonorPhonenumber = c.Field<string>("DonorPhonenumber") + "",
                       // CreatedByName = c.Field<string>("CreatedByName") + "",
                        CreatedDate = c.Field<DateTime?>("CreatedDate") ?? null,

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion 


        #region Insert_Update_Report
        public dynamic Insert_Update_Report(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<report> userdata = new JavaScriptSerializer().Deserialize<List<report>>(xml);
                var data = XMLConvert.GetXmlArrayString<report>(userdata);
                ds = db.SaveDataReturn1(SPS.Insert_Update_Report.ToString(), data, flag);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region  BG_Observation_Crud  --manohar
        public dynamic BG_Observation_Crud(string xmlparam, string flag)
        {
            try
            {
                var MSG = "";
                List<Observation> userdata = new JavaScriptSerializer().Deserialize<List<Observation>>(xmlparam);
                var data = XMLConvert.GetXmlArrayString<Observation>(userdata);
                ds = db.SaveDataReturn1(SPS.BG_Observation_Crud.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdata = db.CheckDatainDS(ds, 0);
                    if (checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new Observation()
                            {
                                ObsID = A.Field<Int32?>("ObsID") + 0,
                                ObservationReason = A.Field<string>("ObservationReason") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return userdata;
                        }
                    }
                    //  ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region  BG_Work_Place_Crud  --manohar
        public dynamic BG_Work_Place_Crud(string xmlparam, string flag)
        {
            try
            {
                var MSG = "";
                List<Work> userdata = new JavaScriptSerializer().Deserialize<List<Work>>(xmlparam);
                var data = XMLConvert.GetXmlArrayString<Work>(userdata);
                ds = db.SaveDataReturn1(SPS.BG_Work_Place_Crud.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdata = db.CheckDatainDS(ds, 0);
                    if (checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new Work()
                            {
                                WorkID = A.Field<Int32?>("WorkID") + 0,
                                WorkPlace = A.Field<string>("WorkPlace") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return userdata;
                        }
                    }
                    //  ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_PresentationDetails --->Amrutha
        public dynamic Get_PresentationDetails()
        {
            try
            {
                List<Get_RequestPresentation> userdata = new List<Get_RequestPresentation>();
                ds = db.GetData(SPS.Get_PresentationDetails.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Get_RequestPresentation
                    {
                        RPId = A.Field<Int32?>("RPId") + 0,
                       // RegId = A.Field<Int32?>("RegId") + 0,
                        Mode = A.Field<string>("Mode") + "",
                        Audiance = A.Field<string>("Audiance") + "",
                        Venue = A.Field<string>("Venue") + "",
                        Venue_name = A.Field<string>("Venue_name") + "",
                        Address = A.Field<string>("Address") + "",
                        Area = A.Field<string>("Area") + "",
                       // StateName = A.Field<string>("StateName") + "",
                       // DistrictName = A.Field<string>("DistrictName") + "",
                        //CityName = A.Field<string>("CityName") + "",
                        newStatename = A.Field<string>("newStatename") + "",
                        newDistrictname = A.Field<string>("newDistrictname") + "",
                        newCityname = A.Field<string>("newCityname") + "",
                        Pincode = A.Field<string>("Pincode") + "",
                        Contact_name = A.Field<string>("Contact_name") + "",
                        Contact_number = A.Field<string>("Contact_number") + "",
                        Message = A.Field<string>("Message") + "",
                        CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                        CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                        ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                        ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                        Addedby = A.Field<string>("Addedby") + "",
                       // Devicetoken = A.Field<string>("Devicetoken") ?? "",
                        RequestDate = A.Field<DateTime?>("RequestDate") ?? null,
                        Latitude = A.Field<string>("Latitude") + "",
                        Longitude = A.Field<string>("Longitude") + "",
                        Requestedto = A.Field<Int32?>("Requestedto") + 0,

                        RequestAcceptStatus = A.Field<Int32?>("RequestAcceptStatus") + 0,
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        //#region Get_RequestPresentation  --Rambabu
        //public dynamic Get_RequestPresentation(string param)
        //{
        //    try
        //    {
        //        List<Get_RequestPresentation> userdata = new List<Get_RequestPresentation>();
        //        ds = db.GetDataWithSingleParam(SPS.Get_RequestPresentation.ToString(), param);
        //        bool checkdb = db.CheckDatainDS(ds, 0);
        //        if (checkdb == true)
        //        {
        //            userdata = ds.Tables[0].AsEnumerable().Select(A => new Get_RequestPresentation
        //            {
        //                RPId = A.Field<Int32?>("RPId") + 0,
        //                RegId = A.Field<Int32?>("RegId") + 0,
        //                Mode = A.Field<string>("Mode") + "",
        //                Audiance = A.Field<string>("Audiance") + "",
        //                Venue = A.Field<string>("Venue") + "",
        //                Venue_name = A.Field<string>("Venue_name") + "",
        //                Address = A.Field<string>("Address") + "",
        //                Area = A.Field<string>("Area") + "",
        //                StateName = A.Field<string>("StateName") + "",
        //                DistrictName = A.Field<string>("DistrictName") + "",
        //                CityName = A.Field<string>("CityName") + "",
        //                Pincode = A.Field<string>("Pincode") + "",
        //                Contact_name = A.Field<string>("Contact_name") + "",
        //                Contact_number = A.Field<string>("Contact_number") + "",
        //                Message = A.Field<string>("Message") + "",
        //                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
        //                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
        //                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
        //                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
        //                Addedby = A.Field<string>("Addedby") + "",
        //                Devicetoken = A.Field<string>("Devicetoken") ?? "",
        //                RequestDate = A.Field<DateTime?>("RequestDate") ?? null,
        //                Latitude = A.Field<string>("Latitude") + "",
        //                Longitude = A.Field<string>("Longitude") + "",
        //                Requestedto = A.Field<Int32?>("Requestedto") + 0,

        //                RequestAcceptStatus = A.Field<Int32?>("RequestAcceptStatus") + 0,


        //            }).ToList();
        //        }
        //        return userdata;
        //    }
        //    catch (Exception ex)
        //    {
        //        saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
        //        exp.ExceptionHandler(ex);
        //        throw ex;
        //    }
        //}
        //#endregion
        #region Get_RequestPresentation  --Rambabu
        public dynamic Get_RequestPresentation(string Param1, string Param2, string Param3)
        {
            try
            {
                List<Get_RequestPresentation> userdata = new List<Get_RequestPresentation>();
                ds = db.GetDataWithThreeParam(SPS.Get_RequestPresentation.ToString(), Param1, Param2, Param3);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Get_RequestPresentation
                    {
                        RPId = A.Field<Int32?>("RPId") + 0,
                        RegId = A.Field<Int32?>("RegId") + 0,
                        Mode = A.Field<string>("Mode") + "",
                        Audiance = A.Field<string>("Audiance") + "",
                        Venue = A.Field<string>("Venue") + "",
                        Venue_name = A.Field<string>("Venue_name") + "",
                        Address = A.Field<string>("Address") + "",
                        Area = A.Field<string>("Area") + "",
                        StateName = A.Field<string>("StateName") + "",
                        DistrictName = A.Field<string>("DistrictName") + "",
                        CityName = A.Field<string>("CityName") + "",
                        Pincode = A.Field<string>("Pincode") + "",
                        Contact_name = A.Field<string>("Contact_name") + "",
                        Contact_number = A.Field<string>("Contact_number") + "",
                        Message = A.Field<string>("Message") + "",
                        CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                        CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                        ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                        ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                        Addedby = A.Field<string>("Addedby") + "",
                        //Devicetoken = A.Field<string>("Devicetoken") ?? "",
                        RequestDate = A.Field<DateTime?>("RequestDate") ?? null,
                        Latitude = A.Field<string>("Latitude") + "",
                        Longitude = A.Field<string>("Longitude") + "",
                        Requestedto = A.Field<Int32?>("Requestedto") + 0,

                        RequestAcceptStatus = A.Field<Int32?>("RequestAcceptStatus") + 0,
                        PresentationID = A.Field<Int32?>("PresentationID") + 0,
                        OrgPresentation = A.Field<bool?>("OrgPresentation"),


                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region  Volunteerlist_crud  --manohar
        public dynamic Volunteerlist_crud(string xmlparam, string flag)
        {
            try
            {
                var MSG = "";
                List<Volunteer> userdata = new JavaScriptSerializer().Deserialize<List<Volunteer>>(xmlparam);
                var data = XMLConvert.GetXmlArrayString<Volunteer>(userdata);
                ds = db.SaveDataReturn1(SPS.Volunteerlist_crud.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdata = db.CheckDatainDS(ds, 0);
                    if (checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new Volunteer()
                            {
                                RegId = A.Field<Int32?>("RegId") + 0,
                                RoleId = A.Field<Int32?>("RoleId") + 0,
                                FullName = A.Field<string>("FullName") + "",
                                Phonenumber = A.Field<string>("Phonenumber") + "",
                                Password = A.Field<string>("Password") + "",
                                Email = A.Field<string>("Email") + "",
                                Age = A.Field<string>("Age") + "",
                                Gender = A.Field<string>("Gender") + "",
                                TokenId = A.Field<Guid>("TokenId") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                BLGName = A.Field<string>("BLGName") + "",
                                StateName = A.Field<string>("StateName") + "",
                                DistrictName = A.Field<string>("DistrictName") + "",
                                CityName = A.Field<string>("CityName") + "",
                                newStatename = A.Field<string>("newStatename") + "",
                                newDistrictname = A.Field<string>("newDistrictname") + "",
                                newCityname = A.Field<string>("newCityname") + "",
                                UserAddress = A.Field<string>("UserAddress") + "",
                                Area = A.Field<string>("Area") + "",
                                Pincode = A.Field<string>("Pincode") + "",
                                Addedby = A.Field<string>("Addedby") + "",


                            }).ToList();
                            return userdata;
                        }
                    }
                    //  ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Insert_Update_Addmembers -- manohar
        public dynamic Insert_Update_Addmembers(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<Doners> userdata = new JavaScriptSerializer().Deserialize<List<Doners>>(xml);
                var data = XMLConvert.GetXmlArrayString<Doners>(userdata);
                ds = db.SaveDataReturn1(SPS.Insert_Update_Addmembers.ToString(), data, flag);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1" || flag == "2")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region Get_DonorReports-->Commented By Naushad,Rambabu
        public dynamic Get_DonorReports(string adminDetails)
        {
            try
            {
                List<DonorReports> orderDetails = new List<DonorReports>();
                List<DonorReports> adminDetailsList = new JavaScriptSerializer().Deserialize<List<DonorReports>>(adminDetails);
                var Data1 = XMLConvert.GetXmlArrayString(adminDetailsList);
                var MSG = "";
                ds = db.SaveDataReturnSingleXML(SPS.Get_DonorReports.ToString(), Data1);
                bool CheckDb = db.CheckDatainDS(ds, 0);
                if (CheckDb == false)
                {
                    return MSG;
                }
                else
                {
                    if (CheckDb == true)
                    {
                        orderDetails = ds.Tables[0].AsEnumerable().Select(
                       c => new DonorReports()
                       {
                           //RegId = c.Field<Int32?>("RegId") ?? 0,
                           FullName = c.Field<string>("FullName") + "",
                           Observation = c.Field<string>("Observation") + "",
                           Comments = c.Field<string>("Comments") + "",
                           CommentedDate = c.Field<DateTime?>("CommentedDate") ?? null,

                       }).ToList();
                        return orderDetails;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_Users_basedOn_Pincode --manohar
        public dynamic Get_Users_basedOn_Pincode(string pincode)
        {
            try
            {
                var MSG = "";
                List<pincodedetails> userdata = new List<pincodedetails>();
                ds = db.GetDataWithSingleParam(SPS.Get_Users_basedOn_Pincode.ToString(), pincode);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdb = db.CheckDatainDS(ds, 0);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (checkdb == true)
                    {
                        userdata = ds.Tables[0].AsEnumerable().Select(A => new pincodedetails
                        {
                            RegId = A.Field<Int32?>("RegId") + 0,
                            Email = A.Field<string>("Email") + "",
                            Phonenumber = A.Field<string>("Phonenumber") + "",
                            Pincode = A.Field<string>("Pincode") + ""

                        }).ToList();
                        return userdata;
                    }

                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                throw ex;
            }

        }
        #endregion

        #region  Get_image_uploadDetails --manohar
        public dynamic Get_image_uploadDetails(string regid, string catid)
        {
            try
            {
                var MSG = "";
                List<ImageDetails> image = new List<ImageDetails>();
                ds = db.GetDataWithTwoParameters(SPS.Get_image_uploadDetails.ToString(), regid, catid);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdb = db.CheckDatainDS(ds, 0);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (checkdb == true)
                    {
                        image = ds.Tables[0].AsEnumerable().Select(A => new ImageDetails
                        {
                            RegId = A.Field<Int32?>("RegId") + 0,
                            Categoryid = A.Field<Int32?>("Categoryid") + 0,
                            CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                            Institutionname = A.Field<string>("Institutionname") + "",
                            GalleryImages = A.Field<string>("GalleryImages") + "",
                        }).ToList();
                        return image;
                    }

                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region Upload Blood Donation Image by Donor    -- Suresh
        public dynamic UploadBloodDonationImage(string BloodDonationImage, string BloodRequestID)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithTwoParameters(SPS.BG_Upload_BloodDonationImage.ToString(), BloodDonationImage, BloodRequestID);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    MSG = db.GetMessage(ds);
                    return MSG;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion
        #region Upload Blood Donation Image by Donor roleid    -- Suraj
        public dynamic UploadBloodDonationImagebyDonorwitroleid(string BloodDonationImage, string BloodRequestID, string RoleId)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithThreeParameters(SPS.BG_Upload_BloodDonationImage.ToString(), BloodDonationImage, BloodRequestID, RoleId);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    MSG = db.GetMessage(ds);
                    return MSG;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Upload Blood Donation Image by Donor    -- suraj
        public dynamic UploadBloodDonationImagemobile(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<MyDonationrequest> userdata = new JavaScriptSerializer().Deserialize<List<MyDonationrequest>>(xml);
                var data = XMLConvert.GetXmlArrayString<MyDonationrequest>(userdata);
                ds = db.SaveDataReturn1(SPS.BG_Upload_BloodDonationImage_mobile.ToString(), data, flag);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    if (flag == "1")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region  Get Closed Blood Requests   -- Suresh
        public dynamic Get_ClosedBloodRequests(string Param)
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetDataWithSingleParam(SPS.BG_Get_ClosedBloodRequests
                     .ToString(), Param);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    if (Param == "1")
                    {
                        userdata = ds.Tables[0].AsEnumerable().Select(c => new RequestForm()
                        {
                            BloodRequestID = c.Field<string>("BloodRequestID") + "",//suresh
                            Requestedby = c.Field<Int32?>("Requestedby") ?? 0,

                            //suraj
                            FullName = c.Field<string>("FullName") + "",
                            BLGName = c.Field<string>("BLGName") + "",
                            CityName = c.Field<string>("CityName") + "",
                            StateName = c.Field<string>("StateName") + "",
                            Area = c.Field<string>("Area") + "",
                            DoctorName = c.Field<string>("DoctorName") + "",
                            HospitalAddress = c.Field<string>("HospitalAddress") + "",
                            HospitalName = c.Field<string>("HospitalName") + "",
                            UserAddress = c.Field<string>("UserAddress") + "",
                            Phonenumber = c.Field<string>("Phonenumber") + "",
                            DistrictName = c.Field<string>("DistrictName") + "",
                            Gender = c.Field<string>("Gender") + "",
                            Age = c.Field<string>("Age") + "",
                            BloodRequestDate = c.Field<string>("BloodRequestDate") + "",
                            RequestTime = c.Field<string>("RequestTime") + "",
                            Purpose = c.Field<string>("Purpose") + "",
                            ContactPerson = c.Field<string>("ContactPerson") + "",
                            ContactMobile = c.Field<string>("ContactMobile") + "",
                            HospitalPhonenumber = c.Field<string>("HospitalPhonenumber") + "",
                            //BloodRequestID = c.Field<string>("BloodRequestID") + "",
                            BloodDonationImage = c.Field<string>("BloodDonationImage") + "",
                        }).ToList();
                        return userdata;
                    }
                    else if (Param == "2")
                    {
                        userdata = ds.Tables[0].AsEnumerable().Select(c => new RequestForm()
                        {
                            FullName = c.Field<string>("FullName") + "",
                            BLGName = c.Field<string>("BLGName") + "",
                            CityName = c.Field<string>("CityName") + "",
                            StateName = c.Field<string>("StateName") + "",
                            Area = c.Field<string>("Area") + "",
                            DoctorName = c.Field<string>("DoctorName") + "",
                            HospitalAddress = c.Field<string>("HospitalAddress") + "",
                            HospitalName = c.Field<string>("HospitalName") + "",
                            UserAddress = c.Field<string>("UserAddress") + "",
                            Phonenumber = c.Field<string>("Phonenumber") + "",
                            DistrictName = c.Field<string>("DistrictName") + "",
                            Gender = c.Field<string>("Gender") + "",
                            Age = c.Field<string>("Age") + "",
                            BloodRequestDate = c.Field<string>("BloodRequestDate") + "",
                            RequestTime = c.Field<string>("RequestTime") + "",
                            Purpose = c.Field<string>("Purpose") + "",
                            ContactPerson = c.Field<string>("ContactPerson") + "",
                            ContactMobile = c.Field<string>("ContactMobile") + "",
                            HospitalPhonenumber = c.Field<string>("HospitalPhonenumber") + "",
                            BloodRequestID = c.Field<string>("BloodRequestID") + "",
                            BloodDonationImage = c.Field<string>("BloodDonationImage") + "",
                        }).ToList();
                        return userdata;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion



        #region  Crud_Notifications ----RK 
        public dynamic Crud_Notifications(string xmlparam, string flag)
        {
            try
            {
                var MSG = "";
                List<Crud_Notifications> userdata = new JavaScriptSerializer().Deserialize<List<Crud_Notifications>>(xmlparam);
                var data = XMLConvert.GetXmlArrayString<Crud_Notifications>(userdata);
                ds = db.SaveDataReturn1(SPS.Crud_Notifications.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdata = db.CheckDatainDS(ds, 0);
                    if (checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "6")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new Crud_Notifications()
                            {
                                NotificationsID = A.Field<Int32?>("NotificationsID") + 0,
                                NotificationsDesc = A.Field<string>("NotificationsDesc") + "",
                                NotificationsImage = A.Field<string>("NotificationsImage") + "",
                                RegID = A.Field<Int32?>("RegID") + 0,
                                NotiRecevieID = A.Field<Int32?>("NotiRecevieID") + 0,
                                Status = A.Field<Boolean?>("Status") ?? null,
                                AcceptStatus = A.Field<Boolean?>("AcceptStatus") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null

                            }).ToList();
                            return userdata;
                        }
                    }
                    //  ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion


        #region suraj
        public dynamic MobileDashboard(string Param1, string Param2)
        {
            try
            {
                List<Dashboardmobile> dash = new List<Dashboardmobile>();
                ds = db.GetDataWithTwoParameters(SPS.BG_dashboardcount_mobile.ToString(), Param1, Param2);
                bool checkdata = db.CheckDatainDS(ds, 0);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                if (MSG == "Invalid User")
                {
                    return MSG;
                }
                else
                {
                    if (checkdata == true)
                    {
                        dash = ds.Tables[0].AsEnumerable().Select(A => new Dashboardmobile()

                        {
                            Donorcount = A.Field<Int32?>("Donorcount") + 0,
                            Referalcount = A.Field<Int32?>("Referalcount") + 0,
                            Adddonorscount = A.Field<Int32?>("Adddonorscount") + 0,
                            Leaderreferalcount = A.Field<Int32?>("Leaderreferalcount") + 0,
                            Dotationcount = A.Field<Int32?>("Dotationcount") + 0,
                            Bannercount = A.Field<Int32?>("Bannercount") + 0,
                            Presentationcount = A.Field<Int32?>("Presentationcount") + 0,
                            Acceptedcount = A.Field<Int32?>("Acceptedcount") + 0,

                        }).ToList();
                        return dash;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region DeleteAccount_Covert_as_Leader --manohar
        public dynamic DeleteAccount_Covert_as_Leader(string flag, string Param2)
        {
            try
            {
                var MSG = "";
                List<Delete> UserData = new List<Delete>();
                ds = db.GetDataWithTwoParameters(SPS.DeleteAccount_Covert_as_Leader.ToString(), flag, Param2);
                MSG = ds.Tables[0].Rows[0].ToString();
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    if (flag == "1" || flag == "2")
                    {
                        MSG = db.GetMessage(ds);
                        return MSG;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region Gethospitaldetails_mobile  --suraj
        public dynamic Get_Hospitaldetails_mobile()
        {
            try
            {
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetData(SPS.BG_gethospitaldetails_mobile.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(s => new RequestForm
                    {
                        FullName = s.Field<string>("FullName") + "",

                        //CityName = s.Field<string>("CityName") + "",
                        // StateName = s.Field<string>("StateName") + "",
                        // Area = s.Field<string>("Area") + "",
                        DoctorName = s.Field<string>("DoctorName") + "",

                        HospitalAddress = s.Field<string>("HospitalAddress") + "",
                        HospitalName = s.Field<string>("HospitalName") + "",
                        // UserAddress = s.Field<string>("UserAddress") + "",
                        //Phonenumber = s.Field<string>("Phonenumber") + "",
                        //DistrictName = s.Field<string>("DistrictName") + "",
                        Gender = s.Field<string>("Gender") + "",
                        BloodRequestID = s.Field<string>("BloodRequestID") + "",
                        // Age = s.Field<string>("Age") + "",
                        BloodRequestDate = s.Field<string>("BloodRequestDate") + "",
                        RequestTime = s.Field<string>("RequestTime") + "",
                        //Purpose = s.Field<string>("Purpose") + "",
                        //ContactPerson = s.Field<string>("ContactPerson") + "",
                        //ContactMobile = s.Field<string>("ContactMobile") + "",
                        HospitalPhonenumber = s.Field<string>("HospitalPhonenumber") + "",
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Update Device Token
        public dynamic UpdateDeviceToken(string tokenDetails)
        {
            try
            {
                List<UpdateToken> token = new JavaScriptSerializer().Deserialize<List<UpdateToken>>(tokenDetails);
                string XML = XMLConvert.GetXmlArrayString(token);
                ds = db.SaveDataReturnSingleXML(SPS.BG_UpdateDeviceToken.ToString(), XML);
                var checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {
                //saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                throw ex;
            }
        }
        #endregion

        public dynamic BG_Getbloodrequest_notification()
        {
            try
            {
                var MSG = "";

                // Retrieve data using the provided pincode
                var ds = db.GetData(SPS.BG_Getbloodrequest_notification.ToString());

                // Ensure that the DataSet has tables and rows
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    MSG = "No data found";
                    return MSG;
                }

                // Extract message from the first row of the first table
                MSG = ds.Tables[0].Rows[0][0].ToString();

                // Handle the case where no records exist
                if (MSG == "NOTEXIST")
                {
                    return MSG;
                }

                // Proceed if the data is valid
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb)
                {
                    var data = ds.Tables[0].AsEnumerable().Select(row => new Doners
                    {
                        RegId = row.Field<int?>("RegId") ?? 0,
                        Devicetoken = row.Field<string>("Devicetoken") ?? "",
                        FullName = row.Field<string>("FullName") ?? "",
                        Pincode = row.Field<string>("Pincode") ?? "",

                        TokenId = row.Field<Guid?>("TokenId")?.ToString() ?? "",
                        Rolestatus = row.Field<bool?>("Rolestatus")
                    }).ToList();

                    return data;
                }

                // Return message if data check fails
                return MSG;
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                exp.ExceptionHandler(ex);
                throw new ApplicationException("An error occurred while retrieving notifications.", ex);
            }
        }

        public dynamic BG_GetNotificationForUsers(string pincode)
        {
            try
            {
                var MSG = "";

                // Retrieve data using the provided pincode
                var ds = db.GetDataWithSingleParam(SPS.BG_GetNotificationForUsers.ToString(), pincode);

                // Ensure that the DataSet has tables and rows
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    MSG = "No data found";
                    return MSG;
                }

                // Extract message from the first row of the first table
                MSG = ds.Tables[0].Rows[0][0].ToString();

                // Handle the case where no records exist
                if (MSG == "NOTEXIST")
                {
                    return MSG;
                }

                // Proceed if the data is valid
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb)
                {
                    var data = ds.Tables[0].AsEnumerable().Select(row => new Doners
                    {
                        RegId = row.Field<int?>("RegId") ?? 0,
                        Devicetoken = row.Field<string>("Devicetoken") ?? "",
                        FullName = row.Field<string>("FullName") ?? "",
                        Pincode = row.Field<string>("Pincode") ?? "",

                        TokenId = row.Field<Guid?>("TokenId")?.ToString() ?? "",
                        Rolestatus = row.Field<bool?>("Rolestatus")
                    }).ToList();

                    return data;
                }

                // Return message if data check fails
                return MSG;
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                exp.ExceptionHandler(ex);
                throw new ApplicationException("An error occurred while retrieving notifications.", ex);
            }
        }

        public dynamic BG_Get_Referalcode_Notification(string RegId)
        {
            try
            {
                var MSG = "";

                // Retrieve data using the provided pincode
                var ds = db.GetDataWithSingleParam(SPS.BG_Get_Referalcode_Notification.ToString(), RegId);

                // Ensure that the DataSet has tables and rows
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    MSG = "No data found";
                    return MSG;
                }

                // Extract message from the first row of the first table
                MSG = ds.Tables[0].Rows[0][0].ToString();

                // Handle the case where no records exist
                if (MSG == "NOTEXIST")
                {
                    return MSG;
                }

                // Proceed if the data is valid
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb)
                {
                    var data = ds.Tables[0].AsEnumerable().Select(row => new Doners
                    {

                        Devicetoken = row.Field<string>("Devicetoken") ?? "",

                    }).ToList();

                    return data;
                }

                // Return message if data check fails
                return MSG;
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                exp.ExceptionHandler(ex);
                throw new ApplicationException("An error occurred while retrieving notifications.", ex);
            }
        }


        //suraj 

        public string SendFCMNotification(string deviceId, string text, string senderName, string path, string img, string accessToken)
        {
            try
            {
                // Set the Firebase project ID
                string projectId = "bloodgroup-94ed5"; // Replace with your actual Firebase project ID

                // Create the FCM HTTP v1 API endpoint URL
                string url = $"https://fcm.googleapis.com/v1/projects/{projectId}/messages:send";

                // Build the notification payload
                var message = new
                {
                    message = new
                    {
                        token = deviceId,
                        notification = new
                        {
                            body = text,
                            title = senderName,
                            image = img
                        },
                        data = new
                        {
                            sound = "default",
                            channel_id = "your-channel-id",
                            clickAction = path
                        }
                    }
                };

                // Serialize the payload to JSON
                var jsonMessage = JsonConvert.SerializeObject(message);
                var byteArray = Encoding.UTF8.GetBytes(jsonMessage);

                // Create an HTTP POST request
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add($"Authorization: Bearer {accessToken}");

                // Write the request body
                request.ContentLength = byteArray.Length;
                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                // Get the response from FCM
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var dataStreamResponse = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(dataStreamResponse))
                        {
                            string responseFromServer = reader.ReadToEnd();

                            // Parse the response to check if it contains a message name
                            if (responseFromServer.Contains("\"name\":"))
                            {
                                return "SUCCESS";
                            }
                            else
                            {
                                return "Failed to send notification";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                // Handle exceptions
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                return "ERROR";
            }
        }





        //public string SendFCMNotificationold(string DeviceId, string Text, string SenderName, string path, string Img)
        //{
        //    try
        //    {
        //        // Server key
        //        var applicationID = "AAAAx4aIIJA:APA91bEYIOJoC-z9e6SIl6_hL2MJyeZDvI-F-2mw6G68R_wOAbdrWxdgLf7NpR22SDm5ccpyDy0i3DI7KQNYIOSE377TK76USXXZml0EJXIjBmrTOjRWbQWr-6Ggf-aapgPUnpCeVsR0";
        //        // Sender Id
        //        var senderId = "856955560080";
        //        string deviceId = DeviceId;

        //        var request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        request.Method = "POST";
        //        request.ContentType = "application/json";
        //        request.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        request.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        var message = new
        //        {
        //            to = deviceId,
        //            notification = new
        //            {
        //                body = Text,
        //                title = SenderName,
        //                //image = "https://www.kasandbox.org/programming-images/avatars/spunky-sam.png"
        //                Image = Img
        //            },
        //            data = new
        //            {
        //                sound = "null",
        //                channel_id = "null",
        //                clickAction = path
        //            }
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(message);
        //        var byteArray = Encoding.UTF8.GetBytes(json);

        //        request.ContentLength = byteArray.Length;
        //        using (Stream dataStream = request.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //        }

        //        using (var response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (var dataStreamResponse = response.GetResponseStream())
        //            {
        //                using (var reader = new StreamReader(dataStreamResponse))
        //                {
        //                    string sResponseFromServer = reader.ReadToEnd();
        //                    return sResponseFromServer;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exp.ExceptionHandler(ex);
        //        // saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
        //        return "";
        //    }
        //}


        public dynamic BDE_GET_EmployessforDayincentive1()
        {
            try
            {
                var MSG = "";
                List<Replying> userdata = new List<Replying>();

                // Validate date parameter if necessary
                //DateTime parsedDate;
                //  bool isValidDate = DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);

                //  if (!isValidDate)
                //     {
                //     throw new ArgumentException("Invalid date format. Please provide the date in 'yyyy-MM-dd' format.");
                //   }

                // Use the provided date in the query
                ds = db.GetData(SPS.GetOldInactiveRegistrations.ToString());

                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Replying
                    {
                        RegId = A.Field<Int32?>("RegId") ?? 0,

                        Email = A.Field<string>("Email") + "",
                        FullName = A.Field<string>("FullName") + ""
                    }).ToList();
                    return userdata;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return " ";
            }
        }


        public dynamic BDE_GET_EmployessforDayincentive()
        {
            try
            {
                List<Replying> TaskDetails = new List<Replying>();

                // Open the database connection before executing the query
                ds = db.GetData(SPS.GetOldInactiveRegistrations.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    TaskDetails = ds.Tables[0].AsEnumerable().Select(A =>
                    new Replying()
                    {
                        RegId = A.Field<Int32?>("RegId") ?? 0,
                        Email = A.Field<string>("Email") + "",
                        FullName = A.Field<string>("FullName") + ""
                    }).ToList();
                }

                // Group tasks by AssignedTo
                var tasksByAssignedTo = TaskDetails.GroupBy(t => t.Email).ToDictionary(g => g.Key, g => g.ToList());

                // Iterate over each group and send email
                foreach (var kvp in tasksByAssignedTo)
                {
                    string Email = kvp.Key;
                    List<Replying> tasksForAssignedTo = kvp.Value;

                    // Serialize the list of tasks to JSON
                    string tasksJson = new JavaScriptSerializer().Serialize(tasksForAssignedTo);

                    // Send the email with the JSON content
                    var mailResult = mail.Sendmails_Remindertasks1(tasksJson);

                    // Check mailResult and handle it if necessary
                    if (mailResult == "SUCCESS")
                    {
                        // Handle success case, log it, etc.
                    }
                    else
                    {
                        // Handle failure case, log it, etc.
                    }
                }

                // Close the database connection after executing the query
                return TaskDetails;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return null; // Ensure a value is returned in case of an exception
            }
        }

        #region Get_Notification_basedon_UserId -- manohar
        public dynamic Get_Notification_basedon_UserId(string param)
        {
            try
            {
                var MSG = "";
                List<Crud_Notifications> data = new List<Crud_Notifications>();
                ds = db.GetDataWithOneParameter(SPS.Get_Notification_basedon_UserId.ToString(), param);
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    data = ds.Tables[0].AsEnumerable().Select(A => new Crud_Notifications
                    {
                        NotificationsID = A.Field<Int32?>("NotificationsID") + 0,
                        NotificationsDesc = A.Field<string>("NotificationsDesc") + "",
                        NotificationsImage = A.Field<string>("NotificationsImage") + "",
                        RegID = A.Field<Int32?>("RegID") + 0,
                        Status = A.Field<Boolean?>("Status") ?? null,
                        CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                        CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                        ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                        ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null
                    }).ToList();
                    return data;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Weabapi");
                return "";
            }

        }

        #endregion 

        #region Get_USERrequest_Closedrequest_Idbased -- manohar
        public dynamic Get_USERrequest_Closedrequest_Idbased(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                List<Doners> request = new List<Doners>();
                ds = db.GetDataWithTwoParameters(SPS.Get_USERrequest_Closedrequest_Idbased.ToString(), Param1, Param2);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdb = db.CheckDatainDS(ds, 0);
                    if (checkdb == true)
                    {
                        request = ds.Tables[0].AsEnumerable().Select(A => new Doners
                        {
                            UdId = A.Field<Int32?>("UdId") ?? 0,
                            RegId = A.Field<Int32?>("RegId") ?? 0,
                            ApprovalStatus = A.Field<Int32?>("ApprovalStatus") ?? 0,
                            FullName = A.Field<string>("FullName") + "",
                            Gender = A.Field<string>("Gender") + "",
                            BloodRequestDate = A.Field<string>("BloodRequestDate") + "",
                            RequestTime = A.Field<string>("RequestTime") + "",
                            BLGName = A.Field<string>("BLGName") + "",
                            Typesofblood = A.Field<string>("Typesofblood") + "",
                            CityName = A.Field<string>("CityName") + "",
                            newStatename = A.Field<string>("newStatename") + "",
                            newDistrictname = A.Field<string>("newDistrictname") + "",
                            newCityname = A.Field<string>("newCityname") + "",
                            StateName = A.Field<string>("StateName") + "",
                            Area = A.Field<string>("Area") + "",
                            DoctorName = A.Field<string>("DoctorName") + "",
                            Pincode = A.Field<string>("Pincode") + "",
                            Latitude = A.Field<string>("Latitude") + "",
                            Longitude = A.Field<string>("Longitude") + "",

                            patientname = A.Field<string>("patientname") + "",
                            HospitalAddress = A.Field<string>("HospitalAddress") + "",
                            HospitalName = A.Field<string>("HospitalName") + "",
                            UserAddress = A.Field<string>("UserAddress") + "",
                            Phonenumber = A.Field<string>("Phonenumber") + "",
                            DistrictName = A.Field<string>("DistrictName") + "",
                            Age = A.Field<string>("Age") + "",
                            Purpose = A.Field<string>("Purpose") + "",
                            ContactPerson = A.Field<string>("ContactPerson") + "",
                            ContactMobile = A.Field<string>("ContactMobile") + "",
                            HospitalPhonenumber = A.Field<string>("HospitalPhonenumber") + "",

                        }).ToList();
                        return request;
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }

        #endregion

        #region Crud_Banners -- manohar
        public dynamic Crud_Banners(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<Banner> userdata = new JavaScriptSerializer().Deserialize<List<Banner>>(xml);
                var data = XMLConvert.GetXmlArrayString<Banner>(userdata);
                ds = db.SaveDataReturn1(SPS.Crud_Banners.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdb = db.CheckDatainDS(ds, 0);
                    if (checkdb == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag =="5")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new Banner()
                            {
                                BannerId = A.Field<int>("BannerId") + 0,
                                BannerName = A.Field<string>("BannerName") + "",
                                BannerPath = A.Field<string>("BannerPath") + "",
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                CratedDate = A.Field<DateTime?>("CratedDate") ?? null,

                            }).ToList();
                            return userdata;
                        }
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }

        #endregion


        #region GetBanners --venkat
        public dynamic GetBanners()
        {
            try
            {
                var MSG = "";
                List<Banners> data = new List<Banners>();
                ds = db.GetData(SPS.GetBanners.ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var CheckDb = db.CheckDatainDS(ds, 0);
                    if (CheckDb == true)
                    {
                        data = ds.Tables[0].AsEnumerable().Select(
                            A => new Banners()
                            {

                                BannerID = A.Field<Int32?>("BannerID") + 0,
                                BannerImage = A.Field<string>("BannerImage") + "",
                                Sequence = A.Field<Int32?>("Sequence") + 0,
                                BannerStatus = A.Field<Boolean?>("BannerStatus") ?? false,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,
                            }).ToList();
                        return data;
                    }
                }
                return MSG;

            }
            catch (Exception ex)
            {
                //  saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                throw ex;
            }

        }
        #endregion

        #region Crud_ThreeImages ---->Amrutha written by 14/10/2025
        public dynamic Crud_ThreeImages(string xml, string flag)
        {
            try
            {
                var MSG = "";
                List<ThreeImages> userdata = new JavaScriptSerializer().Deserialize<List<ThreeImages>>(xml);
                var data = XMLConvert.GetXmlArrayString<ThreeImages>(userdata);
                ds = db.SaveDataReturn1(SPS.Crud_ThreeImages.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdb = db.CheckDatainDS(ds, 0);
                    if (checkdb == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new ThreeImages()
                            {
                                BannerID = A.Field<Int32?>("BannerID") + 0,
                                BannerImage = A.Field<string>("BannerImage") + "",
                                Sequence = A.Field<Int32?>("Sequence") + 0,
                                BannerStatus = A.Field<Boolean?>("BannerStatus") ?? false,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,

                            }).ToList();
                            return userdata;
                        }
                    }
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }

        #endregion





        #region Update_ActivateAccount -- manohar
        public dynamic Update_ActivateAccount(string email)
        {
            try
            {
                var MSG = "";
                List<active> userdata = new List<active>();
                ds = db.GetDataWithSingleParam(SPS.Update_ActivateAccount.ToString(), email);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                    return MSG;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        #region GetUsers_basedonPincode_BloodreqCondition -- manohar
        public dynamic GetUsers_basedonPincode_BloodreqCondition(string code, string bloodid)
        {
            try
            {
                var MSG = "";
                List<Bloodreqcondition> userdata = new List<Bloodreqcondition>();
                ds = db.GetDataWithTwoParameters(SPS.GetUsers_basedonPincode_BloodreqCondition.ToString(), code, bloodid);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Bloodreqcondition
                    {
                       
                        RegId = A.Field<int?>("RegId"),
                        RoleId = A.Field<int?>("RoleId"),
                        Devicetoken = A.Field<string>("Devicetoken") ?? "",
                        FullName = A.Field<string>("FullName") ?? "",
                        BloodGroupName = A.Field<string>("BLGName") ?? "",
                        Pincode = A.Field<string>("Pincode") ?? "",
                        Lastdonatedate = A.Field<DateTime?>("Lastdonatedate")?? null,
                        Phonenumber = A.Field<string>("Phonenumber") ?? ""
                    }).ToList();
                    return userdata;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }

        #endregion

        #region Update_Usercontrollers -- manohar
        public dynamic Update_Usercontrollers(string noti, string whatsapp, string emailsta, string Status, string ExpairyDate, string Statusphn, string TimePeriod)
        {
            try
            {
                var MSG = "";
                List<NotiStatus> statusdata = new List<NotiStatus>();
                ds = db.GetDataWithSevenParam(SPS.Update_Usercontrollers.ToString(), noti, whatsapp, emailsta, Status, ExpairyDate, Statusphn, TimePeriod);
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }

        }

        #endregion



        #region Update_request_donors -- suraj
        public dynamic Update_request_donors(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetDataWithTwoParameters(SPS.Approve_Requests_byDonors.ToString(), Param1, Param2);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                    return MSG;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        //#region
        //public class TranslationContext : DbContext
        //{
        //    public TranslationContext() : base("name=BloodGroup") { }

        //    public DbSet<Translation> Translations { get; set; }
        //}

        //#endregion


        #region  BG_GetUserControllers 
        public dynamic BG_GetUserControllers()
        {
            try
            {
                var MSG = "";
                List<BG_GetUserControllers> userdata = new List<BG_GetUserControllers>();
                ds = db.GetData(SPS.BG_GetUserControllers.ToString());
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new BG_GetUserControllers()
                    {
                        NotificationID = A.Field<Int32?>("NotificationID") + 0,
                        NotificationsDesc = A.Field<string>("NotificationsDesc") + "",
                        NotificationsImage = A.Field<string>("NotificationsImage") + "",
                        UserID = A.Field<Int32?>("UserID") + 0,
                        ExpairyDate = A.Field<DateTime?>("ExpairyDate") ?? null,
                        WhatsappStatus = A.Field<Boolean?>("WhatsappStatus") ?? null,
                        CallStatus = A.Field<Boolean?>("CallStatus") ?? null,
                        SMSStatus = A.Field<Boolean?>("SMSStatus") ?? null,
                        TimePeriod = A.Field<string>("TimePeriod") + null,
                        RoleId = A.Field<Int32?>("RoleId") + 0,
                        NotificationStatus = A.Field<Boolean?>("NotificationStatus") ?? null,
                        EmailStatus = A.Field<Boolean?>("EmailStatus") ?? null,
                        CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                        CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                        ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                        ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0,



                    }).ToList();
                }
                return userdata;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region BG_UserControllersInsert 
        public dynamic BG_UserControllersInsert(string UserID, string RoleId, string ExpairyDate, string WhatsappStatus, string CallStatus, string SMSStatus, string NotificationStatus, string EmailStatus, string TimePeriod)
        {
            try
            {
                var MSG = "";
                List<BG_UserControllersInsert> statusdata = new List<BG_UserControllersInsert>();
                ds = db.GetDataWithNineParam(SPS.BG_UserControllersInsert.ToString(), UserID, RoleId, ExpairyDate, WhatsappStatus, CallStatus, SMSStatus, NotificationStatus, EmailStatus, TimePeriod);
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }

        }
        #endregion


        #region BG_UserControllersUpdate 
        public dynamic BG_UserControllersUpdate(string @Param1, string @Param2, string @Flag)
        {
            try
            {
                var MSG = "";
                List<NotiStatus> statusdata = new List<NotiStatus>();
                ds = db.GetDataWithTwoParametersSingleFlag(SPS.BG_UserControllersUpdate.ToString(), Param1, Param2, Flag);
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }

        }

        #endregion


        public dynamic Get_RoleforRolechange(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithTwoParameters(SPS.GetRoleforRolechange.ToString(), Param1, Param2);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();

                    var checkdb = db.CheckDatainDS(ds, 0);
                    if (checkdb == true)
                    {
                        if (Param2 == "1")
                        {
                            // Return list of RegId and RoleId
                            var request = ds.Tables[0].AsEnumerable().Select(A => new Roleees
                            {
                                RegId = A.Field<Int32?>("RegId") ?? 0,
                                RoleId = A.Field<Int32?>("RoleId") ?? 0,                        
                                Availablestatus = A.Field<Boolean?>("Availablestatus") ?? null
                            }).ToList();

                            return request;
                        }
                        else if (Param2 == "2")
                        {
                            // Return list of Lastdonatedate
                            var request = ds.Tables[0].AsEnumerable().Select(A => new
                            {
                                NextEligibleDonateDate = A.Field<DateTime?>("NextEligibleDonateDate") ?? null,                         
                            }).ToList();

                            return request;
                        }
                    }
                }

                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw;
            }
        }


        #region  Crud_UserCommunicationPreferences ----Upendra
        public dynamic Crud_UserCommunicationPreferences(string xmlparam, string flag)
        {
            try
            {
                var MSG = "";
                List<Crud_UserCommunication> userdata = new JavaScriptSerializer().Deserialize<List<Crud_UserCommunication>>(xmlparam);
                var data = XMLConvert.GetXmlArrayString<Crud_UserCommunication>(userdata);
                ds = db.SaveDataReturn1(SPS.UserCommunicationPreferences.ToString(), data, flag);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();
                    var checkdata = db.CheckDatainDS(ds, 0);
                    if (checkdata == true)
                    {
                        if (flag == "1" || flag == "2" || flag == "3" || flag == "6")
                        {
                            MSG = db.GetMessage(ds);
                            return MSG;
                        }
                        else if (flag == "4")
                        {
                            userdata = ds.Tables[0].AsEnumerable().Select(A => new Crud_UserCommunication()
                            {
                                CommunicationID = A.Field<Int32?>("CommunicationID") + 0,
                                UserId = A.Field<Int32?>("UserId") + 0,
                                PhoneCallFreq = A.Field<Int32?>("PhoneCallFreq") + 0,
                                WhatsAppFreq = A.Field<Int32?>("WhatsAppFreq") + 0,
                                NotificationFreq = A.Field<Int32?>("NotificationFreq") + 0,
                                SMSFreq = A.Field<Int32?>("SMSFreq") + 0,
                                PhoneCallLast = A.Field<DateTime?>("PhoneCallLast") ?? null,
                                PhoneCallNext = A.Field<DateTime?>("PhoneCallNext") ?? null,
                                WhatsAppLast = A.Field<DateTime?>("WhatsAppLast") ?? null,
                                WhatsAppNext = A.Field<DateTime?>("WhatsAppNext") ?? null,
                                NotificationLast = A.Field<DateTime?>("NotificationLast") ?? null,
                                NotificationNext = A.Field<DateTime?>("NotificationNext") ?? null,
                                SMSLast = A.Field<DateTime?>("SMSLast") ?? null,
                                SMSNext = A.Field<DateTime?>("SMSNext") ?? null,
                                Status = A.Field<Boolean?>("Status") ?? null,
                                CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                                CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                                ModifiedDate = A.Field<DateTime?>("ModifiedDate") ?? null,
                                ModifiedBy = A.Field<Int32?>("ModifiedBy") + 0                             
                            }).ToList();
                            return userdata;
                        }
                    }
                    //  ds.Clear();
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Get_CommunicationMaster --> Upendra
        public dynamic Get_CommunicationMaster()
        {
            try
            {
                List<UserCommunication> allExceptions = new List<UserCommunication>();
                ds = db.GetData(SPS.Get_CommunicationMaster.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new UserCommunication()
                    {
                        CommTypeId = c.Field<Int32?>("CommTypeId") ?? 0,                       
                        CommTypeName = c.Field<string>("CommTypeName") + "",
                        Description = c.Field<string>("Description") + "",
                        Status = c.Field<Boolean?>("Status") ?? null

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion



        #region Get_userCommunicationPreferences --Upendra
        public dynamic Get_userCommunicationPreferences(string Param1)
        {
            try
            {            
                List<UserCommunicationn> userdata = new List<UserCommunicationn>();
                ds = db.GetDataWithSingleParam(SPS.Get_UserCommunicationPreferences.ToString(), Param1);
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(c =>
                    new UserCommunicationn()
                    {
                        CommunicationID = c.Field<Int32?>("CommunicationID") ?? 0,
                        UserId = c.Field<Int32?>("UserId") ?? 0,
                        PhoneCallFreq = c.Field<Int32?>("PhoneCallFreq") ?? 0,
                        WhatsAppFreq = c.Field<Int32?>("WhatsAppFreq") ?? 0,
                        SMSFreq = c.Field<Int32?>("SMSFreq") ?? 0,
                        NotificationFreq = c.Field<Int32?>("NotificationFreq") ?? 0,
                        Status = c.Field<bool?>("Status")
                        
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        #region Update_CommunicationChannel 
        public dynamic Update_CommunicationChannel(string @Param1, string @Param2)
        {
            try
            {
                var MSG = "";
                List<NotiStatus> statusdata = new List<NotiStatus>();
                ds = db.GetDataWithTwoParameters(SPS.Update_CommunicationChannel.ToString(), Param1, Param2);
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }

        }

        #endregion


        #region BloodRequestClosed -- Upendra
        public dynamic BloodRequestClosed(string Param1)
        {
            try
            {
                var MSG = "";
                List<NotiStatus> statusdata = new List<NotiStatus>();
                ds = db.GetDataWithSingleParam(SPS.BloodRequestClosed.ToString(), Param1);
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }

        }

        #endregion

        #region  AfterAdminApprovedNotificationtoAll --Upendra
        public dynamic AfterAdminApprovedNotificationtoAll(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                List<Doners> userdata = new List<Doners>();
                ds = db.GetDataWithTwoParameters(SPS.AfterAdminApprovedNotificationtoAll.ToString(), Param1, Param2);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Doners()
                    {
                        RegId = A.Field<int?>("RegId") ?? 0,
                        RoleId = A.Field<int?>("RoleId") ?? 0,
                        Devicetoken = A.Field<string>("Devicetoken") ?? "",
                        FullName = A.Field<string>("FullName") ?? "",
                        Pincode = A.Field<string>("Pincode") ?? "",

                        Phonenumber = A.Field<string>("Phonenumber") + "",
                        Password = A.Field<string>("Password") + "",
                        Email = A.Field<string>("Email") + "",
                        Age = A.Field<string>("Age") + "",
                        Gender = A.Field<string>("Gender") + "",
                        TokenId = A.Field<Guid?>("TokenId")?.ToString() ?? "",
                        Rolestatus = A.Field<bool?>("Rolestatus")


                    }).ToList();
                    return userdata;
                }
                return MSG;

            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion

        #region DashBoardDetails --Upendra
        public dynamic DashBoardDetails(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithTwoParameters(SPS.DashBoardDetails.ToString(), Param1, Param2);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (!checkdb)
                    return MSG;

                switch (Param2)
                {
                    case "1":
                        // Case 1: GalleryUserDetails
                        var galleryList = ds.Tables[0].AsEnumerable().Select(A => new Gallery()
                        {
                            GalleryImages = A.Field<string>("GalleryImages") + "",
                            RegId = A.Field<Int32?>("RegId") + 0,
                            Email = A.Field<string>("Email") + "",
                            Gender = A.Field<string>("Gender") + "",
                            Phonenumber = A.Field<string>("Phonenumber") + "",
                            Institutionname = A.Field<string>("Institutionname") + "",
                            Age = A.Field<string>("Age") + "",
                            CreatedDate = A.Field<DateTime?>("CreatedDate") ?? null,
                            CreatedBy = A.Field<Int32?>("CreatedBy") + 0,
                            HospitalPhonenumber = A.Field<string>("HospitalPhonenumber") + "",
                            FullName = A.Field<string>("FullName") + "",
                            HospitalAddress = A.Field<string>("HospitalAddress") + "",
                            HospitalName = A.Field<string>("HospitalName") + "",
                            newStatename = A.Field<string>("newStatename") + "",
                            newDistrictname = A.Field<string>("newDistrictname") + "",
                            newCityname = A.Field<string>("newCityname") + "",
                            Pincode = A.Field<string>("Pincode") + "",
                            Typesofblood = A.Field<string>("Typesofblood") + "",
                            Purpose = A.Field<string>("Purpose") + ""
                        }).ToList();
                        return galleryList;

                    case "6":
                        // Case 2: UserDetailsRequest
                        var userDetailsList = ds.Tables[0].AsEnumerable().Select(A => new UserDetailsModel()
                        {
                            UdId = A.Field<int>("UdId"),
                            RegId = A.Field<int>("RegId"),
                            BloodRequestID = A.Field<string>("BloodRequestID"),
                            FullName = A.Field<string>("Patientname") ?? "",
                            ContactMobile = A.Field<string>("ContactMobile") ?? "",
                            DoctorName = A.Field<string>("DoctorName") ?? "",
                            HospitalName = A.Field<string>("HospitalName") ?? "",
                            Purpose = A.Field<string>("Purpose") ?? "",
                            Typesofblood = A.Field<string>("Typesofblood") ?? "",
                            BloodRequestDate = A.Field<string>("BloodRequestDate"),
                            RequestTime = A.Field<string>("RequestTime") ?? "",
                            Area = A.Field<string>("Area") ?? "",
                            Latitude = A.Field<string>("Latitude") ?? "",
                            Longitude = A.Field<string>("Longitude") ?? "",
                            BLGName = A.Field<string>("BLGName") ?? "",
                            UnitsofBlood = A.Field<string>("UnitsofBlood") ?? "",
                            HospitalAddress = A.Field<string>("HospitalAddress") ?? "",
                            ContactPerson = A.Field<string>("ContactPerson") ?? "",
                            // Add more fields as needed
                        }).ToList();
                        return userDetailsList;

                    case "4":
                    case "7":
                        // Case 3 and 4: Referred Users (RoleId 4 or 2)
                        var registrationList = ds.Tables[0].AsEnumerable().Select(A => new RegistrationModel()
                        {
                            RegId = A.Field<int>("RegId"),
                            FullName = A.Field<string>("FullName") ?? "",
                            Email = A.Field<string>("Email") ?? "",
                            Age = A.Field<string>("Age") ?? "",
                            Gender = A.Field<string>("Gender") ?? "",
                            Phonenumber = A.Field<string>("Phonenumber") ?? "",
                            RoleId = A.Field<int>("RoleId"),
                            ReffererId = A.Field<int?>("ReffererId") ?? 0,
                            CreatedDate = A.Field<DateTime?>("CreatedDate")
                            // Add other fields if needed
                        }).ToList();
                        return registrationList;

                    case "5":
                        // Case 5: Download Brochure
                        var brochureList = ds.Tables[0].AsEnumerable().Select(A => new BrochureModel()
                        {
                            FullName = A.Field<string>("FullName") ?? "",
                            Place = A.Field<string>("Place") ?? "",
                            Place_name = A.Field<string>("Place_name") ?? "",
                            Location = A.Field<string>("Location") ?? "",
                            Pincode = A.Field<string>("Pincode") ?? ""
                        }).ToList();
                        return brochureList;

                    default:
                        return MSG;
                }
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region Update_RegisterApprovalStatus --Venkat
        public dynamic Update_RegisterApprovalStatus(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithTwoParameters(SPS.Update_RegisterApprovalStatus.ToString(), Param1, Param2);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }
                return MSG;
            }
            catch (Exception ex)
            {

                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return " ";
            }
        }

        #endregion 


        #region GetLocation_Donationdate_basedonBrequestID-->Commented By Amrutha
        public dynamic GetLocation_Donationdate_basedonBrequestID(string Param1)
        {
            try
            {
                List<RequestForm> services = new List<RequestForm>();

                ds = db.GetDataWithSingleParam(SPS.GetLocation_Donationdate_basedonBrequestID.ToString(), Param1);

                bool hasData = db.CheckDatainDS(ds, 0);

                 
                // Map dataset rows into model
                services = ds.Tables[0].AsEnumerable().Select(c => new RequestForm()
                {
                    RegId = c.Field<Int32?>("RegId") ?? 0,
                    FullName = c.Field<string>("FullName") + "",
                    BloodRequestID = c.Field<string>("BloodRequestID") + "",
                    BloodRequestDate = c.Field<string>("BloodRequestDate") + "",
                    HospitalAddress = c.Field<string>("HospitalAddress") + "",
                }).ToList();

                return services;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw;
            }
        }
        #endregion


        #region Update_SYSSubmitted_ByBloodRequestID ---Amrutha
        public dynamic Update_SYSSubmitted_ByBloodRequestID(string Param1)
        {
            try
            {
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetDataWithSingleParam(SPS.Update_SYSSubmitted_ByBloodRequestID.ToString(), Param1);
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(c =>
                    new RequestForm()
                    {
                       
                        UdId = c.Field<Int32?>("UdId") + 0,
                        ApprovalStatus = c.Field<Int32?>("ApprovalStatus") + 0,
                        FullName = c.Field<string>("FullName") + "",
                        
                        Area = c.Field<string>("Area") + "",
                        DoctorName = c.Field<string>("DoctorName") + "",
                        Latitude = c.Field<string>("Latitude") + "",
                        Longitude = c.Field<string>("Longitude") + "",
                        BloodRequestID = c.Field<string>("BloodRequestID") + "",
                        SYSSubmitted = c.Field<Int32?>("SYSSubmitted") + 0, 
                        BloodRequestDate = c.Field<string>("BloodRequestDate") + "",
                        RequestTime = c.Field<string>("RequestTime") + "",
                        Purpose = c.Field<string>("Purpose") + "",
                        UnitsofBlood = c.Field<string>("UnitsofBlood") + "",
                        Typesofblood = c.Field<string>("Typesofblood") + "",
                     

                      
                       

                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion

        #region  InsertContactReport_MOBILE ----Amrutha
        public dynamic InsertContactReport_MOBILE(string xmlParam)
        {
            try
            {
                var MSG = "";
                List<ContactReport> userdata = new JavaScriptSerializer().Deserialize<List<ContactReport>>(xmlParam);
                var data = xml.GetXmlArrayString<ContactReport>(userdata);
                ds = db.GetsinglexmlParam(SPS.InsertContactReport_MOBILE.ToString(), data);

                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                    return MSG;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "0", "Webapi");
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region BG_GetContactReport  ---->Written By Amrutha
        public dynamic BG_GetContactReport()
        {
            try
            {
                List<ContactReport> allExceptions = new List<ContactReport>();
                ds = db.GetData(SPS.BG_GetContactReport.ToString());
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata == true)
                {
                    allExceptions = ds.Tables[0].AsEnumerable().Select(c =>
                    new ContactReport()
                    {
                        RegId = c.Field<Int32?>("RegId") + 0, 
                        Report = c.Field<string>("Report") + "",
                        Comments = c.Field<string>("Comments") + "",
                        FullName = c.Field<string>("FullName") + "",
                        Phonenumber = c.Field<string>("Phonenumber") + "",
                        Email = c.Field<string>("Email") + "",
                        CreatedDate = c.Field<DateTime?>("CreatedDate") ?? null,

                    }).ToList();
                }
                return allExceptions;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region GetLatestLastDonationDate  ---->Written By Amrutha
        public dynamic GetLatestLastDonationDate(string Param1, string Param2)
        {
            try
            {
                var MSG = "";
                ds = db.GetDataWithTwoParameters(SPS.GetLatestLastDonationDate.ToString(), Param1, Param2);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MSG = ds.Tables[0].Rows[0][0].ToString();

                    var checkdb = db.CheckDatainDS(ds, 0);
                    if (checkdb == true)
                    {
                        if (Param2 == "1")
                        {
                            // Return list of RegId and RoleId
                            var request = ds.Tables[0].AsEnumerable().Select(A => new Roleees
                            {
                                RegId = A.Field<Int32?>("RegId") ?? 0,
                                RoleId = A.Field<Int32?>("RoleId") ?? 0
                            }).ToList();

                            return request;
                        }
                        else if (Param2 == "2" || Param2 =="3" )
                        {
                            // Return list of Lastdonatedate
                            var request = ds.Tables[0].AsEnumerable().Select(A => new
                            {
                                Lastdonatedate = A.Field<string>("Lastdonatedate") + ""
                            }).ToList();

                            return request;
                        }
                    }
                }

                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw;
            }
        }
        #endregion

        #region CheckAnydonorisaccepted -- Upendra
        public dynamic CheckAnydonorisaccepted(string Param1)
        {
            try
            {
                var MSG = "";
                List<RequestForm> userdata = new List<RequestForm>();
                ds = db.GetDataWithOneParameter(SPS.CheckAnydonorisaccepted.ToString(), Param1);
                MSG = ds.Tables[0].Rows[0][0].ToString();
                var checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                    return MSG;
                }
                return MSG;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "webapi");
                throw ex;
            }
        }
        #endregion


        #region BG_UpdatePassword_ByEmail --Asif
        public dynamic BG_UpdatePassword_ByEmail(string Param1, string Param2)
        {
            try
            {
                string MSG = "";

                // Call the SP with two parameters
                ds = db.GetDataWithTwoParameters(SPS.BG_UpdatePassword_ByEmail.ToString(), Param1, Param2);

                // Check if SP returned data
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    MSG = db.GetMessage(ds);
                }

                return MSG; // Returns 'UPDATED' or 'USER_NOT_FOUND'
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                return " ";
            }
        }
        #endregion
        #region UpdateNotificationstatus -- Upendra

        public dynamic UpdateNotificationstatus(string Param1, string Param2)
        {
            try
            {
             ds = db.GetDataWithTwoParameters(SPS.NotificationStatusUpdate.ToString(),Param1,Param2);
                bool checkdata = db.CheckDatainDS(ds, 0);
                if (checkdata)
                {                 
                    if (Param2 == "1")
                    {
                        var result = ds.Tables[0].AsEnumerable().Select(c => new
                        {
                            MSG = c.Field<string>("MSG")
                        }).ToList();

                        return result;
                    }
                   
                    if (Param2 == "2")
                    {
                        var result = ds.Tables[0].AsEnumerable().Select(c => new
                        {
                            Count = c.Field<Int32?>("Count") ?? 0
                        }).ToList();

                        return result;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message, ex.StackTrace).ToString(), "1", "webapi");
                throw;
            }
        }
        #endregion


        #region Get_PresentationDetails --->Upendra
        public dynamic Get_PresentationAcceptedCount(string Param1)
        {
            try
            {
                List<Counts> userdata = new List<Counts>();
                ds = db.GetDataWithSingleParam(SPS.Get_PresentationAcceptedCount.ToString(), Param1);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Counts
                    {
                        Accepted = A.Field<Int32?>("Accepted") + 0,
                       
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion


        #region Get_PresentationDetails --->Upendra
        public dynamic Get_PresentationAcceptedDetails(string Param1)
        {
            try
            {
                List<Countss> userdata = new List<Countss>();
                ds = db.GetDataWithSingleParam(SPS.Get_Acceptedpersons.ToString(), Param1);
                bool checkdb = db.CheckDatainDS(ds, 0);
                if (checkdb == true)
                {
                    userdata = ds.Tables[0].AsEnumerable().Select(A => new Countss
                    {
                        Mode = A.Field<string>("Mode") + "",
                        Audiance = A.Field<string>("Audiance") + "",
                        Venue = A.Field<string>("Venue") + "",
                        Venue_name = A.Field<string>("Venue_name") + "",
                        Address = A.Field<string>("Address") + "",
                        newStatename = A.Field<string>("newStatename") + "",
                        newCityname = A.Field<string>("newCityname") + "",
                        Area = A.Field<string>("Area") + "",
                        Pincode = A.Field<string>("Pincode") + "",
                        Contact_name = A.Field<string>("Contact_name") + "",
                        Contact_number = A.Field<string>("Contact_number") + "",
                        Message = A.Field<string>("Message") + "",
                        Latitude = A.Field<string>("Latitude") + "",
                        Longitude = A.Field<string>("Longitude") + "",
                        FullName = A.Field<string>("FullName") + "",
                        RPId = A.Field<Int32?>("RPId") + 0,
                        Requestedto = A.Field<Int32?>("Requestedto") + 0,
                        PresentationID = A.Field<Int32?>("PresentationID") + 0,
                        RequestAcceptStatus = A.Field<Int32?>("RequestAcceptStatus") + 0,
                        RequestDate = A.Field<DateTime?>("RequestDate") ?? null,

                        RegId = A.Field<Int32?>("RegId") + 0,
                        Phonenumber = A.Field<string>("Phonenumber") + "",
                        RegState = A.Field<string>("RegState") + "",
                        RegDistrict = A.Field<string>("RegDistrict") + "",
                        RegCity = A.Field<string>("RegCity") + "",
                        RegPin = A.Field<string>("RegPin") + "",
                        RegArea = A.Field<string>("RegArea") + "",
                    }).ToList();
                }
                return userdata;
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion
    }

}

