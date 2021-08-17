namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Services.CategoryServices;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.OrderServices;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsWebStore.Web.Infrastructure.Extentions;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductService products;
        private readonly ICategoryService category;
        private readonly IOrderService order;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public OrderController(IProductService products, ICategoryService category, IOrderService order, IDealerService dealers, IMapper mapper)
        {
            this.products = products;
            this.category = category;
            this.order = order;
            this.dealers = dealers;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Cart(int? id = null)
        {
            //if (id == null || id <= 0) return BadRequest();

            var cart = this.order.getCart(id);

            return View(cart);
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult AddToCart(int? productId = null)
        {
            if (productId == null || productId <= 0) return BadRequest();

            var userId = User.GetId();

            var isAdded = order.AddProductToUserCart(userId, productId);

            if (!isAdded) return Json(new { error = true, productCount = 0 });

            var productCount = order.GetUserShoppingCartProductsCount(userId);

            return Json(new { error = false, productCount });
        }
    }
}
