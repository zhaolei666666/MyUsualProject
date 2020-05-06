using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.RequestDto
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 验证码 现在还没加
        /// </summary>
        public string VerifyCode { get; set; }
    }
}
