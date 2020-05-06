using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO.RequestDto;
using DTO.ResponseDto;
using Service;

namespace ImmortalBird.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            LoginService ls = new LoginService();
            ResultDTO Result = ls.Login(model);

            if (Result.Code == 1)
                Session["username"] = model.UserName;

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }
}