namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Models.ServiceModels.Orders;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.OrderServices;
    using MyAutoPartsStore.Services.UserService;
    using MyAutoPartsWebStore.Web.Infrastructure.Extentions;
    using System.Linq;
    using System.Threading.Tasks;

    using static WebConstants;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orders;
        private readonly IUserService userService;
        private readonly IDealerService dealers;

        public OrderController(IOrderService orders, IUserService userService, IDealerService dealers)
        {
            this.orders = orders;
            this.userService = userService;
            this.dealers = dealers;
        }

        [Authorize]
        public IActionResult Cart()
        {
            var userId = this.User.GetId();

            var cart = this.orders.GetCart(userId);

            return View(cart);
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult AddToCart(int? productId = null)
        {
            if (productId == null || productId <= 0) return BadRequest();

            var userId = this.User.GetId();

            var isAdded = this.orders.AddProductToUserCart(userId, productId);

            if (!isAdded)
            {
                return Json(new { error = true, productCount = 0 });
            }

            var productCount = this.orders.GetUserShoppingCartProductsCount(userId);

            return Json(new { error = false, productCount });
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult DeleteFromCart(int? productId = null)
        {
            var userId = this.User.GetId();

            var isDeleted = this.orders.RemoveFromCart(productId, userId);

            return Json(new { error = !isDeleted, productCount = orders.GetUserShoppingCartProductsCount(userId) });
        }

        public IActionResult Checkout()
        {
            var userId = this.User.GetId();

            var orderModel = new DealerOrderFormServiceModel
            {
                Email = this.userService.GetUserEmailById(userId)
            };

            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(DealerOrderFormServiceModel order)
        {
            var userId = this.User.GetId();

            await this.orders.CreateOrder(order, userId);

            TempData[GlobalMessageKey] = "Purchase Successfull!";

            return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult SaveCart(int productId, int productQuantity)
        {
            var userId = this.User.GetId();

            var isSaved = this.orders.SaveCart(userId, productId, productQuantity);

            return Json(new { error = !isSaved });
        }

        public IActionResult DealerOrders()
        {
            var userId = this.User.GetId();

            var dealerId = this.dealers.GetDealerById(userId);

            var viewModel = this.orders
                .GetOrderedItemsFromDealer<BasicOrderInformation>(dealerId)
                .Distinct()
                .ToList();

            return View(viewModel);
        }

        public IActionResult OrderDetails(int orderId)
        {
            var userId = this.User.GetId();

            var dealerId = this.dealers.GetDealerById(userId);

            if (dealerId <= 0) return BadRequest();

            var viewModel = this.orders.GetOrderDetails(orderId);

            var products = this.orders.GetOrderProducts(orderId);

            viewModel.Products = products;

            return View(viewModel);
        }
    }
}
