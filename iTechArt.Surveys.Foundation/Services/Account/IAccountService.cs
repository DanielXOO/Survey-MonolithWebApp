using System.Threading.Tasks;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Foundation.Services.Account
{
    public interface IAccountService
    {
        Task<ServiceResult> SignInAsync(string username, string password);

        Task<ServiceResult> RegisterAsync(User user, string password);

        Task SignOutAsync();
    }
}