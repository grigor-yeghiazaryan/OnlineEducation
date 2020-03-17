using OnlineEducation.DAL.Entities;
using System.Collections.Generic;

namespace OnlineEducation.BLL.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
