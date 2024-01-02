using Ads.Domain.Entities.Concrete;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ads.Persistence.Contexts
{
    public static class DbSeeder
    {
        public static void SeedData(AppDbContext context)
        {

            context.Database.Migrate();

            if (!context.Settings.Any())
            {
                SeedSettings(context);
            }

            if (!context.Pages.Any())
            {
                SeedPages(context);
            }

            SeedUsers(context);

            //if (!context.Roles.Any())
            //{
            //    SeedRoles(context);
            //}

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
        private static void SeedSettings(AppDbContext context)
        {
            // Genel Setting nesneleri oluşturuluyor.
            var userThemeSettingOn = new Setting { Key = "Make the Theme White", Value = "True", CreatedDate = DateTime.Now };
            var userThemeSettingOff = new Setting { Key = "Make the Theme White", Value = "False", CreatedDate = DateTime.Now };
            var pageVisibilitySettingOn = new Setting { Key = "Make the Page Visible", Value = "True", CreatedDate = DateTime.Now };
            var pageVisibilitySettingOff = new Setting { Key = "Make the Page Visible", Value = "False", CreatedDate = DateTime.Now };

            context.Settings.AddRange(userThemeSettingOn, userThemeSettingOff, pageVisibilitySettingOn, pageVisibilitySettingOff);
            context.SaveChanges();
        }

        private static void SeedPages(AppDbContext context)
        {
            // Öncelikle gerekli Setting nesnelerini oluşturun veya var olanları kullanın
            var pageVisibilitySetting = context.Settings.FirstOrDefault(s => s.Key == "Make the Page Visible" && s.Value == "True");


            // Sayfalar oluşturuluyor.
            var pages = new List<Page>
            {
                new Page
                {
                    Name = "Blog Page",
                    Title1 = "Introduction",
                    Title2 = "How we can help",
                    Content1 = "Lorem ipsum dolor sit amet, consectetu adipiscing  elit.",
                    Content2 = "Vestibulum vel dolor sit amet risus efficitur faucibus.",
                    PageImagePath = "~/dist/images/blog.jpg",
                    CreatedDate = DateTime.Now,
                    SettingId = pageVisibilitySetting.Id // SettingId atanıyor
                },
                new Page
                {
                    Name = "About Us",
                    Title1 = "Our History",
                    Title2 = "Our Mission",
                    Content1 = "Here is some content about our history.",
                    Content2 = "Here is some content about our mission.",
                    PageImagePath = "~/dist/images/about-us.jpg",
                    CreatedDate = DateTime.Now,
                    SettingId = pageVisibilitySetting.Id // SettingId atanıyor
                },
                new Page
                {
                    Name = "Contact Us",
                    Title1 = "İletişim Başlığı 1",
                    Title2 = "İletişim Başlığı 2",
                    Content1 = "İletişim içeriği 1 buraya yazılacak.",
                    Content2 = "İletişim içeriği 2 buraya yazılacak.",
                    PageImagePath = "~/dist/images/about/about.jpg",
                    CreatedDate = DateTime.Now,
                    SettingId = pageVisibilitySetting.Id // SettingId atanıyor
                },
            };

            context.Pages.AddRange(pages);
            context.SaveChanges();
        }


        //private static void SeedRoles(AppDbContext context)
        //{

        //    var roleNames = new List<string>
        //    {
        //        "Admin",
        //        "Moderator",
        //        "User"
        //    };

        //    var roles = roleNames.Select(roleName => new AppRole
        //    {
        //        Name = roleName
        //    }).ToList();

        //    context.Roles.AddRange(roles);
        //    context.SaveChanges();
        //}

        private static void SeedUsers(AppDbContext context)
        {
            var userRoleId = context.Roles.FirstOrDefault(r => r.Name == "User")?.Id;
            if (userRoleId == null) return;

            // "Make the Theme White" adında ve "Value"su "True" olan bir Setting alınıyor.
            var userThemeSetting = context.Settings.FirstOrDefault(s => s.Key == "Make the Theme White" && s.Value == "True");

            var passwordHasher = new PasswordHasher<AppUser>();
            var userFaker = new Faker<AppUser>()
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, (f, u) => new PasswordHasher<AppUser>().HashPassword(u, f.Internet.Password()))
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.EmailConfirmed, true)
                .RuleFor(u => u.IsActive, f => f.Random.Bool())
                .RuleFor(u => u.CreatedDate, f => f.Date.Past(1))
                .RuleFor(u => u.UserImagePath, f => f.Internet.Avatar())
                .RuleFor(u => u.RoleId, userRoleId)
                .RuleFor(u => u.SettingId, userThemeSetting.Id); // SettingId atanıyor

            var ekKullanicilar = userFaker.Generate(5);

            foreach (var user in ekKullanicilar)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    context.Users.Add(user);
                }
            }

            context.SaveChanges(); // Tüm kullanıcılar eklendikten sonra SaveChanges çağırılır.
        }



        private static void SeedCategories(AppDbContext context)
        {
            var mainCategories = new List<Category>
            {
                new Category { Name = "Electronics", Description = "Electronic devices and gadgets", CategoryIcon = "fa fa-laptop icon-bg-1", CreatedDate = DateTime.Now },
                new Category { Name = "Food", Description = "Food and beverages", CategoryIcon = "fa fa-apple icon-bg-2", CreatedDate = DateTime.Now },
                new Category { Name = "Fashion", Description = "Clothing, footwear, and accessories", CategoryIcon = "fa fa-tshirt icon-bg-4", CreatedDate = DateTime.Now },
                new Category { Name = "Home & Garden", Description = "Home improvement, decor, and gardening products", CategoryIcon = "fa fa-home icon-bg-3", CreatedDate = DateTime.Now },
                new Category { Name = "Sports & Outdoors", Description = "Sports equipment and outdoor gear", CategoryIcon = "fa fa-ball icon-bg-5", CreatedDate = DateTime.Now },
                new Category { Name = "Beauty & Health", Description = "Beauty products and health care", CategoryIcon = "fa fa-health icon-bg-8", CreatedDate = DateTime.Now },
                new Category { Name = "Toys & Hobbies", Description = "Toys, games, and hobby essentials", CategoryIcon = "fa fa-toy icon-bg-7", CreatedDate = DateTime.Now },
                new Category { Name = "Automotive", Description = "Automotive parts and accessories", CategoryIcon = "fa fa-car icon-bg-6", CreatedDate = DateTime.Now }
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
                .RuleFor(a => a.UserId, f => f.Random.Int(3, 7)); // Varsayım: 3 ile 7 arasında geçerli UserId'ler var

            var adverts = advertFaker.Generate(50);

            context.Adverts.AddRange(adverts);
            context.SaveChanges();
        }

        private static void SeedAdvertComments(AppDbContext context)
        {
            var advertCommentFaker = new Faker<AdvertComment>()
                .RuleFor(ac => ac.Comment, f => f.Rant.Review())
                .RuleFor(ac => ac.IsActive, f => f.Random.Bool())
                .RuleFor(ac => ac.AdvertId, f => f.Random.Int(1, 50)) // Varsayım: 1 ile 50 arasında geçerli AdvertId'ler var
                .RuleFor(a => a.UserId, f => f.Random.Int(3, 7)) // Varsayım: 3 ile 7 arasında geçerli UserId'ler var
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));

            var advertComments = advertCommentFaker.Generate(50);

            context.AdvertComments.AddRange(advertComments);
            context.SaveChanges();
        }

        private static void SeedAdvertImages(AppDbContext context)
        {
            var advertImageFaker = new Faker<AdvertImage>()
                .RuleFor(ai => ai.AdvertId, f => f.Random.Int(1, 50)) // Varsayım: 1 ile 50 arasında geçerli AdvertId'ler var
                .RuleFor(ai => ai.AdvertImagePath, f => f.Image.PicsumUrl())
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));

            var advertImages = advertImageFaker.Generate(50);

            context.AdvertImages.AddRange(advertImages);
            context.SaveChanges();
        }

        private static void SeedCategoryAdverts(AppDbContext context)
        {
            var categoryAdvertFaker = new Faker<CategoryAdvert>()
                .RuleFor(ca => ca.CategoryId, f => f.Random.Int(1, 8)) // 1 ile 8 arasında geçerli CategoryId'ler
                .RuleFor(ca => ca.AdvertId, f => f.Random.Int(1, 50)) // 1 ile 50 arasında geçerli AdvertId'ler
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));


            var categoryAdverts = categoryAdvertFaker.Generate(50);

            context.CategoryAdverts.AddRange(categoryAdverts);
            context.SaveChanges();
        }

        private static void SeedSubCategoryAdverts(AppDbContext context)
        {
            var subCategoryAdvertFaker = new Faker<SubCategoryAdvert>()
                .RuleFor(sca => sca.SubCategoryId, f => f.Random.Int(1, 48)) // 1 ile 48 arasında geçerli SubCategoryId'ler
                .RuleFor(sca => sca.AdvertId, f => f.Random.Int(1, 50)) // 1 ile 50 arasında geçerli AdvertId'ler
                .RuleFor(ac => ac.CreatedDate, f => f.Date.Past(1));

            var subCategoryAdverts = subCategoryAdvertFaker.Generate(50);

            context.SubCategoryAdverts.AddRange(subCategoryAdverts);
            context.SaveChanges();
        }


        private static void SeedAdvertRatings(AppDbContext context)
        {
            List<Dictionary<string, object>> advertRatings = new List<Dictionary<string, object>>();
            Random random = new Random();

            for (int userId = 3; userId <= 7; userId++)
            {
                for (int advertId = 1; advertId <= 50; advertId++)
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


