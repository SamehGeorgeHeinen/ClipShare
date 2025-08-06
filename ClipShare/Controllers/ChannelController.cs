using ClipShare.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClipShare.Controllers
{
    public class ChannelController : Controller
    {
        [Authorize(Roles =$"{SD.UserRole}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
