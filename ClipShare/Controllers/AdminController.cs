using ClipShare.Core.Entities;
using ClipShare.DataAccess.Repo;
using ClipShare.Utility;
using ClipShare.ViewModels;
using ClipShare.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ClipShare.Controllers
{
    [Authorize(Roles = $"{SD.AdminRole}")]
    public class AdminController : CoreController
    {
        public IActionResult Category()
        {
            return View();
        }
        #region API Endpoints
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await UnitOfWork.CategorylRepo.GetAllAsync();
            var toReturn = categories.Select(x => new CategoryAddEdit_vm
            {
                Id = x.Id,
                Name = x.Name,


            });
            return Json(new ApiResponse(200, result: toReturn));
        }


        [HttpPost]
        public async Task<IActionResult> AddEditCategory(CategoryAddEdit_vm model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    UnitOfWork.CategorylRepo.Add(new Category() { Name = model.Name });
                    await UnitOfWork.CompleteAsync();
                    return Json(new ApiResponse(201, "Created", "New Category was added"));
                }

                else
                {
                    var category = await UnitOfWork.CategorylRepo.GetByIdAsync(model.Id);
                    if (category == null) { return Json(new ApiResponse(404)); }
                    var OldName = category.Name;
                    category.Name = model.Name;
                    await UnitOfWork.CompleteAsync();
                    return Json(new ApiResponse(200, "Updated", $"Category of {OldName} upddated to {model.Name}"));


                }
            }
            return  Json(new ApiResponse(400,message:"Nmae is Required"));



        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await UnitOfWork.CategorylRepo.GetByIdAsync(id);
            if(category!=null)
            {
                UnitOfWork.CategorylRepo.Remove(category);
                await UnitOfWork.CompleteAsync();
                return Json(new ApiResponse(200,"Deleted","Category of "+ category.Name+"has been removed"));

            }
            return Json(new ApiResponse(404, message: "category was not found"));


        }
        #endregion
    }
}
