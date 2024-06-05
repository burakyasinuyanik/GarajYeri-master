using GarajYeri.Data;
using GarajYeri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GarajYeri.Web.Controllers
{
    public class VehicleProcessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleProcessController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var vehicleProcesses = _context.VehicleProcesses.Where(vp => vp.IsDeleted == false).Select(vp => new
            {
                vp.Id,
                vp.Description,
                vp.Odemeter,
                vp.Price,
                VehicleProcessTypeName = vp.VehicleProcessType.Name,
                VehicleName = vp.Vehicle.Name
            }).ToList();
            return Json(new { data = vehicleProcesses });
        }



        [HttpPost]
        public IActionResult GetById(int id)
        {
            return Json(_context.VehicleProcesses.Where(vp => vp.Id == id).Select(vp => new VehicleProcess
            {
                Description = vp.Description,
                Odemeter = vp.Odemeter,
                Price = vp.Price,
                VehicleProcessType = vp.VehicleProcessType,
                Vehicle = vp.Vehicle,
            }).First());

        }

        [HttpPost]
        public IActionResult HArdDelete(VehicleProcess vehicleProcess)
        {
            _context.VehicleProcesses.Remove(vehicleProcess);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult SoftDelete(int id)
        {
            var vehicleProcess = _context.VehicleProcesses.Find(id);

            if (vehicleProcess != null)
            {
                vehicleProcess.IsDeleted = true;
                _context.VehicleProcesses.Update(vehicleProcess);
                try
                {
                    _context.SaveChanges();
                    return Ok(vehicleProcess);
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
        public IActionResult Update(VehicleProcess vehicleProcess)
        {
            _context.VehicleProcesses.Update(vehicleProcess);
            _context.SaveChanges();
            return Ok(vehicleProcess);
        }
        [HttpPost]
        public IActionResult Add(VehicleProcess vehicleProcess)
        {

            _context.VehicleProcesses.Add(vehicleProcess);
          
            _context.SaveChanges();

            return Ok(new {Id=vehicleProcess.Id,
                vehicleName="",
                vehicleProcessTypeName="",
                description=vehicleProcess.Description,
                price=vehicleProcess.Price,
                odemeter=vehicleProcess.Odemeter
            });
        }

    }
}
