namespace Order.Core.BaseModel.DataResult
{
    public class BaseResultDto
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        public BaseResultDto()
        {

        }

        public BaseResultDto(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
