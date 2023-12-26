using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ads.Persistence.DataContext
{
    public static class DbSeeder
    {
        public static void SeedData(AppDbContext context)
        {

            context.Database.Migrate();

            if (!context.Roles.Any())
            {
                SeedRoles(context);
            }

            if (!context.Pages.Any())
            {
                SeedPages(context);
            }
               if (!context.Users.Any())
            {
                SeedUsers(context);
            }
            if (!context.Settings.Any())
            {
                SeedSettings(context);
            }

            if (!context.Categories.Any())
            {
                SeedCategories(context);
            }

            if (!context.SubCategories.Any())
            {
                SeedSubCategories(context);
            }

            if (!context.Adverts.Any())
            {
                SeedAdverts(context);
            }

            if (!context.AdvertComments.Any())
            {
                SeedAdvertComments(context);
            }

            if (!context.AdvertImages.Any())
            {
                SeedAdvertImages(context);
            }

            if (!context.CategoryAdverts.Any())
            {
                SeedCategoryAdverts(context);
            }

            if (!context.SubCategoryAdverts.Any())
            {
                SeedSubCategoryAdverts(context);
            }

            if (!context.AdvertRatings.Any())
            {
                SeedAdvertRatings(context);
            }
        }

        private static void SeedRoles(AppDbContext context)
        {
           
                var currentDate = DateTime.Now;

                var roles = new List<Role>
                {
                    new Role { Name = "Admin", CreatedDate = currentDate },
                    new Role { Name = "Moderator", CreatedDate = currentDate },
                    new Role { Name = "User", CreatedDate = currentDate }
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
           
        }

        private static void SeedPages(AppDbContext context)
        {
            var pages = new List<Page>
        {
            new Page
            {
                Name = "Blog Page",
                Title1 = "Introduction",
                Title2 = "How we can help",
                Content1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc est justo, aliquam nec tempor fermentum, commodo et libero. Quisque et rutrum arcu. Vivamus dictum tincidunt magna id euismod. Nam sollicitudin mi quis orci lobortis feugiat.",
                Content2 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc est justo, aliquam nec tempor fermentum, commodo et libero. Quisque et rutrum arcu. Vivamus dictum tincidunt magna id euismod. Nam sollicitudin mi quis orci lobortis feugiat.",
                PageImagePath = "~/dist/images/about/about.jpg",
                SettingId = 2, 
                CreatedDate = DateTime.Now
            },
            new Page
            {
                Name = "About Us",
                Title1 = "Hakkımızda Başlığı 1",
                Title2 = "Hakkımızda Başlığı 2",
                Content1 = "Hakkımızda içeriği 1 buraya yazılacak.",
                Content2 = "Hakkımızda içeriği 2 buraya yazılacak.",
                PageImagePath = "~/dist/images/about/about.jpg",
                SettingId = 2,
                CreatedDate = DateTime.Now,
            },
            new Page
            {
                Name = "Contact Us",
                Title1 = "İletişim Başlığı 1",
                Title2 = "İletişim Başlığı 2",
                Content1 = "İletişim içeriği 1 buraya yazılacak.",
                Content2 = "İletişim içeriği 2 buraya yazılacak.",
                PageImagePath = "~/dist/images/about/about.jpg",
                SettingId = 2,
                CreatedDate = DateTime.Now,
            },

        };

            context.Pages.AddRange(pages);
            context.SaveChanges();
        }

        private static void SeedUsers(AppDbContext context)
        {
            var settingId = context.Settings.FirstOrDefault()?.Id; // Varsayılan bir ayar ID'si
            // Admin kullanıcısını kontrol et ve ekle
            if (!context.Users.Any(u => u.Email == "admin@test.com"))
            {
                var adminRoleId = context.Roles.FirstOrDefault(r => r.Name == "Admin")?.Id;


                var passwordHasher = new PasswordHasher<User>();
                var adminUser = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    Email = "admin@test.com",
                    Username = "admin",
                    Password = passwordHasher.HashPassword(null, "123"), // Admin şifresi: "123"
                    RoleId = adminRoleId ?? 1, // Eğer "Admin" rolü yoksa varsayılan olarak 1 ID'sini kullan
                    Phone = "9050",
                    Address = "Turkey",
                    UserImagePath = "admin.jpg",
                    SettingId = settingId ?? 1 // Eğer bir ayar yoksa varsayılan olarak 1 ID'sini kullan
                };

                context.Users.Add(adminUser);
            }

            // Diğer kullanıcıları oluştur
            var roleIds = context.Roles.Where(r => r.Name != "Admin").Select(r => r.Id).ToList();

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
                .RuleFor(u => u.Password, (f, u) => new PasswordHasher<User>().HashPassword(u, f.Internet.Password()))
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
                .RuleFor(u => u.Username, (f, u) => f.Internet.UserName())
                .RuleFor(u => u.Address, (f, u) => f.Address.FullAddress())
                .RuleFor(u => u.Phone, (f, u) => f.Phone.PhoneNumber())
                .RuleFor(u => u.IsActive, f => f.Random.Bool())
                .RuleFor(u => u.CreatedDate, f => f.Date.Past(1))
                .RuleFor(u => u.UserImagePath, f => f.Internet.Avatar())
                .RuleFor(u => u.RoleId, f => f.PickRandom(roleIds))
                .RuleFor(u => u.SettingId, f => f.PickRandom(settingId));

            var users = userFaker.Generate(9); // 9 adet diğer kullanıcı oluştur
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void SeedSettings(AppDbContext context)
        {
            var settings = new List<Setting>
                {
                    //// Kullanıcı için örnek bir tema ayarı
                    new Setting
                    {
                        Key = "User Theme",
                        Value = "Dark", // Varsayılan tema
                        UserId = 1,
                        CreatedDate = DateTime.Now,
        },
                     //Sayfa için örnek bir görünürlük ayarı
                    new Setting
                    {
                        Key = "Page Visibility",
                        Value = "True", // Sayfa varsayılan olarak görünür
                        PageId = 1,
                        CreatedDate = DateTime.Now,
        }
                };
            context.Settings.AddRange(settings);
            context.SaveChanges();
        }
        private static void SeedCategories(AppDbContext context)
        {
            var mainCategories = new List<Category>
                {
                    new Category { Name = "Electronics", Description = "Electronic devices and gadgets", CategoryIconPath = "fa fa-laptop icon-bg-1",CreatedDate = DateTime.Now },
                    new Category { Name = "Food", Description = "Food and beverages", CategoryIconPath = "fa fa-apple icon-bg-2",CreatedDate = DateTime.Now },
                    new Category { Name = "Fashion", Description = "Clothing, footwear, and accessories", CategoryIconPath = "fa fa-tshirt icon-bg-4",CreatedDate = DateTime.Now },
                    new Category { Name = "Home & Garden", Description = "Home improvement, decor, and gardening products", CategoryIconPath = "fa fa-home icon-bg-3",CreatedDate = DateTime.Now },
                    new Category { Name = "Sports & Outdoors", Description = "Sports equipment and outdoor gear", CategoryIconPath = "fa fa-ball icon-bg-5",CreatedDate = DateTime.Now },
                    new Category { Name = "Beauty & Health", Description = "Beauty products and health care", CategoryIconPath = "fa fa-health icon-bg-8",CreatedDate = DateTime.Now },
                    new Category { Name = "Toys & Hobbies", Description = "Toys, games, and hobby essentials", CategoryIconPath = "fa fa-toy icon-bg-7",CreatedDate = DateTime.Now },
                    new Category { Name = "Automotive", Description = "Automotive parts and accessories", CategoryIconPath = "fa fa-car icon-bg-6",CreatedDate = DateTime.Now }
                };

            context.Categories.AddRange(mainCategories);
            context.SaveChanges();
        }
        private static void SeedSubCategories(AppDbContext context)
        {
            var categoryIds = context.Categories.ToDictionary(c => c.Name, c => c.Id);

            var subCategoryNames = new Dictionary<string, string[]>
                {
                    {"Electronics", new[] {"Phones", "Computers", "TVs", "Tablets", "Audio Devices", "Gaming Consoles"}},
                    {"Food", new[] {"Fruits", "Vegetables", "Drinks", "Snacks", "Dairy Products", "Meat and Poultry"}},
                    {"Fashion", new[] {"Clothing", "Shoes", "Accessories", "Jewelry", "Watches", "Bags"}},
                    {"Home & Garden", new[] {"Furniture", "Decor", "Gardening Tools", "Kitchenware", "Bedding", "Lighting"}},
                    {"Sports & Outdoors", new[] {"Fitness Equipment", "Outdoor Gear", "Sportswear", "Footwear", "Camping Equipment", "Bicycles"}},
                    {"Beauty & Health", new[] {"Skincare", "Haircare", "Makeup", "Fragrances", "Wellness", "Personal Care"}},
                    {"Toys & Hobbies", new[] {"Board Games", "Puzzles", "Model Kits", "Crafts", "Collectibles", "Outdoor Toys"}},
                    {"Automotive", new[] {"Car Accessories", "Motorcycle Accessories", "Tools & Equipment", "Car Electronics", "Tires & Wheels", "Parts"}}
                };

            var subCategories = new List<SubCategory>();

            foreach (var category in subCategoryNames)
            {
                int categoryId = categoryIds[category.Key];
                foreach (var subCategoryName in category.Value)
                {
                    subCategories.Add(new SubCategory { Name = subCategoryName, CategoryId = categoryId, CreatedDate = DateTime.Now });
                }
            }
            context.SubCategories.AddRange(subCategories);
            context.SaveChanges();
        }

        private static void SeedAdverts(AppDbContext context)
        {
            var advertFaker = new Faker<Advert>()
                .RuleFor(a => a.Title, f => f.Commerce.ProductName())
                .RuleFor(a => a.Description, f => f.Commerce.ProductDescription())
                .RuleFor(a => a.Type, f => f.Random.Bool())
                .RuleFor(a => a.OnSale, f => f.Random.Bool())
                .RuleFor(a => a.Price, f => f.Random.Int(0, 1000000))
                .RuleFor(a => a.ClickCount, f => f.Random.Int(0, 200))
                .RuleFor(a => a.CreatedDate, f => f.Date.Past(1))
                .RuleFor(a => a.UserId, f => f.Random.Int(1, 10)); // Varsayım: 1 ile 10 arasında geçerli UserId'ler var

            var adverts = advertFaker.Generate(150);

            context.Adverts.AddRange(adverts);
            context.SaveChanges();
        }

        private static void SeedAdvertComments(AppDbContext context)
        {
            var advertCommentFaker = new Faker<AdvertComment>()
                .RuleFor(ac => ac.Comment, f => f.Rant.Review())
                .RuleFor(ac => ac.IsActive, f => f.Random.Bool())
                .RuleFor(ac => ac.AdvertId, f => f.Random.Int(1, 150)) // Varsayım: 1 ile 150 arasında geçerli AdvertId'ler var
                .RuleFor(ac => ac.UserId, f => f.Random.Int(1, 10)) // Varsayım: 1 ile 10 arasında geçerli UserId'ler var
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));

            var advertComments = advertCommentFaker.Generate(200);

            context.AdvertComments.AddRange(advertComments);
            context.SaveChanges();
        }

        private static void SeedAdvertImages(AppDbContext context) 
        {
            var advertImageFaker = new Faker<AdvertImage>()
                .RuleFor(ai => ai.AdvertId, f => f.Random.Int(1, 150)) // Varsayım: 1 ile 150 arasında geçerli AdvertId'ler var
                .RuleFor(ai => ai.AdvertImagePath, f => f.Image.Business()) // Rastgele bir resim URL'si
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));

            var advertImages = advertImageFaker.Generate(250);

            context.AdvertImages.AddRange(advertImages);
            context.SaveChanges();
        }

        private static void SeedCategoryAdverts(AppDbContext context)
        {
            var categoryAdvertFaker = new Faker<CategoryAdvert>()
                .RuleFor(ca => ca.CategoryId, f => f.Random.Int(1, 8)) // 1 ile 8 arasında geçerli CategoryId'ler
                .RuleFor(ca => ca.AdvertId, f => f.Random.Int(1, 150)) // 1 ile 150 arasında geçerli AdvertId'ler
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));


            var categoryAdverts = categoryAdvertFaker.Generate(300);

            context.CategoryAdverts.AddRange(categoryAdverts);
            context.SaveChanges();
        }

        private static void SeedSubCategoryAdverts(AppDbContext context)
        {
            var subCategoryAdvertFaker = new Faker<SubCategoryAdvert>()
                .RuleFor(sca => sca.SubCategoryId, f => f.Random.Int(1, 48)) // 1 ile 48 arasında geçerli SubCategoryId'ler
                .RuleFor(sca => sca.AdvertId, f => f.Random.Int(1, 150)) // 1 ile 150 arasında geçerli AdvertId'ler
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));

            var subCategoryAdverts = subCategoryAdvertFaker.Generate(300);

            context.SubCategoryAdverts.AddRange(subCategoryAdverts);
            context.SaveChanges();
        }


        private static void SeedAdvertRatings(AppDbContext context)
        {
            List<Dictionary<string, object>> advertRatings = new List<Dictionary<string, object>>();
            Random random = new Random();

            for (int userId = 1; userId <= 10; userId++)
            {
                for (int advertId = 1; advertId <= 150; advertId++)
                {
                    Dictionary<string, object> advertRating = new Dictionary<string, object>
                    {
                        ["UserId"] = userId,
                        ["AdvertId"] = advertId,
                        ["Rating"] = random.Next(1, 6),
                        ["CreatedDate"] = DateTime.Now.AddDays(-random.Next(1, 365))
                    };

                    advertRatings.Add(advertRating);
                }
            }

            foreach (var rating in advertRatings)
            {
                var advertRating = new AdvertRating
                {
                    UserId = (int)rating["UserId"],
                    AdvertId = (int)rating["AdvertId"],
                    Rating = (int)rating["Rating"],
                    CreatedDate = (DateTime)rating["CreatedDate"]
                };

                context.AdvertRatings.Add(advertRating);
            }

            context.SaveChanges();
        }


    }
}
