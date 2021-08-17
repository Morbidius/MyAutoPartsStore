namespace MyAutoPartsWebStore.Web.Areas.Admin.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Models.BaseModels;
    using MyAutoPartsStore.Models.ViewModels.Products;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsWebStore.Globals.Extensions;

    public class ProductsController : AdminController
    {
        private readonly IProductService products;

        public ProductsController(IProductService products)
        {
            this.products = products;
        }

        public IActionResult AllProducts(AllProductsQueryModel viewmodel, int page = 1, int quantity = 20)
        {
            var productsCount = products.AllCounts(viewmodel.SearchTerm);

            var maxPage = (int)Math.Ceiling(productsCount / (quantity * 1.0));

            if (page <= 0 || page > maxPage) page = 1;

            viewmodel.Products = products.All();

            viewmodel.Products = viewmodel.Products.Paging(page, quantity);

            viewmodel = SetPages(viewmodel, page, maxPage);

            return View(viewmodel);
        }

        public IActionResult Approve(int id)
        {
            this.products.Approve(id);

            return RedirectToAction(nameof(AllProducts));
        }

        private T SetPages<T>(T viewModel, int page, int maxPages)
            where T : IPagingModel
        {
            viewModel.CurrentPage = page;
            viewModel.NextPage = page >= maxPages ? null : page + 1;
            viewModel.PreviousPage = page <= 1 ? null : page - 1;
            viewModel.MaxPages = maxPages;

            return viewModel;
        }
    }
}
