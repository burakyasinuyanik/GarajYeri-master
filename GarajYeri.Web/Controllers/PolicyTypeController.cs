using GarajYeri.Data;
using GarajYeri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarajYeri.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PolicyTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicyTypeController(ApplicationDbContext context)
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
            return Json(new {data=_context.PolicyTypes.Where(pt=>pt.IsDeleted==false)});
        }

        [HttpPost]
        public IActionResult Add(PolicyType policyType)
        {
            _context.PolicyTypes.Add(policyType);
            _context.SaveChanges();
            return Ok(policyType);

        }

        [HttpPost]
        public IActionResult HardDelete(PolicyType policyType)
        {
            _context.PolicyTypes.Remove(policyType);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult SoftDelete(int id)
        {
           var policyType= _context.PolicyTypes.Find(id);
            if (policyType != null)
            {
                policyType.IsDeleted = true;
                _context.PolicyTypes.Update(policyType);

                try
                {
                    _context.SaveChanges();
                    return Ok(policyType);
                }
                catch(Exception ex)
                {
                    //return StatusCode(500, ex.Message);
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Gönderilen ID Geçersizdir.");
            }
        }
        [HttpPost]
        public IActionResult GetById(int id)
        {
            return Ok(_context.PolicyTypes.Find(id));
        }

        [HttpPost]
        public IActionResult Update(PolicyType policyType)
        {
            _context.PolicyTypes.Update(policyType);
            _context.SaveChanges();
            return Ok(policyType);
        }
    }
}
