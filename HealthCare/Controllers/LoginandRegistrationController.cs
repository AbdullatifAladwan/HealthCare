using HealthCare.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Controllers
{
    public class LoginandRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public LoginandRegistrationController(ModelContext context, IWebHostEnvironment _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("UserId,Username,Firstname,Lastname,Imagepath,ImageFile,Password,Phonenumber,Address,Email,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    string webrootpath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                    string path = Path.Combine(webrootpath + "/Images/" + fileName);

                    using (var streamfile = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(streamfile);
                    }
                    //assgin value of file name to image path
                    user.Imagepath = fileName;
                }


                // assgin user name and pass into userlogin
                User userLogin = new User
                {
                    Password = user.Password,
                    Username = user.Username,
                    RoleId = 2,
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Phonenumber = user.Phonenumber,
                    Address = user.Address,
                    Imagepath = user.Imagepath,
                };


                // add pass and user of the new customer that registerd ;
                _context.Add(userLogin);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "LoginandRegistration");


            }

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Username , Password ")] User userLogin, Doctor doctor)
        {
            {
                var auth1 = _context.Doctors.Where(x => x.Username == userLogin.Username && x.Password == userLogin.Password).SingleOrDefault();

                var auth = _context.Users.Where(x => x.Username == userLogin.Username && x.Password == userLogin.Password).SingleOrDefault();
                if (auth1 != null)
                {
                    switch (auth1.RoleId)
                    {
                       //Doctor
                        case 3:
                            {
                                HttpContext.Session.SetString("Username1", auth1.Firstname);
                                HttpContext.Session.SetInt32("DoctorId", (int)auth1.DoctorId);

                                return RedirectToAction("HomeDoctor", "Home");
                            }
                        case null:
                            {
                                ViewData["Message"] = "wrong user name password";


                                return RedirectToAction("Login", "LoginandRegistration");
                            }
                    }
                }

                else if (auth != null )
                      {
                        switch (auth.RoleId)
                        {
                            //admin
                            case 1:
                                {
                                    HttpContext.Session.SetString("AdminName", auth.Firstname);
                                HttpContext.Session.SetInt32("AdminId", (int)auth.UserId);
                                return RedirectToAction("Homeadmin", "Home");
                                }
                            //user
                            case 2:
                                {
                                HttpContext.Session.SetString("Username", auth.Firstname);

                                HttpContext.Session.SetInt32("UserId", (int)auth.UserId);

                                return RedirectToAction("HomeUser", "Home");
                                }
                      
                            case null:
                                {
                                    ViewData["Message"] = "wrong user name password";


                                    return RedirectToAction("Login", "LoginandRegistration");
                                }
                        }

                    }

                }
               
                return View();
            }
        }
    }
