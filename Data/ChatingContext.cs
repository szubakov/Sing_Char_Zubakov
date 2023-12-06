using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Sing_Char_Zubakov.Data
{
    public class ChatingContext : DbContext
    {
        public DbSet<Mess> Mess { get; set; }

        public DbSet<User> Users { get; set; }

        public ChatingContext(DbContextOptions<ChatingContext> options) : base(options)
        {

        }
    }
}
