using MyWebServer.Http;
using MyWebServer.Controllers;
using CarShop.Models.Cars;
using CarShop.Services;
using CarShop.Data;
using System.Linq;
using CarShop.Data.Models;
using System.Collections.Generic;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly CarShopDbContex contex;

        public CarsController(IValidator validator, CarShopDbContex contex)
        {
            this.validator = validator;
            this.contex = contex;
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (UserIsMechanic())
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddCarForModel model)
        {
            if (UserIsMechanic())
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateCarCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            contex.Cars.Add(car);

            contex.SaveChanges();

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            List<CarListingViewModel> cars;

            if (UserIsMechanic())
            {
                cars = this.contex
                    .Cars
                    .Where(c=>c.Issue.Any(i=>!i.IsFixed))
                    .Select(c => new CarListingViewModel
                    {
                        Id = c.Id,
                        Model = c.Model,
                        Year = c.Year,
                        Image = c.PictureUrl,
                        PlateNumer = c.PlateNumber,
                        FixedIssues = c.Issue
                           .Where(i => i.IsFixed).Count(),
                        RemainingIssues = c.Issue
                           .Where(i => !i.IsFixed).Count()
                    })
                   .ToList();
            }
            else
            {
                cars = this.contex
                   .Cars
                   .Where(c => c.OwnerId == this.User.Id)
                   .Select(c => new CarListingViewModel
                   {
                       Id = c.Id,
                       Model = c.Model,
                       Year = c.Year,
                       Image = c.PictureUrl,
                       PlateNumer = c.PlateNumber,
                       FixedIssues = c.Issue
                           .Where(i => i.IsFixed).Count(),
                       RemainingIssues = c.Issue
                           .Where(i => !i.IsFixed).Count()
                   })
                   .ToList();
            }
            
            return View(cars);
        }

        private bool UserIsMechanic()
            => this.contex
                .Users
                .Any(u => u.Id == User.Id
                && u.IsMechanic);
    }
}
