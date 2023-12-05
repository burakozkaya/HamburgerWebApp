using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HamburgerWebApp.UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderSizeRepository _orderSizeRepository;
        private readonly IMenuService _menuService;
        private readonly IExtraService _extraService;
        private readonly IOrderSizeService _orderSizeService;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderService orderService, IOrderSizeRepository orderSizeRepository, IMenuService menuService, IExtraService extraService, IOrderSizeService orderSizeService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _orderSizeRepository = orderSizeRepository;
            _menuService = menuService;
            _extraService = extraService;
            _orderSizeService = orderSizeService;
            _userManager = userManager;
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

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public async Task<ActionResult> Create()
        {
            var menus=await _menuService.GetAll();
            var orderSize = await _orderSizeService.GetAll();
            var orders = await _orderService.GetAll();
            var extra=await _extraService.GetAll();

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
            var Extra = menus.Select(order => new SelectListItem
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
        public async Task<ActionResult> Create(Order order)
        {
            // Code for creating a new record in the database
            order.AppUserId=User.FindFirstValue(claimType:ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                //order.Extras = _orderService.GetAll().Result.Where(e => order.Extras.Contains<Extra>(e.Id)).ToList();
                order.OrderPrice = _orderService.CalculateOrderTotal(order);
                await _orderService.Add(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Update(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
