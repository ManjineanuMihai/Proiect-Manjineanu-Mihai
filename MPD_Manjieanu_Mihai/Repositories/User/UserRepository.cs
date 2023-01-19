using Microsoft.EntityFrameworkCore;
using MPD_Manjineanu_Mihai.Data;
using MPD_Manjineanu_Mihai.Models;

namespace MPD_Manjineanu_Mihai.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _data;

        public UserRepository(DataContext data)
        {
            _data = data;
        }

        public async Task<UserModel> GetUser(string username)
        {
            if (string.IsNullOrEmpty(username)) return  new UserModel();

            return await _data.Users.Where(x => x.Username == username).FirstOrDefaultAsync();

        }
        public async Task<UserModel> GetUserById(int id)
        {
            return await _data.Users.Where(x=>x.UserID == id).FirstOrDefaultAsync();
        }

        public bool LoginUser(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;

            var dbUser = _data.Users.Where(x=> x.Username == username).FirstOrDefault();
            if(dbUser is null) return false;

            if (dbUser.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task RegisterUser(UserModel user)
        {
            await _data.Users.AddAsync(user);
            await _data.SaveChangesAsync();
        }
        public async Task DeleteUser(string username)
        {
            var user = await _data.Users.FirstOrDefaultAsync(x=>x.Username== username);
             _data.Users.Remove(user);
            await _data.SaveChangesAsync();
        }
        public async Task Update(UserModel user)
        {
            _data.Users.Update(user);
            await _data.SaveChangesAsync();
        }
    }
}
