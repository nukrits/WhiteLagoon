using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenitiyVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(amenitiyVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {
            

            if (ModelState.IsValid )
            {
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenitiy has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int amenitiyId)
        {
            AmenityVM amenitiyVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenitiyId)
            };
            if (amenitiyVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenitiyVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenitiyVM)
        {
            if (ModelState.IsValid)
                           
                {                 
                    _unitOfWork.Amenity.Update(amenitiyVM.Amenity);
                    _unitOfWork.Save();
                    TempData["success"] = "The amenitiy has been updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
            

            amenitiyVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenitiyVM);
        }

        public IActionResult Delete(int amenitiyId)
        {
            AmenityVM amenitiyVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenitiyId)
            };
            if (amenitiyVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenitiyVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenitiyVM)
        {
            var objFromDb = _unitOfWork.Amenity.Get(u => u.Id == amenitiyVM.Amenity.Id);
            if (objFromDb != null)
            {
                _unitOfWork.Amenity.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The amenitiy has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The amenitiy could not be deleted";
            return View(amenitiyVM);
        }
    }
}
