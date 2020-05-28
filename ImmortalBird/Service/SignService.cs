using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUtilLibrary.RedisH;

namespace Service
{
    public class SignService
    {
        public void DoSign(string UserName)
        {
            StackExchangeRedisManager redisClient = new StackExchangeRedisManager();
            {
                redisClient.StringSet(UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    TimeSpan.FromSeconds(Util.Other.TimeHelper.GetSeconds()));
            }
        }

        public bool IsSigned(string UserName)
        {
            string second = string.Empty;
            StackExchangeRedisManager redisClient = new StackExchangeRedisManager();
            {
                second = redisClient.StringGet(UserName);
            }
            if (!string.IsNullOrWhiteSpace(second))
                return true;

            return false;
        }

    }
}
