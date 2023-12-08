using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            if (User.IsInRole("Admin"))
            {
                var menuList = await _menuService.GetAll();
                ViewBag.Message = menuList.Message;
                if (menuList.IsSuccess)
                    return View(menuList.Data);
                return View();
            }

            else
            {
                var manulist=await _menuService.GetAll();
                ViewBag.Message = manulist.Message;
                if (manulist.IsSuccess)
                    return View(manulist.Data);
                return View();
            }
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
            ViewBag.Message = menu.Message;
            if (menu.IsSuccess)
            {
                var tempUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(menu.Data);
            }
            return View();
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var response = await _menuService.Update(menu);
                if (response.IsSuccess)
                    return RedirectToAction("Index");
                ViewBag.Message = response.Message;
            }
            return View(menu);
        }

        // GET: MenuController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var menu = await _menuService.GetById(id);
            if (menu.IsSuccess)
                await _menuService.Delete(menu.Data);
            ViewBag.Message = menu.Message;
            return RedirectToAction("Index", "Menu");
        }
    }
}
