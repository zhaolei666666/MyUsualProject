using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResponseDto
{
    public class ResultDTO
    {
        /// <summary>
        /// 1为成功  0为失败
        /// </summary>
        public int Code { get; set; }

        public string Message { get; set; }
    }
}
