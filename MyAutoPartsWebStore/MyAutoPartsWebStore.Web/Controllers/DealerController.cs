namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsWebStore.Web.Infrastructure;
    using MyAutoPartsWebStore.Web.Models.Dealers;
    using System.Linq;

    public class DealerController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;

        public DealerController(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult BecomeDealer() => View();

        [HttpPost]
        [Authorize]
        public IActionResult BecomeDealer(BecomeDealerViewModel dealer)
        {
            //if (UserIsDealerAlready())
            //{
            //    return BadRequest();
            //}

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var userId = this.User.GetId();

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

        private bool UserIsDealerAlready()
        {
            var userId = this.User.GetId();

            var userIsDealer = !this.data
                .Dealers
                .Any(d => d.UserId == userId);

            return userIsDealer;
        }
    }
}
