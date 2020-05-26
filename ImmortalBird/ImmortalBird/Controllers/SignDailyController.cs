using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmortalBird.Controllers
{
    public class SignDailyController : BaseController
    {
        // GET: SignDaily
        public ActionResult Index()
        {
            //string username = RandomStr.CreatenNonce_str(8);

            //StackExchangeRedisManager redisClient = new StackExchangeRedisManager();
            //{
            //    redisClient.StringSet("K1", "V1", TimeSpan.FromSeconds(100));
            //}
            return View();
        }
    }
}