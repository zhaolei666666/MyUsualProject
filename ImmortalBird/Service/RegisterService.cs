using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Business;
using Domain.EF;
using DTO.ResponseDto;
using Util.Encryption;

namespace Service
{
    public class RegisterService
    {
        private Register register = new Register();

        public ResultDTO AddUser(string UserName, string Password)
        {
            ResultDTO result = new ResultDTO();

            Users user = new Users()
            {
                UserId = System.Guid.NewGuid().ToString("N"),
                AccountName = UserName,
                Password = MD5ER.MD5Encryp(Password),
                Status = 1
            };

            register.AddUser(user);

            result.Code = 0;
            result.Message = "注册成功";

            return result;
        }

    }
}
