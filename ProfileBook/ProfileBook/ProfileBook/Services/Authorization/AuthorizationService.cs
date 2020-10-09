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
            User userResult = _repository.GetItemByLogin(userLogin);
            if (userResult?.UserPassword == userPassword)
            {
                App.Current.Properties.Add("UserLogin", userLogin);
                return true;
            }
            return false;
        }
        public void LogOut(string userLogin)
        {
            App.Current.Properties.Remove("UserLogin");
        }
    }
}
