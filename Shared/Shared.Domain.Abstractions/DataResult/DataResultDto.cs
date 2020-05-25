namespace Order.Core.BaseModel.DataResult
{
    public class DataResultDto : BaseResultDto
    {
        public object Data { get; set; }

        public DataResultDto()
        {

        }
        public DataResultDto(int code, string message, object data) : base(code, message)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }

    public class DataListResultDto : BaseResultDto
    {
        public object Data { get; set; }
        public int Count { get; set; }

        public DataListResultDto()
        {

        }
        public DataListResultDto(int code, string message, object data, int count) : base(code, message)
        {
            Code = code;
            Message = message;
            Data = data;
            Count = count;
        }
    }
}
