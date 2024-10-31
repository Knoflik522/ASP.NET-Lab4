using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace WebApplication3Lab4.Controllers
{
    [Route("Library")]
    public class LibraryController : Controller
    {
        private readonly IConfiguration _configuration;

        public LibraryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            //return View();
            return Content("Вітаємо у бібліотеці!");
        }

        [HttpGet("Books")]
        public IActionResult Books()
        {
            var books = _configuration.GetSection("Books").Get<List<string>>();
            return books != null ? Json(books) : Content("Список книг не знайдено");
        }

        [HttpGet("Profile/{id:int?}")]
        public IActionResult Profile(int? id)
        {
            if (id is null)
            {
                return Content("Інформація про користувача бібліотеки: \n Name: Anna Cheban \n Email: ivanov@example.com \n MembershipDate\": \"2022-01-10\"");
            }
            else if (id >= 0 && id <= 5)
            {
                var userSection = _configuration.GetSection($"Users:User{id}");
                if (!userSection.Exists())
                {
                    return Content("Інформація про користувача не знайдена");
                }

                var userInfo = new
                {
                    Name = userSection["Name"],
                    Email = userSection["Email"],
                    MembershipDate = userSection["MembershipDate"]
                };
                return Json(userInfo);
            }
            return BadRequest("Неприпустимий параметр id. Будь ласка, введіть число від 0 до 5.");
        }
    }
}
