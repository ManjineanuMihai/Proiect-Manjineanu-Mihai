using Microsoft.AspNetCore.Mvc;
using MPD_Manjineanu_Mihai.Models;

namespace MPD_Manjineanu_Mihai.Repositories
{
    public interface IUserRepository
    {
         Task RegisterUser(UserModel user);
         bool LoginUser(string username, string password);
         Task<UserModel> GetUser(string username);
        Task<UserModel> GetUserById(int id);
        Task DeleteUser(string username);
        Task Update(UserModel user);
    }
}