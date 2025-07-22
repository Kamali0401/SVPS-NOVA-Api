using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Domain.Entities;
using SonaNova.Application.Dtos;
using SonaNova.Domain.Entities;

namespace RestaurantManagement.Application.Mappings
{
    /// <summary>
    /// Represents a AutoMapper mapping profile for mapping between entities and DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            // Define mappings between Brand and BrandDto

            CreateMap<ContentLib, ContentLibDto>().ReverseMap();
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<HouseActivity, HouseActivityDto > ().ReverseMap();
            CreateMap<HousePointModel, HousePointModelDto>().ReverseMap();
            CreateMap<Roles, RoleDto>().ReverseMap();
            CreateMap<StudentDto, Students>().ReverseMap();
            CreateMap<StudentDropdown, StudentDropdownDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<TimeTable, TimeTableDto>().ReverseMap();
            //CreateMap<RestaurantProfiles, RestaurantProfilesDto>().ReverseMap();
            CreateMap<Assignment, AssignmentDto>().ReverseMap();
            CreateMap<UpcomingCompetition, UpcomingCompetitionDto>().ReverseMap();
            CreateMap<House, HouseDto>().ReverseMap();
            CreateMap<HolidayCalendar, HolidayCalendarDto>().ReverseMap();
            CreateMap<SectionMaster, SectionDto>().ReverseMap();
            CreateMap<Faculty, FacultyDto>().ReverseMap();
            CreateMap<FacultyDropdown, FacultyDropdowndto>().ReverseMap();
           // CreateMap<OrderTypes, OrderTypeDto>().ReverseMap();
            CreateMap<TableMappingDetails, TableMappingDetailsDto>().ReverseMap();
            CreateMap<Leave, LeaveDto>().ReverseMap();
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<AcademicCalendar, AcademicCalendarDto>().ReverseMap();
            CreateMap<StudentFeedbackDto, StudentFeedback>().ReverseMap();
            CreateMap<SectionStudentMapping, SectionStudentMappingDto>().ReverseMap();
            CreateMap<SectionSubjectMapping, SectionSubjectMappingDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<ExcelUpload, ExcelUploadDto>().ReverseMap();
            CreateMap<BillingUpdateDto, BillingUpdate>().ReverseMap();
            CreateMap<UploadFile, UploadfileDto>().ReverseMap();
            CreateMap<StudentDropdownModel, StudentDropdownModelDto>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<StudentAttendanceModel, StudentAttendanceModelDto>().ReverseMap();
            CreateMap<Mark, MarkDto>().ReverseMap();

            CreateMap<InfoGalore, InfoGaloreDto>().ReverseMap();
            CreateMap<InfoAttachmentModelDto, InfoAttachmentModel>().ReverseMap();
            CreateMap<StudentSemDateModel, StudentSemDateModelDto>().ReverseMap();
            CreateMap<Announcement, AnnouncementDto>().ReverseMap();
            CreateMap<AttachmentModelDto, AttachmentModel>().ReverseMap();
            

        }
    }
}
