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

			modelBuilder.Entity<User>()
				.HasOne(u => u.Setting)
				.WithMany(u => u.Users)
				.HasForeignKey(u => u.SettingId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<AdvertComment>()
			   .HasOne<User>(ac => ac.User)
			   .WithMany(u => u.AdvertComments)
			   .HasForeignKey(ac => ac.UserId)
			   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Advert>()
                .HasMany(a => a.AdvertRatings)
                .WithOne(r => r.Advert)
                .HasForeignKey(r => r.AdvertId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdvertRating>()
            	.HasKey(ar => new { ar.UserId, ar.AdvertId }); // Bileşik anahtar tanımı

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

            modelBuilder.Entity<Role>().HasData(new Role
            {
				Id = 1,
				Name = "Admin",
                CreatedDate = DateTime.Now,
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 2,
                Name = "User",
                CreatedDate = DateTime.Now,
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 3,
                Name = "Customer",
                CreatedDate = DateTime.Now,
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Elektronik",
				CategoryIconPath = "Elektronik.jpg",
				Description = "Elektronik ürünleri",
                CreatedDate = DateTime.Now,
            });

            modelBuilder.Entity<SubCategory>().HasData(new SubCategory
            {
                Id = 1,
                Name = "Telefon",
				CategoryId = 1,
                CreatedDate = DateTime.Now,
            });

            modelBuilder.Entity<User>().HasData(new User
            {
				Id = 1,
				FirstName = "Admin",
				LastName = "Admin",
                IsActive = true,
				CreatedDate = DateTime.Now,
				Email = "admin@test.com",
                Username = "admin",
                Password = "123",
                //Rol = new Rol { Id = 1},
                RoleId = 1,
                Phone = "0850",
				Address = "Ankara",
				UserImagePath = "Ankara Ankara Ankara",
				SettingId = 1,
				
            });

            modelBuilder.Entity<Setting>().HasData(new Setting
            {
                Id = 1,
                Key = "Dark Theme",
				Value = "Black"
            });

            base.OnModelCreating(modelBuilder);


        }

        //public override int SaveChanges()
        //{
        //	var datas = ChangeTracker.Entries<IAuiditEntity>();
        //	var currentTime = DateTime.Now;

        //	foreach (var data in datas)
        //	{
        //		switch (data.State)
        //		{
        //			case EntityState.Added:
        //				data.Entity.CreatedDate = currentTime;
        //				break;

        //			case EntityState.Modified:
        //				data.Entity.UpdatedDate = currentTime;
        //				break;

        //				//case EntityState.Deleted:
        //				//    // İstersen silinen kaydı kalıcı olarak silmek yerine, bir "Soft Delete" işlemi uygulayabilirsin.
        //				//    // data.State = EntityState.Modified;
        //				//    // data.Entity.DeletedDate = currentTime;
        //				//    // data.Entity.IsDeleted = true;
        //				//    // veya tamamen kaldırmak için şu satırı açabilirsin:
        //				//    // data.State = EntityState.Detached;
        //				//    break;
        //		}
        //	}

        //	return base.SaveChanges();
        //}

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
