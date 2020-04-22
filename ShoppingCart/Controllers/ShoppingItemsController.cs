using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class ShoppingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingItemsController (ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ShoppingItems
        public async Task<ActionResult> Index()
        {
            // getting the current user
            var user = await GetCurrentUserAsync();

            // filtering items so we only see our own and not other users
            var items = await _context.ShoppingItem
                .Where(si => si.ApplicationUserId == user.Id)
                .ToListAsync();

            return View(items);
        }

        // GET: ShoppingItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Shoppingitem shoppingItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                shoppingItem.ApplicationUserId = user.Id;

                _context.ShoppingItem.Add(shoppingItem);
               await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingItems/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _context.ShoppingItem.FirstOrDefaultAsync(si => si.Id == id);
            var loggedInUser = await GetCurrentUserAsync();

            if (item.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View();
        }

        // POST: ShoppingItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Shoppingitem shoppingitem)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                shoppingitem.ApplicationUserId = user.Id;

                _context.ShoppingItem.Update(shoppingitem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}