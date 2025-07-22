using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using SonaNova.Application.Interfaces;
using SonaNova.Infrastructure.Interfaces;
using SonaNova.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Services
{/// <summary>
 /// Service class for performing CRUD operations on Assignments.
 /// </summary>
    public class ChangePasswordService : IChangePasswordService
    {

       
        private readonly IChangePasswordRepository _ChangePasswordRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        // private readonly JWTSettings _jwtSettings;
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordService"/> class.
        /// </summary>
        /// <param name="ChangePasswordRepository">The repository for accessing ChangePasswordDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public ChangePasswordService(IChangePasswordRepository ChangePasswordRepository, IStudentRepository studentRepository, IMapper mapper, IConfiguration configuration)
        {
            _ChangePasswordRepository = ChangePasswordRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _configuration = configuration;

        }

        /// <inheritdoc/>

        public async Task<FacultyDto> GetVerifyPassword(string userName, string password)
        {
            var facultyEntity = await _ChangePasswordRepository.GetVerifyPassword(userName, password);
            var facultyDto = _mapper.Map<FacultyDto>(facultyEntity);
            return facultyDto;
        }

        public async Task<string> UpdateVerifyPassword(string userName, string newPassword, string oldPassword, long facultyId)
        {
            return await _ChangePasswordRepository.UpdateVerifyPassword(userName, newPassword, oldPassword, facultyId);
        }
        public async Task<int> PasswordReset(string userName, string password)
        {
            return await _ChangePasswordRepository.PasswordReset(userName, password);
        }
        public async Task<TokenDto> LoginAsync(LoginUserDto user)
        {
            try
            {
                string userRole = user.Role.ToString();

                if (user.Role == "Teacher" || user.Role == "Admin" || user.Role == "Principal" || user.Role == "Director")
                {
                    var faculties = await _ChangePasswordRepository.GetUserDetails(user.Username, user.Password, user.Role);
                    if (faculties == null || !faculties.Any())  // Fix: Count should be compared with 0
                    {
                        throw new Exception("The username and password do not match.");
                    }

                    var faculty = faculties.FirstOrDefault(x =>
                        (x.UserName?.Equals(user.Username) ?? false) &&
                        (x.Password?.Equals(user.Password) ?? false)); // Fix: Ensure both conditions match for login

                    if (faculty != null)
                    {
                        return GetToken(faculty.FacultyName, userRole, faculty.RoleId, faculty.Id, faculty.UserName);
                    }
                    else
                    {
                        throw new Exception("The username and password do not match.");
                    }
                }
                else if (user.Role.ToString() == "Parent")
                {
                    var students = await _studentRepository.GetStudentDetails(null);
                    if (students == null || !students.Any()) // Fix: Count should be compared with 0
                    {
                        throw new Exception("The username and password do not match.");
                    }
                    var student = students.FirstOrDefault(x => x.AdmissionNumber?.Equals(user.AdmissionNo) ?? false);
                    //var student = students.FirstOrDefault(x =>
                    //    (x.Father_MobileNumber?.Equals(user.MobileNo) ?? false) ||
                    //    (x.Mother_MobileNumber?.Equals(user.MobileNo) ?? false));
                    if (student != null)
                    {
                        string userName = student.AdmissionNumber.Equals(user.AdmissionNo) ?
                            student.FatherName : student.MotherName;
                        return GetToken(userName, userRole, student.RoleId, student.Id, "empty");
                    }
                    else
                    {
                        throw new Exception("The mobile number does not match.");
                    }

                    //if (student != null)
                    //{
                    //    string userName = student.Father_MobileNumber.Equals(user.MobileNo) ?
                    //        student.FatherName : student.MotherName;
                    //    return GetToken(userName, userRole,student.RoleId, student.Id,"empty");
                    //}
                    //else
                    //{
                    //    throw new Exception("The mobile number does not match.");
                    //}
                }

                // Default case: if user.Role is not Faculty or Parent
                throw new Exception("Invalid role specified.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error during login: " + ex.Message); // Better exception handling
            }

        }
        private TokenDto GetToken(string userName, string userRole, int roleId, int userId, string facultyUsername)
        {
            var jwtService = new JwtService(_configuration["JWTSettings:SecretKey"] ?? string.Empty,
                  _configuration["JWTSettings:Issuer"] ?? string.Empty, _configuration["JWTSettings:Audience"] ?? string.Empty);

            var claims = new List<Claim>
    {
        new (ClaimTypes.Name, userName),
        new (ClaimTypes.Role, userRole),
        new Claim("Role", userRole),
        new Claim("UserId", userId.ToString())
    };

            var expires = DateTime.UtcNow.AddHours(2);
            var token = jwtService.GenerateToken(claims, expires);

            return new TokenDto
            {
                AccessToken = token,
                ExpiresAt = expires,
                UserRole = userRole,
                RoleId = roleId,
                Username = userName,
                FacultyUsername = facultyUsername,
                UserId = userId
            };
        }
    }
    }
