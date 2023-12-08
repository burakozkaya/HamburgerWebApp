using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            if (User.IsInRole("Admin"))
            {
                var extraList = await _extraService.GetAll();
                ViewBag.Massage = extraList.Message;
                if(extraList.IsSuccess)
                    return View(extraList.Data);
                return View(extraList);
            }
            else
            {
                var extraList = await _extraService.GetAll();
                if (extraList.IsSuccess)
                    return View(extraList.Data);
                return View();
            }
           
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
                var response=await _extraService.Add(extra);
                ViewBag.Message = response.Message;
                if (response.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            return View(extra);
        }

        // GET: ExtraController/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            var extra = await _extraService.GetById(id);
            ViewBag.Message = extra.Message;
            if (extra.IsSuccess)
            {
                var tempUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(extra.Data);
            }
                return View();
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
               var response= await _extraService.Update(extra);
                if(response.IsSuccess)
                    
                return RedirectToAction("Index", "Extra");
                ViewBag.Message=response.Message;
            }
            return View(extra);
        }

        // GET: ExtraController/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            var entity = _extraService.GetById(id).Result;
            if(entity.IsSuccess)
            {
                await _extraService.Delete(entity.Data);
            }
            ViewBag.Message = entity.Message;
            return RedirectToAction("Index", "Extra");
        }

    }
}
