using GaumataWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GaumataWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ShowHero = true;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Process contact form - send email, save to DB etc.
                TempData["SuccessMessage"] = "आपला संदेश पाठविला गेला आहे! आम्ही लवकरच संपर्क करू.";
                return RedirectToAction("Contact");
            }
            return View(model);
        }
    }

    public class ContactFormModel
    {
        [Required(ErrorMessage = "नाव आवश्यक आहे")]
        public string Name { get; set; }

        [Required(ErrorMessage = "मोबाइल नंबर आवश्यक आहे")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "संदेश आवश्यक आहे")]
        public string Message { get; set; }
    }
}
