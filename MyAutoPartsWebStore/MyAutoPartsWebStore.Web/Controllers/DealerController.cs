namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsWebStore.Web.Infrastructure.Extentions;
    using MyAutoPartsWebStore.Web.Models.Dealers;

    public class DealerController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IDealerService dealerService;

        public DealerController(MyAutoPartsStoreDbContext data, IDealerService dealerService)
        {
            this.data = data;
            this.dealerService = dealerService;
        }


        public IActionResult BecomeDealer() => View();

        [HttpPost]
        [Authorize]
        public IActionResult BecomeDealer(BecomeDealerViewModel dealer)
        {
            var userId = this.User.GetId();

            if (this.dealerService.IsDealer(userId))
            {
                return RedirectToAction("MyProducts");
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                CompanyName = dealer.CompanyName,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId,
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
