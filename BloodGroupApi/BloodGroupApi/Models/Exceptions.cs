using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace BloodGroupApi.Models
{
    public class Exceptions
    {

        #region ExceptionHandler
        public void ExceptionHandler(Exception ex)
        {
            string folderPath = System.AppDomain.CurrentDomain.BaseDirectory + "Content\\FileException";
            bool exists = System.IO.Directory.Exists(folderPath);
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Content\\FileException\\" + System.DateTime.Now.Date.ToString("dd-MM-yyyy") + ".txt";
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(folderPath);
                using (FileStream w = File.Create(filePath))
                {
                    AddText(w, "Message :" + ex.Message + " | Source :" + ex.Source + " | StackTrace :" + ex.StackTrace + " | Time :" + System.DateTime.Now);
                    w.Close();
                }
            }
            else
            {
                if (!File.Exists(filePath))
                {
                    using (FileStream w = File.Create(filePath))
                    {
                        AddText(w, "Message :" + ex.Message + " | Source :" + ex.Source + " | StackTrace :" + ex.StackTrace + " | Time :" + System.DateTime.Now);
                        w.Close();
                    }
                }
                else if (File.Exists(filePath))
                {
                    StreamWriter w = File.AppendText(filePath);
                    w.WriteLine("Message :" + ex.Message + " | Source :" + ex.Source + " | StackTrace :" + ex.StackTrace + " | Time :" + System.DateTime.Now);
                    w.Close();
                }
            }
        }
        #endregion

        #region AddText
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        #endregion
    }
}