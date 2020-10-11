using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthorizationService
    {
        void SingUp(string userLogin, string userPassword);
        Task<bool> SingInAsync(string userLogin, string userPassword);
        void LogOut(string userLogin);
    }
}
