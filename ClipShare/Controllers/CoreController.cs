using ClipShare.Core.IRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClipShare.Controllers
{
    public class CoreController : Controller
    {
        private IUnitOfWork _unitofwork;
        private IConfiguration _configuration;
        protected IUnitOfWork UnitOfWork => _unitofwork ??= HttpContext.RequestServices.GetService<IUnitOfWork>();
        protected IConfiguration Configuration => _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>();


    }
}
