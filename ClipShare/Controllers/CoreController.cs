using ClipShare.Core.IRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ClipShare.Controllers
{
    public class CoreController : Controller
    {
        private IUnitOfWork _unitofwork;
        protected IUnitOfWork UnitOfWork => _unitofwork ??= HttpContext.RequestServices.GetService<IUnitOfWork>();
           
    }
}
