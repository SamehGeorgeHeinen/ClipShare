using AutoMapper;
using ClipShare.Core.IRepo;
using ClipShare.DataAccess.Data;
using ClipShare.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClipShare.Controllers
{
    public class CoreController : Controller
    {
        private IUnitOfWork _unitofwork;
        private Context _context;
        private IMapper _mapper;
        private IConfiguration _configuration;
        private IPhotoService _photoService;
        protected IUnitOfWork UnitOfWork => _unitofwork ??= HttpContext.RequestServices.GetService<IUnitOfWork>();
        protected Context Context => _context ??= HttpContext.RequestServices.GetService<Context>();
        protected IPhotoService PhotoService => _photoService ??= HttpContext.RequestServices.GetService<IPhotoService>();

        protected IConfiguration Configuration => _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();


    }
}
