using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Linq;
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
        public async Task<bool> SingInAsync(string userLogin, string userPassword)
        {
            //User userResult = await _repository.GetUserByLoginAsync(userLogin);
            var items = await _repository.GetItemsAsync<User>();
            User userResult = items.Where(x => x.UserLogin == userLogin).FirstOrDefault();

            if (userResult?.UserPassword == userPassword)
            {
                App.Current.Properties.Add("User", userResult);
                return true;
            }
            return false;
        }
        public void LogOut(string userLogin)
        {
            App.Current.Properties.Remove("User");
        }
    }
}
