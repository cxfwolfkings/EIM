using System.Net;

namespace ConsoleApp1.Model
{
    public class ResponseEntity<T>
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 返回的json反序列化出来的对象
        /// </summary>
        public T Body { get; set; }
        /// <summary>
        /// 响应的报文头
        /// </summary>
        public HttpResponseHeader Headers { get; set; }
    }
}
