namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Services.CategoryServices;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.OrderServices;
    using MyAutoPartsStore.Services.ProductServices;

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
        public IActionResult Cart() => View();
    }
}
