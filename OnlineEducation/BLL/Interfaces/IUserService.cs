using OnlineEducation.DAL.Entities;
using System.Threading.Tasks;

namespace OnlineEducation.BLL.Interfaces
{
    public interface IUserService : IServiceBase<User>
    {
        Task<User> Authenticate(string email, string password);
    }
}