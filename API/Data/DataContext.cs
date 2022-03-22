using API.Entites;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UserDocument> UsersDocuments { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Connection> Connections { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Group>()
                      .HasMany(x => x.Connections)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserDocument>()
                .HasOne(u => u.User)
                .WithMany(ur => ur.UserDocuments)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserDocument>()
                 .HasOne(u => u.Document)
                .WithMany(ur => ur.UserDocuments)
                .HasForeignKey(ur => ur.DocumentId);

        }


    }
}