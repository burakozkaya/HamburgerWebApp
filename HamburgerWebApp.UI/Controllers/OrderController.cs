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

            var orderSizeList = orderSize.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Size
            }).Distinct();

            var menuList = menus.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();
            var Extra = extra.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();

            //Order order=new Order({
            //    MenuId = menuList
            //}) 
            ViewBag.ExtrasList = Extra;
            ViewBag.OrderSizeList = orderSizeList;
            ViewBag.MenuList = menuList;

            var tempUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewBag.OrderSizeList = orderSizes;
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order, string[] selectedExtra)
        {
            ModelState.Remove("OrderSize");
            ModelState.Remove("Menu");
            ModelState.Remove("AppUser");
            ModelState.Remove("Extras");
            ModelState.Remove("AppUserId");

            // Code for creating a new record in the database
            if (ModelState.IsValid)
            {
                order.OrderPrice = await _orderService.CalculateOrderTotal(order, selectedExtra);
                await _orderService.Add(order);
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
            var orders = await _orderService.GetAll();
            var extra = await _extraService.GetAll();

            var orderSizeList = orderSize.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Size
            }).Distinct();

            var menuList = menus.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();
            var Extra = extra.Select(order => new SelectListItem
            {
                Value = order.Id.ToString(),
                Text = order.Name
            }).Distinct();

            ViewBag.ExtrasList = Extra;
            ViewBag.OrderSizeList = orderSizeList;
            ViewBag.MenuList = menuList;

            var tempUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Order order, string[] selectedExtra)
        {

            order.AppUserId = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            ModelState.Remove("OrderSize");
            ModelState.Remove("Menu");
            ModelState.Remove("AppUser");
            ModelState.Remove("Extras");
            ModelState.Remove("AppUserId");
            // Code for creating a new record in the database
            if (ModelState.IsValid)
            {
                if (selectedExtra == null)
                {
                    selectedExtra = new string[0];
                }
                order.OrderPrice = await _orderService.CalculateOrderTotal(order, selectedExtra);
                await _orderService.Update(order);
                return RedirectToAction(nameof(Index));
            }

            var temp = ModelState.ErrorCount;
            var tempx = ModelState.Values;
            if (ModelState.IsValid)
            {
                await _orderService.Update(order);
                return RedirectToAction("Index");
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
