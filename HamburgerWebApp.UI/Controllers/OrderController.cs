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

        public OrderController(IOrderService orderService, IOrderSizeRepository orderSizeRepository)
        {
            _orderService = orderService;
            _orderSizeRepository = orderSizeRepository;
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
            var orderSizes = await _orderService.GetAll().Select(os => new SelectListItem
            {
                Value = os.Id.ToString(),
                Text = os.Size
            });
            var menus = await _orderService.GetAll().Select(menu => new SelectListItem
            {
                Value = menu.Id.ToString(),
                Text = menu.Name
            });
            ViewBag.MenuList = menus;

            var appUsers = await _orderService.GetAll().Select(user => new SelectListItem
            {
                Value = user.Id,
                Text = user.UserName
            });
            ViewBag.AppUserList = appUsers;

            //ViewBag.OrderSizeList = orderSizes;
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order)
        {
            // Code for creating a new record in the database
           
            if (ModelState.IsValid)
            {
                order.Extras = _orderService.GetAll().Where(e => order.SelectedExtras.Contains(e.Id)).ToList();
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
        public ActionResult Edit(int id, IFormCollection collection)
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
