

using Util.Text;

namespace Util.Other
{
    /// <summary>
    /// 验证码操作
    /// </summary>
    public class ValidateHelper
    {
        const string CodeName = "validateCodeName";

        #region 生成验证码
        public static void Render(ValidateCodeType type)
        {
            Render(type,4);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        public static void Render(ValidateCodeType type, int length)
        {
            string code = string.Empty;
            switch (type)
            {
                case ValidateCodeType.Char:
                    {
                        code = CharCode.GetRandomChar(length);
                        break;
                    }
                case ValidateCodeType.Chinese:
                    {
                        code = ChineseCode.GetChinese(length);
                        break;
                    }
                case ValidateCodeType.Num:
                    {
                        code = RandomStr.GetRandomNum(length);
                        break;
                    }
            }            
            System.Web.HttpContext.Current.Session[CodeName] = code;
            
            ImagesHelper images = new ImagesHelper();
            images.Width = 100;
            images.Height = 31;
            images.Text = code;
            images.LineNoise = ImagesHelper.LineNoiseLevel.Extreme;
            images.BackgroundNoise = ImagesHelper.BackgroundNoiseLevel.Extreme;
            images.FontWarp = ImagesHelper.FontWarpFactor.Extreme;

            images.CreateImage();
        }
        #endregion

        #region 验证合法性
        public static bool Validate(string inputCode)
        {
            if(!string.IsNullOrEmpty(inputCode))
            {
                if (System.Web.HttpContext.Current.Session[CodeName] != null)
                {
                    string code = System.Web.HttpContext.Current.Session[CodeName].ToString();
                    //清理
                    System.Web.HttpContext.Current.Session.Abandon();
                    System.Web.HttpContext.Current.Session.Clear();
                    if (code.ToLower() == inputCode.ToLower()) return true;
                }
            }
            return false;
        }
        #endregion 
    }

    /// <summary>
    /// 验证码类型
    /// </summary>
    public enum ValidateCodeType
    {
        /// <summary>
        /// 中文
        /// </summary>
        Chinese = 0,

        /// <summary>
        /// 字母和数字
        /// </summary>
        Char = 1,
        /// <summary>
        /// 数字
        /// </summary>
        Num = 2
    }

    /// <summary>
    /// 存储方式
    /// </summary>
    public enum StoreType
    {
        /// <summary>
        /// Cookie
        /// </summary>
        Cookie = 0,

        /// <summary>
        /// Session
        /// </summary>
        Session = 1
    }  
}
