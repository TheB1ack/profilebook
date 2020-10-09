using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthenticationService
    {
        void SingUp(string userLogin, string userPassword, IRepository<User> repository);
        bool SingIn(string userLogin, string userPassword, IRepository<User> repository);
    }
}
