using Microsoft.AspNetCore.Mvc;

namespace Q_Manage.Controllers
{
    public class ClienteVistaApi : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
