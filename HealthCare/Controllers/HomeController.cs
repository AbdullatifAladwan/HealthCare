using HealthCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult HomeUser()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            var item1 = _context.Websites.ToList();
            var item2 = _context.Testimonials.ToList();
            var item3 = _context.Attendances.ToList();
            var collection = new Tuple<IEnumerable<HealthCare.Models.Website>, IEnumerable<HealthCare.Models.Testimonial>, IEnumerable<HealthCare.Models.Attendance>>(item1, item2, item3);
            return View(collection);
        }
        public async Task<IActionResult> Index()
        {
            var item1 = _context.Websites.ToList();
            var item2 = _context.Testimonials.ToList();
            var item3 = _context.Attendances.ToList();
            var collection = new Tuple<IEnumerable<HealthCare.Models.Website>, IEnumerable<HealthCare.Models.Testimonial>, IEnumerable<HealthCare.Models.Attendance>>(item1, item2, item3);
            return View(collection);


            //return View();
        }
        public async Task<IActionResult> HomeAdmin()
        {
            ViewBag.NumberofUser = _context.Users.Count();
            ViewBag.NumberofDoctor = _context.Doctors.Count();
            ViewBag.NumberofAPP = _context.Appointments.Count();
            ViewBag.NumberofSp = _context.Specializations.Count();
            ViewBag.NumberofSp1 = _context.Testimonials.Count();
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");

            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            
            var modelContext = _context.Appointments.Include(a => a.Card).Include(a => a.Doctor).Include(a => a.User);
            return View(await modelContext.ToListAsync());

            
        }
        [HttpPost]
        public async Task<IActionResult> HomeAdmin(DateTime? date)
        {

            ViewBag.NumberofUser = _context.Users.Count();
            ViewBag.NumberofDoctor = _context.Doctors.Count();
            ViewBag.NumberofAPP = _context.Appointments.Count();
            ViewBag.NumberofSp = _context.Specializations.Count();
            ViewBag.NumberofSp1 = _context.Testimonials.Count();



            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");

            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            var result = _context.Appointments.Include(p => p.Card).Include(p => p.User).Include(p => p.Doctor);

            if (date == null )
            {
                return View(result);
            }
            else if ( date != null)
            {
                var SearchResult = await result.Where(x => x.Data >= date).ToListAsync();
                return View(SearchResult);
            }

            else if (date != null)
            {
                var SearchResult = await result.Where(x => x.Data <= date).ToListAsync();
                return View(SearchResult);
            }
            else
            {
                var SearchResult = await result.Where(x => x.Data <= date ).ToListAsync();
                return View(SearchResult);
            }
        }

        public async Task<IActionResult> HomeDoctor()
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            var modelContext = _context.Doctors.Include(d => d.Role).Include(d => d.Specialization).Include(d =>d.Appointments).Include(d => d.Attendances);
            return View(await modelContext.ToListAsync());
           
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
