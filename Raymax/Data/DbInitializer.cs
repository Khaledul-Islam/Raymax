using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Raymax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raymax.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                
            }
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@raymax.com",
                Email = "admin@raymax.com",
                FirstName = "SH",
                LastName = "Shuvo",
                EmailConfirmed = true,
                PhoneNumber = "017174*3735",
            }, "Admin1234*").GetAwaiter().GetResult();

        }

    }
}
