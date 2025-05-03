using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeMate.Web.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth"); // elle yönlendir

            return View();
        }

    }
}
