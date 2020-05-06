using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO.ResponseDto;
using Service;

namespace ImmortalBird.Controllers
{
    public class RegisterController : Controller
    {
        private RegisterService register = new RegisterService();
        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string UserName, string Password)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                return Json(new { Code = 1, Message = "请输入用户名" });
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                return Json(new { Code = 1, Message = "请输入密码" });
            }

            ResultDTO result = register.AddUser(UserName, Password);
            return Json(result);
        }

    }
}