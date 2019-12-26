using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [Route("Send")]
        public bool Send(string msg)
        {
            Console.WriteLine("发送邮件" + msg);
            return true;
        }
    }
}