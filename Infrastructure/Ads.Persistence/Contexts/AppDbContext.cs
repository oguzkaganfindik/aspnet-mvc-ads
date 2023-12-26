using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Ads.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<AdvertComment> AdvertComments { get; set; }
        public DbSet<AdvertImage> AdvertImages { get; set; }
        public DbSet<AdvertRating> AdvertRatings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAdvert> CategoryAdverts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SubCategoryAdvert> SubCategoryAdverts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ve Setting arasındaki One-to-One ilişkiyi yapılandırma
            modelBuilder.Entity<User>()
                .HasOne(u => u.Setting)
                .WithOne(s => s.User)
                .HasForeignKey<Setting>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Page ve Setting arasındaki One-to-One ilişkiyi yapılandırma
            modelBuilder.Entity<Page>()
                .HasOne(p => p.Setting)
                .WithOne(s => s.Page)
                .HasForeignKey<Setting>(s => s.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdvertComment>()
               .HasOne(ac => ac.User)
               .WithMany(u => u.AdvertComments)
               .HasForeignKey(ac => ac.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Advert>()
                .HasMany(a => a.AdvertRatings)
                .WithOne(r => r.Advert)
                .HasForeignKey(r => r.AdvertId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdvertRating>()
                .HasIndex(ar => new { ar.UserId, ar.AdvertId })
                .IsUnique();

            modelBuilder.Entity<AdvertRating>()
                .HasOne(ar => ar.User)
                .WithMany(u => u.AdvertRatings) // User sınıfında AdvertRatings koleksiyonu olmalı
                .HasForeignKey(ar => ar.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdvertRating>()
                .HasOne(ar => ar.Advert)
                .WithMany(a => a.AdvertRatings) // Advert sınıfında AdvertRatings koleksiyonu olmalı
                .HasForeignKey(ar => ar.AdvertId)
                .OnDelete(DeleteBehavior.Restrict);  
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<IAuiditEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        data.Entity.DeletedDate = DateTime.Now;
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }



    }
}