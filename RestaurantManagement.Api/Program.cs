using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Mappings;
using RestaurantManagement.Application.Services;
using RestaurantManagement.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Application.Interfaces;
using SonaNova.Application.Services;
using SonaNova.Infrastructure.Interfaces;
using SonaNova.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using RestaurantManagement.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add AutoMapper and configure mapping profiles
services.AddAutoMapper(typeof(MappingProfile));
services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
//services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IContentLibService, ContentLibService>();
services.AddScoped<IContentLibRepository, ContentLibRepository>();
services.AddScoped<IActivityService, ActivityService>();
services.AddScoped<IActivityRepository, ActivityRepository>();
services.AddScoped<ISubjectService, SubjectService>();
services.AddScoped<ISubjectRepository, SubjectRepository>();
services.AddScoped<IHouseActivityService, HouseActivityService>();
services.AddScoped<IHouseActivityRepository, HouseActivityRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IStudentService, StudentService>();
services.AddScoped<IStudentRepository, StudentRepository>();
services.AddScoped<ITimeTableService, TimeTableService>();
services.AddScoped<ITimeTableRepository, TimeTableRepository>();
//services.AddScoped<IRestaurantProfileService, RestaurantProfileService>();
//services.AddScoped<IRestaurantProfileRepository, RestaurantProfileRepository>();
//services.AddScoped<IOrderTypeService, OrderTypeService>();
//services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
services.AddScoped<IHolidayCalendarService, HolidayCalendarService>();
services.AddScoped<IHolidayCalendarRepository, HolidayCalendarRepository>();
services.AddScoped<IHouseService, HouseService>();
services.AddScoped<IHouseRepository, HouseRepository>();
services.AddScoped<ISectionService, SectionService>();
services.AddScoped<ISectionRepository, SectionRepository>();
services.AddScoped<IFacultyService, FacultyService>();
services.AddScoped<IFacultyRepository, FacultyRepository>();

services.AddScoped<ILeaveService, LeaveService>();
services.AddScoped<ILeaveRepository, LeaveRepository>();
services.AddScoped<IExamService, ExamService>();
services.AddScoped<IExamRepository, ExamRepository>();
services.AddScoped<IStudentFeedbackService, StudentFeedbackService>();
services.AddScoped<IStudentFeedbackRepository, StudentFeedbackRepository>();

services.AddScoped<IAcademicCalendarService, AcademicCalendarService>();
services.AddScoped<IAcademicCalendarRepository, AcademicCalendarRepository>();
services.AddScoped<IAssignmentService, AssignmentService>();
services.AddScoped<IAssignmentRepository, AssignmentRepository>();
services.AddScoped<IUpcomingCompetitionRepository, UpcomingCompetitionRepository>();
services.AddScoped<IUpcomingCompetitionService, UpcomingCompetitionService>();
services.AddScoped<ISectionSubjectMappingRepository, SectionSubjectMappingRepository>();
services.AddScoped<ISectionSubjectMappingService, SectionSubjectMappingService>();
services.AddScoped<ISectionStudentMappingService, SectionStudentMappingService>();
services.AddScoped<ISectionStudentMappingRepository, SectionStudentMappingRepository>();

services.AddScoped<INotificationService, NotificationService>();
services.AddScoped<INotificationRepository, NotificationRepository>();
services.AddScoped<IBulkUploadService, BulkUploadServices>();
services.AddScoped<IBulkuploadRepository, BulkUploadRepository>();
services.AddScoped<IAnnouncementService, AnnouncementService>();
services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

services.AddScoped<IUploadAttachmentsService, UploadAttachmentsService>();
services.AddScoped<IUploadAttachmentsRepository, UploadAttachmentsRepository>();
services.AddScoped<IChangePasswordService, ChangePasswordService>();
services.AddScoped<IChangePasswordRepository, ChangePasswordRepository>();

services.AddScoped<IMarkServices, MarkServices>();
services.AddScoped<IMarkRepository, MarkRepository>();

services.AddScoped<IAttendanceService, AttendanceService>();
services.AddScoped<IAttendanceRepository, AttendanceRepository>();
services.AddScoped<IAttendanceReport, AttendancereportService>();
services.AddScoped<IAttendanceReportRepository, AttendanceReportRepository>();

services.AddScoped<IInfoGaloreRepository, InfoGaloreRepository>();
services.AddScoped<IInfoGaloreService, InfoGaloreService>();
services.AddScoped<IDataBaseConnection, DataBaseConnection>();

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

services.AddLocalization();
services.AddMvc();
services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTSettings:SecretKey"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTSettings:Audience"]
    };
});
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SonaNova.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "SonaNova.API Authorization",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SonaNova.Service.API v1"));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyAllowSpecificOrigins");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

