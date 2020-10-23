using Acr.UserDialogs;
using Plugin.Settings;
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
        public async Task<bool> SingUpAsync(string userLogin, string userPassword)
        {
            var items = await _repository.GetItemsAsync<User>();
            User userResult = items.Where(x => x.UserLogin == userLogin).FirstOrDefault();
            if (userResult != null)
            {
                string text = Resources.Resource.SingUpPage_AlertLoginMatch;
                UserDialogs.Instance.Alert(text, "", "OK");
                return false;
            }
            else
            {
                User user = new User()
                {
                    UserLogin = userLogin,
                    UserPassword = userPassword,
                };
                await _repository.SaveItemAsync(user);
            }
            return true;
        }
        public async Task<bool> SingInAsync(string userLogin, string userPassword)
        {
            var items = await _repository.GetItemsAsync<User>();
            User userResult = items.Where(x => x.UserLogin == userLogin).FirstOrDefault();

            if (userResult?.UserPassword == userPassword)
            {
                CrossSettings.Current.AddOrUpdateValue("UserId", userResult.UserId);
                return true;
            }
            return false;
        }
        public void LogOut()
        {
            CrossSettings.Current.Remove("UserId");
        }
    }
}
