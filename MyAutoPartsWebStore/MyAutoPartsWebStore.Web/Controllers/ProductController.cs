namespace MyAutoPartsWebStore.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsWebStore.Web.Infrastructure;
    using MyAutoPartsWebStore.Web.Models.Products;

    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly IProductService products;
        private readonly IDealerService dealers;
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IMapper mapper;

        public ProductController(MyAutoPartsStoreDbContext data,
            IProductService products, IDealerService dealers, IMapper mapper)
        {
            this.data = data;
            this.products = products;
            this.dealers = dealers;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealerController.BecomeDealer), "Dealer");
            }

            return View(new ProductFormModel()
            {
                Categories = this.products.AllCategories()
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
                return RedirectToAction(nameof(DealerController.BecomeDealer), "Dealer");
            }

            if (!this.products.CategoryЕxists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            var productToDel = data.Products.FirstOrDefault(x => x.Id == id);

            if (productToDel == null) return NotFound();

            var viewModel = new DeleteProductViewModel()
            {
                Id = productToDel.Id,
                Name = productToDel.Name,
                Image = productToDel.ImageUrl,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(DeleteProductViewModel viewProduct, int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(viewProduct);

            var product = this.data
                .Products
                .FirstOrDefault(p => p.Id == viewProduct.Id);

            if (product == null) return NotFound();

            this.data.Remove(product);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Description(int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            var details = this.products.Details(id);

            if (details == null) return NotFound();

            return View(details);
        }

        public IActionResult Search(string searchTerm)
        {
            var productQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = productQuery.Where(p =>
                p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var products = productQuery
                .OrderByDescending(p => p.Name)
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    SizeCapacity = p.SizeCapacity,
                    Weight = p.Weight,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                }).ToList();

            var productCategories = this.data
                .Products
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();

            return View(new ProductsSearchQueryModel
            {
                Products = products,
                SearchTerm = searchTerm,
            });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealerController.BecomeDealer), "Dealer");
            }

            var product = this.products.Details(id);

            if (product.UserId != this.User.GetId() && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var productForm = this.mapper.Map<ProductFormModel>(product);
            productForm.Categories = this.products.AllCategories();

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
                return RedirectToAction(nameof(DealerController.BecomeDealer), "Dealer");
            }

            if (!this.products.CategoryЕxists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

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
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("MyProducts");
        }

        [Authorize]
        public IActionResult MyProducts()
        {
            var products = this.products.ProductByUser(this.User.GetId());

            return View(products);
        }

        private DeleteProductViewModel GetProductName()
            => this.data
            .Products
            .Select(p => new DeleteProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
            }).FirstOrDefault();
    }
}
