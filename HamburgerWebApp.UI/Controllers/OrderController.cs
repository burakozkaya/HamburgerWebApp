using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HamburgerWebApp.UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;
        private readonly IExtraService _extraService;
        private readonly IOrderSizeService _orderSizeService;

        public OrderController(IOrderService orderService, IMenuService menuService, IExtraService extraService, IOrderSizeService orderSizeService)
        {
            _orderService = orderService;
            _menuService = menuService;
            _extraService = extraService;
            _orderSizeService = orderSizeService;
        }


        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var orderList = await _orderService.GetAll();
                return View(orderList);
            }
            else
            {
                var orderList = await _orderService.GetAll(order => order.AppUserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(orderList);
            }
        }


        // GET: OrderController/Create
        public async Task<ActionResult> Create()
        {
            var menus = await _menuService.GetAll();
            var orderSize = await _orderSizeService.GetAll();
            var extra = await _extraService.GetAll();

            ViewBag.OrderSizeList = orderSize.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Size
            }).Distinct();

            ViewBag.MenuList = menus.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();
            ViewBag.ExtrasList = extra.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order, string[] selectedExtra)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Add(order, selectedExtra);
                return RedirectToAction(nameof(Index));
            }

            var temp = ModelState.ErrorCount;
            var tempx = ModelState.Values;
            return View(order);
        }

        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var order = await _orderService.GetById(id);

            var menus = await _menuService.GetAll();
            var orderSize = await _orderSizeService.GetAll();
            var extra = await _extraService.GetAll();

            ViewBag.OrderSizeList = orderSize.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Size
            }).Distinct();

            ViewBag.MenuList = menus.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();
            ViewBag.ExtrasList = extra.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();

            var tempUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Order order, string[] selectedExtra)
        {

            order.AppUserId = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                await _orderService.Update(order, selectedExtra);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        // GET: OrderController/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var entity = await _orderService.GetById(id);
            if (entity != null)
            {
                await _orderService.Delete(entity);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
