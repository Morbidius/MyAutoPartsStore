namespace MyAutoPartsWebStore.Web.Controllers
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using MyAutoPartsStore.Services.CategoryServices;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsWebStore.Web.Infrastructure.Extentions;
    using MyAutoPartsWebStore.Web.Models.Products;

    using static WebConstants;

    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly IProductService products;
        private readonly ICategoryService category;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public ProductController(IProductService products, ICategoryService category, IDealerService dealers, IMapper mapper)
        {
            this.products = products;
            this.category = category;
            this.dealers = dealers;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealerController.BecomeDealer), typeof(DealerController).GetControllerName());
            }

            return View(new ProductFormModel()
            {
                Categories = this.category.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            var dealerId = this.dealers.GetDealerById(this.User.GetId());

            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId))
            {
                return RedirectToAction(nameof(DealerController.BecomeDealer), typeof(DealerController).GetControllerName());
            }

            if (!this.category.CategoryЕxists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.category.AllCategories();

                return View(product);
            };

            this.products.Create(
                product.Name,
                product.Description,
                product.Price,
                product.SizeCapacity,
                product.Weight,
                product.ImageUrl,
                product.CategoryId,
                dealerId);

            TempData[GlobalMessageKey] = "Product added successfully and is awaiting admin approval.";

            return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
        }

        public IActionResult Delete(int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            var productToDel = this.products.GetProductById(id);

            if (productToDel == null) return NotFound();

            var viewModel = new ProductServiceDeleteModel()
            {
                Id = productToDel.Id,
                Name = productToDel.Name,
                Image = productToDel.ImageUrl,
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(ProductServiceDeleteModel viewProduct, int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(viewProduct);

            var product = this.products.GetProductById(id);

            if (product == null) return NotFound();

            this.products.Delete(id);

            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId))
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }
            else
            {
                TempData[GlobalMessageKey] = "Product deleted successfully.";

                return RedirectToAction(nameof(ProductController.MyProducts));
            }
        }

        public IActionResult Description(int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            var details = this.products.Details(id);

            if (details == null) return NotFound();

            return View(details);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealerController.BecomeDealer), typeof(DealerController).GetControllerName());
            }

            var product = this.products.Details(id);

            if (product.DealerUserId != this.User.GetId() && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var productForm = this.mapper.Map<ProductFormModel>(product);
            productForm.Categories = this.category.AllCategories();

            return View(productForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            var dealerId = this.dealers.GetDealerById(this.User.GetId());

            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealerController.BecomeDealer), typeof(DealerController).GetControllerName());
            }

            if (!this.category.CategoryЕxists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.category.AllCategories();

                return View(product);
            };

            if (!this.products.isByDealer(id, dealerId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var productIsEdited = this.products.Edit(
                id,
                product.Name,
                product.Description,
                product.Price,
                product.SizeCapacity,
                product.Weight,
                product.ImageUrl,
                product.CategoryId);

            if (User.IsAdmin())
            {
                TempData[GlobalMessageKey] = "Product edited successfully.";

                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            TempData[GlobalMessageKey] = "Product edited successfully.";

            return RedirectToAction(nameof(ProductController.MyProducts));
        }

        [Authorize]
        public IActionResult MyProducts()
        {
            var products = this.products
                .ProductByUser(this.User.GetId())
                .Where(p => p.IsAllowed == true);

            return View(products);
        }
    }
}
