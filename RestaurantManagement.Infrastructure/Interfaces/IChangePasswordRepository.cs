using RestaurantManagement.Domain.Entities;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    public  interface IChangePasswordRepository
    {
        Task<string> UpdateVerifyPassword(string UserName, string NewPassword, string OldPassword, long FacultyId);
        Task<Faculty> GetVerifyPassword(string UserName, string NewPassword);
        Task<int> PasswordReset(string userName, string password);
        Task<List<UserModel>> GetUserDetails(string Username, string Password, string role);
    }
}
