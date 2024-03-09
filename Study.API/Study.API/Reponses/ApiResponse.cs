namespace Study.API.Reponses
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public T? Result { get; set; }
    }
}
