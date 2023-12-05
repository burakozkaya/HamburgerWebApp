using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerWebApp.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExtraController : Controller
    {
        private readonly IExtraService _extraService;

        public ExtraController(IExtraService extraService)
        {
            _extraService = extraService;
        }
        // GET: ExtraController
        public async Task<ActionResult> Index()
        {
            var extraList = await _extraService.GetAll();
            return View(extraList);
        }

        // GET: ExtraController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExtraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Extra extra)
        {
            // Code for creating a new record in the database
            ModelState.Remove("Orders");
            if (ModelState.IsValid)
            {
                await _extraService.Add(extra);
                return RedirectToAction(nameof(Index));
            }
            return View(extra);
        }

        // GET: ExtraController/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            var extra = await _extraService.GetById(id);
            return View(extra);
        }

        // POST: ExtraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(Extra extra)
        {
            // Code for updating an existing record in the database
            ModelState.Remove("Orders");
            if (ModelState.IsValid)
            {
                await _extraService.Update(extra);
                return RedirectToAction("Index", "Extra");
            }
            return View(extra);
        }

        // GET: ExtraController/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            var entity = _extraService.GetById(id).Result;
            await _extraService.Delete(entity);
            return RedirectToAction("Index", "Extra");
        }

    }
}
