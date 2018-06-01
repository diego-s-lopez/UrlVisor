using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlVisor.Bussiness;
using UrlVisor.Model;
using UrlVisor.Model.Login;

namespace UrlVisor.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DoLogin(string user, string pass)
        {
            try
            {
                var usuario = _loginAdmin.LogIn(user, pass);
                return Json(new LoginResponse {LoginOk = true, Token = usuario.Token});
            }
            catch (LogInException ex)
            {
                return Json(new LoginResponse {LoginOk = false, LoginError = ex.Message});
            }
        }

        [HttpPost]
        public IActionResult DoRegister(string user, string pass, string repeatedPass)
        {
            try
            {
                var usuario = _loginAdmin.CreateUser(user, pass, repeatedPass);
                return Json(new LoginResponse { LoginOk = true, Token = usuario.Token });
            }
            catch (LogInException ex)
            {
                return Json(new LoginResponse { LoginOk = false, LoginError = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DoChangePassword(string user, string oldPass, string newPass, string repeteadNewPass)
        {
            try
            {
                _loginAdmin.ChangePassword(user, oldPass, newPass, repeteadNewPass);
                return Json(new ChangePasswordResponse());
            }
            catch (LogInException ex)
            {
                return Json(new ChangePasswordResponse { LoginOk = false, LoginError = ex.Message});
            }
        }

        public LoginController(ILoginAdmin loginAdmin)
        {
            _loginAdmin = loginAdmin ?? throw new ArgumentNullException(nameof(loginAdmin));
        }

        private readonly ILoginAdmin _loginAdmin;
    }
}