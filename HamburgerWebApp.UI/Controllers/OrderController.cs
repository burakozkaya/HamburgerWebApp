using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.DAL.Concrete;
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
        public async Task<ActionResult> Create(Order order, string selectedExtra)
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
                if (!string.IsNullOrEmpty(selectedExtra) && int.TryParse(selectedExtra, out int extraId))
                {
                    var selectedExtraa = await _extraService.GetById(extraId);

                    if (selectedExtra != null)
                    {
                        // Seçilen Extra'yı Order'ın Extras listesine ekleyin
                        order.Extras = new List<Extra> { selectedExtraa };
                    }
                }
                // order.Extras = _orderService.GetAll().Result.Where(e => order.Extras.Contains<Extra>(e.Id)).ToList();
                order.OrderPrice = _orderService.CalculateOrderTotal(order);
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

            //Order order=new Order({
            //    MenuId = menuList
            //}) 
            ViewBag.ExtrasList = Extra;
            ViewBag.OrderSizeList = orderSizeList;
            ViewBag.MenuList = menuList;

            var tempUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewBag.OrderSizeList = orderSizes;
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Order order, string selectedExtra)
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
                if (!string.IsNullOrEmpty(selectedExtra) && int.TryParse(selectedExtra, out int extraId))
                {
                    var selectedExtraa = await _extraService.GetById(extraId);

                    if (selectedExtra != null)
                    {
                        // Seçilen Extra'yı Order'ın Extras listesine ekleyin
                        order.Extras = new List<Extra> { selectedExtraa };
                    }
                }
                // order.Extras = _orderService.GetAll().Result.Where(e => order.Extras.Contains<Extra>(e.Id)).ToList();
                order.OrderPrice = _orderService.CalculateOrderTotal(order);
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

   
        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = _orderService.GetById(id).Result;
            await _orderService.Delete(entity);
            return RedirectToAction("Index");
        }
    }
}
