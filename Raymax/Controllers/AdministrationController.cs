using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raymax.Data;
using Raymax.Models;

namespace Raymax.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var FindID = await _userManager.FindByIdAsync(id);

            if(FindID==null)
            {
                return NotFound();
            }

            return View(FindID);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var objFromDb = await _userManager.FindByIdAsync(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.DeleteAsync(objFromDb);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Administration");
            }
            else
            {
                return null;
            }

        }

        public async Task<IActionResult> Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUser = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (identityUser == null)
            {
                return NotFound();
            }
            identityUser.LockoutEnd = DateTime.Now.AddYears(1000);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnLock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var identityUser = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (identityUser == null)
            {
                return NotFound();
            }

            //applicationUser.LockoutEnd = DateTime.Now;
            identityUser.LockoutEnd = DateTime.Now;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        #region API CALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Json(new
            {
                data = await _userManager.Users.ToListAsync()
            });
        }
        #endregion
    }
}
