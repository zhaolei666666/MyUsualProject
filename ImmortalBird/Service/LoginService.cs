using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Business;
using Domain.EF;
using DTO.RequestDto;
using DTO.ResponseDto;
using Util.Encryption;

namespace Service
{
    public class LoginService
    {
        private Login login = new Login();
        public ResultDTO Login(LoginDTO model)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                {
                    result.Code = 0;
                    result.Message = "please input username";
                }
                Users user = login.GetUsers(model.UserName);

                if (user != null)
                {
                    string MD5Pwd = MD5ER.MD5Encryp(model.Password);
                    if (MD5Pwd != user.Password)
                        result.Message = "密码错误";
                    else
                    {
                        result.Code = 1;
                        result.Message = "正在登陆。。。";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = 0;
                result.Message = "系统错误";
            }
            return result;
        }
    }
}
