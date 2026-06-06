using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodGroupApi.Models
{
    public enum SPS
    {
        StatesMaster_crud,    //--deeksha 14/feb/2024
        BloodGroupMaster_CRUD, //--deeksha 14/feb/2024
        DistrictMaster_crud,  //--Amrutha 14/feb/2024
        UnitsofBlood_CRUD,    //--Amrutha 14/feb/2024
        Oldagehome_CRUD,      //--Amrutha 15/feb/2024
        BG_dashboardcount,    //--deeksha,Amrutha 16/feb/2024
        HospitalDetails_CRUD, //--deeksha,Amrutha 
        INSERT_EXCEPTIONS,    //Naushad
        BG_GetExeption,      //Naushad
        BG_Webapi_Exceptions,//Naushad
        BG_GetAllExeption,//Naushad
        Donorlist_crud,//Naushad
        Recepient_crud,//Naushad
        Insert_Update_DonersForm,//manohar 14/feb/2024
        Insert_Update_requestForm,//manohar 19/feb/2024
        BG_Brochers_Crud,//manohar 14/feb/2024
        BG_Admin_Login,//Naushad
        Register_User_Curd,
        CitiesMaster_Crud,//Naushad
        BG_GeetExeption,
        Orphanage_CRUD,//Naushad
        Delete_Exceptions,//Naushad
        Gallery_Crud,//Upendra
        GetSmtpDetails,//Naushad
        BG_MailCrud,//Naushad
        Approve_Requests,//Naushad
        ShareYourService,
        Download_Brochures,
        Get_donor,//manoahr
        Get_Requestform,//manoahr
        User_SearchDeatils,
        Get_Areas, //srinivas
        Get_Requestbasedon_presonID,//manohar
        GalleryCategory_Crud,//Upendra
        Get_Gallerycatwise,//Upendra
        Get_Address,
        GetOrphan_Address,
        Oldagehomedetails,
        orphanDeatils,
        InsertBrochures_Deatails,
        All_Gallery_Search,
        Get_cities,
        Get_Gallaryimages_bycategory,
        Get_Phonenumber,
        Insert_Update_Requestpresent,
        Gallery_View,
        Get_Hospital,
        BG_Login,
        BG_Forgot_Password,
        Get_PincodeDropdown,
        Get_GallaryImagedBasedOn_ID,
        Insert_Update_Report,
        BG_Observation_Crud,//Naushad
        BG_Work_Place_Crud,//Naushad
        Get_RequestPresentation,//Rambabu
        Insert_Update_Addmembers,
        Volunteerlist_crud,//Naushad
        Get_DonorReports,//Naushad,Rambabu
        Get_Users_basedOn_Pincode,//manohar
        Get_image_uploadDetails,//manohar
        BG_Upload_BloodDonationImage,//Suresh
        BG_Get_ClosedBloodRequests,//Suresh
        Crud_Notifications,//Rk
        BG_dashboardcount_mobile, //suraj
        DeleteAccount_Covert_as_Leader,//manohar
        BG_gethospitaldetails_mobile, //suraj
        BG_UpdateDeviceToken, //prshnth
        BG_GetNotificationForUsers,
        BG_Getbloodrequest_notification,
        BG_Get_Referalcode_Notification,
        GetOldInactiveRegistrations,
        Get_Notification_basedon_UserId,//manohar,mobile
        Get_USERrequest_Closedrequest_Idbased,//manohar,mobile
        Crud_Banners,//manohar
        GetBanners, //
        Update_ActivateAccount,//manohar
        GetUsers_basedonPincode_BloodreqCondition,//manohar
        Update_Usercontrollers,//manohar
        Approve_Requests_byDonors, //suraj
        Get_Requestbasedon_presonID_mobile, //suraj
        Get_Mobile_AllGallerycatagrise, // suraj
        BG_GetBloodRequestsID, //suraj
        BG_Upload_BloodDonationImage_mobile, //suraj
        Get_Approve_Requests_byDonors_mobile, //suraj
        BG_Get_AcceptRequest_mobile, //suraj
        CustomersLoginByStatus,
        BG_GetUserControllers,
        BG_UserControllersInsert,
        BG_UserControllersUpdate,
        GetRoleforRolechange,//Upendra
        Get_CommunicationMaster, //Upendra
        UserCommunicationPreferences, //Upendra
        Get_UserCommunicationPreferences,// Upendra
        Update_CommunicationChannel,//Upendra
        BloodRequestClosed,//Upendra
        AfterAdminApprovedNotificationtoAll,//Upendra
        DashBoardDetails,// UIpendra
        Update_RegisterApprovalStatus, //Venkat
        Get_PresentationDetails,//Amrutha
        Crud_ThreeImages,//Amrutha
        BG_GetObservationReport,//Amrutha
        GetLocation_Donationdate_basedonBrequestID,//Amrutha
        Update_SYSSubmitted_ByBloodRequestID,//Amrutha
        InsertContactReport_MOBILE,//Amrutha 08/12/2025
        BG_GetContactReport,//Amrutha 09/12/2025
        GetLatestLastDonationDate,//Amrutha 16/12/2025
        CheckAnydonorisaccepted,//Upendra
        BG_UpdatePassword_ByEmail,//Asif
        NotificationStatusUpdate, //Upendra
        Get_PresentationAcceptedCount,//Upendra
        Get_Acceptedpersons,//Upendra
        CastMaster_crud,// Upendra
    }
}
