using System.ComponentModel;
using System.Reflection;

namespace Management_Webapi.Model.Response
{
    public class BaseResponse
    {
        /// <summary>
        /// 
        /// <para>0 Thành công</para>
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// Nội dung thông báo hiển thị lên ứng dụng cho người dùng
        /// </summary>
        public string? messageTitle { get; set; }
        public string? message { get; set; }
        /// <summary>
        /// Nội dung thông báo lỗi phục vụ cho dev (ko hiển thị lên cho người dùng)
        /// </summary>
        public string? errormessage { get; set; }
        public dynamic? data { get; set; }

        public string? externalmessage { get; set; }

        public void SetResponseError(ResponseError responseError)
        {
            //if (mwg.common.Helpers.CommonHelper.IsProduction)
            //    SetResponseError(responseError, false);
            //else
            SetResponseError(responseError, true);
        }

        public void SetResponseError(ResponseError responseError, bool isShowErrorCode)
        {
            if (this != null)
            {
                if (responseError.HasError())
                {
                    this.code = (int)responseError;
                    this.message = !string.IsNullOrEmpty(externalmessage) ? externalmessage : responseError.ToErrorMessage(isShowErrorCode);
                    this.errormessage = this.message;
                }
            }
        }
    }
    public static class ExtResponseError
    {
        public static bool HasError(this ResponseError responseError)
        {
            return responseError != ResponseError.NoError;
        }

        public static string ToErrorMessage(
            this ResponseError enumValue,
            bool isShowErrorCode = false
        )
        {
            if (isShowErrorCode == true)
                return enumValue.ToDescription() + " #" + Convert.ToInt32(enumValue);

            return enumValue.ToDescription();
        }

        public static string ToDescription(this Enum enumValue)
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DescriptionAttribute>()
                ?.Description ?? string.Empty;
        }
    }
}
