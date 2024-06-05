using GarajYeri.Data;
using GarajYeri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarajYeri.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll() {

            var vehicle = _context.Vehicles.Where(v => v.IsDeleted == false).Select(v => new
            {
                v.Id,
                v.LicensePlate,
                v.Name,
                v.Odometer,
                VehicleTypeName=v.VehicleType.Name,
                AppUserName=v.AppUser.FullName,
                VehiclePhotos = v.VehiclePhotos.Select(p => new {p.Path,p.Id}),
                VehicleProcess=v.VehicleProcesses.Select(p => new {p.VehicleProcessTypeId}),
                VehiclePolicies=v.VehiclePolicies.Select(p => new {p.Id, p.Name}),
                VehicleInspection=v.VehicleInspections.Select(p => new {p.Id, p.Description})
            }).ToList();

            return Json(new { data=vehicle});

        }
        [HttpPost]
        public IActionResult GetById(int id)
        {
            return Json(_context.Vehicles
                .Where(v => v.Id == id && v.IsDeleted == false)
                .Select(v => new
                {
                    v.Id,
                    v.LicensePlate,
                    v.Name,
                    v.Odometer,
                    VehicleTypeName = v.VehicleType.Name,
                    AppUserName = v.AppUser.FullName,
                    VehiclePhotos = v.VehiclePhotos.Select(p => new { p.Path, p.Id }),
                    VehicleProcess = v.VehicleProcesses.Select(p => new { p.Id, p.VehicleProcessTypeId }),
                    VehiclePolicies = v.VehiclePolicies.Select(p => new { p.Id, p.Name }),
                    VehicleInspection = v.VehicleInspections.Select(p => new { p.Id, p.Description })
                })
                .First());
           
            
        }
        [HttpPost]
        public IActionResult HardDelete(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
            return Ok();

        }
        public IActionResult SoftDelete(int id)
        {
          var vehicle= _context.Vehicles.Find(id);
            if(vehicle != null)
            {
                vehicle.IsDeleted = true;
                _context.Vehicles.Update(vehicle);
                try
                {
                    _context.SaveChanges();
                    return Ok(vehicle);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Gönderilen ID geçersizdir");
            }
            
           
            
        }

        [HttpPost]
        public IActionResult Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
             return Ok(vehicle);
            
            
        }



    }
}
