using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ads.Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<AdvertComment> AdvertComments { get; set; }
        public DbSet<AdvertImage> AdvertImages { get; set; }
        public DbSet<AdvertRating> AdvertRatings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAdvert> CategoryAdverts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AppUser> Users { get; set; }
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

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Role) // 1 kullanıcı 1 rolde olabilir
                .WithMany(u => u.Users)// 1 rolde birden fazla kullanıcı olabilir
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            // Setting ile User arasındaki ilişki
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Setting) // User, bir Setting ile ilişkilendirilir.
                .WithMany(s => s.Users) // Bir Setting, birden çok User ile ilişkilendirilebilir.
                .HasForeignKey(u => u.SettingId) // ForeignKey olarak User'daki SettingId kullanılır.
                .OnDelete(DeleteBehavior.Restrict); // İsteğe bağlı: User silindiğinde ilişkili Setting'in silinmemesi için.

            // Setting ile Page arasındaki ilişki
            modelBuilder.Entity<Page>()
                .HasOne(p => p.Setting) // Page, bir Setting ile ilişkilendirilir.
                .WithMany(s => s.Pages) // Bir Setting, birden çok Page ile ilişkilendirilebilir.
                .HasForeignKey(p => p.SettingId) // ForeignKey olarak Page'deki SettingId kullanılır.
                .OnDelete(DeleteBehavior.Restrict); // İsteğe bağlı: Page silindiğinde ilişkili Setting'in silinmemesi için.


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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdvertRating>()
                .HasOne(ar => ar.Advert)
                .WithMany(a => a.AdvertRatings) // Advert sınıfında AdvertRatings koleksiyonu olmalı
                .HasForeignKey(ar => ar.AdvertId)
                .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "Admin", CreatedDate = DateTime.Now },
                new AppRole { Id = 2, Name = "Moderator", CreatedDate = DateTime.Now },
                new AppRole { Id = 3, Name = "User", CreatedDate = DateTime.Now }
             );
            base.OnModelCreating(modelBuilder);

            // design time da çalışan seeder
            var passwordHasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                IsActive = true,
                CreatedDate = DateTime.Now,
                Email = "admin@test.com",
                UserName = "admin",
                Password = passwordHasher.HashPassword(null, "123"),
                RoleId = 1,
                Phone = "9050",
                Address = "Turkey",
                UserImagePath = "admin.jpg",
            };
            modelBuilder.Entity<AppUser>().HasData(adminUser);

            var moderatorUser = new AppUser
            {
                Id = 2,
                FirstName = "Moderator",
                LastName = "Moderator",
                IsActive = true,
                CreatedDate = DateTime.Now,
                Email = "moderator@test.com",
                UserName = "moderator",
                Password = passwordHasher.HashPassword(null, "123"),
                RoleId = 2,
                Phone = "9050",
                Address = "Turkey",
                UserImagePath = "moderator.jpg",

            };
            modelBuilder.Entity<AppUser>().HasData(moderatorUser);
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