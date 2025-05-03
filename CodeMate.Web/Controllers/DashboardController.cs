using Microsoft.AspNetCore.Mvc;

namespace CodeMate.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
