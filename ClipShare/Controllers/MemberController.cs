using ClipShare.Core.Entities;
using ClipShare.DataAccess.Data;
using ClipShare.Extensions;
using ClipShare.Utility;
using ClipShare.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClipShare.Controllers
{
    [Authorize]
    public class MemberController : CoreController
    {

        public  IActionResult Channel(int id)
        {
            return View();
        }
       
    }
}