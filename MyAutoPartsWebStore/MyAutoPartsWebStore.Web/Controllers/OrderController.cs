namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Orders;
    using MyAutoPartsStore.Services.CategoryServices;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.OrderServices;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsStore.Services.UserService;
    using MyAutoPartsWebStore.Web.Infrastructure.Extentions;
    using System.Threading.Tasks;
    using static WebConstants;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orders;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public OrderController(IOrderService orders, IDealerService dealers, IMapper mapper, IUserService userService)
        {
            this.orders = orders;
            this.dealers = dealers;
            this.mapper = mapper;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Cart()
        {
            var userId = User.GetId();

            var cart = this.orders.GetCart(userId);

            return View(cart);
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult AddToCart(int? productId = null)
        {
            if (productId == null || productId <= 0) return BadRequest();

            var userId = User.GetId();

            var isAdded = orders.AddProductToUserCart(userId, productId);

            if (!isAdded) return Json(new { error = true, productCount = 0 });

            var productCount = orders.GetUserShoppingCartProductsCount(userId);

            return Json(new { error = false, productCount });
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult DeleteFromCart(int? productId = null)
        {
            var userId = User.GetId();

            var isDeleted = this.orders.RemoveFromCart(productId, userId);

            return Json(new { error = !isDeleted, productCount = orders.GetUserShoppingCartProductsCount(userId) });
        }

        public IActionResult Checkout()
        {
            var userId = User.GetId();

            var orderModel = new DealerOrderFormServiceModel
            {
                Email = userService.GetUserEmailById(userId)
            };

            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(DealerOrderFormServiceModel order)
        {
            var userId = User.GetId();
            
            await orders.CreateOrder(order, userId);

            TempData[GlobalMessageKey] = "Purchase Successfull!";

            return RedirectToAction("Index", "Home");
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult SaveCart(int productId, int productQuantity)
        {
            var userId = this.User.GetId();

            var isSaved = orders.SaveCart(userId, productId, productQuantity);

            return Json(new { error = !isSaved });
        }
    }
}
