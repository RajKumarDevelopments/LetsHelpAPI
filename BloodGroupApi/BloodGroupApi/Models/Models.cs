using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace BloodGroupApi.Models

{
    public class Models
    {
        public class response
        {
            public string access_token { set; get; }
            public Int32? expires_in { set; get; }
            public string token_type { get; set; }
        }


        public class TranslationRequest2
        {
            public string Text { get; set; }
            public string SourceLanguage { get; set; }
            public string TargetLanguage { get; set; }
        }

        public class TranslationResponse
        {
            public string TranslatedText { get; set; }
        }

        public class TranslationRequest
        {
            public string Text { get; set; }
            public string SourceLanguage { get; set; }
            public string TargetLanguage { get; set; }
        }



        #region StatesMaster_crud  ---JDeeksha

        public class StatesMaster_crud
        {
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
            public Int32? StateId { get; set; }
            public string StateName { get; set; }
            public Int32? CountryId { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }
        #endregion

        #region BloodGroupMaster_CRUD  ---JDeeksha

        public class BloodGroupMaster_CRUD
        {
            public Int32? BLGId { get; set; }
            public string BLGName { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }
        #endregion

        #region DistrictMaster_crud  ---Amrutha

        public class DistrictMaster_crud
        {
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
            public Int32? DistrictID { get; set; }
            public string DistrictName { get; set; }
            public string StateName { get; set; }
            public Int32? StateId { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }
        #endregion

        #region UnitsofBlood_CRUD  ---Amrutha

        public class UnitsofBlood_CRUD
        {
            public Int32? UnitsofBloodId { get; set; }
            public string UnitsofBlood { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }
        #endregion

        #region Oldagehome_CRUD  ---Amrutha

        public class Oldagehome_CRUD
        {
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
            public Int32? Old_ID { get; set; }
            public string HomeName { get; set; }
            public string Address { get; set; }
            public string Pincode { get; set; }
            public string PhoneNumber { get; set; }
            public Int32? StateId { get; set; }
            public Int32? DistrictID { get; set; }
            public Int32? CityId { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string SiteURL { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public string FullName { get; set; }
            public string CreatedByMobile { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }
        #endregion

        #region BG_dashboardcount  ---Deeksha,Amrutha

        public class BG_dashboardcount
        {
            public Int32? Donors { get; set; }
            public Int32? Leaders { get; set; }
            public Int32? BrotureDownloaded { get; set; }
            public Int32? Approved { get; set; }
            public Int32? Pending { get; set; }
            public Int32? Requests { get; set; }
            public Int32? Aplus { get; set; }
            public Int32? Aminus { get; set; }
            public Int32? Bplus { get; set; }
            public Int32? Bminus { get; set; }
            public Int32? Oplus { get; set; }
            public Int32? Ominus { get; set; }
            public Int32? ABplus { get; set; }
            public Int32? ABminus { get; set; }
            public Int32? Bombay { get; set; }
            public Int32? HsptCount { get; set; }
            public Int32? OldageHome { get; set; }
            public Int32? Orphanage { get; set; }
        }

        #endregion

        #region HospitalDetails_CRUD  ---Amrutha,deeksha

        public class HospitalDetails_CRUD
        {
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
            public Int32? HsptID { get; set; }
            public string HsptName { get; set; }
            public string HsptPhNum { get; set; }
            public string HsptAddress { get; set; }
            public string DoctorName { get; set; }
            public string CityName { get; set; }

            public string DistrictName { get; set; }

            public string StateName { get; set; }

            public Boolean? Status { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public Int32? CreatedBy { get; set; }
            public string FullName { get; set; }
            public string Phonenumber { get; set; }
            public string ContactPerson { get; set; }
            public string ContactMobile { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public string POCName { get; set; }
            public string POC_ContactNumber { get; set; }
            public string Pincode { get; set; }
            public Int32? CityId { get; set; }
            public Int32? DistrictID { get; set; }
            public Int32? StateId { get; set; }
            public string HStatus { get; set; }

            public string newStatename { get; set; } //suraj
            public string newDistrictname { get; set; } //suraj
            public string newCityname { get; set; } //suraj
        }
        #endregion

        #region EnquiryInfo  ---Amrutha,deeksha
        public class EnquiryInfo
        {
            public Int64 UserId { get; set; }
            public string FarmerName { get; set; }
            public string FullName { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Price { get; set; }
            public string Subject { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public string Area { get; set; }
            public string Pincode { get; set; }
            public string Message { get; set; }
            public string BrandName { get; set; }
            public string TypeName { get; set; }
            public string Vehiclenumber { get; set; }
            public string Comments { get; set; }
        }
        #endregion

        #region SmtpDetails -----------Deeksha,Amrutha
        public class SmtpDetails
        {
            public Int32? Id { get; set; }
            public string host { get; set; }
            public string fromaddress { get; set; }
            public string frompassword { get; set; }
            public string ToAddress { get; set; }
            public string BccAddress { get; set; }
            public Boolean? enableSsl { get; set; }
            public int port { get; set; }
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
        }
        #endregion

        #region Download_Brochures  ---Deeksha,Amrutha

        public class Download_Brochures
        {
            public Int32? Brochureid { get; set; }
            public string StateName { get; set; } 
            public string DistrictName { get; set; }
            public string CityName { get; set; }

            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string Location { get; set; }
            public string Pincode { get; set; }
            public string Place { get; set; }
            public string Place_name { get; set; }
            public Int32? Createdby { get; set; }
            public string FullName { get; set; }
            public string Phonenumber { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? Createddate { get; set; }
        }
        #endregion

        #region ShareYourService  ---Deeksha,Amrutha

        public class ShareYourService
        {
            public string FullName { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string categoryname { get; set; }
            public string GalleryImages { get; set; }
            public string Message { get; set; }
            public string Institutionname { get; set; }
            public string Email { get; set; }
            public DateTime? Dateofservice { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? GalleryID { get; set; }
        }
        #endregion
        public class allExceptions
        {
            public Int64? ExID { get; set; }
            public string Ex_ErrLine { get; set; }
            public string Ex_ErrMsg { get; set; }
            public string Ex_ErrProc { get; set; }
            public string Ex_Source { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int64? Ex_UserID { get; set; }
        }

        public class Get_Exceptions
        {

            public Int64? ExID { get; set; }
            public Int64? Ex_UserID { get; set; }
            public string Ex_ErrLine { get; set; }
            public string Ex_ErrMsg { get; set; }
            public string Ex_ErrProc { get; set; }
            public string Ex_Source { get; set; }
            public string CreatedTime { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string Ex_ErrNum { get; set; }
            public string Ex_ErrSeverity { get; set; }
            public string Ex_ErrState { get; set; }
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
        }

        public class Donor
        {
            public Int32? RegId { get; set; }
            public Int32? RoleId { get; set; }
            public Int32? Weight { get; set; }
            public Int32? CityId { get; set; }
            public Int32? DistrictID { get; set; }
            public Int32? BLGId { get; set; }
            public Int32? StateId { get; set; }
            public string FullName { get; set; }
            public string MiddleName { get; set; }
            public string SurName { get; set; }
            public string Phonenumber { get; set; }
            public string PastDonation { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }
            public string TokenId { get; set; }
            public Boolean? Availablestatus { get; set; }
            public Boolean? Status { get; set; }
            public Boolean? Statusphn { get; set; }
            public Int32? EmailStatus { get; set; }
            public Int32? WhatappStatus { get; set; }
            public string Devicetoken { get; set; }

            public Boolean? Rolestatus { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string DOB { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public string Lastdonatedate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public string BLGName { get; set; }
            public string Pincode { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string UserAddress { get; set; }
            public string Area { get; set; } 
            public string Messagetorequester { get; set; }
            public string Addedby { get; set; }
            public Int32? Categoryid { get; set; }

            public string Reffercode { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public Int32? ReffererId { get; set; }

            public Int32? Refferpoints { get; set; }
            public DateTime? imguploaddate { get; set; }

        }

        public class Recepient
        {
            public Int32? RegId { get; set; }
            public Int32? UdId { get; set; }
            public Int32? RoleId { get; set; }
            public string FullName { get; set; }
            public string Creatername { get; set; }
            public string Acceptedby { get; set; }
            public string Phonenumber { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }
            public string TokenId { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public DateTime? ApprovedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public string Devicetoken { get; set; }
            public string BLGName { get; set; }
            public string UnitsofBlood { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string BloodRequestDate { get; set; }
            public string RequestTime { get; set; }
            public string BloodDonationImage { get; set; }
            public string Receiptimage { get; set; }
            public string HospitalAddress { get; set; }
            public string HospitalName { get; set; }
            public string DoctorName { get; set; }
            public string Purpose { get; set; }
            public string Area { get; set; }
            public string Pincode { get; set; }
            public string ContactPerson { get; set; }
            public string ContactMobile { get; set; }
            public Int32? ApprovalStatus { get; set; }
            public Int32? userid { get; set; }

        }

        public class Brocheres
        {
            public Int32? BroID { get; set; }
            public string BrochureName { get; set; }
            public string BrochurePath { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }

        }

        public class Doners
        {
            public Int32? RegId { get; set; }
            public Int32? RoleId { get; set; }
            public string FullName { get; set; }
            public string MiddleName { get; set; }
            public string SurName { get; set; }
            public string Phonenumber { get; set; }
          
            public string Devicetoken { get; set; }
            public string TokenId { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string Age { get; set; }
            public string Typesofblood { get; set; }
            public Boolean? Availablestatus { get; set; }
            public Boolean? Status { get; set; }
            public Boolean? Statusphn { get; set; }
            public Boolean? Rolestatus { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string Password { get; set; }
            public Int32? ApprovalStatus { get; set; }
            public Int32? UdId { get; set; }
            public Int32? BloodGroupId { get; set; }
            public Int32? UnitsofBloodId { get; set; }
            public Int32? StateId { get; set; }
            public Int32? DistrictId { get; set; }
            public Int32? CityId { get; set; }

            public string newStatename { get; set; } //suraj
            public string newDistrictname { get; set; } //suraj
            public string newCityname { get; set; } //suraj
            public Int32? requestid { get; set; }
            public Int32? Refferpoints { get; set; } //suraj
            public string Receiptimage { get; set; } //suraj
            public string Reffercode { get; set; }//suraj
            public string InviteCode { get; set; }//suraj
            public Int32? ReffererId { get; set; }//suraj
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string UserAddress { get; set; }
            public string Area { get; set; }
            public string DoctorName { get; set; }
            public Int32? Purpose { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }
            public string PastDonation { get; set; }
            public Int32? Weight { get; set; }
            public string Lastdonatedate { get; set; }
            public string DOB { get; set; }
            public string patientname { get; set; }
            public string Messagetorequester { get; set; }
            public string HospitalPhonenumber { get; set; }
            public string BloodRequestDate { get; set; }
            public string RequestTime { get; set; }
            public string ContactPerson { get; set; }
            public string ContactMobile { get; set; }
            public string Note { get; set; }
            public string Pincode { get; set; }
            public string HsptName { get; set; }
            public string BLGName { get; set; }
            public string unitsofblood { get; set; }
            public string Observation { get; set; }
            public string Comments { get; set; }
            public Int32 CreatedBy { get; set; }
            public Int32 Requestedby { get; set; }
            public string Dateofservice { get; set; }
            public string State { get; set; }
            public Int32 NotificationStatus { get; set; }
            public string CreatedByEmail { get; set; }
            public string Reason { get; set; }
            public string DonorAddByLeader { get; set; }
            public Int32 WhatappStatus { get; set; }
            public Int32 EmailStatus { get; set; }
           

        }
        public class Logins
        {
            public Int32? RegId { get; set; }
            public Int32? RoleId { get; set; }
            public Int32? Weight { get; set; }
            public Int32? CityId { get; set; }
            public Int32? DistrictID { get; set; }
            public Int32? BLGId { get; set; }
            public Int32? StateId { get; set; }
            public string FullName { get; set; }
            public string Phonenumber { get; set; }
            public string PastDonation { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }
            public string TokenId { get; set; }
            public Boolean? Availablestatus { get; set; }
            public Boolean? Status { get; set; }
            public Boolean? Statusphn { get; set; }
            public Int32? EmailStatus { get; set; }
            public Int32? WhatappStatus { get; set; }
            public Int32? CallStatus { get; set; }
            public Int32? SMSStatus { get; set; }

            public Boolean? Rolestatus { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string DOB { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public string Lastdonatedate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public string BLGName { get; set; }
            public string Pincode { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string UserAddress { get; set; }
            public string Area { get; set; }
            public string Messagetorequester { get; set; }
            public string Addedby { get; set; }
            public Int32? Categoryid { get; set; }

            public string Reffercode { get; set; }
            public Int32? ReffererId { get; set; }

            public Int32? Refferpoints { get; set; }
            public DateTime? imguploaddate { get; set; }

          
            public Boolean? Currentstatus { get; set; }
            public DateTime? ExpairyDate { get; set; }

              public string newStatename { get; set; }
              public string newDistrictname { get; set; }
              public string newCityname { get; set; }

        }


        public class CitiesMaster
        {
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
            public Int32? CityId { get; set; }
            public string CityName { get; set; }
            public Int32? DistrictId { get; set; }
            public string DistrictName { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }

        public class Orphanages
        {
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
            public Int32? Orph_ID { get; set; }
            public string Orphanage_Name { get; set; }
            public string Address { get; set; }
            public string Pincode { get; set; }
            public string PhoneNumber { get; set; }
            public Int32? StateId { get; set; }
            public Int32? DistrictID { get; set; }
            public Int32? CityId { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string SiteURL { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public string FullName { get; set; }
            public string CreatedByMobile { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }

        public class Gallery
        {

            public Int32? Requestedby { get; set; }
            public Int32? RoleId { get; set; }
            public Int32? GalleryID { get; set; }
            public string GalleryImages { get; set; }
            public Int32? RegId { get; set; }
            public Int32? Categoryid { get; set; }
            public DateTime? Dateofservice { get; set; }
            public Int32? State { get; set; }
            public Int32? District { get; set; }
            public Int32? City { get; set; }
            public string Institutionname { get; set; }
            public string FullName { get; set; }
            public string Message { get; set; }
            public string StateName { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string Age { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public string CategoryName { get; set; }
            public string Phonenumber { get; set; }
            public string Email { get; set; }
            public string Pincode { get; set; }
            public string Gender { get; set; }

            public string newStatename { get; set; } //suraj
            public string newDistrictname { get; set; } //suraj
            public string newCityname { get; set; } //suraj
            public string DoctorName { get; set; }
            public string Purpose { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }
            public string Typesofblood { get; set; }
            public string HospitalPhonenumber { get; set; }



        }



        public class search
        {

            public Int32? RegId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string DistrictName { get; set; }
            public string Phonenumber { get; set; }
            public string Gender { get; set; }
            public string DoctorName { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }
            public string UserAddress { get; set; }
            public string Area { get; set; }

            public Int32? ApprovalStatus { get; set; }
            public string BLGName { get; set; }
            public string StateName { get; set; }
            public string CountryName { get; set; }
            public string CityName { get; set; }
            public string Address { get; set; }
            public string Orphanage_Name { get; set; }
            public string PhoneNumber { get; set; }
            public string SiteURL { get; set; }
            public string Pincode { get; set; }
            public string HomeName { get; set; }
            public string Messagetorequester { get; set; }
            public string Age { get; set; }
            public string Devicetoken { get; set; }

            


            
            public DateTime? PhoneCallNext { get; set; }
            public DateTime? WhatsAppNext { get; set; }
            public DateTime? NotificationNext { get; set; }
            public DateTime? SMSNext { get; set; }
            public Boolean? Status { get; set; }
        }

        public class Request
        {
            public Int32? UdId { get; set; }
            public Int32? RegId { get; set; }
            public Int32? Requestedby { get; set; }
            public string ContactMobile { get; set; }
            public string FullName { get; set; }
            public string BLGName { get; set; }
            public string Gender { get; set; }
            public string Typesofblood { get; set; }
            public Int32? Purpose { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }


            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string BloodRequestDate { get; set; }
            public string RequestTime { get; set; }
            public string Pincode { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string CreatedByEmail { get; set; }
            public string Reason { get; set; }

       

        }



        public class RequestForm
        {
            public Int32? RegId { get; set; }
            public Int32? Requestedby { get; set; }
            public Int32? UdId { get; set; }
            public string FullName { get; set; }
            public string DistrictName { get; set; }
            public string Phonenumber { get; set; }
            public string Gender { get; set; }
            public string UserAddress { get; set; }
            public string Area { get; set; }
            public string Email { get; set; }
            public string Devicetoken { get; set; }
            public string Lastdonatedate { get; set; }

            public Int32? ApprovalStatus { get; set; }
            public string BLGName { get; set; }
            public string StateName { get; set; }
            public string CountryName { get; set; }
            public string CityName { get; set; }
            public string DoctorName { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }
            public string HospitalPhonenumber { get; set; }
            public string BloodRequestDate { get; set; }
            public DateTime? Dateofservice { get; set; }
            public Int32? ModifiedBy { get; set; }

            public string RequestTime { get; set; }
            public string Age { get; set; }
            public Int32? Purpose { get; set; }
            public string ContactPerson { get; set; }
            public string ContactMobile { get; set; }
            public string Messagetorequester { get; set; }
            public string BloodRequestID { get; set; }
            public string BloodDonationImage { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string UnitsofBlood { get; set; }
            public string Typesofblood { get; set; }
            public string Pincode { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string Reason { get; set; }
            public Int32? CreatedBy { get; set; }
            public Int32? BloodGroupId { get; set; }
            public Int32? UnitsofBloodId { get; set; }
            public Int32? StateId { get; set; }
            public Int32? DistrictId { get; set; }
            public Int32? CityId { get; set; }
            public Int32? AcceptedBy { get; set; }
            public Int32? SYSSubmitted { get; set; }


          
        }

       

        public class Gallerycategory
        {

            public Int32? Categoryid { get; set; }
            public string categoryname { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public string GalleryImages { get; set; }
        }

        public class Download
        {
            public string newStatename { get; set; } //suraj
            public string newDistrictname { get; set; } //suraj
            public string newCityname { get; set; } //suraj

            public Int32? Brochureid { get; set; }
            public Int32? stateid { get; set; }
            public Int32? districtid { get; set; }
            public Int32? cityid { get; set; }
            public Int32? Pincode { get; set; }
            public Boolean? Status { get; set; }
            public Int32? Createddate { get; set; }
            public string Place { get; set; }
            public string Place_name { get; set; }
            public Int32? Createdby { get; set; }

            public string Location { get; set; }
        }

        public class city
        {
            public string CityName { get; set; }
        }

        public class pincode
        {
            public string Pincode { get; set; }
        }

        public class Users
        {
            public string Phonenumber { get; set; }
            public string Email { get; set; }
            public Int32? RegId { get; set; }
            public string Password { get; set; }
        }

        public class hospital
        {
            public Int32? UdId { get; set; }
            public string StateName { get; set; }
            public string CountryName { get; set; }
            public string CityName { get; set; }
            public string DoctorName { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }
            public string HospitalPhonenumber { get; set; }
            public string Area { get; set; }

        }

        public class Hospital
        {

            public Int32? HsptID { get; set; }
            public Int32? CityId { get; set; }
            public Int32? DistrictID { get; set; }
            public Int32? StateId { get; set; }
            public string HsptName { get; set; }
            public string HsptPhNum { get; set; }
            public string HsptAddress { get; set; }
            public string DoctorName { get; set; }
            public string CityName { get; set; }

            public string DistrictName { get; set; }

            public string StateName { get; set; }
            public string Pincode { get; set; }

            public Boolean? Status { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string POCName { get; set; }
            public string POC_ContactNumber { get; set; }
        }
        public class request
        {

            public Int32? RPId { get; set; }
            public string Mode { get; set; }
            public string Audiance { get; set; }
            public string Venue { get; set; }
            public string Venue_name { get; set; }
            public string Address { get; set; }
            public string Area { get; set; }
            public string Pincode { get; set; }
            public string Contact_name { get; set; }
            public string Contact_number { get; set; }
            public string Message { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }

            public Int32? StateId { get; set; }
            public Int32? DistrictId { get; set; }
            public Int32? CityId { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public Int32? CreatedBy { get; set; }
            public Int32? Requestedto { get; set; }
            public Int32? RequestAcceptStatus { get; set; }
            public Boolean? OrgPresentation { get; set; }
            public DateTime? RequestDate { get; set; }

            public DateTime? CreatedDate { get; set; }
        }

        public class Donationrequest
        {
            public Int32? RegId { get; set; }

            public DateTime? Dateofservice { get; set; }

            public string Institutionname { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }
            public string newStatename { get; set; } //suraj
            public string newDistrictname { get; set; } //suraj
            public string newCityname { get; set; } //suraj
            public string BloodDonationImage { get; set; }
            public string GalleryImages { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public Int32? CreatedBy { get; set; }

            public DateTime? CreatedDate { get; set; }
        }
        public class MyDonationrequest //suraj
        {
            public Int32? RoleId { get; set; }
            public Int32? RegId { get; set; }

            public DateTime? Dateofservice { get; set; }

            public string Institutionname { get; set; }
            public string HospitalName { get; set; }
            public string HospitalAddress { get; set; }
            public string newStatename { get; set; } //suraj
            public string newDistrictname { get; set; } //suraj
            public string newCityname { get; set; } //suraj
            public Int32? Categoryid { get; set; }
            public string BloodDonationImage { get; set; }
            public string GalleryImages { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string BloodRequestID { get; set; }
            public Int32? CreatedBy { get; set; }

            public DateTime? CreatedDate { get; set; }
            public Int32? SYSSubmitted { get; set; }
        }

        public class report
        {
            public Int32? RId { get; set; }
            public Int32? RegId { get; set; }
            public Int32? CreatedBy { get; set; }
            public string Observation { get; set; }
            public string Comments { get; set; }
            public string FullName { get; set; } 
            public string DonorName { get; set; } 
            public string Phonenumber { get; set; }
            public string DonorPhonenumber { get; set; } 
            public DateTime? CreatedDate { get; set; }
            

        }

        public class Observation
        {

            public Int32? ObsID { get; set; }
            public string ObservationReason { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
        }

        public class Work
        {

            public Int32? WorkID { get; set; }
            public string WorkPlace { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
        }

        public class Get_RequestPresentation
        {
            public Int32? RPId { get; set; }
            public Int32? RegId { get; set; }
            public string Mode { get; set; }
            public string Audiance { get; set; }
            public string Venue { get; set; }
            public string Venue_name { get; set; }
            public string Address { get; set; }
            public string Area { get; set; }
            public Int32? StateId { get; set; }
            public Int32? DistrictId { get; set; }
            public Int32? CityId { get; set; }
            public string Pincode { get; set; }
            public string Contact_name { get; set; }
            public string Contact_number { get; set; }
            public string Message { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string Addedby { get; set; }
            public string Devicetoken { get; set; }
            public Int32? Requestedto { get; set; }
            public Int32? RequestAcceptStatus { get; set; }
            public DateTime? RequestDate { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public int? PresentationID { get; set; }
            public Boolean? OrgPresentation { get; set; }
        }

        public class Volunteer
        {
            public Int32? RegId { get; set; }
            public Int32? RoleId { get; set; }
            public string FullName { get; set; }
            public string Phonenumber { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }
            public string BLGName { get; set; }
            public string Addedby { get; set; }
            public string TokenId { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public string Pincode { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string CityName { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string UserAddress { get; set; }
            public string Area { get; set; }
        }

        public class DonorReports
        {
            public Int32? RegId { get; set; }
            public string TokenId { get; set; }
            public string FullName { get; set; }
            public string Observation { get; set; }
            public string Comments { get; set; }
            public DateTime? CommentedDate { get; set; }

        }

        public class pincodedetails
        {
            public Int32? RegId { get; set; }
            public string Email { get; set; }
            public string Phonenumber { get; set; }
            public string Pincode { get; set; }
            public string Venue_name { get; set; }
            public string Mode { get; set; }
            public string Audiance { get; set; }
            public string Contact_number { get; set; }
            public string Contact_name { get; set; }
            public string Address { get; set; }
            public string Message { get; set; }

            public string FullName { get; set; }

        }

        public class Replying
        {
            public Int32? RegId { get; set; }
            public string Email { get; set; }
            public string Phonenumber { get; set; }
            public string Pincode { get; set; }
            public string Venue_name { get; set; }
            public string Mode { get; set; }
            public string Audiance { get; set; }
            public string Contact_number { get; set; }
            public string Contact_name { get; set; }
            public string Address { get; set; }
            public string Message { get; set; }

            public string FullName { get; set; }

        }

        public class ImageDetails
        {
            public Int32? RegId { get; set; }
            public Int32? Categoryid { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string GalleryImages { get; set; }
            public string Institutionname { get; set; }
        }

        #region Crud_Notifications --suraj
        public class Crud_Notifications
        {
            public Int32? NotificationsID { get; set; }
            public string NotificationsDesc { get; set; }
            public string NotificationsImage { get; set; }
            public Int32? RegID { get; set; }
            public Int32? RPId { get; set; }
            public Int32? NotiRecevieID { get; set; }
            public Boolean? Status { get; set; }
            public Boolean? AcceptStatus { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? RequestAcceptStatus { get; set; }
            public Int32? Count { get; set; }

        }
        

        public class Dashboardmobile
        {
            public Int32? Referalcount { get; set; }
            public Int32? Adddonorscount { get; set; }
            public Int32? Dotationcount { get; set; }
            public Int32? Bannercount { get; set; }
            public Int32? Presentationcount { get; set; }
            public Int32? LeadPresentationcount { get; set; }


        }
        #endregion

        public class Delete
        {
            public Int32? RegId { get; set; }
        }

        public class UpdateToken
        {
            public Int32? RegId { get; set; }
           
            public string Devicetoken { get; set; }

           
        }

        public class Banner
        {
            public Int32? BannerId { get; set; }
            public string BannerName { get; set; }
            public string BannerPath { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CratedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }


        public class Banners
        {
            public Int32? BannerID { get; set; }
            public Int32? Roleid { get; set; }
            public string BannerImage { get; set; }
            public Int32? Sequence { get; set; }
            public Boolean? BannerStatus { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
        }

        public class active
        {
            public Boolean? Status { get; set; }
            public string Email { get; set; }

        }

        public class Bloodreqcondition
        {
            public Int32? RegId { get; set; }
            public Int32? RoleId { get; set; }
            public string Devicetoken { get; set; }
          
            public string Pincode { get; set; }
            public string FullName { get; set; }
            public string BloodGroupName { get; set; }
            public DateTime? Lastdonatedate { get; set; }
            public string Phonenumber { get; set; }
        }

        public class NotiStatus
        {
            public Int32? NotificationStatus { get; set; }
            public Int32? WhatappStatus { get; set; }
            public Int32? EmailStatus { get; set; }
            public string TimePeriod { get; set; }
        }

        #region /*//suraj*/
        public class Translation
        {
            [Key]
            public int Id { get; set; }
            public string Key { get; set; }
            public string English { get; set; }
            public string Hindi { get; set; }
            public string Telugu { get; set; }
        }
        #endregion

        public class BG_GetUserControllers
        {
            public Int32? NotificationID { get; set; }
            public string NotificationsDesc { get; set; }
            public string NotificationsImage { get; set; }
            public string TimePeriod { get; set; }
            public Int32? UserID { get; set; }
            public DateTime? ExpairyDate { get; set; }
            public Boolean? WhatsappStatus { get; set; }
            public Boolean? CallStatus { get; set; }
            public Boolean? SMSStatus { get; set; }
           
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
            public Int32? RoleId { get; set; }
            public Boolean? NotificationStatus { get; set; }
            public Boolean? EmailStatus { get; set; }

        }

        public class BG_UserControllersInsert
        {
            public Int32? NotificationID { get; set; }
            public Int32? UserID { get; set; }
            public Int32? RoleId { get; set; }
            public DateTime? ExpairyDate { get; set; }
            public Boolean? WhatsappStatus { get; set; }
            public Boolean? CallStatus { get; set; }
            public Boolean? SMSStatus { get; set; }
            public Boolean? NotificationStatus { get; set; }
            public string TimePeriod { get; set; }
        }

        public class Roleees
        {
            public Int32? RegId { get; set; }
            public Int32? RoleId { get; set; }
            public DateTime? NextEligibleDonateDate { get; set; }
            public Boolean? Availablestatus { get; set; }



        }

        public class Crud_UserCommunication
        {
            public Int32? CommunicationID { get; set; }
            public Int32? UserId { get; set; }
            public Int32? PhoneCallFreq { get; set; }
            public Int32? WhatsAppFreq { get; set; }
            public Int32? NotificationFreq { get; set; }
            public Int32? SMSFreq { get; set; }           
            public DateTime? PhoneCallLast { get; set; }
            public DateTime? PhoneCallNext { get; set; }
            public DateTime? WhatsAppLast { get; set; }
            public DateTime? WhatsAppNext { get; set; }
            public DateTime? NotificationLast { get; set; }
            public DateTime? NotificationNext { get; set; }
            public DateTime? SMSLast { get; set; }
            public DateTime? SMSNext { get; set; }
            public Boolean? Status { get; set; }
            public Int32? CreatedBy { get; set; }
            public Int32? ModifiedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }

        public class UserCommunication
        {
            public Int32? CommTypeId { get; set; }
            public string CommTypeName { get; set; }
            public string Description { get; set; }
            public Boolean? Status { get; set; }
           
        }
        public class UserCommunicationn
        {
            public Int32? CommunicationID { get; set; }
            public Int32? UserId { get; set; }
            public Int32? PhoneCallFreq { get; set; }
            public Int32? WhatsAppFreq { get; set; }
            public Int32? SMSFreq { get; set; }
            public Int32? NotificationFreq { get; set; }
            public Boolean? Status { get; set; }
           

        }

        public class RequestInfo
        {
          
            public string DonorName { get; set; }
            public string UserName { get; set; }
            public string Mobile { get; set; }
            public string FromMail { get; set; }
            public string ToMail { get; set; }
            public string BLGName { get; set; }
            public string CityName { get; set; }
            
        }
        public class UserDetailsModel
        {
            public int UdId { get; set; }
            public int RegId { get; set; }
            public string BloodRequestID { get; set; }
            public string BLGName { get; set; }
            public string UnitsofBlood { get; set; }
            public string FullName { get; set; }
            public string ContactMobile { get; set; }
            public string DoctorName { get; set; }
            public string HospitalName { get; set; }
            public string Purpose { get; set; }
            public string Typesofblood { get; set; }
            public string BloodRequestDate { get; set; }
            public string RequestTime { get; set; }
            public string Area { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string HospitalAddress { get; set; }
            public string ContactPerson { get; set; }
        }

        public class RegistrationModel
        {
            public int RegId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }
            public string Phonenumber { get; set; }
            public int RoleId { get; set; }
            public int ReffererId { get; set; }
            public DateTime? CreatedDate { get; set; }
        }

        public class BrochureModel
        {
            public string FullName { get; set; }
            public string Place { get; set; }
            public string Place_name { get; set; }
            public string Location { get; set; }
            public string Pincode { get; set; }
        }
        public class EnquiryRequest
        {
            public string Type { get; set; }
            public string Fullname { get; set; }
            public string EmailID { get; set; }
            public string Comments { get; set; }
            public string AttachmentPath { get; set; }
        }
        public class DonorSMSRequest
        {
            public string MobileNo { get; set; }
            public string DonorName { get; set; }
            public string PatientName { get; set; }
            public string BloodGroup { get; set; }
            public string CityName { get; set; }
        }
         

        public class ThreeImages
        {
            public Int32? BannerID { get; set; } 
            public string BannerImage { get; set; }
            public Int32? Sequence { get; set; }
            public Boolean? BannerStatus { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
        }

        public class ContactReport
        {

            public Int32? CId { get; set; }
            public Int32? RegId { get; set; }
            public string Report { get; set; }
            public string Comments { get; set; }
            public string FullName { get; set; }
            public string Phonenumber { get; set; }
            public string Email { get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public Int32? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public Int32? ModifiedBy { get; set; }
        }

        public class ForgotPasswordModel
        {
            public string Email { get; set; }
            public string NewPassword { get; set; }
        }
        public class Counts
        {      
            public Int32? Accepted { get; set; }
        }
        public class Countss
        {
            public Int32? RPId { get; set; }
            public Int32? PresentationID { get; set; }
            public string Mode { get; set; }
            public string Audiance { get; set; }
            public string Venue { get; set; }
            public string Venue_name { get; set; }
            public string Address { get; set; }
            public string Area { get; set; }
            public string newStatename { get; set; }
            public string newCityname { get; set; }
            public string Pincode { get; set; }
            public string Contact_name { get; set; }
            public string Contact_number { get; set; }
            public string Message { get; set; }
            public DateTime? RequestDate { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public int? Requestedto { get; set; }
            public int? RequestAcceptStatus { get; set; }
            public string FullName { get; set; }

            public Int32? RegId { get; set; }
            public string Phonenumber { get; set; }
            public string RegState { get; set; }
            public string RegDistrict { get; set; }
            public string RegCity { get; set; }
            public string RegPin { get; set; }
            public string RegArea { get; set; }

        }

        public class CastMaster
        {
            public int? CID { get; set; }
            public string CastName {  get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public int? ModifiedBy { get; set; }

        }
        public class ReasonMaster
        {
            public int? RID { get; set; }
            public string Reason {  get; set; }
            public Boolean? Status { get; set; }
            public DateTime? CreatedDate { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public int? ModifiedBy { get; set; }

        }


        public class acceptedusers
        {
            public int? BAID { get; set; }
            public int? UnitsofBlood { get; set; }
            public int? AcceptBy { get; set; }
            public string BloodRequestID { get; set; }
            public string FullName { get; set; }
            public string Phonenumber { get; set; }
            public string newStatename { get; set; }
            public string newDistrictname { get; set; }
            public string newCityname { get; set; }
            public string Area { get; set; }
            public string Pincode { get; set; }
            public int? Count { get; set; }
            public DateTime? CreatedDate { get; set; }


        }
    }
}
