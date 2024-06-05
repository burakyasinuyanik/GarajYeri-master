using GarajYeri.Data;
using GarajYeri.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarajYeri.Web.Controllers
{
    public class VehicleProcessTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleProcessTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetById(int id)
        {
            return Ok(_context.VehicleProcessTypes.Find(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vehicleProcessTypes = _context.VehicleProcessTypes.Where(vpt => vpt.IsDeleted == false).Select(vpt => new { vpt.Name,vpt.Id }).ToList();
            return Json(new { data = vehicleProcessTypes });
        }




        [HttpPost]
        public IActionResult HardDelete(VehicleProcessType vehicleProcessType)
        {
            _context.VehicleProcessTypes.Remove(vehicleProcessType);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult SoftDelete(int id)
        {
            var vehicleProcessType = _context.VehicleProcessTypes.Find(id);
            if(vehicleProcessType != null)
            {
                vehicleProcessType.IsDeleted = true;
                _context.VehicleProcessTypes.Update(vehicleProcessType);

                try
                {
                    _context.SaveChanges();
                    return Ok(vehicleProcessType);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Gönderilen Id eşleşmiyor");
            }
        }

        [HttpPost]
        public IActionResult Update(VehicleProcessType vehicleProcessType)
        {
            _context.VehicleProcessTypes.Update(vehicleProcessType);
            _context.SaveChanges();
            return Ok(vehicleProcessType);
        }

        [HttpPost]
        public IActionResult Add(VehicleProcessType vehicleProcessType)
        {
            _context.VehicleProcessTypes.Add(vehicleProcessType);
            _context.SaveChanges();
            return Ok(vehicleProcessType);
        }

    }
}
