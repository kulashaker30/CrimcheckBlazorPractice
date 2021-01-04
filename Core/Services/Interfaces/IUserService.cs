using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Data.Repositories;

namespace OnlineClinic.Core.Services
{
    public interface IUserService
    {
        bool Authenticate(string username, string password, bool AdminUser = false);

        bool IsValidUser(string username);
    }
}