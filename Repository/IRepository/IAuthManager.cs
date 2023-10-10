using Backend_Task.Entities;
using Backend_Task.Models.User;
using Backend_Task.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Backend_Task.Repository.IRepository
{
    public interface IAuthManager
    {
        //Create User
        Task<IEnumerable<IdentityError>> RegisterUser(CreateUser user);
        //Login User
        Task<ApiUserAuthenticationResponse> LoginUser(GetUser user);

        Task<string> CreateRefreshToken();

        Task<ApiUserAuthenticationResponse> VerifyRefreshToken(ApiUserAuthenticationResponse request);


    }
}
