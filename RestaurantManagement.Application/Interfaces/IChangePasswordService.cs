using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IChangePasswordService
    {
        Task<string> UpdateVerifyPassword(string UserName, string NewPassword, string OldPassword, long FacultyId);
        Task<FacultyDto> GetVerifyPassword(string UserName, string NewPassword);
        Task<int> PasswordReset(string userName, string password);
        Task<TokenDto> LoginAsync(LoginUserDto user);
    }
}
