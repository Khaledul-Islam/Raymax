using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raymax.Data;
using Raymax.Models;
using Raymax.Models.ViewModels;

namespace Raymax.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InvoiceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var List = await _db.Invoices.Include(a => a.Products).ToListAsync();
            return View(List);
        }

        //create get
        public IActionResult Create()
        {
            return View();
        }

        //CreatePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //edit get
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EditID = await _db.Invoices.Include(m => m.Products).FirstOrDefaultAsync(m => m.ID == id);

            if (EditID == null)
            {
                return NotFound();
            }

            EditInvoiceViewModel model = new EditInvoiceViewModel()
            {
                Invoice = EditID
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditInvoiceViewModel EIVM)
        {
            if (ModelState.IsValid)
            {
                var subCatFromDb = await _db.Invoices.Include(m => m.Products).FirstOrDefaultAsync(m => m.ID == id);
                subCatFromDb.CustomerName = EIVM.Invoice.CustomerName;
                subCatFromDb.Address = EIVM.Invoice.Address;
                subCatFromDb.Contact = EIVM.Invoice.Contact;
                subCatFromDb.Date_Created = EIVM.Invoice.Date_Created;
                
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            EditInvoiceViewModel modelVM = new EditInvoiceViewModel()
            {
                Invoice = EIVM.Invoice
            };
            return View(modelVM);
        }



        #region API CALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new
            {
                data = await _db.Invoices.Include(a => a.Products).ToListAsync()
            });
        }

        [HttpDelete]
        public IActionResult DeleteInvoice(int id)
        {
            var objFromDb = _db.Invoices.SingleOrDefault(a => a.ID == id);
            if (objFromDb == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting."
                });
            }

            _db.Invoices.Remove(objFromDb);
            _db.SaveChanges();
            return Json(new
            {
                success = true,
                message = "Deleted Successfully."
            });

        }
        #endregion

        public IActionResult Print(int? id)
        {
            PrintViewModel model = new PrintViewModel()
            {
                Invoice = _db.Invoices.Include(a => a.Products).FirstOrDefault(a => a.ID == id),
                Terms = _db.Terms.ToList(),
            };

            return View(model);
        }

    }
}