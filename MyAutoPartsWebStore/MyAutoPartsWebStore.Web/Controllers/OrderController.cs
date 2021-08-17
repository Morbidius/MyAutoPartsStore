namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Models.ServiceModels.Orders;
    using MyAutoPartsStore.Services.CategoryServices;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.OrderServices;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsWebStore.Web.Infrastructure.Extentions;

    using static WebConstants;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductService products;
        private readonly ICategoryService category;
        private readonly IOrderService orders;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public OrderController(IProductService products, ICategoryService category, IOrderService orders, IDealerService dealers, IMapper mapper)
        {
            this.products = products;
            this.category = category;
            this.orders = orders;
            this.dealers = dealers;
            this.mapper = mapper;
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

            return Json(new { error = !isDeleted , productCount = orders.GetUserShoppingCartProductsCount(userId) });
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult Checkout(DealerOrderFormServiceModel order)
        {
            var dealerId = this.dealers.GetDealerById(this.User.GetId());

            var userId = this.User.GetId();

            //this.orders.CheckoutFormToDealer(
            //    order.BuyerEmail,
            //    order.BuyerAddress,
            //    order.BuyerPhone,
            //    order.Note,
            //    order.UserId,
            //    order.OrderedOn,
            //    dealerId);

            TempData[GlobalMessageKey] = "Purchase Successfull!";

            return RedirectToAction("Index", "Home");
        }
    }
}
