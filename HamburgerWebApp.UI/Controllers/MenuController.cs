using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerWebApp.UI.Controllers
{
    [Authorize(Roles = "Admin")]

    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: MenuController
        public async Task<ActionResult> Index()
        {
            var menuList = await _menuService.GetAll();
            return View(menuList);
        }

        // GET: MenuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _menuService.Add(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // POST: MenuController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var menu = await _menuService.GetById(id);
            return View(menu);
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _menuService.Update(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: MenuController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var menu = await _menuService.GetById(id);
            await _menuService.Delete(menu);
            return RedirectToAction("Index", "Menu");
        }
    }
}
