using Microsoft.EntityFrameworkCore;
using SouliCool.Tutorials.Entities.Models;

namespace SouliCool.Tutorials.Entities
{
    public class EntitiesDbContext: DbContext
    {
        public EntitiesDbContext(DbContextOptions<EntitiesDbContext> context) : base(context)
        {

        }

        public DbSet<Entity> Entities { set; get; }
    }
}
