using Microsoft.EntityFrameworkCore;
using UserCrud.Model;

namespace UserCrud.DataBaseAccess
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }


    }
}

