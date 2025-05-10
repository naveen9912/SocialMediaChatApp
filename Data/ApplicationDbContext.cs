using Microsoft.EntityFrameworkCore;
using SocialMediaChatApp.Models;

namespace SocialMediaChatApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       public DbSet<Message> Messages { get; set; }
    }
}
