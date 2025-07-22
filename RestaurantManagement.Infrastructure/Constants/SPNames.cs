using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Constants
{
    public static class SPNames
    {
        public const string SP_GETAllBILLINGDETAIL = "[sp_GetBillInformation]";
        public const string SP_INSERTBILLINGDETAIL = "[sp_InsertBillInformation]";
        public const string SP_UPDATEBILLPAIDSTATUS = "sp_UpdateBillPaidStatus";
        public const string SP_DELETEBILLINGDETAIL = "sp_DeleteBillInformation";

        public const string SP_GETACTIVITYDETAILS = "sp_GetActivityData";
        public const string SP_GETAllACTIVITYDETAILS = "sp_GetAllActivityData";
        public const string SP_INSERTACTIVITYDETAILS = "sp_InsertActivityData";
        public const string SP_UPDATEACTIVITYDETAILS = "sp_UpdateActivityData";
        public const string SP_DELETEACTIVITYDETAILS = "sp_DeleteActivityData";



        public const string SP_GETHOUSEPOINT = "sp_GetHousepointbyHouseId";

        public const string SP_GETHOUSEACTIVITY = "[sp_GetHouseActivitypoint]";
        public const string SP_INSERTHOUSEACTIVITY = "InsertHousePointDetail";
        public const string SP_UPDATEHOUSEACTIVITY = "sp_UpdateHousePointDetail";
        public const string SP_DELETEHOUSEACTIVITY = "[sp_DeleteHousePointDetail]";

        public const string SP_GETSTUDENT = "sp_GetStudentDetails";
        public const string SP_INSERTSTUDENT = "sp_InsertStudentDetails";
        public const string SP_UPDATESTUDENT = "sp_UpdateStudentDetails";
        public const string SP_DELETESTUDENT = "sp_DeleteStudentDetails";
        public const string SP_GETSTUDENTBYNAME = "getStudentdetailsbyName";

        public const string SP_GETALLSUBJECT = "sp_GetAllSubject";
        public const string SP_INSERTSUBJECT = "sp_InsertSubject";
        public const string SP_UPDATESUBJECT = "sp_UpdateSubject";
        public const string SP_DELETESUBJECT = "sp_DeleteSubject";

        public const string SP_GETROLEMASTER = "sp_GetRoleMaster";
       
        public const string SP_INSERTROLEMASTER = "sp_InsertRoleMaster";
        public const string SP_UPDATEROLEMASTER = "sp_UpdateRoleMaster";       
       
        public const string SP_DELETEROLEMASTER = "sp_DeleteRoleMaster";

        public const string SP_GETHOLIDAYCALENDAR = "[sp_GetHolidayCalendar]";
        public const string SP_DELETEHOLIDAYCALENDAR = "[sp_DeleteHolidayCalendar]";
        public const string SP_INSERTHOLIDAYCALENDAR = "[sp_InsertHolidayCalendar]";
        public const string SP_UPDATEHOLIDAYCALENDAR = "[sp_UpdateHolidayCalendar]";

        public const string SP_GETALLASSIGNMENTDETAILS = "sp_GetAllAssignment";
        public const string SP_INSERTASSIGNMENTDETAILS = "[sp_InsertAssignment]";
        public const string SP_UPDATEASSIGNMENTDETAILS = "[sp_UpdateAssignment]";
        public const string SP_DELETEASSIGNMENTFORM = "[sp_DeleteAssignment]";
        public const string SP_GETALLASSIGNMENTBYSTUDENT = "sp_GetAllAssignmentByStudent";

        public const string SP_GETALLLEAVE = "sp_GetLeaveRequests";
        public const string SP_GETLEAVEBYID = "[sp_GetLeaveRequestsById]";

        public const string SP_INSERTLEAVE = "sp_InsertLeaveRequests";


        public const string SP_UPDATELEAVE = "sp_UpdateLeaveRequests";

        public const string SP_DELETELEAVE = "sp_DeleteLeaveRequests";



        public const string SP_GETAllRESTAURANTPROFILEMASTER = "sp_GetRestaurantProfile";
        public const string SP_UPDATERESTAURANTPROFILEMASTER = "sp_UpdateRestaurantProfile";

        public const string SP_GETAllUSERMASTER = "sp_GetUserMaster";
        public const string SP_INSERTUSERMASTER = "[sp_InsertUserMaster]";
        public const string SP_UPDATEUSERMASTER = "sp_UpdateUserMaster";
        public const string SP_DELETEUSERMASTER = "sp_DeleteUserMaster";


        public const string SP_UPDATEPASSWORD = "sp_UpdatePassword";

        public const string SP_GETCONTENTLIBDETAILS = "sp_GetContentLib";
        public const string SP_INSERTCONTENTLIBDETAILS = "[sp_InsertContentLib]";
        public const string SP_UPDATECONTENTLIBDETAILS = "[sp_UpdateContentLib]";
        public const string SP_DELETECONTENTLIB = "[sp_DeleteContentLib]";
        public const string SP_GETALLCONTENTLIBBYSTUDENT = "sp_GetAllContentLibByStudent";

        public const string SP_GETALLHOUSE = "sp_GetHouseDetails";
        public const string SP_INSERTHOUSE = "sp_InsertHouseDetails";
        public const string SP_UPDATEHOUSE = "sp_UpdateHouseDetails";
        public const string SP_DELETEHOUSE = "sp_DeleteHouseDetails";


        public const string SP_UPDATEINTERESTED = "[sp_UpdateInterestedCompetition]";
        public static string SP_INSERTUPCOMINGCOMPETITION = "[sp_InsertUpcomingCompetition]";
        public static string SP_GETALLUPCOMINGCOMPETITION = "[sp_GetUpcomingCompetitions]";
        public static string SP_GETUPCOMINGCOMPETITIONBYID = "[sp_GetUpcomingCompetitionsById]";
        public static string SP_UPDATEUPCOMINGCOMPETITION = "[sp_UpdateUpcomingCompetition]";
        public static string SP_DELETEUPCOMINGCOMPETITION = "[sp_DeleteUpcomingCompetition]";
        public const string SP_INTERESTEDSTUDENTLIST = "[sp_GetInterestStudentListForCompetition]";

        public const string SP_GETALLTIMETABLEDETAILS = "sp_GetAllTimetable";
        public const string SP_GetTimeTableBySectionIdDETAILS = "sp_GetTimeTableBySectionId";
        public const string SP_INSERTTIMETABLEDETAILS = "[sp_InsertTimetable]";
        public const string SP_UPDATETIMETABLEDETAILS = "[sp_UpdateTimetable]";
        public const string SP_DELETETIMETABLE = "[sp_DeleteTimetable]";

        public static string SP_GETEXAMS = "[sp_GetExamMaster]";
        public static string SP_INSERTEXAMS = "[sp_InsertExamMaster]";
        public static string SP_UPDATEEXAMS = "[sp_UpdateExamMaster]";
        public static string SP_DELETEEXAMS = "[sp_DeleteExamMaster]";

        public const string SP_GETALLSECTION = "sp_GetSectionMaster";
        public const string SP_INSERTSECTION = "sp_InsertSectionMaster";
        public static string SP_UPDATESECTION = "sp_UpdateSectionMaster";
        public static string SP_DELETESECTION = "sp_DeleteSectionMaster";

        public const string SP_GETFACULTY = "sp_GetFacultyDetailsById";
        public const string SP_INSERTFACULTY = "sp_InsertFacultyDetails";
        public const string SP_UPDATEFACULTY = "sp_UpdateFacultyDetails";
        public const string SP_DELETEFACULTY = "sp_DeleteFacultyDetails";
        public const string SP_GETFACULTYBYNAME = "getFacultydetailsbyName";

        public const string SP_GETACADEMICCALENDERBYID = "sp_GetAcademicCalenderById";
        public const string SP_GETACADEMICCALENDER = "sp_GetAcademicCalender";
        public const string SP_INSERTACADEMICCALENDER = "sp_InsertAcademicCalender";
        public const string SP_UPDATEACADEMICCALENDER = "sp_UpdateAcademicCalender";
        public const string SP_DELETEACADEMICCALENDER = "sp_DeleteAcademicCalender";

        public const string SP_GETSTUDENTFEEDBACKBYID = "sp_GetStudentFeedbackById";
        public const string SP_GETALLSTUDENTFEEDBACKDETAILS = "sp_GetAllStudentFeedback";
        public const string SP_INSERTSTUDENTFEEDBACKDETAILS = "[sp_InsertStudentFeedback]";
        public const string SP_UPDATESTUDENTFEEDBACKDETAILS = "[sp_UpdateStudentFeedback]";
        public const string SP_DELETESTUDENTFEEDBACK = "[sp_DeleteStudentFeedback]";

        public const string SP_INSERTSECTIONSTUDMAP = "[sp_InsertSectionStudsMappings]";
        public const string SP_GETALLSECTIONSTUDMAP = "[sp_GetSectionStudsMappings]";
        public static string SP_UPDATESECTIONSTUDMAP = "sp_UpdateSectionStudsMappings";
        public static string SP_DELETESECTIONSTUDMAP = "[sp_DeleteSectionStudsMappings]";
        public static string SP_UPDATESECTIONSTUDACTIVEMAP = "[sp_UpdateActiveBatchStudMapping]";
        public static string SP_GETMAPPEDSTUDENTBYNAME = "getMappedStudentbyName";

        public const string SP_INSERTBATCHSUBMAP = "sp_InsertSectionSubMappings";
        public const string SP_GETALLBATCHSUBMAP = "sp_GetSectionSubFacultyMappings";
        public static string SP_UPDATEBATCHSUBMAP = "sp_UpdateSectionSubMappings";
        public static string SP_DELETEBATCHSUBMAP = "sp_DeleteBatchSubsMappings";
        public const string SP_GetFacultyListBySectionIdDETAILS = "sp_GetFacultyListBySectionId";

        public const string SP_GETNOTIFICATION = "sp_GetNotification";
        public const string SP_UPDATENOTIFICATION = "[sp_UpdateNotification]";
        public const string SP_GETUSERTOKEN = "sp_GetUserToken";
        public const string SP_INSERTUSERFCMTOKENS = "sp_InsertUserFCMTokens";
        public const string SP_GETNOTIFICATIONBYID = "[sp_GetNotificationById]";

        public const string SP_GETAllORDERTYPEMASTER = "sp_GetOrderTypeMaster";
        public const string SP_INSERTORDERTYPEMASTER = "[sp_InsertOrderTypeMaster]";
        public const string SP_UPDATEORDERTYPEMASTER = "sp_UpdateOrderTypeMaster";
        public const string SP_DELETEORDERTYPEMASTER = "sp_DeleteOrderTypeMaster";

        public const string SP_BULKSTUDENTUPLOAD = "[sp_BulkUploadStudent]";
        public const string SP_BULKFACULTYUPLOAD = "[sp_BulkUploadFaculty]";
        public const string SP_BULKSUBJECTUPLOAD = "[sp_BulkUploadSubject]";
        public const string SP_BULKHOLIDAYUPLOAD = "[sp_BulkUploadHoliday]";
        public const string SP_BULKTIMETABLEUPLOAD = "[sp_BulkUploadTimetable]";
        public const string SP_BULKACADEMICUPLOAD = "[sp_BulkUploadAcademicCalendar]";
        public const string TBL_STUDENTMARKS = "tbl_studentMarks";

        public static string SP_GETANNOUNCEMENTBYID = "[sp_GetAnnouncementById]";
        public static string SP_INSERTANNOUNCEMENTDETAILS = "sp_InsertAnnoucement";
        public static string SP_GETALLANNOUNCEMENTDETAILS = "[sp_GetAnnoucement]";
        public static string SP_DELETEANNOUNCEMENTDETAILS = "[ sp_DeleteAnnouncementDetails]";

        public const string SP_UPDATEACTIVITYFILE = "sp_UpdateActivityFile";

        public const string SP_UPDATEFILE = "sp_UpdateFile";

        public static string SP_GETVERIFYPASSWORD = "sp_GetVerifyPassword";
        public static string SP_UPDATEVERIFYPASSWORD = "sp_UpdateVerifyPassword";

        public const string SP_GETORDERITEMTRANSACTION = "sp_GetOrderItemTransaction";

        public const string SP_INSERTORDERITEMTRANSACTION = "sp_InsertOrderItemTransaction";

        public const string SP_UPDATEACTIVEORDERITEMTRANSACTION = "sp_UpdateActiveOrderItemTransaction";
        public const string SP_GETALLTABLEMAPPEDDETAILS = "sp_GetTableStatusMapping";

        public static string SP_UPDATEEMAIL = "[UpdateSendEmail]";
        public static string SP_DELETEMARK = "[sp_DeleteMark]";
        public static string SP_MARKTEMPLATE = "[sp_GetMarkTemplate]";
        public static string SP_Getsubjectsformarks = "[getsubjectsformarks]";


        public static string SP_GETSTUDENTMARKS = "[sp_getStudentMarks]";
        public static string SP_GETSTUDENTMARKSBYSTUDENT = "[SP_GetStudentMarkById]";
        public static string SP_INSERTSTUDENTMARKS = "[sp_insertStudentMarks]";
        public const string SP_GETSTUDENTMARKBYID = "sp_getStudentMarksById"; 

        public static string SP_GETALLATTENDANCE = "sp_GetAttendanceNew";
        public static string SP_GETALLATTENDANCEBYID = "[sp_GetAttendanceById]";
        public static string SP_GETALLATTENDANCEBYSTUDENTID = "sp_GetMonthAttendanceByStudentId";
        public static string SP_INSERTATTENDANCE = "sp_InsertAttendance";
        public static string SP_UPDATEATTENDANCE = "sp_UpdateAttendance";
        public static string SP_PASSWORDRESET = "sp_PasswordReset";
        public static string SP_DELETEATTENDANCE = "sp_DeleteAttendance1";

        public static string SP_MonthwiseAttendance = "GetStudentCumulativeAttendanceReport";
        //public static string SP_MonthwiseDynamicAttendance = "sp_GetMonthAttendanceByMONTH";
        public static string SP_MonthwiseDynamicAttendance = "[sp_GetMonthAttendanceByMonth_prasath]";
        public static string SP_CumulativeAttendance = "GetAnnualAttendanceRegister";

        public const string SP_GETATTENDANCEBYID = "sp_GetMonthAttendanceByStudentId";

        public const string TBL_SUBJECT = "dbo.tbl_Subject";

        public const string SP_GETALLSTUDENTDETAILWITHSECTION = "[Sp_GetAllStudentDetailWithSection]";
        public const string SP_GETALLFACULTYLIST = "[sp_GetAllFacultyList]";

        public static string SP_UPDATESTUDENTSEMDATE = "[Sp_UpdateStudentSemDate]";
        public static string SP_GETSTDCONGIG = "sp_GetAllStudentConfig";

        public const string SP_GETINFOGALORE = "sp_GetAllInfoGalore";
        public const string SP_INSERTINFOGALORE = "sp_InsertInfoGalore";
        public const string SP_UPDATEINFOGALORE = "sp_UpdateInfoGalore";
        public const string SP_GETUSERDETAILS = "sp_GetUserDetails";
    }


}
