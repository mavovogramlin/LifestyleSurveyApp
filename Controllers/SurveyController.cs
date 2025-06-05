using LifestyleSurveyApp.Data;
using LifestyleSurveyApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace LifestyleSurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new Survey());
        }


        [HttpPost]
        public IActionResult Index(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Warning = "Please complete all required fields before submitting.";
                return View(survey);
            }

            _context.Surveys.Add(survey);
            _context.SaveChanges();
            TempData["Success"] = "Survey submitted successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Results()
        {
            var surveys = _context.Surveys.ToList();
            if (!surveys.Any())
            {
                ViewBag.Message = "No Surveys Available.";
                return View();
            }

            ViewBag.Total = surveys.Count;
            ViewBag.AverageAge = Math.Round(surveys.Average(s => s.Age), 1);
            ViewBag.Oldest = surveys.Max(s => s.Age);
            ViewBag.Youngest = surveys.Min(s => s.Age);

            // Food preferences
            ViewBag.PizzaPercent = Math.Round((surveys.Count(s => s.LikesPizza) * 100.0 / surveys.Count), 1);
            ViewBag.PastaPercent = Math.Round((surveys.Count(s => s.LikesPasta) * 100.0 / surveys.Count), 1);
            ViewBag.PapAndWorsPercent = Math.Round((surveys.Count(s => s.LikesPapAndWors) * 100.0 / surveys.Count), 1);
            ViewBag.OtherPercent = Math.Round((surveys.Count(s => s.LikesOther) * 100.0 / surveys.Count), 1);

            // Ratings
            ViewBag.MovieAvg = Math.Round(surveys.Average(s => s.MovieRating), 1);
            ViewBag.RadioAvg = Math.Round(surveys.Average(s => s.RadioRating), 1);
            ViewBag.EatOutAvg = Math.Round(surveys.Average(s => s.EatOutRating), 1);
            ViewBag.TVAvg = Math.Round(surveys.Average(s => s.TVRating), 1);

            return View(surveys);
        }
    }
}