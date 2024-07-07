using RFID.SimpleTask.Application.Common.Models;

namespace RFID.SimpleTask.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
    Task<Result> SignInAsync(string email, string password, bool rememberMe = false);

    Task SignOutAsync();

}
