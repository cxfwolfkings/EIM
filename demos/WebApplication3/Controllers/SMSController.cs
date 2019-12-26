using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        [Route("Send")]
        public bool Send(string msg)
        {
            Console.WriteLine("发送短信" + msg);
            return true;
        }
    }
}