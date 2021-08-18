namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsWebStore.Web.Infrastructure.Extentions;
    using MyAutoPartsWebStore.Web.Models.Dealers;

    public class DealerController : Controller
    {
        private readonly IDealerService dealer;

        public DealerController(IDealerService dealer)
        {
            this.dealer = dealer;
        }

        public IActionResult BecomeDealer() => View();

        [HttpPost]
        [Authorize]
        public IActionResult BecomeDealer(BecomeDealerFormModel newDealer)
        {
            var userId = this.User.GetId();

            if (this.dealer.IsDealer(userId))
            {
                return RedirectToAction(nameof(ProductController.MyProducts));
            }

            if (!ModelState.IsValid)
            {
                return View(newDealer);
            }

            this.dealer.Become(
                newDealer.Name,
                newDealer.CompanyName,
                newDealer.PhoneNumber,
                userId);

            return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
        }
    }
}
