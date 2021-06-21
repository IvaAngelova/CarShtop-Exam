using System.Linq;

using MyWebServer.Http;
using MyWebServer.Controllers;

using CarShop.Data;
using CarShop.Models.Issues;
using CarShop.Data.Models;
using CarShop.Services;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IValidator validator;
        private readonly CarShopDbContex contex;

        public IssuesController(IValidator validator, CarShopDbContex contex)
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
        public HttpResponse Add(AddIssueForModel model)
        {
            var modelErrors = this.validator.ValidateIssuesCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var issue = new Issue
            {
                Description = model.Description,
                CarId = model.CarId
            };

            this.contex.Issues.Add(issue);
            this.contex.SaveChanges();

            return this.View();
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var carWithIssues = this.contex
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssuesViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    Issues = c.Issue
                        .Select(i => new IssueListingViewModel
                        {
                            Id = i.Id,
                            Description = i.Description,
                            IsFixed = i.IsFixed
                        })
                })
                .FirstOrDefault();

            if (carWithIssues == null)
            {
                return Error($"Car with '{carId}' does not exist.");
            }

            return View(carWithIssues);
        }

        [Authorize]
        public HttpResponse Delete(string issueId)
        {
            var issue = this.contex
                .Issues.FirstOrDefault(i => i.Id == issueId);

            this.contex.Issues.Remove(issue);
            this.contex.SaveChanges();

            return View();
        }

        public HttpResponse Fix(string issueId)
        {
            if (!UserIsMechanic())
            {
                return Error("You do not have access to fix this car.");
            }

            var issue = this.contex
                .Issues
                .FirstOrDefault(i => i.Id == issueId);

            issue.IsFixed = true;

            this.contex.Update(issue);
            this.contex.SaveChanges();

            return this.Redirect("/");
        }

        private bool UserIsMechanic()
            => this.contex
                .Users
                .Any(u => u.Id == this.User.Id && u.IsMechanic);
    }
}
