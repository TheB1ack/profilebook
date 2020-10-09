using ProfileBook.Models;
using ProfileBook.Services.Repository;

namespace ProfileBook.Services.Authorization
{
    public class AuthorizationService: IAuthorizationService
    {
        public User UserAuthorization(string userLogin,IRepository<User> repository)
        {
            return repository.GetItemByLogin(userLogin);
        }
    }
}
