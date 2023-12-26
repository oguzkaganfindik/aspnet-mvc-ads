//using Ads.Persistence.Contexts;
//using Ads.Persistence.DataContext;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ads.Persistence.Initializer
//{
//    public class DbInitializer : IDbInitializer
//    {
//        private readonly AppDbContext _context;
//        private readonly ILogger<DbInitializer> _logger;

//        public DbInitializer(AppDbContext context, ILogger<DbInitializer> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        public void Initialize()
//        {
//            try
//            {
//                if (_context.Database.EnsureCreated())
//                {
//                    DbSeeder.SeedData(_context);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Veritabanı başlangıç verisi yüklenirken hata oluştu.");
//            }
//        }
//    }
//}