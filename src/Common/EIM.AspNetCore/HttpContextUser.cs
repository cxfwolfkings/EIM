using EIM.Core;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EIM.AspNetCore
{
    /// <summary>
    /// Http上下文用户
    /// </summary>
    public class HttpContextUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Id => _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "sub").Value;

        public string Name => _httpContextAccessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> Claims => _httpContextAccessor.HttpContext.User.Claims;
    }
}
