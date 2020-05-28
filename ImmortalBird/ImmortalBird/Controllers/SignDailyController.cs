using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO.ResponseDto;
using MyUtilLibrary.RedisH;
using Service;

namespace ImmortalBird.Controllers
{
    public class SignDailyController : BaseController
    {
        // GET: SignDaily
        public ActionResult Index()
        {
            ViewBag.IsSign = false;
            SignService signService = new SignService();
            if (signService.IsSigned(UserName))
                ViewBag.IsSign = true;

            return View();
        }

        public ActionResult SignNow()
        {
            SignService signService = new SignService();
            if (signService.IsSigned(UserName))
                return Json(new ResultDTO { Code = 0, Message = "已签到" });

            signService.DoSign(UserName);
            return Json(new ResultDTO { Code = 1, Message = "签到成功" });
        }

    }
}