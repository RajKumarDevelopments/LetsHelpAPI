using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Web.Http;
using System.Xml;
using System.Web.Http;
using BloodGroupApi.Models;
using System.Web.UI;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using PdfImage = iTextSharp.text.Image;
using static BloodGroupApi.Models.Models;
using System.Web;

namespace BloodGroupApi.Models
{
    public class Mail
    {

        #region declarations -- Shekar
        StringBuilder sbDesignAdmin = new StringBuilder();
        StringBuilder sbDesign = new StringBuilder();
        StringBuilder maildesignPart1 = new StringBuilder();
        StringBuilder maildesignPart2 = new StringBuilder();
        Database db = new Database();
        Exceptions exp = new Exceptions();
        //YKLABS_XmlProviders xml = new YKLABS_XmlProviders();
        DataSet ds = new DataSet();
        string MSG = "";
        #endregion

        public void saveExceptions(string ex, string UserID, string source)
        {
            db.GetDataWithThreeParameters(SPS.BG_Webapi_Exceptions.ToString(), UserID, ex, source);
            //mail.MailForExceptions(ex);
        }

        #region  EnquiryMailTo_BloodCustomer5 -- Deeksha,Amrutha 
        public dynamic EnquiryMailTo_BloodCustomer5(string Email)
        {
            try
            {
                List<EnquiryInfo> Ticket = new JavaScriptSerializer().Deserialize<List<EnquiryInfo>>(Email);
                string str = Ticket[0].FarmerName;
                char[] a = str.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                string CustomerName = new string(a);
                {

                    sbDesign.Append("<table = '' style=\"width: 50%;border:none;font-family:poppins;background-color: rgba(0, 0, 0, 0.10196078431372549);padding:1%;line-height: 1.3;\">");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + "Mr/Ms." + Ticket[0].FullName + "</td>");
                    sbDesign.Append("</tr>");


                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Email</strong></td>");
                    sbDesign.Append("<td style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].Address + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Comments</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + Ticket[0].Comments + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("</table>");

                    sbDesign.Append("<table = ''>");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Regards, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #d7127b;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "");
                    sbDesign.Append("</tr>");


                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Tel:+1234567890</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Email: test@gmail</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com/</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("</table>");


                    var subject = "Enquiry Details";
                    var Mail = SendMail2(Ticket[0].Email, sbDesign.ToString(), subject);


                    sbDesign.Clear();
                }


                return "SUCCESS";
            }
            catch (Exception ex)
            {
                //exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion


        #region Send Mail Dynamic
        public string SendMail2(string Email, string sbDesign, string subject)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();

                if (listsmtpdetails == null || listsmtpdetails.Count == 0)
                {
                    throw new Exception("SMTP details not found.");
                }

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(
                        listsmtpdetails[0].fromaddress,
                        "Let's Help",
                        Encoding.UTF8
                    );

                    // TO → Admin (Required)
                    if (!string.IsNullOrWhiteSpace(listsmtpdetails[0].ToAddress))
                    {
                        mail.To.Add(listsmtpdetails[0].ToAddress);
                    }
                    else
                    {
                        throw new Exception("Admin email (ToAddress) is missing in MailDetails table.");
                    }

                    // CC → User (Optional)
                    if (!string.IsNullOrWhiteSpace(Email) &&
                        !Email.Equals("undefined", StringComparison.OrdinalIgnoreCase))
                    {
                        mail.CC.Add(Email);
                    }

                    // BCC (Optional)
                    if (!string.IsNullOrWhiteSpace(listsmtpdetails[0].BccAddress))
                    {
                        mail.Bcc.Add(listsmtpdetails[0].BccAddress);
                    }

                    mail.Subject = subject;
                    mail.Body = sbDesign;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.Priority = MailPriority.High;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = listsmtpdetails[0].host;
                        smtp.Port = listsmtpdetails[0].port;
                        smtp.EnableSsl = (bool)listsmtpdetails[0].enableSsl;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(
                            listsmtpdetails[0].fromaddress,
                            listsmtpdetails[0].frompassword
                        );

                        smtp.Send(mail);
                    }
                }

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion


        #region Send mail2 ----Amrutha/Upendra
        public string SendMailAccepted(string Email, string CreatedByEmail, string sbDesign, string subject)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(
                    listsmtpdetails[0].fromaddress,
                    "Let's Help",
                    Encoding.UTF8
                );
                mail.To.Add(CreatedByEmail);
                if (!string.IsNullOrEmpty(Email) && Email != "undefined")
                {
                    mail.CC.Add(Email);
                }



                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = sbDesign;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host);
                smtp.Port = listsmtpdetails[0].port;

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    listsmtpdetails[0].fromaddress,
                    listsmtpdetails[0].frompassword
                );

                smtp.EnableSsl = true;
                smtp.Send(mail);

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Send mail  --deeksha,Amrutha
        public string SendMail(string EmailId, string sbDesign, string subject)
        {
            try
            {

                List<SmtpDetails> listsmtpdetails = new List<SmtpDetails>();
                listsmtpdetails = GetMailsmtpdetails();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(listsmtpdetails[0].fromaddress, "Let's Help", System.Text.Encoding.UTF8);
                
                if (!string.IsNullOrEmpty(EmailId) && EmailId != "undefined")
                {
                    mail.To.Add(EmailId);
                }


                mail.IsBodyHtml = true;
                mail.Subject = subject;
                Label body = new Label();
                body.Text = sbDesign.ToString();
                mail.Body = body.Text;

                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host);
                smtp.Port = listsmtpdetails[0].port;
                NetworkCredential credentials = new NetworkCredential();
                smtp.UseDefaultCredentials = true;
               

                string password = listsmtpdetails[0].frompassword;
               
                smtp.Credentials = new NetworkCredential(listsmtpdetails[0].fromaddress, listsmtpdetails[0].frompassword);
              

                smtp.EnableSsl = true;
                smtp.Send(mail);
                return "SUCCESS";


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Send mail3  -- upendra
        public string SendMail3(string EmailId1, string EmailId2, string sbDesign, string subject)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = new List<SmtpDetails>();
                listsmtpdetails = GetMailsmtpdetails();

                MailMessage mail = new MailMessage();

                // FROM (must match SMTP authenticated user)
                mail.From = new MailAddress(listsmtpdetails[0].fromaddress, "Let's Help", System.Text.Encoding.UTF8);

                // TO Email
                if (!string.IsNullOrEmpty(EmailId1) && EmailId1 != "undefined")
                {
                    mail.To.Add(new MailAddress(EmailId1));
                }
                // CC as fromaddress itself
                mail.CC.Add(new MailAddress(EmailId2));
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = sbDesign.ToString();
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host);
                smtp.Port = listsmtpdetails[0].port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(listsmtpdetails[0].fromaddress, listsmtpdetails[0].frompassword);
                smtp.EnableSsl = true;

                smtp.Send(mail);

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Send mail4  -- upendra
        public string SendMail4(string EmailId1, string EmailId2, string sbDesign, string subject, string attachmentPath = null)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();

                using (MailMessage mail = new MailMessage())
                {
                    // FROM (must match SMTP authenticated user)
                    mail.From = new MailAddress(listsmtpdetails[0].fromaddress, "Let's Help", System.Text.Encoding.UTF8);


                    if (!string.IsNullOrEmpty(EmailId1) && EmailId1 != "undefined")
                    {

                        mail.To.Add(new MailAddress(EmailId1));
                    }
                    else
                    {

                        var adminEmail = listsmtpdetails[0].ToAddress?.Trim();

                        if (!string.IsNullOrWhiteSpace(adminEmail) && adminEmail.Contains("@"))
                        {
                            mail.To.Add(new MailAddress(adminEmail));
                        }
                        else
                        {
                            throw new Exception("Invalid Admin Email configured in DB");
                        }

                    }


                    // CC
                    if (!string.IsNullOrEmpty(EmailId2))
                    {
                        var userEmail = EmailId2?.Trim();

                        if (!string.IsNullOrWhiteSpace(userEmail) &&
                            userEmail != "undefined" &&
                            userEmail.Contains("@"))
                        {
                            mail.CC.Add(new MailAddress(userEmail));
                        }

                    }

                    mail.IsBodyHtml = true;
                    mail.Subject = subject;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.Priority = MailPriority.High;

                    // Handle attachment (both embed + attach)
                    if (!string.IsNullOrEmpty(attachmentPath))
                    {
                        try
                        {
                            using (var client = new WebClient())
                            {
                                byte[] fileData = client.DownloadData(attachmentPath);
                                var stream = new MemoryStream(fileData);
                                string fileName = Path.GetFileName(attachmentPath);

                                // 1️⃣ Add as attachment
                                mail.Attachments.Add(new Attachment(new MemoryStream(fileData), fileName));
                            }
                        }
                        catch (Exception ex)
                        {
                            sbDesign += $"<p style='color:red'>[Image could not be loaded: {ex.Message}]</p>";
                        }
                    }

                    // Append signature
                    sbDesign += @"
                <br/><hr/>
                <p>Thank you,</p>
                <p><b>Regards,</b></p>
                <p><b>LET'S HELP</b></p>
                <p>Tel: +91 1234567890</p>
                <p>Email: test@gmail.com</p>
                <p><a href='https://letshelp.in/'>https://letshelp.in/</a></p>";

                    mail.Body = sbDesign;

                    using (SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host))
                    {
                        smtp.Port = listsmtpdetails[0].port;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(listsmtpdetails[0].fromaddress, listsmtpdetails[0].frompassword);
                        smtp.EnableSsl = true;

                        smtp.Send(mail);
                    }
                }

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw new Exception("Mail sending failed: " + ex.Message, ex);
            }
        }
        #endregion


        #region SendMail23 --manohar
        public string SendMail23(string EmailId, string sbDesign, string subject)
        {
            try
            {

                List<SmtpDetails> listsmtpdetails = new List<SmtpDetails>();
                listsmtpdetails = GetMailsmtpdetails();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(listsmtpdetails[0].fromaddress, "Let's Help", System.Text.Encoding.UTF8);
                //  mail.CC.Add("suraj.t@gagriglobal.com");
                mail.To.Add("suraj.t@gagriglobal.com");
               


                mail.IsBodyHtml = true;
                mail.Subject = subject;
                Label body = new Label();
                body.Text = sbDesign.ToString();
                mail.Body = body.Text;

                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host);
                smtp.Port = listsmtpdetails[0].port;
                NetworkCredential credentials = new NetworkCredential();
                smtp.UseDefaultCredentials = true;
                

                string password = listsmtpdetails[0].frompassword;
                smtp.Credentials = new System.Net.NetworkCredential(listsmtpdetails[0].fromaddress, password);
                smtp.EnableSsl = true;
                smtp.Send(mail);

              
                return "SUCCESS";


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Send OTP to mail when login      -- Suresh
        public string SendOTPtoMail(string EmailId, string Content)
        {
            try
            {
                var subject = "";
                subject = "Login OTP";
                sbDesign.Append("<tr ><td  style=\"background-color: #ffffff; border-right:solid; text-align:left; padding-left:50px; padding-right:50px; border-left:solid; color: #4A4A4A; border-color:#008ACE;\"><br>");
                sbDesign.Append("<br><br>" + Content + ".<br><b>Let's Help</b><br></td></tr> ");
                SendMail2(EmailId, sbDesign.ToString(), subject);
                sbDesign.Clear();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region GetMailsmtpdetails--->manohar db
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
                exp.ExceptionHandler(ex);

                return " ";
            }
        }
        #endregion


        public class PasswordEncryption
        {
            public static string HashPassword(string password)
            {
                byte[] salt;
                byte[] buffer2;
                if (password == null)
                {
                    throw new ArgumentNullException("password");
                }
                using (SHA256Managed managed = new SHA256Managed())
                {
                    salt = managed.ComputeHash(Encoding.UTF8.GetBytes("salt"));
                    buffer2 = managed.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                byte[] dst = new byte[salt.Length + buffer2.Length];
                Buffer.BlockCopy(salt, 0, dst, 0, salt.Length);
                Buffer.BlockCopy(buffer2, 0, dst, salt.Length, buffer2.Length);
                return Convert.ToBase64String(dst);
            }



        }

        #region Send mail1 -- Amrutha 19/12/2025
        public string SendMail1CCC(string Email, string sbDesign, string Subject)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();
                var smtpDetails = listsmtpdetails[0];

                MailMessage mail = new MailMessage();

                // FROM
                mail.From = new MailAddress(
                    smtpDetails.fromaddress,
                    "Let's Help",
                    Encoding.UTF8
                );

                // ✅ TO → Admin mail (from DB)
                if (!string.IsNullOrWhiteSpace(smtpDetails.ToAddress))
                {
                    mail.To.Add(smtpDetails.ToAddress);
                }
                else
                {
                    throw new Exception("Admin email (ToAddress) is missing in MailDetails table");
                }

                // ✅ CC → Sender mail
                if (!string.IsNullOrWhiteSpace(Email) && Email != "undefined")
                {
                    mail.CC.Add(Email);
                }

                // mail.To.Add(Email);
                mail.Subject = Subject; // Set the subject of the email
                mail.IsBodyHtml = true;

                Label body = new Label();
                body.Text = sbDesign.ToString();
                mail.Body = body.Text;

                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host);
                smtp.Port = listsmtpdetails[0].port;
                NetworkCredential credentials = new NetworkCredential();
                smtp.UseDefaultCredentials = true;

                string password = listsmtpdetails[0].frompassword;
                smtp.Credentials = new System.Net.NetworkCredential(listsmtpdetails[0].fromaddress, password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion






        #region Send mail1 -- upendra 
        public string SendMail1CCC12(string Email, string sbDesign, string Subject)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = new List<SmtpDetails>();
                listsmtpdetails = GetMailsmtpdetails();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(listsmtpdetails[0].fromaddress, "Let's Help", System.Text.Encoding.UTF8);
                if (!string.IsNullOrEmpty(Email) && Email != "undefined")
                {
                    mail.To.Add(Email);
                }

                // mail.To.Add(Email);
                mail.Subject = Subject; // Set the subject of the email
                mail.IsBodyHtml = true;

                Label body = new Label();
                body.Text = sbDesign.ToString();
                mail.Body = body.Text;

                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host);
                smtp.Port = listsmtpdetails[0].port;
                NetworkCredential credentials = new NetworkCredential();
                smtp.UseDefaultCredentials = true;

                string password = listsmtpdetails[0].frompassword;
                smtp.Credentials = new System.Net.NetworkCredential(listsmtpdetails[0].fromaddress, password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Send mail1 -- upendra 
        public string SendMail1CCC1(string Email, string sbDesign, string Subject)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = new List<SmtpDetails>();
                listsmtpdetails = GetMailsmtpdetails();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(listsmtpdetails[0].fromaddress, "Let's Help", System.Text.Encoding.UTF8);
                if (!string.IsNullOrEmpty(Email) && Email != "undefined")
                {
                    mail.To.Add(Email);
                }

                // mail.To.Add(Email);
                mail.Subject = Subject; // Set the subject of the email
                mail.IsBodyHtml = true;

                Label body = new Label();
                body.Text = sbDesign.ToString();
                mail.Body = body.Text;

                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient(listsmtpdetails[0].host);
                smtp.Port = listsmtpdetails[0].port;
                NetworkCredential credentials = new NetworkCredential();
                smtp.UseDefaultCredentials = true;

                string password = listsmtpdetails[0].frompassword;
                smtp.Credentials = new System.Net.NetworkCredential(listsmtpdetails[0].fromaddress, password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void SafeLogEmail(string message)
        {
            try
            {
                string logDir = null;
                if (System.Web.Hosting.HostingEnvironment.IsHosted)
                {
                    logDir = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                }
                else if (HttpContext.Current != null && HttpContext.Current.Server != null)
                {
                    logDir = HttpContext.Current.Server.MapPath("~/");
                }
                
                if (string.IsNullOrEmpty(logDir))
                {
                    logDir = AppDomain.CurrentDomain.BaseDirectory;
                }

                string logFile = Path.Combine(logDir, "EmailLog.txt");
                File.AppendAllText(logFile, message);
            }
            catch
            {
                // Safe logging must never throw exceptions or interrupt business flow
            }
        }

        private string GetAssetFilePath(string fileName)
        {
            try
            {
                if (System.Web.Hosting.HostingEnvironment.IsHosted)
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Image/" + fileName);
                    if (File.Exists(path)) return path;
                }
                if (HttpContext.Current != null && HttpContext.Current.Server != null)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Image/" + fileName);
                    if (File.Exists(path)) return path;
                }
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string localPath = Path.Combine(baseDir, "Image", fileName);
                if (File.Exists(localPath)) return localPath;

                string devPath = Path.Combine(@"C:\BloodDonatioProject\LetsHelpAPI\BloodGroupApi\BloodGroupApi\Image", fileName);
                if (File.Exists(devPath)) return devPath;
            }
            catch { }
            return null;
        }

        private System.Drawing.Image GetWelcomeCardAsset(string fileName)
        {
            try
            {
                // 1. Try System.Web.Hosting.HostingEnvironment.MapPath (Works when hosted on IIS / webservices virtual path)
                try
                {
                    if (System.Web.Hosting.HostingEnvironment.IsHosted)
                    {
                        string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Image/" + fileName);
                        if (File.Exists(path))
                        {
                            return System.Drawing.Image.FromFile(path);
                        }
                    }
                }
                catch { }

                // 2. Try HttpContext.Current Server.MapPath
                try
                {
                    if (HttpContext.Current != null)
                    {
                        string path = HttpContext.Current.Server.MapPath("~/Image/" + fileName);
                        if (File.Exists(path))
                        {
                            return System.Drawing.Image.FromFile(path);
                        }
                    }
                }
                catch { }

                // 3. Try AppDomain BaseDirectory
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string localPath = Path.Combine(baseDir, "Image", fileName);
                if (File.Exists(localPath))
                {
                    return System.Drawing.Image.FromFile(localPath);
                }

                // 4. Try local dev workspace path
                string devPath = Path.Combine(@"C:\BloodDonatioProject\LetsHelpAPI\BloodGroupApi\BloodGroupApi\Image", fileName);
                if (File.Exists(devPath))
                {
                    return System.Drawing.Image.FromFile(devPath);
                }

                // 5. Fallback to production URL path
                string remoteUrl = "https://letshelp.in/webservices/Image/" + fileName;
                using (WebClient wc = new WebClient())
                {
                    byte[] bytes = wc.DownloadData(remoteUrl);
                    MemoryStream ms = new MemoryStream(bytes);
                    return System.Drawing.Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                SafeLogEmail($"[{DateTime.Now}] Error loading Welcome Card asset {fileName}: {ex.Message}\n");
                return null;
            }
        }

        public byte[] GenerateWelcomeCard(string firstName, string memberId, string regDate, string referralCode = "")
        {
            try
            {
                using (System.Drawing.Image templateImage = GetWelcomeCardAsset("WelcomeCardTemplate.jpg"))
                {
                    if (templateImage == null)
                    {
                        return null;
                    }

                    using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(templateImage))
                    {
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                            float scale = bitmap.Width / 703.25f;

                            // 1. Font Selection (Gordita Medium -> fallback Segoe UI / Trebuchet MS / Arial)
                            System.Drawing.FontFamily greetingFamily;
                            try { greetingFamily = new System.Drawing.FontFamily("Gordita"); }
                            catch {
                                try { greetingFamily = new System.Drawing.FontFamily("Segoe UI"); }
                                catch { greetingFamily = System.Drawing.FontFamily.GenericSansSerif; }
                            }

                            // Draw "Hi, I'm <FirstName>." (Font Size: 22px as specified)
                            string nameToDraw = string.IsNullOrWhiteSpace(firstName) ? "Member" : firstName.Trim();
                            string prefixText = "Hi, I'm ";
                            string suffixText = nameToDraw + ".";

                            using (System.Drawing.Font fontPrefix = new System.Drawing.Font(greetingFamily, 22f * scale, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel))
                            using (System.Drawing.Font fontSuffix = new System.Drawing.Font(greetingFamily, 22f * scale, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel))
                            using (System.Drawing.Brush brushDark = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(17, 17, 17)))
                            using (System.Drawing.Brush brushRed = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(166, 25, 25)))
                            {
                                System.Drawing.SizeF prefixSize = g.MeasureString(prefixText, fontPrefix);
                                System.Drawing.SizeF suffixSize = g.MeasureString(suffixText, fontSuffix);
                                float totalWidth = prefixSize.Width + suffixSize.Width;

                                float startX = (bitmap.Width - totalWidth) / 2f;
                                float startY = 246f * scale;

                                g.DrawString(prefixText, fontPrefix, brushDark, startX, startY);
                                g.DrawString(suffixText, fontSuffix, brushRed, startX + prefixSize.Width - (4f * scale), startY);
                            }

                            // 2. Draw QR Code (centered inside the red box outline)
                            using (System.Drawing.Image qrImage = GetWelcomeCardAsset("app_qr_code.png"))
                            {
                                if (qrImage != null)
                                {
                                    float qrBoxX = 273.125f * scale;
                                    float qrBoxY = 528f * scale;
                                    float qrBoxW = 180f * scale;
                                    float qrBoxH = 180f * scale;

                                    g.DrawImage(qrImage, qrBoxX, qrBoxY, qrBoxW, qrBoxH);
                                }
                            }

                            // 3. Font Selection (Lato Bold -> fallback Segoe UI / Arial)
                            System.Drawing.FontFamily latoFamily;
                            try { latoFamily = new System.Drawing.FontFamily("Lato"); }
                            catch {
                                try { latoFamily = new System.Drawing.FontFamily("Segoe UI"); }
                                catch { latoFamily = System.Drawing.FontFamily.GenericSansSerif; }
                            }

                            // 3. Draw Reference Code / Referral Code (Font Size: 18px, Lato Bold as specified)
                            if (!string.IsNullOrWhiteSpace(referralCode))
                            {
                                string refLabelText = "Referral Code : ";
                                string refCodeText = referralCode.Trim();

                                using (System.Drawing.Font fontRef = new System.Drawing.Font(latoFamily, 18f * scale, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel))
                                using (System.Drawing.Brush brushDark = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(17, 17, 17)))
                                using (System.Drawing.Brush brushRed = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(166, 25, 25)))
                                {
                                    System.Drawing.SizeF refLabelSize = g.MeasureString(refLabelText, fontRef);
                                    System.Drawing.SizeF refCodeSize = g.MeasureString(refCodeText, fontRef);
                                    float refTotalWidth = refLabelSize.Width + refCodeSize.Width;

                                    float refStartX = (bitmap.Width - refTotalWidth) / 2f;
                                    float refStartY = 3180f;

                                    g.DrawString(refLabelText, fontRef, brushDark, refStartX, refStartY);
                                    g.DrawString(refCodeText, fontRef, brushRed, refStartX + refLabelSize.Width - (4f * scale), refStartY);
                                }
                            }

                            // 4. Draw Member ID and Registration Date (Font Size: 21.5px, Lato Bold as specified)
                            using (System.Drawing.Font fontIdDate = new System.Drawing.Font(latoFamily, 21.5f * scale, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel))
                            using (System.Drawing.Brush brushRed = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(166, 25, 25)))
                            {
                                // Draw Member ID (under "Member ID:")
                                string memberIdText = string.IsNullOrWhiteSpace(memberId) ? "LH0000011" : memberId.Trim();
                                g.DrawString(memberIdText, fontIdDate, brushRed, 181.5f * scale, 3755f);

                                // Draw Registration Date (under "Proud Member Since:")
                                string regDateText = string.IsNullOrWhiteSpace(regDate) ? DateTime.Now.ToString("dd MMMM yyyy") : regDate.Trim();
                                g.DrawString(regDateText, fontIdDate, brushRed, 436f * scale, 3755f);
                            }
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
            }
            catch (Exception ex)
            {
                SafeLogEmail($"[{DateTime.Now}] Error generating card image: {ex.Message}\n{ex.StackTrace}\n");
                return null;
            }
        }

        public byte[] GenerateLeaderIDCardFront(string fullName, string memberId, string title, string bloodGroup, string email, string phoneNo, string middleName = "", string surName = "", string firstName = "")
        {
            try
            {
                using (System.Drawing.Image templateImage = GetWelcomeCardAsset("LeaderIDCardFront.jpg"))
                {
                    if (templateImage == null)
                    {
                        return null;
                    }

                    using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(templateImage))
                    {
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                            // 1. Font for Name (Alegreya -> fallback Times New Roman / Georgia / Segoe UI)
                            System.Drawing.Text.PrivateFontCollection pfc = null;
                            System.Drawing.FontFamily nameFamily = null;
                            try
                            {
                                string alegreyaPath = GetAssetFilePath("Alegreya.ttf");
                                if (!string.IsNullOrEmpty(alegreyaPath) && File.Exists(alegreyaPath))
                                {
                                    pfc = new System.Drawing.Text.PrivateFontCollection();
                                    pfc.AddFontFile(alegreyaPath);
                                    if (pfc.Families.Length > 0)
                                    {
                                        nameFamily = pfc.Families[0];
                                    }
                                }
                            }
                            catch { }

                            if (nameFamily == null)
                            {
                                try { nameFamily = new System.Drawing.FontFamily("Alegreya"); }
                                catch
                                {
                                    try { nameFamily = new System.Drawing.FontFamily("Times New Roman"); }
                                    catch { nameFamily = System.Drawing.FontFamily.GenericSerif; }
                                }
                            }

                            // Draw Full Name (Centered - Format: FirstName MiddleName SurName)
                            List<string> nameParts = new List<string>();
                            if (!string.IsNullOrWhiteSpace(firstName)) nameParts.Add(firstName.Trim());
                            if (!string.IsNullOrWhiteSpace(middleName)) nameParts.Add(middleName.Trim());
                            if (!string.IsNullOrWhiteSpace(surName)) nameParts.Add(surName.Trim());

                            string displayName = string.Join(" ", nameParts);
                            if (string.IsNullOrWhiteSpace(displayName))
                            {
                                displayName = string.IsNullOrWhiteSpace(fullName) ? "Leader Member" : fullName.Trim();
                            }

                            float nameFontSize = 90f;
                            using (System.Drawing.Brush nameBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(25, 25, 25)))
                            {
                                using (System.Drawing.Font testFont = new System.Drawing.Font(nameFamily, nameFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel))
                                {
                                    System.Drawing.SizeF size = g.MeasureString(displayName, testFont);
                                    if (size.Width > 1600f)
                                    {
                                        nameFontSize = nameFontSize * (1600f / size.Width);
                                    }
                                }

                                using (System.Drawing.Font nameFont = new System.Drawing.Font(nameFamily, nameFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel))
                                {
                                    System.Drawing.SizeF nameSize = g.MeasureString(displayName, nameFont);
                                    float nameX = (bitmap.Width - nameSize.Width) / 2f;
                                    float nameY = 1320f;
                                    g.DrawString(displayName, nameFont, nameBrush, nameX, nameY);
                                }
                            }

                            // 2. Font for Details (Times New Roman -> fallback Georgia / Segoe UI)
                            System.Drawing.FontFamily detailsFamily;
                            try { detailsFamily = new System.Drawing.FontFamily("Times New Roman"); }
                            catch
                            {
                                try { detailsFamily = new System.Drawing.FontFamily("Georgia"); }
                                catch { detailsFamily = System.Drawing.FontFamily.GenericSerif; }
                            }

                            float valX = 835f;
                            float detailsFontSize = 58f;
                            using (System.Drawing.Brush valBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(35, 35, 35)))
                            using (System.Drawing.Font valFont = new System.Drawing.Font(detailsFamily, detailsFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel))
                            {
                                // USER ID
                                string displayUserId = string.IsNullOrWhiteSpace(memberId) ? "LHF000123" : memberId.Trim();
                                g.DrawString(displayUserId, valFont, valBrush, valX, 1538f);

                                // TITLE
                                string displayTitle = string.IsNullOrWhiteSpace(title) ? "Community Leader" : title.Trim();
                                g.DrawString(displayTitle, valFont, valBrush, valX, 1724f);

                                // BLOOD GRP
                                string displayBloodGroup = string.IsNullOrWhiteSpace(bloodGroup) ? "-" : bloodGroup.Trim();
                                g.DrawString(displayBloodGroup, valFont, valBrush, valX, 1904f);

                                // EMAIL
                                string displayEmail = string.IsNullOrWhiteSpace(email) ? "-" : email.Trim();
                                System.Drawing.SizeF emailSize = g.MeasureString(displayEmail, valFont);
                                if (emailSize.Width > 1050f)
                                {
                                    float emailFontSize = detailsFontSize * (1050f / emailSize.Width);
                                    using (System.Drawing.Font emailFont = new System.Drawing.Font(detailsFamily, emailFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel))
                                    {
                                        g.DrawString(displayEmail, emailFont, valBrush, valX, 2085f + (detailsFontSize - emailFontSize) / 2f);
                                    }
                                }
                                else
                                {
                                    g.DrawString(displayEmail, valFont, valBrush, valX, 2085f);
                                }

                                // PHONE NO
                                string displayPhone = string.IsNullOrWhiteSpace(phoneNo) ? "-" : phoneNo.Trim();
                                if (!displayPhone.StartsWith("+") && displayPhone.Length == 10)
                                {
                                    displayPhone = "+91 " + displayPhone;
                                }
                                g.DrawString(displayPhone, valFont, valBrush, valX, 2275f);
                            }

                            if (pfc != null)
                            {
                                pfc.Dispose();
                            }
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SafeLogEmail($"[{DateTime.Now}] Error generating Leader ID Card Front: {ex.Message}\n{ex.StackTrace}\n");
                return null;
            }
        }

        public byte[] GenerateLeaderIDCardBack()
        {
            try
            {
                using (System.Drawing.Image templateImage = GetWelcomeCardAsset("LeaderIDCardBack.jpg"))
                {
                    if (templateImage == null)
                    {
                        return null;
                    }

                    using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(templateImage))
                    {
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                            using (System.Drawing.Image qrImage = GetWelcomeCardAsset("app_qr_code.png"))
                            {
                                if (qrImage != null)
                                {
                                    // Golden square frame bounds: X: 569..1432 (W: 863), Y: 1219..2077 (H: 858)
                                    int qrX = 569 + 15;
                                    int qrY = 1219 + 15;
                                    int qrW = 863 - 30;
                                    int qrH = 858 - 30;
                                    g.DrawImage(qrImage, qrX, qrY, qrW, qrH);
                                }
                            }
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SafeLogEmail($"[{DateTime.Now}] Error generating Leader ID Card Back: {ex.Message}\n{ex.StackTrace}\n");
                return null;
            }
        }

        public string SendLeaderWelcomeEmail(string email, string firstName, string base64Image, out string savedImagePath, string roleId = "4", string memberId = "", string regId = "", string fullName = "", string bloodGroup = "", string phoneNumber = "", string title = "", string referralCode = "", string middleName = "", string surName = "")
        {
            savedImagePath = null;
            try
            {
                SafeLogEmail($"\n[{DateTime.Now}] Starting SendLeaderWelcomeEmail for {email} (RoleId: {roleId}, MemberId: {memberId}, ReferralCode: {referralCode})...\n");

                byte[] imageBytes = null;
                if (!string.IsNullOrEmpty(base64Image))
                {
                    string base64Data = base64Image;
                    if (base64Image.Contains(","))
                    {
                        base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1);
                    }
                    imageBytes = Convert.FromBase64String(base64Data);
                    SafeLogEmail($"[{DateTime.Now}] Base64 image provided from client. Size: {imageBytes.Length} bytes.\n");
                }

                string cardMemberId = memberId;
                if (string.IsNullOrWhiteSpace(cardMemberId) && !string.IsNullOrWhiteSpace(regId))
                {
                    cardMemberId = "LH" + regId.PadLeft(7, '0');
                }

                if (imageBytes == null || imageBytes.Length == 0)
                {
                    SafeLogEmail($"[{DateTime.Now}] Generating Welcome Card image on server side...\n");
                    string cardRegDate = DateTime.Now.ToString("dd MMMM yyyy");
                    imageBytes = GenerateWelcomeCard(firstName, cardMemberId, cardRegDate, referralCode);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        SafeLogEmail($"[{DateTime.Now}] Server-side Welcome Card successfully generated. Size: {imageBytes.Length} bytes.\n");
                    }
                    else
                    {
                        SafeLogEmail($"[{DateTime.Now}] WARNING: Server-side Welcome Card generation returned null or empty.\n");
                    }
                }

                bool isLeader = (roleId == "4");
                bool isDonor = (roleId == "2");

                StringBuilder sbDesign = new StringBuilder();
                sbDesign.Append("<!DOCTYPE html>");
                sbDesign.Append("<html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'>");
                sbDesign.Append("<style>");
                sbDesign.Append("@media only screen and (max-width: 600px) {");
                sbDesign.Append("  .email-container { width: 100% !important; max-width: 100% !important; }");
                sbDesign.Append("  .content-cell { padding: 22px 18px !important; }");
                sbDesign.Append("  .header-cell { padding: 22px 18px !important; }");
                sbDesign.Append("}");
                sbDesign.Append("</style>");
                sbDesign.Append("</head>");
                sbDesign.Append("<body style='margin:0; padding:0; background-color:#f4f6f8; font-family: -apple-system, BlinkMacSystemFont, \"Segoe UI\", Roboto, Helvetica, Arial, sans-serif; -webkit-font-smoothing: antialiased;'>");
                sbDesign.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#f4f6f8; padding: 25px 15px;'>");
                sbDesign.Append("<tr><td align='center'>");
                sbDesign.Append("<table class='email-container' width='100%' cellpadding='0' cellspacing='0' border='0' style='max-width: 720px; background-color:#ffffff; border-radius: 12px; overflow: hidden; box-shadow: 0 4px 16px rgba(0,0,0,0.06); border: 1px solid #eaeaea; margin: 0 auto;'>");

                // Header Banner
                sbDesign.Append("<tr><td class='header-cell' style='background: linear-gradient(135deg, #b71c1c 0%, #d32f2f 100%); padding: 28px 32px; text-align: center; color: #ffffff;'>");
                sbDesign.Append("<h1 style='margin: 0; font-size: 24px; font-weight: 700; letter-spacing: 0.5px;'>LET'S HELP FOUNDATION</h1>");
                sbDesign.Append("<p style='margin: 5px 0 0 0; font-size: 13px; opacity: 0.92; letter-spacing: 1px; text-transform: uppercase;'>Together for a Noble Cause</p>");
                sbDesign.Append("</td></tr>");

                // Content Body
                sbDesign.Append("<tr><td class='content-cell' style='padding: 32px 35px; color: #2d3748; line-height: 1.65; font-size: 15px;'>");
                string saluteName = !string.IsNullOrWhiteSpace(fullName) ? fullName : firstName;
                sbDesign.Append($"<p style='font-size: 16px; margin-top: 0;'>Dear <b>{saluteName}</b>,</p>");

                if (isDonor)
                {
                    sbDesign.Append("<p style='color: #1a202c;'>Welcome to the <b style='color: #c62828;'>Let's Help Foundation</b> family!</p>");
                    sbDesign.Append("<p>Thank you for registering as a blood donor. Your noble decision to donate blood has the power to save lives and bring hope to families in critical times. By joining our platform, you have become a vital part of a community dedicated to ensuring that no one suffers due to the unavailability of blood.</p>");
                    sbDesign.Append("<div style='background-color: #fff5f5; border-left: 4px solid #e53e3e; padding: 18px 22px; border-radius: 8px; margin: 24px 0;'>");
                    sbDesign.Append("<p style='margin: 0 0 10px 0; font-weight: bold; color: #9b2c2c; font-size: 16px;'>As a registered Donor on Let's Help:</p>");
                    sbDesign.Append("<ul style='margin: 0; padding-left: 20px; color: #4a5568; line-height: 1.6;'>");
                    sbDesign.Append("<li style='margin-bottom: 8px;'>You will receive notifications whenever an urgent blood request matches your blood group and location.</li>");
                    sbDesign.Append("<li style='margin-bottom: 8px;'>You can keep your donation status updated so patient families and coordinators can reach out when needed.</li>");
                    sbDesign.Append("<li style='margin-bottom: 0;'>You inspire others in your community to step forward and donate blood.</li>");
                    sbDesign.Append("</ul></div>");
                }
                else
                {
                    sbDesign.Append("<p style='color: #1a202c;'>Congratulations, and welcome to the <b style='color: #c62828;'>Let's Help Leadership Team</b>!</p>");
                    sbDesign.Append("<p>Thank you for stepping forward to make a difference. By becoming a Let's Help Leader, you've joined a community of volunteers committed to creating awareness, encouraging blood donation, and helping save lives.</p>");
                    
                    sbDesign.Append("<div style='background-color: #fff5f5; border-left: 4px solid #e53e3e; padding: 18px 22px; border-radius: 8px; margin: 24px 0;'>");
                    sbDesign.Append("<p style='margin: 0 0 10px 0; font-weight: bold; color: #9b2c2c; font-size: 16px;'>As a Leader, you can:</p>");
                    sbDesign.Append("<ul style='margin: 0 0 18px 0; padding-left: 20px; color: #4a5568; line-height: 1.6;'>");
                    sbDesign.Append("<li style='margin-bottom: 8px;'>Register blood donors who may not be able to use the app themselves.</li>");
                    sbDesign.Append("<li style='margin-bottom: 8px;'>Organize blood donation awareness presentations in schools, colleges, and communities.</li>");
                    sbDesign.Append("<li style='margin-bottom: 8px;'>Guide and inspire others to become regular blood donors.</li>");
                    sbDesign.Append("<li style='margin-bottom: 8px;'>Spread awareness using our official brochures, banners, and social media materials.</li>");
                    sbDesign.Append("<li style='margin-bottom: 0;'>Help build a stronger and more connected donor network in your community.</li>");
                    sbDesign.Append("</ul>");

                    sbDesign.Append("<p style='margin: 18px 0 10px 0; font-weight: bold; color: #1a202c; font-size: 15px; border-top: 1px solid #fed7d7; padding-top: 14px;'>To help you get started, we've attached the following:</p>");
                    sbDesign.Append("<p style='margin: 8px 0; color: #2d3748;'>📌 <b>Leader ID Card</b> – Your official Let's Help Leader identification.</p>");
                    sbDesign.Append("<p style='margin: 8px 0; color: #2d3748;'>📘 <b>Leader Guide</b> – Everything you need to know about your role, responsibilities, and the Leader features available in the app.</p>");
                    sbDesign.Append("<p style='margin: 8px 0 0 0; color: #2d3748;'>🖼️ <b>Awareness Card</b> – A ready-to-share digital card that you can share with your family, friends, colleagues, and social networks to inspire more people to join our mission.</p>");
                    sbDesign.Append("</div>");
                }

                if (isDonor)
                {
                    sbDesign.Append("<p>Every donor is a lifesaver, and every donation counts. We are immensely grateful to have you with us on this noble mission.</p>");
                    sbDesign.Append("<p>Thank you once again for your commitment to saving lives.</p>");
                }
                else
                {
                    sbDesign.Append("<p>Every person you educate, every donor you register, and every awareness session you organize has the potential to save lives. Together, we can ensure that no one suffers due to the unavailability of blood.</p>");
                    sbDesign.Append("<p>Thank you once again for becoming a Let's Help Leader. We look forward to seeing the positive impact you'll create.</p>");
                }

                if (isLeader)
                {
                    sbDesign.Append("<p style='font-size: 13px; color: #718096; margin-top: 20px;'><i>(Please find your official Welcome Membership Card, Leader ID Cards, and Leader Guide attached to this email.)</i></p>");
                }
                else
                {
                    sbDesign.Append("<p style='font-size: 13px; color: #718096; margin-top: 20px;'><i>(Please find your official Welcome Membership Card attached to this email.)</i></p>");
                }

                // Sign-off
                sbDesign.Append("<div style='margin-top: 25px; padding-top: 18px; border-top: 1px solid #edf2f7;'>");
                sbDesign.Append("<p style='margin: 0; color: #718096; font-size: 14px;'>Warm regards,</p>");
                sbDesign.Append("<p style='margin: 4px 0 0 0; font-size: 15px; font-weight: bold; color: #1a202c;'>RK</p>");
                sbDesign.Append("<p style='margin: 2px 0 0 0; font-size: 13px; color: #e53e3e; font-weight: 600;'>Director, Let's Help Foundation</p>");
                sbDesign.Append("<p style='margin: 2px 0 0 0; font-size: 12px; color: #a0aec0;'>Together for a Noble Cause</p>");
                sbDesign.Append("<div style='margin-top: 10px;'><img src='https://letshelp.in/webservices/Image/logo.png' alt='Let&apos;s Help Foundation' style='height: 38px; width: auto; display: block;' /></div>");
                sbDesign.Append("</div>");

                sbDesign.Append("</td></tr>");

                // Footer
                sbDesign.Append("<tr><td style='background-color: #f7fafc; padding: 20px 35px; text-align: center; border-top: 1px solid #edf2f7; font-size: 12px; color: #a0aec0;'>");
                sbDesign.Append("<p style='margin: 0;'>© " + DateTime.Now.Year + " Let's Help Foundation. All rights reserved.</p>");
                sbDesign.Append("<p style='margin: 4px 0 0 0;'>Website: <a href='https://letshelp.in' style='color: #e53e3e; text-decoration: none;'>letshelp.in</a></p>");
                sbDesign.Append("</td></tr>");

                sbDesign.Append("</table></td></tr></table></body></html>");

                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();
                if (listsmtpdetails == null || listsmtpdetails.Count == 0)
                {
                    SafeLogEmail($"[{DateTime.Now}] ERROR: SMTP Details table is empty.\n");
                    throw new Exception("SMTP configuration details not found");
                }
                var smtpDetails = listsmtpdetails[0];
                SafeLogEmail($"[{DateTime.Now}] Fetched SMTP Details: Host={smtpDetails.host}, Port={smtpDetails.port}, From={smtpDetails.fromaddress}\n");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpDetails.fromaddress, "Let's Help", Encoding.UTF8);
                
                if (!string.IsNullOrWhiteSpace(email) && email != "undefined")
                {
                    mail.To.Add(email.Trim());
                }
                else
                {
                    SafeLogEmail($"[{DateTime.Now}] ERROR: User email is missing or undefined.\n");
                    throw new Exception("User email is missing");
                }

                mail.Subject = isDonor ? "Welcome to Let's Help Foundation - Thank You for Becoming a Donor!" : "Welcome to the Let's Help Leadership Team!";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.Body = sbDesign.ToString();
                mail.BodyEncoding = Encoding.UTF8;

                // 1. Attach Welcome Card
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    MemoryStream msAttachment = new MemoryStream(imageBytes);
                    mail.Attachments.Add(new Attachment(msAttachment, "Welcome_Card.png", "image/png"));
                    SafeLogEmail($"[{DateTime.Now}] Attached Welcome_Card.png ({imageBytes.Length} bytes).\n");
                }

                // 2. If Leader (roleId == "4"), generate and attach Leader ID Cards (Front and Back)
                if (isLeader)
                {
                    SafeLogEmail($"[{DateTime.Now}] Generating Leader ID Cards for Leader role...\n");
                    List<string> cardNameParts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(firstName)) cardNameParts.Add(firstName.Trim());
                    if (!string.IsNullOrWhiteSpace(middleName)) cardNameParts.Add(middleName.Trim());
                    if (!string.IsNullOrWhiteSpace(surName)) cardNameParts.Add(surName.Trim());

                    string cardFullName;
                    if (cardNameParts.Count > 0)
                    {
                        cardFullName = string.Join(" ", cardNameParts);
                    }
                    else if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        cardFullName = fullName.Trim();
                    }
                    else
                    {
                        cardFullName = "Leader Member";
                    }

                    string cardTitle = !string.IsNullOrWhiteSpace(title) ? title : "Community Leader";
                    string cardBloodGroup = !string.IsNullOrWhiteSpace(bloodGroup) ? bloodGroup : "-";
                    string cardPhone = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : "-";

                    byte[] frontBytes = GenerateLeaderIDCardFront(cardFullName, cardMemberId, cardTitle, cardBloodGroup, email, cardPhone, middleName, surName, firstName);
                    if (frontBytes != null && frontBytes.Length > 0)
                    {
                        MemoryStream msFront = new MemoryStream(frontBytes);
                        mail.Attachments.Add(new Attachment(msFront, "Leader_ID_Card_Front.png", "image/png"));
                        SafeLogEmail($"[{DateTime.Now}] Successfully generated and attached Leader_ID_Card_Front.png ({frontBytes.Length} bytes).\n");
                    }
                    else
                    {
                        SafeLogEmail($"[{DateTime.Now}] WARNING: GenerateLeaderIDCardFront returned null or empty.\n");
                    }

                    byte[] backBytes = GenerateLeaderIDCardBack();
                    if (backBytes != null && backBytes.Length > 0)
                    {
                        MemoryStream msBack = new MemoryStream(backBytes);
                        mail.Attachments.Add(new Attachment(msBack, "Leader_ID_Card_Back.png", "image/png"));
                        SafeLogEmail($"[{DateTime.Now}] Successfully generated and attached Leader_ID_Card_Back.png ({backBytes.Length} bytes).\n");
                    }
                    else
                    {
                        SafeLogEmail($"[{DateTime.Now}] WARNING: GenerateLeaderIDCardBack returned null or empty.\n");
                    }

                    // Attach LeaderGuide.pdf
                    try
                    {
                        string leaderGuidePath = GetAssetFilePath("LeaderGuide.pdf");
                        if (!string.IsNullOrEmpty(leaderGuidePath) && File.Exists(leaderGuidePath))
                        {
                            byte[] pdfBytes = File.ReadAllBytes(leaderGuidePath);
                            if (pdfBytes != null && pdfBytes.Length > 0)
                            {
                                MemoryStream msPdf = new MemoryStream(pdfBytes);
                                mail.Attachments.Add(new Attachment(msPdf, "LeaderGuide.pdf", "application/pdf"));
                                SafeLogEmail($"[{DateTime.Now}] Successfully attached LeaderGuide.pdf ({pdfBytes.Length} bytes).\n");
                            }
                        }
                        else
                        {
                            SafeLogEmail($"[{DateTime.Now}] WARNING: LeaderGuide.pdf file not found.\n");
                        }
                    }
                    catch (Exception exPdf)
                    {
                        SafeLogEmail($"[{DateTime.Now}] LeaderGuide.pdf file not found: {exPdf.Message}\n");
                    }

                }

                // 3. If Donor (roleId == "2"), attach APPGuide.pdf
                if (isDonor)
                {
                    try
                    {
                        string appGuidePath = GetAssetFilePath("APPGuide.pdf");
                        if (string.IsNullOrEmpty(appGuidePath) || !File.Exists(appGuidePath))
                        {
                            appGuidePath = GetAssetFilePath("APPGuide");
                        }

                        if (!string.IsNullOrEmpty(appGuidePath) && File.Exists(appGuidePath))
                        {
                            byte[] pdfBytes = File.ReadAllBytes(appGuidePath);
                            if (pdfBytes != null && pdfBytes.Length > 0)
                            {
                                MemoryStream msPdf = new MemoryStream(pdfBytes);
                                mail.Attachments.Add(new Attachment(msPdf, "APPGuide.pdf", "application/pdf"));
                                SafeLogEmail($"[{DateTime.Now}] Successfully attached APPGuide.pdf ({pdfBytes.Length} bytes).\n");
                            }
                        }
                        else
                        {
                            SafeLogEmail($"[{DateTime.Now}] WARNING: APPGuide.pdf file not found.\n");
                        }
                    }
                    catch (Exception exPdf)
                    {
                        SafeLogEmail($"[{DateTime.Now}] ERROR attaching APPGuide.pdf: {exPdf.Message}\n");
                    }
                }

                SafeLogEmail($"[{DateTime.Now}] Configuring SmtpClient...\n");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Ensure modern TLS is used
                SmtpClient smtp = new SmtpClient(smtpDetails.host);
                smtp.Port = smtpDetails.port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smtpDetails.fromaddress, smtpDetails.frompassword);
                smtp.EnableSsl = (smtpDetails.enableSsl ?? true);
                
                SafeLogEmail($"[{DateTime.Now}] Attempting to send email via SMTP...\n");
                smtp.Send(mail);
                SafeLogEmail($"[{DateTime.Now}] Email sent successfully via SMTP!\n");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    try
                    {
                        // 1. Determine directory path for WelcomeTemplate
                        string folderPath = null;
                        if (HttpContext.Current != null && HttpContext.Current.Server != null)
                        {
                            folderPath = HttpContext.Current.Server.MapPath("~/Image/WelcomeTemplate/");
                        }
                        else if (System.Web.Hosting.HostingEnvironment.IsHosted)
                        {
                            folderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Image/WelcomeTemplate/");
                        }

                        if (string.IsNullOrEmpty(folderPath))
                        {
                            folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "WelcomeTemplate");
                        }

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // 2. Generate unique filename (using memberId/regId + timestamp)
                        string idForFile = !string.IsNullOrWhiteSpace(cardMemberId) ? cardMemberId.Trim() : (!string.IsNullOrWhiteSpace(regId) ? regId.Trim() : "user");
                        foreach (char c in Path.GetInvalidFileNameChars())
                        {
                            idForFile = idForFile.Replace(c, '_');
                        }

                        string fileName = $"WelcomeCard_{idForFile}_{DateTime.Now:yyyyMMdd_HHmmssfff}.png";
                        string fullSavePath = Path.Combine(folderPath, fileName);

                        // 3. Save physical file to disk in .png format
                        File.WriteAllBytes(fullSavePath, imageBytes);
                        SafeLogEmail($"[{DateTime.Now}] Welcome template card image saved to {fullSavePath}\n");

                        // 4. Return relative path for database storage and future access
                        savedImagePath = "Image/WelcomeTemplate/" + fileName;
                    }
                    catch (Exception exSave)
                    {
                        SafeLogEmail($"[{DateTime.Now}] WARNING: Failed to save welcome template card image file: {exSave.Message}\n{exSave.StackTrace}\n");
                    }
                }

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                SafeLogEmail($"[{DateTime.Now}] EXCEPTION: {ex.Message}\nStack Trace: {ex.StackTrace}\n");
                throw ex;
            }
        }

        public string SendMail1CC(string Email, string sbDesign, string Subject, byte[] attachmentBytes)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();
                var smtpDetails = listsmtpdetails[0];

                MailMessage mail = new MailMessage();

                // FROM
                mail.From = new MailAddress(
                    smtpDetails.fromaddress,
                    "Let's Help",
                    Encoding.UTF8
                );

                //User email
                if (!string.IsNullOrWhiteSpace(Email) && Email != "undefined")
                {
                    mail.To.Add(Email);
                }
                else
                {
                    throw new Exception("User email is missing");
                }

                // Admin email
                if (!string.IsNullOrWhiteSpace(smtpDetails.ToAddress))
                {
                    mail.CC.Add(smtpDetails.ToAddress);
                }

                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = sbDesign;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;

                //Attach PDF
                if (attachmentBytes != null && attachmentBytes.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(attachmentBytes);
                    mail.Attachments.Add(
                        new Attachment(ms, "BloodDonationCertificate.pdf", "application/pdf")
                    );
                }

                // SMTP
                SmtpClient smtp = new SmtpClient(smtpDetails.host);
                smtp.Port = smtpDetails.port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    smtpDetails.fromaddress,
                    smtpDetails.frompassword
                );
                smtp.EnableSsl = true;

                smtp.Send(mail);
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Mail Template Design --upendra
        public dynamic mailTEMPLATE()
        {
            #region Part 1
            maildesignPart1.Append("<html><head><style type = \"text/css\">");
            maildesignPart1.Append("img { max-width: 600px;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;}");
            maildesignPart1.Append("a {text-decoration: none;border: 0;outline: none;color: #bbbbbb;}");
            maildesignPart1.Append("a img {border: none;}");
            maildesignPart1.Append(" td, h1, h2, h3  {font-family: Helvetica, Arial, sans-serif;font-weight: 400;}");
            maildesignPart1.Append(" td {text-align: center;}");
            maildesignPart1.Append("body {-webkit-font-smoothing:antialiased;-webkit-text-size-adjust:none;width: 100 %;height: 100 %;color: #37302d;background: #ffffff;font-size: 16px;}");
            maildesignPart1.Append("table {border-collapse: collapse !important;}");
            maildesignPart1.Append(".headline {color: #ffffff; font-size: 36px;}");
            maildesignPart1.Append(" .force-full-width {width: 100 % !important;}");
            maildesignPart1.Append(" </style><style media = \"only screen and (max-width: 480px)\" type = \"text/css\" >@media only screen and(max-width: 480px) {table[class=\"w320\"] {width: 320px !important;}}</style></head>");
            //******End of head tag******//

            //******Start of body tag******//
            maildesignPart1.Append("<body bgcolor=\"#ffffff\" class=\"body\" style=\"padding:0; margin:0; display:block; OVERFLOW-X: HIDDEN; background:#ffffff; -webkit-text-size-adjust:none\">");
            maildesignPart1.Append("<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" height=\"100% \" width=\"100%\">");
            maildesignPart1.Append("<tbody><tr><td align=\"center\" bgcolor=\"#ffffff\" valign=\"top\" width=\"100%\">");
            maildesignPart1.Append("<center><table cellpadding=\"0\" cellspacing=\"0\" class=\"w320\" style=\"margin: 0 auto; \" width=\"600\">");
            maildesignPart1.Append("<tbody><tr><td align = \"center\"  valign=\"top\"><table cellpadding = \"0\" cellspacing=\"0\" style=\"margin: 0 auto;\" width=\"100%\"><tbody><tr><td style=\"font-size: 30px; text-align:center;\"></td></tr></tbody></table>");
            maildesignPart1.Append("<table bgcolor=\"#008ACE\" cellpadding=\"0\" cellspacing=\"0\"  style=\"margin: 0 auto;\" width=\"100%\">");
            //******Logo*****//
            maildesignPart1.Append("<tbody ><tr><td ><br><!-- <img alt = \"robot picture\"  height=\"70\" src=\"https://d1pgqke3goo8l6.cloudfront.net/onWRO1YrQiiubAgCRwAx_white_logo.png\" width=\"70\"> --><br></td></tr>");
            maildesignPart1.Append("<tr ><td class=\"headline\"><span style=\"font-weight:bold;\">Silveni Ads</span><span style=\"font-weight:lighter;\" ></span></td></tr>");
            maildesignPart1.Append("<tr><td><center><table cellpadding = \"0\" cellspacing=\"0\"  style=\"margin: 0 auto;\" width=\"75%\"><tbody ><tr ><td  style=\"color:#A0D8F4;\"></td></tr></tbody></table></center></td></tr>");
            maildesignPart1.Append("<tr><td><div ><a style=\"background-color:#73BD4D;border-radius:4px;color:#ffffff;display:inline-block;font-family:Helvetica, Arial, sans-serif;font-size:18px;font-weight:normal;line-height:50px;text-align:center;text-decoration:none;width:350px;-webkit-text-size-adjust:none;\"></a></div><br></td></tr></tbody></table>");
            maildesignPart1.Append("<table bgcolor=\"#f5774e\" cellpadding=\"0\" cellspacing=\"0\"  style=\"margin: 0 auto;\" width=\"100%\"><tbody ><tr ><td class=\"headline\" style=\"border-right: solid; border-left: solid; background-color:#ffffff; color:#008ACE\"></td>");
            #endregion

            #region Part 2
            maildesignPart2.Append("<tr><td ><center cellspacing=\"0\"  style=\"background-color: #ffffff; border-right:solid; border-left:solid; color: #008ACE;\"></center></td></tr>");
            maildesignPart2.Append("<tr><td  style=\"background-color: #ffffff; border-right:solid; border-left:solid; color: #008ACE;\"><div></div><br></td></tr></tbody></table>");
            maildesignPart2.Append("<table bgcolor=\"#414141\" cellpadding=\"0\" cellspacing=\"0\" class=\"force-full-width\" style=\"margin: 0 auto;width:100%;\"><tbody ><tr><td  style=\"background-color:#414141;\"></td></tr>");
            maildesignPart2.Append("<tr><td  style=\"color:#bbbbbb; font-size:12px;\"></td></tr>");
            maildesignPart2.Append("<tr><td  style=\"color:#bbbbbb; font-size:12px;\"><br><br>");
            ////Terms Of service Link
            //maildesignPart2.Append("<a  href=\"\">Terms of Service</a> &nbsp; • &nbsp; ");
            ////Privacy Policy Link
            //maildesignPart2.Append("<a  href=\"\">Privacy Policy</a>&nbsp; • &nbsp;");
            ////Support Link
            //maildesignPart2.Append("<a  href=\"\">Support</a><br><br> ");
            ////Facebook Link
            //maildesignPart2.Append("<img alt=\"facebook\" height=\"32px\" src=\"https://d1pgqke3goo8l6.cloudfront.net/D11l6OhhRVaZGnYCaxtu_Facebook@3x.png\" width=\"32px\">&nbsp;");
            ////Twitter Link
            //maildesignPart2.Append("<img alt=\"facebook\"  height=\"32px\" src=\"https://d1pgqke3goo8l6.cloudfront.net/fRII6ZJ9SEugqa31ignG_Twitter@3x.png\" width=\"32px\">&nbsp;");
            ////Google plus Link
            //maildesignPart2.Append("<img alt=\"facebook\" height=\"32px\" src=\"https://d1pgqke3goo8l6.cloudfront.net/fVAAOjVyR2mKHKgYR1SF_GooglePlus3x.png\" width=\"32px\"><br><br> ");
            maildesignPart2.Append("© 2019 <span style=\"font-weight:bold;\">Avra Synthesis Pvt. Ltd </span><span  style=\"font-weight:lighter;\"></span><br><br></td></tr>");
            maildesignPart2.Append("</tbody></table></td></tr></tbody></table></center></td></tr></tbody></table></body></html>");
            #endregion
            return new { maildesignPart1, maildesignPart2 };
        }
        #endregion


        public dynamic Sendorderdetailstouser(List<Doners> Details1, string baseurl)
        {
            try
            {
                StringBuilder sbDesign = new StringBuilder();
                var template = mailTEMPLATE();
                var part1 = template.maildesignPart1;
                var part2 = template.maildesignPart2;
                var day = DateTime.Now.Day;
                var Month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var Hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;

                for (int i = 0; i < Details1.Count; i++)
                {
                    sbDesign.Append($"Hello {Details1[i].FullName},");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<table>");
                }



                sbDesign.Append("<table style=\"border-collapse: collapse;\">");

                // Loop through Details1 to generate table rows
                for (int i = 0; i < Details1.Count; i++)
                {
                    sbDesign.Append("<tr>");

                    // Contact Person
                    sbDesign.Append("<th style=\"text-align: left; padding: 5px; font-weight: bold;\">Contact Person</th>");
                    sbDesign.Append("<td style=\"text-align: left; padding: 5px; \">");
                    sbDesign.Append(Details1[i].ContactPerson);
                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");

                    // Contact Mobile
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<th style=\"text-align: left; padding: 5px; font-weight: bold;\">Mobile</th>");
                    sbDesign.Append("<td style=\"text-align: left; padding: 5px; \">");
                    sbDesign.Append(Details1[i].ContactMobile);
                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");
                    // Hospital
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<th style=\"text-align: left; padding: 5px; font-weight: bold;\">Hospital</th>");
                    sbDesign.Append("<td style=\"text-align: left; padding: 5px;\">");
                    sbDesign.Append($"{Details1[i].HsptName},{Details1[i].HospitalAddress}<br>");
                    sbDesign.Append($"{Details1[i].StateName} {Details1[i].CityName} {Details1[i].DistrictName} {Details1[i].Pincode}<br>");
                    sbDesign.Append($" ");
                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");

                    // Blood Group
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<th style=\"text-align: left; padding: 5px; font-weight: bold;\">Blood Group</th>");
                    sbDesign.Append("<td style=\"text-align: left; padding: 5px;\">");
                    sbDesign.Append(Details1[i].BLGName);
                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");
                    // Units of Blood
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<th style=\"text-align: left; padding: 5px; font-weight: bold;\">Units of Blood</th>");
                    sbDesign.Append("<td style=\"text-align: left; padding: 5px; \">");
                    sbDesign.Append(Details1[i].unitsofblood);
                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");


                }

                sbDesign.Append("</table>");





                sbDesign.Append("</table>");
                sbDesign.Append("<br/>");
                sbDesign.Append("<br/>");
                sbDesign.Append("Please call the contact number as soon as possible.");
                sbDesign.Append("<br/>");

                sbDesign.Append("<br/>");
                sbDesign.Append("<table>");
                sbDesign.Append("<tbody>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you for your altruistic act!</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Best Regards,</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com");
                sbDesign.Append("</tr>");
                sbDesign.Append("</tbody>");
                sbDesign.Append("</table>");


                string Subject = "We urgently need your help! Please consider donating blood to save a life.";
                SendMail1CCC(Details1[0].Email, sbDesign.ToString(), Subject);


                return "Success";
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");

                throw ex;
            }



        }

        public dynamic SendCertificate(List<Doners> Details1, string baseurl)
        {
            try
            {
                byte[] pdfBytes;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // ===== A4 LANDSCAPE =====
                    Rectangle pageSize = PageSize.A4.Rotate();
                    Document pdfDoc = new Document(pageSize, 0, 0, 0, 0);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                    pdfDoc.Open();

                    // ===== BACKGROUND IMAGE =====
                    iTextSharp.text.Image bg = iTextSharp.text.Image.GetInstance(
                        "https://letshelp.in/webservices/Image/Certificate.jpg"
                    );
                    bg.ScaleAbsolute(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                    bg.SetAbsolutePosition(0, 0);
                    pdfDoc.Add(bg);

                    // ===== CANVAS =====
                    PdfContentByte canvas = writer.DirectContent;

                    // ===== FONT =====
                    BaseFont appreciationFont = BaseFont.CreateFont(
                        BaseFont.TIMES_ROMAN,
                        BaseFont.CP1252,
                        BaseFont.EMBEDDED
                    );

                    // ===== TEXT COLOR =====
                    BaseColor appreciationColor = new BaseColor(80, 80, 80);

                    
                    float pageWidth = pdfDoc.PageSize.Width;

                    // Approx left design (ribbon) width → adjust once and keep
                    float leftDesignWidth = 140; // 🔥 tweak this (130–160)

                    // Calculate usable width center
                    float visualCenterX = leftDesignWidth + ((pageWidth - leftDesignWidth) / 2);

                    float nameY = 270;
                    string name = Details1[0].FullName;

                    canvas.BeginText();
                    canvas.SetColorFill(appreciationColor);
                    canvas.SetFontAndSize(appreciationFont, 28);

                    // ✅ Center relative to white area (NOT full page)
                    canvas.ShowTextAligned(
                        Element.ALIGN_CENTER,
                        name,
                        visualCenterX,
                        nameY,
                        0
                    );

                    canvas.EndText();

                    // ============================
                    // DATE — CENTERED
                    // ============================
                    canvas.BeginText();
                    canvas.SetColorFill(appreciationColor);
                    canvas.SetFontAndSize(appreciationFont, 16);

                    canvas.ShowTextAligned(
                        Element.ALIGN_CENTER,
                        DateTime.Parse(Details1[0].Dateofservice).ToString("dd/MM/yyyy"),
                        310,
                        70,
                        0
                    );
                    canvas.EndText();

                    pdfDoc.Close();
                    writer.Close();

                    pdfBytes = memoryStream.ToArray();
                }

                // ================= EMAIL BODY =================
                StringBuilder sbDesign = new StringBuilder();

                for (int i = 0; i < Details1.Count; i++)
                {
                    sbDesign.Append($"Dear {Details1[i].FullName},<br/><br/>");
                    sbDesign.Append("No words can truly express our gratitude for your selfless act of donating blood. ");
                    sbDesign.Append("Your kindness goes beyond appreciation, it has the power to save lives.<br/><br/>");
                    sbDesign.Append("We’re happy to let you know that your donation photo has been successfully approved. ");
                    sbDesign.Append("Your certificate is attached, and the image has been added to your gallery.<br/><br/>");
                    sbDesign.Append("We encourage you to proudly share your image and certificate across your social media handles ");
                    sbDesign.Append("and on LinkedIn (you can also add the certificate to your profile). ");
                    sbDesign.Append("This isn’t about showcasing, it’s about inspiring others to take that same lifesaving step and spread hope.<br/><br/>");
                }

                sbDesign.Append("With heartfelt thanks,<br/>");
                sbDesign.Append("<b>Team Let's Help Foundation</b><br/>");
                sbDesign.Append("<a href=\"https://letshelp.in\">https://letshelp.in</a><br/>");

                string subject =
                    $"You’re a Saviour! Your Blood Donation Certificate is Ready : Donation Date is " +
                    $"{DateTime.Parse(Details1[0].Dateofservice):dd/MM/yyyy}";

                SendMail1CC(
                    Details1[0].Email,
                    sbDesign.ToString(),
                    subject,
                    pdfBytes
                );

                return "Success";
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions(ex.Message + ex.StackTrace, "1", "Webapi");
                throw;
            }
        }






        public dynamic send_observation(List<Doners> Details1, string baseurl)
        {
            try
            {
                StringBuilder sbDesign = new StringBuilder();
                var template = mailTEMPLATE();
                var part1 = template.maildesignPart1;
                var part2 = template.maildesignPart2;
                var day = DateTime.Now.Day;
                var Month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var Hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;

                for (int i = 0; i < Details1.Count; i++)
                {
                    sbDesign.Append($"Hello {Details1[i].FullName},");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<table>");
                }



                sbDesign.Append("<table style=\"border-collapse: collapse;\">");

                // Loop through Details1 to generate table rows
                for (int i = 0; i < Details1.Count; i++)
                {
                    sbDesign.Append("<tr>");

                    // Contact Person
                    sbDesign.Append("<th style=\"text-align: left; padding: 5px; font-weight: bold;\">Observation</th>");
                    sbDesign.Append("<td style=\"text-align: left; padding: 5px;\">");
                    sbDesign.Append(Details1[i].Observation);
                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");

                    // Contact Mobile
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<th style=\"text-align: left; padding: 5px; font-weight: bold;\">Comments</th>");
                    sbDesign.Append("<td style=\"text-align: left; padding: 5px;\">");
                    sbDesign.Append(Details1[i].Comments);
                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");

                }

                sbDesign.Append("</table>");

                sbDesign.Append("</table>");
                sbDesign.Append("<br/>");
                sbDesign.Append("<br/>");
                sbDesign.Append("Please update your information on our website.");
                sbDesign.Append("<br/>");

                sbDesign.Append("<br/>");
                sbDesign.Append("<table>");
                sbDesign.Append("<tbody>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you for your altruistic act!</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Best Regards,</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help </td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com");
                sbDesign.Append("</tr>");
                sbDesign.Append("</tbody>");
                sbDesign.Append("</table>");

                string Subject = "Please update your information";
                SendMail1CCC(Details1[0].Email, sbDesign.ToString(), Subject);

                return "Success";
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");

                throw ex;
            }



        }


        public dynamic Sendorderdetailstouser1(List<Doners> Details1, string baseurl)
        {
            try
            {
                StringBuilder sbDesign = new StringBuilder();
                var template = mailTEMPLATE();
                var part1 = template.maildesignPart1;
                var part2 = template.maildesignPart2;
                var day = DateTime.Now.Day;
                var Month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var Hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;

                for (int i = 0; i < Details1.Count; i++)
                {
                    sbDesign.Append($"Hello {Details1[i].FullName},");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<table>");
                }


                sbDesign.Append("</table>");
                sbDesign.Append("<br/>");
                sbDesign.Append("<br/>");
                sbDesign.Append("If you're available, please reach out as someone is in need of blood. Let's lend a helping hand.");
                sbDesign.Append("<br/>");

                sbDesign.Append("<br/>");
                sbDesign.Append("<table>");
                sbDesign.Append("<tbody>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you for your altruistic act!</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Best Regards,</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com");
                sbDesign.Append("</tr>");
                sbDesign.Append("</tbody>");
                sbDesign.Append("</table>");


                string Subject = "We urgently need blood! Please consider donating blood to save a life. | Let's help";
                SendMail1CCC(Details1[0].Email, sbDesign.ToString(), Subject);


                return "Success";
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");

                throw ex;
            }



        }

        #region RequestPresentationDetails --manohar
        public dynamic RequestPresentationDetails(string EMail)
        {
            try
            {
                List<pincodedetails> presentation = new JavaScriptSerializer().Deserialize<List<pincodedetails>>(EMail);
                string str = presentation[0].Contact_name;
                //   string baseurl = "https://localhost:44387/";
                string baseurl = "https://letshelp.in/webservices/";
                char[] a = str.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                string CustomerName = new string(a);
                {

                    sbDesign.Append("<table = '' style=\"width: 50%;border:none;font-family:poppins;background-color: rgba(0, 0, 0, 0.10196078431372549);padding:1%;line-height: 1.3;\">");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + "Mr/Ms." + CustomerName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Email</strong></td>");
                    sbDesign.Append("<td style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + presentation[0].Email + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Contact</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + presentation[0].Contact_number + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Audience</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + presentation[0].Audiance + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Venue</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + presentation[0].Venue_name + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Mode</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + presentation[0].Mode + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Address</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + presentation[0].Address + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("</table>");

                    sbDesign.Append("<table = ''>");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");

                    sbDesign.Append("<br><br> If you're Interested, please send an email to the admin  : " + "<a href=" + baseurl + "/api/BG/CustomerReplyingMail?EMaildetails=" + presentation[0].Email + ">Click here to Send</a>");
                    sbDesign.Append("<br><br>At Let's Help.<br><br><br><br></td></tr> ");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Regards, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #d7127b;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Tel:6300127035</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Email: info@letshelp.breakingindiaapp.com</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com/</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("</table>");

                    var subject = "Request Presentation From Customer";
                    var Mail = SendMail2(presentation[0].Email, sbDesign.ToString(), subject);

                    sbDesign.Clear();
                }

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                //exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region CustomerReplyingMail --manohar
        public dynamic CustomerReplyingMail(string EmailId, string Reply)
        {
            try
            {
               
                {
                    sbDesign.Append("<table = '' style=\"width: 50%;border:none;font-family:poppins;background-color: rgba(0, 0, 0, 0.10196078431372549);padding:1%;line-height: 1.3;\">");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Mail</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + "" + EmailId + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Message</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + "Yes I Am Interseted For Presentation" + "</td>");
                    sbDesign.Append("</tr>");

                    var subject = "Customer Intersted For Presentation ";
                    var Mail = SendMail23(EmailId, sbDesign.ToString(), subject);

                    sbDesign.Clear();
                }

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                //exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region customerRegconformmail --manohar
        public dynamic customerRegconformmail(string EMail)
        {
            try
            {

                // string baseurl = "https://localhost:44387/";
                string baseurl = "https://letshelp.in/webservices/";
                string baseurl1 = $"{baseurl}api/BG/Update_ActivateAccount?Email={EMail}";

                sbDesign.Append("<table = '' style=\"width: 50%;border:none;font-family:poppins;background-color: rgba(0, 0, 0, 0.10196078431372549);padding:1%;line-height: 1.3;\">");
                sbDesign.Append("<tbody>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td>" + "</td>");
                sbDesign.Append("<td>" + "</td>");
                sbDesign.Append("</tr>");

                sbDesign.Append("</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("</tbody>");
                sbDesign.Append("</table>");

                sbDesign.Append("<tr>");
                sbDesign.Append("<td>" + "<b>Welcome to the Blood Donors Army!</b>" + "</td>" + "<br>");
                sbDesign.Append("</tr>");

                sbDesign.Append("<tr>");
                sbDesign.Append("<td>" + "<p>You will only be able to keep the newly created account if you activate it. This way, " +
                     "we know that the email address used belongs to you. If you don't activate your account, you'll be able to use it for" +
                     " two days before it's automatically deleted</p>" + "</td>" + "<br>");
                sbDesign.Append("</tr>");

                sbDesign.Append("<tr>");
                sbDesign.Append("<td>" + "<p>You don't know why you received this email? </p>" + " </td>");
                sbDesign.Append("</tr>");

                sbDesign.Append("<tr>");
                sbDesign.Append("<td>" + "<p>Probably someone else created a Let's Help account with your email address. " +
                      "Ignore this message and everything will be fine. However, since we've already gotten to know each other," +
                      " we challenge you to become a blood donorl Thank you!See you at the donation!</p>" + " </td>" + "<br>");
                sbDesign.Append("</tr>");


                sbDesign.Append("<table = ''>");
                sbDesign.Append("<tbody>");
                sbDesign.Append("<tr>");

                //sbDesign.Append("<td style=\"text-align:center\">" + "<a href=" + baseurl + "/api/BG/Update_ActivateAccount?Param=" + EMail + "style =\"display:inline-block; padding:10px 20px; font-size:16px; font-weight:bold; color:white; background-color:#007bff; border-radius:5px; text-decoration:none;\">" + "Activate account</a>" + "</td>");

                // Construct the activation link
                string activationUrl = $"{baseurl}api/BG/Update_ActivateAccount?email={EMail}";
                sbDesign.Append("<p style=\"text-align:center\"><a href=\"" + baseurl1 + "\" style=\"display:inline-block; padding:10px 20px; font-size:16px; font-weight:bold; color:white; background-color:#007bff; border-radius:5px; text-decoration:none;\">Activate account</a></p>");


                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you, </td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Regards, </td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;color: #d7127b;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help, </td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "");
                sbDesign.Append("</tr>");

                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Tel:6300127035</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Email: info@letshelp.breakingindiaapp.com</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("<tr>");
                sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com/</td>");
                sbDesign.Append("</tr>");
                sbDesign.Append("</tbody>");
                sbDesign.Append("</table>");

                var subject = "Activation For A Blood Donation";
                var Mail = SendMail2(EMail, sbDesign.ToString(), subject);

                sbDesign.Clear();


                return "SUCCESS";
            }
            catch (Exception ex)
            {
                //exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion

        #region fromCustomertoapprove --manohar
        public dynamic fromCustomertoapprove(string EmailId, string Reply)
        {
            try
            {
                {
                    sbDesign.Append("<table = '' style=\"width: 50%;border:none;font-family:poppins;background-color: rgba(0, 0, 0, 0.10196078431372549);padding:1%;line-height: 1.3;\">");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Mail</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + "" + EmailId + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Message</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + "Yes I Am Interseted For Presentation" + "</td>");
                    sbDesign.Append("</tr>");

                    var subject = "Customer Intersted For Presentation ";
                    var Mail = SendMail23(EmailId, sbDesign.ToString(), subject);

                    sbDesign.Clear();
                }

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                //exp.ExceptionHandler(ex);
                throw ex;
            }
        }
        #endregion


        #region Volunteer_mail_admin -- Asif
        public dynamic Volunteer_mail_admin(string admin)
        {
            try
            {
                List<pincodedetails> adminreply =
                    new JavaScriptSerializer().Deserialize<List<pincodedetails>>(admin);

                string str = adminreply[0].FullName;
                char[] a = str.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                string CustomerName = new string(a);

                StringBuilder sbDesign = new StringBuilder();

                sbDesign.Append(@"
<table width='100%' cellpadding='0' cellspacing='0' 
       style='font-family:Poppins, Arial, sans-serif; background:#ffffff; padding:15px;'>

  <tr>
    <td style='font-size:14px; color:#000; padding-bottom:10px;'>
      Hi " + CustomerName + @",
    </td>
  </tr>

  <tr>
    <td style='font-size:14px; color:#000; line-height:1.6; padding-bottom:15px;'>
      " + adminreply[0].Message + @"
    </td>
  </tr>

  <tr>
    <td style='font-size:14px; color:#000; padding-top:10px;'>
      Thank you,
    </td>
  </tr>

  <tr>
    <td style='font-size:14px; color:#000;'>
      Regards,
    </td>
  </tr>

  <tr>
    <td style='font-size:14px; font-weight:bold; color:#d7127b; text-transform:uppercase;'>
      LET'S HELP
    </td>
  </tr>

  <tr>
    <td style='font-size:13px; color:#000; padding-top:8px;'>
      Tel: 8328609371
    </td>
  </tr>

  <tr>
    <td style='font-size:13px; color:#000;'>
      Email: info@letshelp.breakingindiaapp.com
    </td>
  </tr>

  <tr>
    <td style='font-size:13px;'>
      <a href='https://letshelp.in/' 
         style='color:#1a73e8; text-decoration:none;'>
        https://letshelp.in
      </a>
    </td>
  </tr>

</table>
");

                var subject = "Leaders Alert Message From Admin";
                var Mail = SendMail(adminreply[0].Email, sbDesign.ToString(), subject);

                sbDesign.Clear();

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region  EnquiryMailTo_Bloodrequest -- Rk
        public dynamic EnquiryMailTo_Bloodrequest(string Email)
        {
            try
            {
                List<Doners> Ticket = new JavaScriptSerializer().Deserialize<List<Doners>>(Email);
                string str = Ticket[0].FullName;
                char[] a = str.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                string CustomerName = new string(a);
                {
                    sbDesign.Append("<b style=\"text-transform: capitalize;width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "Dear Let’s Help Foundation" + ",</b> <br /> <br />");
                    string timeText = Ticket[0].RequestTime != null
                        ? Convert.ToDateTime(Ticket[0].RequestTime).ToString("hh:mm tt")
                        : "--";

                    sbDesign.Append(
                        "<div style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;padding-bottom:10px\">" +
                        "A blood request has been raised on " +
                        Convert.ToDateTime(Ticket[0].BloodRequestDate).ToString("dd-MM-yyyy") +
                        " at " + timeText +
                        ", Kindly review and approve it at the earliest." +
                        "</div>"
                    );

                    // sbDesign.Append("<table = '' style=\"width: 50%;border:none;font-family:poppins;background-color: rgba(0, 0, 0, 0.10196078431372549);padding:1%;line-height: 1.3;\">");
                    sbDesign.Append("<table>");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Patient Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + "" + Ticket[0].FullName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Attender Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + "" + Ticket[0].ContactPerson + "</td>");
                    sbDesign.Append("</tr>");


                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 30%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Attender Mobile</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + Ticket[0].ContactMobile + "</td>");
                    sbDesign.Append("</tr>");


                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Email</strong></td>");
                    sbDesign.Append("<td style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].Email + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Age</strong></td>");
                    sbDesign.Append("<td style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].Age + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Gender</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].Gender + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Blood Group</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].BLGName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Units of Blood</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].UnitsofBloodId + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Purpose</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].Purpose + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Hospital Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].HospitalName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Hospital Address</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].HospitalAddress + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>State Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].StateName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>District Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].DistrictName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>City Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].CityName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Pincode</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].Pincode + "</td>");
                    sbDesign.Append("</tr>");

                   
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" +
                                    "<strong>BloodRequest Date</strong></td>");
                    sbDesign.Append("<td style=\"font-size: 14px;font-family: poppins;color: #000;\">" +
                                    Convert.ToDateTime(Ticket[0].BloodRequestDate).ToString("dd-MM-yyyy") +
                                    "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;\">" +
                                    "<strong>Blood Request Time</strong></td>");
                    sbDesign.Append("<td style=\"font-size: 14px;font-family: poppins;color: #000;\">" +
                                    Convert.ToDateTime(Ticket[0].RequestTime).ToString("hh:mm tt") +
                                    "</td>");
                    sbDesign.Append("</tr>");




                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("</table>");

                    sbDesign.Append("<br/>");
                    sbDesign.Append("<table>");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you for your altruistic act!</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Best Regards,</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("</table>");



                    string Subject = $"Request for Blood Post Approval | {Ticket[0].FullName} | {Ticket[0].ContactMobile} | {DateTime.Now:dd-MMM-yyyy hh:mm tt}";

                    SendMail1CCC(Ticket[0].Email, sbDesign.ToString(), Subject);


                    sbDesign.Clear();
                }


                return "SUCCESS";
            }
            catch (Exception ex)
            {
                //exp.ExceptionHandler(ex);
                throw ex;
            }
        }

        #endregion 



        #region  EnquiryMailTo_BloodCustomer -- RambabuP-2024-08-08
        public dynamic EnquiryMailTo_BloodCustomers(string Email)
        {
            try
            {
                List<Doners> Ticket = new JavaScriptSerializer().Deserialize<List<Doners>>(Email);
                string str = Ticket[0].FullName;
                char[] a = str.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                string CustomerName = new string(a);
                {
                    sbDesign.Append("<style>");
                    sbDesign.Append("@media (max-width: 600px) {");
                    sbDesign.Append("body { padding: 20px; }"); // Adjust the padding value as needed
                    sbDesign.Append(".nowrap { white-space: nowrap; }"); // Add this line for nowrap class

                    sbDesign.Append(".responsive-table { width: 50% !important; margin-left: 0 !important; }");

                    sbDesign.Append("}");
                    sbDesign.Append("</style>");

                    sbDesign.Append("<b style=\"text-transform: capitalize;width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "Hello" + ",</b> <br /> <br />");
                    sbDesign.Append("<div style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;padding-bottom:10px\">The following user has requested approval for blood donation registration:</div>");
                   
                    sbDesign.Append("<tbody>");

                   

                    sbDesign.Append("<br>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + "Mr/Ms." + CustomerName + "</td>");
                    sbDesign.Append("</tr>");



                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Location</strong></td>");
                    sbDesign.Append("<td class='nowrap' style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].HospitalAddress + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Date of Donation</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + Ticket[0].Dateofservice + "</td>");
                    sbDesign.Append("</tr>");


                    sbDesign.Append("</tbody>");
                    sbDesign.Append("<table = ''>");
                    sbDesign.Append("<tbody>");

                    // ADD SPACE HERE
                    sbDesign.Append("<table>");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr><td style='height:20px'></td></tr>");



                    sbDesign.Append("<tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Regards, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #d7127b;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<br>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Tel: +91 1234567890</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td class='nowrap' style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Email:test@gmail.com</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td class='nowrap' style=\"width: 100%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com/</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    


                    string subject = $"Request for Registration of Donation approval | {CustomerName} | {DateTime.Now:dd-MMM-yyyy hh:mm tt}";


                    var Mail = SendMail2(Ticket[0].Email, sbDesign.ToString(), subject);

                    

                    sbDesign.Clear();
                }


                return "SUCCESS";
            }
            catch (Exception ex)
            {
             
                throw ex;
            }
        }

        #endregion


        #region  EnquiryMailTo_Admin -- Naushad-2024-08-08
        public dynamic EnquiryMailTo_Admin(string Email)
        {
            try
            {
                List<EnquiryInfo> Ticket = new JavaScriptSerializer().Deserialize<List<EnquiryInfo>>(Email);
                string str = Ticket[0].FullName;
                char[] a = str.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                string CustomerName = new string(a);
                {
                    sbDesign.Append("<style>");
                    sbDesign.Append("@media (max-width: 600px) {");
                    sbDesign.Append("body { padding: 20px; }"); // Adjust the padding value as needed
                    sbDesign.Append(".nowrap { white-space: nowrap; }"); // Add this line for nowrap class

                    sbDesign.Append(".responsive-table { width: 50% !important; margin-left: 0 !important; }");

                    sbDesign.Append("}");
                    sbDesign.Append("</style>");



                    sbDesign.Append("<b style=\"text-transform: capitalize;width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "Hello" + ",</b> <br /> <br />");
                    sbDesign.Append("<div style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;padding-bottom:10px\">A request to become a partner has been submitted by the following user:</div>");
                    
                    sbDesign.Append("<tbody>");

                    

                    sbDesign.Append("<br>");

                   


                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td  style=\"text-transform: capitalize;width: 30%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + "<strong>Name</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + CustomerName + "</td>");
                    sbDesign.Append("</tr>");



                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 30%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Mobile</strong></td>");
                    sbDesign.Append("<td class='nowrap' style=\"font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + Ticket[0].Mobile + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 40%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.1px;font-weight: 500;\">" + " " + "<strong>Comments</strong></td>");
                    sbDesign.Append("<td style=\"text-transform: capitalize;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;color: #000;\">" + " " + "" + Ticket[0].Comments + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("<table = ''>");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<br/>");





                    sbDesign.Append("<tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Regards, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #d7127b;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help, </td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<br>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Tel: +91 8328609371</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td class='nowrap' style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "Email:mail.raajm@gmail.com</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td class='nowrap' style=\"width: 100%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com/</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    


                    


                    string subject = $"Become a Partner | {CustomerName} | {Ticket[0].Mobile} | {DateTime.Now:dd-MMM-yyyy hh:mm tt}";

                    var Mail = SendMail2(Ticket[0].Email, sbDesign.ToString(), subject);

                    



                    sbDesign.Clear();
                }


                return "SUCCESS";
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        
        public dynamic MailTo_Admin(string Email)
        {
            try
            {
                List<Doners> Ticket = new JavaScriptSerializer().Deserialize<List<Doners>>(Email);
                string str = Ticket[0].FullName;
                char[] a = str.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                string CustomerName = new string(a);
                {
                    sbDesign.Append("<!DOCTYPE html>");
                    sbDesign.Append("<html>");
                    sbDesign.Append("<head>");
                    sbDesign.Append("<meta charset='UTF-8'>");
                    sbDesign.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
                    sbDesign.Append("<link href='https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600&display=swap' rel='stylesheet'>");
                    sbDesign.Append("<style>");
                    sbDesign.Append("body { font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif; color: #333; line-height: 1.6; margin: 0; padding: 0; background-color: #ffffff; }");
                    sbDesign.Append(".container { padding: 15px; }");
                    sbDesign.Append(".greeting { font-size: 16px; font-weight: 500; color: #000; margin-bottom: 15px; }");
                    sbDesign.Append(".message { font-size: 15px; color: #444; margin-bottom: 20px; }");
                    sbDesign.Append("table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }");
                    sbDesign.Append("td { padding: 8px 0; font-size: 14px; }");
                    sbDesign.Append("td:first-child { font-weight: 600; color: #000; width: 180px; }");
                    sbDesign.Append("td:last-child { color: #555; }");
                    sbDesign.Append(".footer { font-size: 14px; color: #666; line-height: 1.8; margin-top: 20px; padding-top: 15px; border-top: 1px solid #eee; }");
                    sbDesign.Append(".company { color: #d7127b; font-weight: 600; }");
                    sbDesign.Append("a { color: #d7127b; text-decoration: none; }");
                    sbDesign.Append("@media (max-width: 600px) { .container { padding: 15px; } td:first-child { width: 140px; } }");
                    sbDesign.Append("</style>");
                    sbDesign.Append("</head>");
                    sbDesign.Append("<body>");

                    sbDesign.Append("<div class='container'>");

                    sbDesign.Append("<div class='greeting'>Hello,</div>");
                    sbDesign.Append("<div class='message'>A blood request has been accepted by the following donor:</div>");

                    sbDesign.Append("<table>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td>Name</td>");
                    sbDesign.Append("<td>Mr/Ms. " + CustomerName + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td>Mobile Number</td>");
                    sbDesign.Append("<td>" + Ticket[0].Phonenumber + "</td>");
                    sbDesign.Append("</tr>");

                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td>Email</td>");
                    sbDesign.Append("<td>" + Ticket[0].Email + "</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</table>");

                    sbDesign.Append("<div class='footer'>");
                    sbDesign.Append("Thank you,<br>");
                    sbDesign.Append("Regards,<br>");
                    sbDesign.Append("<span class='company'>LET'S HELP</span><br><br>");
                    sbDesign.Append("Tel: +91 1234567890<br>");
                    sbDesign.Append("Email: <a href='mailto:test@gmail.com'>test@gmail.com</a><br>");
                    sbDesign.Append("Web: <a href='https://letshelp.in/'>letshelp.breakingindiaapp.com</a>");
                    sbDesign.Append("</div>");

                    sbDesign.Append("</div>");
                    sbDesign.Append("</body>");
                    sbDesign.Append("</html>");

                    string subject = $"Blood Request Accepted by {CustomerName} | {DateTime.Now:dd-MMM-yyyy hh:mm tt}";

                    var Mail = SendMailAccepted(Ticket[0].Email, Ticket[0].CreatedByEmail, sbDesign.ToString(), subject);

                    sbDesign.Clear();
                }

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
      
        public dynamic Sendmails_Remindertasks1(string taskJson)
        {
            try
            {
                // Deserialize the JSON into a list of PendingTasks
                List<Replying> tasksList = JsonConvert.DeserializeObject<List<Replying>>(taskJson);

                if (tasksList != null && tasksList.Count > 0)
                {
                    StringBuilder sbDesign = new StringBuilder();
                    sbDesign.Append($"Hello {tasksList[0].FullName},");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<br/>");
                    string assignedTo = tasksList[0].Email; // Assuming all tasks are for the same user
                                                            //   sbDesign.AppendLine("<h2>Tasks for: " + assignedTo + "</h2>");
                    sbDesign.AppendLine("<table style=\"border-collapse: collapse;\">");

                    // Add table headers

                    sbDesign.Append("</table>");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("<br/>");
                    sbDesign.Append("You're not available since one month please ON your status in my profile.");
                    sbDesign.Append("<br/>");

                    sbDesign.Append("<br/>");
                    sbDesign.Append("<table>");
                    sbDesign.Append("<tbody>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-family: poppins;font-size: 14px;letter-spacing: 0.5px;padding-top: 2%;\">" + " " + "Thank you for your altruistic act!</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;\">" + " " + "Best Regards,</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;color: #000;font-size: 14px;font-family: poppins;letter-spacing: 0.5px;text-transform: uppercase;font-size: 14px;font-weight: bold;\">" + " " + "Let's Help</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("<tr>");
                    sbDesign.Append("<td style=\"width: 50%;font-family: poppins;color: #000;font-size: 14px;letter-spacing: 0.5px;\">" + " " + "https://letshelp.breakingindiaapp.com");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</tbody>");
                    sbDesign.Append("</table>");


                    // Send the email to the first task's EmailID (assuming all tasks are for the same user)
                    var toEmail = tasksList.First().Email;
                    string Subject = "You are not available. | Let's help";
                    var mailResult = SendMail1CCC12(toEmail, sbDesign.ToString(), Subject);

                    sbDesign.Clear();

                    return "SUCCESS";
                }

                return "NO TASKS TO SEND";
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                exp.ExceptionHandler(ex);
                throw;
            }
        }



        #region EnquiryMailTo_BloodCustomer -- Upendra
        public dynamic BloodRequestMailtoDonor(string Email)
        {
            try
            {
                List<RequestInfo> Ticket = new JavaScriptSerializer().Deserialize<List<RequestInfo>>(Email);

                StringBuilder sbDesign = new StringBuilder();

                // Introductory Sentence (Main Highlight Message)
                sbDesign.Append("<div style=\"width:100%;text-align:center;font-family:poppins;font-size:20px;font-weight:bold;color:#d7127b;\">");
                sbDesign.Append("Urgent Blood Request Notification");
                sbDesign.Append("</div>");

                // Custom Sentence Section (Add this Paragraph)
                sbDesign.Append("<div style=\"width:100%;font-family:poppins;font-size:16px;color:#000;margin-top:10px;text-align:justify;padding: 0 20px;\">");
                sbDesign.Append("Dear <b>" + Ticket[0].DonorName + "</b>, someone in <b>" + Ticket[0].CityName + "</b> urgently needs <b>" + Ticket[0].BLGName + "</b> blood. Your contribution can save a life. Please respond at the earliest.");

                sbDesign.Append("</div>");

                // Responsive Style
                sbDesign.Append("<style>");
                sbDesign.Append("@media (max-width: 600px) { body { padding: 20px; } .nowrap { white-space: nowrap; } .responsive-table { width: 50% !important; margin-left: 0 !important; } }");
                sbDesign.Append("</style>");

                // Details Table
                sbDesign.Append("<table class='responsive-table' style=\"width: 60%;margin-left:16%;margin-top: 10px;font-size: 16px;border:4px solid #008acd;font-family:poppins;background-color: white;padding:1%;padding-left:10%;padding-right:10%;line-height: 1.3;\">");
                sbDesign.Append("<tbody>");

                sbDesign.Append("<caption style=\"caption-side: top; text-align: center; font-size: 28px; font-weight: bold; color: white; padding: 10px;background-color:#018ccf;\">Blood Request Details</caption>");

                sbDesign.Append("<tr><td style=\"width: 30%;font-family: poppins;font-weight: 500;\">Donor Name</td>");
                sbDesign.Append("<td style=\"text-transform: capitalize;\">Mr/Ms. " + Ticket[0].DonorName + "</td></tr>");

                sbDesign.Append("<tr><td style=\"width: 30%;font-family: poppins;font-weight: 500;\">Requested By</td>");
                sbDesign.Append("<td style=\"text-transform: capitalize;\">" + Ticket[0].UserName + "</td></tr>");

                sbDesign.Append("<tr><td style=\"width: 30%;font-family: poppins;font-weight: 500;\">Requester Mobile</td>");
                sbDesign.Append("<td>" + Ticket[0].Mobile + "</td></tr>");

                sbDesign.Append("<tr><td style=\"width: 30%;font-family: poppins;font-weight: 500;\">Blood Group</td>");
                sbDesign.Append("<td>" + Ticket[0].BLGName + "</td></tr>");

                sbDesign.Append("<tr><td style=\"width: 30%;font-family: poppins;font-weight: 500;\">City</td>");
                sbDesign.Append("<td>" + Ticket[0].CityName + "</td></tr>");

                sbDesign.Append("</tbody>");
                sbDesign.Append("</table>");

                // Footer Section
                sbDesign.Append("<div style=\"margin-top:20px;font-family:poppins;font-size:14px;color:#000;\">");
                sbDesign.Append("Thank you,<br>Regards,<br><span style=\"color:#d7127b;font-weight:bold;\">Let's Help</span><br>");
                sbDesign.Append("Tel: +91 8328609371<br>");
                sbDesign.Append("Email: mail.raajm@gmail.com<br>");
                sbDesign.Append("<a href=\"https://letshelp.in/\">https://letshelp.in/</a>");
                sbDesign.Append("</div>");


                // Subject and Send Email
                var subject = "Urgent Blood Request";
                var Mail = SendMail3(Ticket[0].ToMail, Ticket[0].FromMail, sbDesign.ToString(), subject);

                sbDesign.Clear();

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SendRejectionMail --Venkat
        public dynamic SendRejectionMail(List<Doners> Details1)
        {
            try
            {
                StringBuilder sbDesign = new StringBuilder();

                for (int i = 0; i < Details1.Count; i++)
                {
                    sbDesign.Append($"Dear {Details1[i].FullName},");
                    sbDesign.Append("<br/><br/>");
                    sbDesign.Append("We truly appreciate your willingness to donate blood and support this noble cause.");
                    sbDesign.Append("<br/><br/>");
                    sbDesign.Append("However, after reviewing your submission, we regret to inform you that your request has been <b>rejected</b> at this time.");
                    sbDesign.Append("<br/><br/>");
                    sbDesign.Append("Please don’t be discouraged—your kindness and intent to help are deeply valued. ");
                    sbDesign.Append("We encourage you to participate in future donation drives and continue to inspire others with your spirit of giving.");
                    sbDesign.Append("<br/><br/>");

                    // Donor details summary
                    sbDesign.Append("<table style=\"border-collapse: collapse;\">");

                    

                    sbDesign.Append("</td>");
                    sbDesign.Append("</tr>");
                    sbDesign.Append("</table>");
                    sbDesign.Append("<br/>");
                }

                sbDesign.Append("<br/>");
                sbDesign.Append("<p style='font-family:Poppins;font-size:14px;color:#000;'>Thank you once again for stepping forward to make a difference. We hope to see your participation in upcoming donation drives.</p>");
                sbDesign.Append("<br/>");
                sbDesign.Append("<p style='font-family:Poppins;font-size:14px;color:#000;'>Warm regards,<br/><b>Team Let's Help</b></p>");
                sbDesign.Append("<p><a href='https://letshelp.in/'>https://letshelp.in/</a></p>");

                string Subject = "Your Blood Donation Submission was Rejected";

                // Send mail without attachment
                SendMail1CC(Details1[0].Email, sbDesign.ToString(), Subject, null);

                return "Success";
            }
            catch (Exception ex)
            {
                exp.ExceptionHandler(ex);
                saveExceptions((ex.Message + ex.StackTrace).ToString(), "1", "Webapi");
                throw ex;
            }
        }
        #endregion

        #region SendForgotPasswordOTP ---Asif
        public string SendForgotPasswordOTP(string Email, string OTP, string Name)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();
                var smtpDetails = listsmtpdetails[0];

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(
                    smtpDetails.fromaddress,
                    "LET'S HELP",
                    Encoding.UTF8
                );

                mail.To.Add(Email);

                mail.Subject = $"Password Reset OTP: {OTP}"; 
                mail.IsBodyHtml = true;

                StringBuilder sb = new StringBuilder();

                sb.Append("<html><body style='font-family:Arial;'>");

                sb.Append("<p><b>Hello " + Name + ",</b></p>");

                sb.Append("<p>You have requested to reset your password.</p>");
                sb.Append("<p>Please use the OTP below to proceed.</p>");

                sb.Append("<p><b>Your OTP:</b></p>");
                sb.Append("<h2 style='color:#d7127b'>" + OTP + "</h2>");

                sb.Append("<p>This OTP is valid for 10 minutes.</p>");

                sb.Append("<br/>");

                sb.Append("<p>Thank you,</p>");
                sb.Append("<p>Regards,</p>");
                sb.Append("<p><b>LET'S HELP</b></p>");

                sb.Append("<br/>");
                sb.Append("<p>Tel: +91 1234567890</p>");
                sb.Append("<p>Email: test@gmail.com</p>");
                sb.Append("<p>https://letshelp.in/</p>");

                sb.Append("</body></html>");

                mail.Body = sb.ToString();

                SmtpClient smtp = new SmtpClient(smtpDetails.host, smtpDetails.port);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    smtpDetails.fromaddress,
                    smtpDetails.frompassword
                );
                smtp.EnableSsl = smtpDetails.enableSsl ?? false;

                smtp.Send(mail);

                return "OTP_SENT";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region SendEmailChangeOTP ---Asif
        public string SendEmailChangeOTP(string Email, string OTP, string Name)
        {
            try
            {
                List<SmtpDetails> listsmtpdetails = GetMailsmtpdetails();
                var smtpDetails = listsmtpdetails[0];

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(
                    smtpDetails.fromaddress,
                    "LET'S HELP",
                    Encoding.UTF8
                );

                mail.To.Add(Email);

                mail.Subject = $"Email Change Verification OTP: {OTP}";
                mail.IsBodyHtml = true;

                StringBuilder sb = new StringBuilder();

                sb.Append("<html><body style='font-family:Arial;'>");

                sb.Append("<p><b>Hello " + Name + ",</b></p>");

                sb.Append("<p>You have requested to change your email address.</p>");
                sb.Append("<p>Please use the OTP below to verify your new email address.</p>");

                sb.Append("<p><b>Your OTP:</b></p>");
                sb.Append("<h2 style='color:#d7127b'>" + OTP + "</h2>");

                sb.Append("<p>This OTP is valid for 10 minutes.</p>");

                sb.Append("<br/>");

                sb.Append("<p>Thank you,</p>");
                sb.Append("<p>Regards,</p>");
                sb.Append("<p><b>LET'S HELP</b></p>");

                sb.Append("<br/>");
                sb.Append("<p>Tel: +91 1234567890</p>");
                sb.Append("<p>Email: test@gmail.com</p>");
                sb.Append("<p>https://letshelp.in/</p>");

                sb.Append("</body></html>");

                mail.Body = sb.ToString();

                SmtpClient smtp = new SmtpClient(smtpDetails.host, smtpDetails.port);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    smtpDetails.fromaddress,
                    smtpDetails.frompassword
                );
                smtp.EnableSsl = smtpDetails.enableSsl ?? false;

                smtp.Send(mail);

                return "OTP_SENT";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion


    }
}
    

      
    





