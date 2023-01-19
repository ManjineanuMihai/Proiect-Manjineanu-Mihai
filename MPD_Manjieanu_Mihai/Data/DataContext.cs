using Microsoft.EntityFrameworkCore;
using MPD_Manjineanu_Mihai.Models;

namespace MPD_Manjineanu_Mihai.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<UserModel>? Users { get; set; }
        public DbSet<CategoryModel>? Categories { get; set; }
        public DbSet<ActivityModel>? Activities { get; set; } 
    }
}
