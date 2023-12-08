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
                ViewBag.Message = orderList.Message;
                if (orderList.IsSuccess)
                    return View(orderList.Data);
                return View();
            }
            else
            {
                var orderList = await _orderService.GetAll(order => order.AppUserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
                ViewBag.Message = orderList.Message;
                if (orderList.IsSuccess)
                    return View(orderList.Data);
                return View();
            }
        }


        // GET: OrderController/Create
        public async Task<ActionResult> Create()
        {

            await Filler();
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order, string[] selectedExtra)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.Add(order, selectedExtra);
                ViewBag.Message = response.Message;
                if (response.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            await Filler();
            return View(order);
        }

        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var order = await _orderService.GetById(id);
            ViewBag.Message = order.Message;
            if (order.IsSuccess)
            {
                await Filler();

                var tempUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(order.Data);
            }

            return View();
        }

        private async Task Filler()
        {
            var menus = await _menuService.GetAll();
            var orderSize = await _orderSizeService.GetAll();
            var extra = await _extraService.GetAll();

            ViewBag.OrderSizeList = orderSize.Data.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Size
            }).Distinct();

            ViewBag.MenuList = menus.Data.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();
            ViewBag.ExtrasList = extra.Data.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Order order, string[] selectedExtra)
        {

            if (ModelState.IsValid)
            {
                var response = await _orderService.Update(order, selectedExtra);
                if (response.IsSuccess)
                    return RedirectToAction(nameof(Index));
                ViewBag.Message = response.Message;
            }
            await Filler();
            return View(order);
        }


        // GET: OrderController/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var entity = await _orderService.GetById(id);
            if (entity.IsSuccess)
                await _orderService.Delete(entity.Data);
            ViewBag.Message = entity.Message;
            return RedirectToAction(nameof(Index));
        }

    }
}
