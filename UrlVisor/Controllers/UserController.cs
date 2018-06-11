using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlVisor.Bussiness;
using UrlVisor.Models;

namespace UrlVisor.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        [Route("Login")]
        public IActionResult LogIn()
        {
            return View(new LogInViewModel());
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LogIn(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
                return View(logInViewModel);

            try
            {
                var user = _userAdmin.LogIn(logInViewModel.User, logInViewModel.Password);
                return Redirect("/Home/Index");
            }
            catch (LogInException ex)
            {
                ModelState.AddModelError("logInError", ex.Message);
                return View(logInViewModel);
            }
        }

        [Route("Create")]
        public IActionResult CreateUser()
        {
            var ud = new UserDataViewModel();
            return View(ud);
        }

        [HttpPost]
        [Route("Create")]        
        public IActionResult CreateUser(UserDataViewModel userData)
        {

            try
            {
                var user = _userAdmin.CreateUser(userData.Usuario, userData.Password, userData.PasswordRepeated);
                return Redirect("Login");
            }
            catch (LogInException ex)
            {
                ModelState.AddModelError("createUserError", ex.Message);
                return View(userData);
            }
        }

        public UserController(IUserAdmin userAdmin)
        {
            this._userAdmin = userAdmin;
        }

        private readonly IUserAdmin _userAdmin;
    }
}