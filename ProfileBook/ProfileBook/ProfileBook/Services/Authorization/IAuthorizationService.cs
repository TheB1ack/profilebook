using ProfileBook.Models;
using ProfileBook.Services.Repository;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        User UserAuthorization(string userLogin, IRepository<User> repository);
    }
}
