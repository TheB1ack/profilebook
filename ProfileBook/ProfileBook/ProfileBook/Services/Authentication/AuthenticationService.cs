using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        public async void SingUp(string userLogin, string userPassword, IRepository<User> repository)
        {
            User user = new User()
            {
                UserLogin = userLogin,
                UserPassword = userPassword,
                //IsLoggedIn = false
            };
           await repository.SaveItemAsync(user);
        }
        public bool SingIn(string userLogin, string userPassword, IRepository<User> repository)
        {
            User userResult = repository.GetItemByLogin(userLogin);
            if(userResult?.UserPassword == userPassword)
            {
                //repository.UpdateItemLogged(userLogin, true);
                return true;
            }
            return false;
        }
    }
}
