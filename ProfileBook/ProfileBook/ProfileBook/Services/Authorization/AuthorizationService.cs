using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository<User> _repository;
        public AuthorizationService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async void SingUp(string userLogin, string userPassword)
        {
            User user = new User()
            {
                UserLogin = userLogin,
                UserPassword = userPassword,
            };
            await _repository.SaveItemAsync(user);
        }
        public bool SingIn(string userLogin, string userPassword)
        {
            User userResult = _repository.GetUserByLogin(userLogin);
            if (userResult?.UserPassword == userPassword)
            {
                //CrossAutoLogin.Current.SaveUserInfos(userLogin, userPassword);
                App.Current.Properties.Add("User", userResult);
               // App.Current.Properties.Add("UserLogin", userResult.UserLogin);
                return true;
            }
            return false;
        }
        public void LogOut(string userLogin)
        {
            //CrossAutoLogin.Current.DeleteUserInfos();
            App.Current.Properties.Remove("User");
            //App.Current.Properties.Remove("UserLogin");
        }
    }
}
