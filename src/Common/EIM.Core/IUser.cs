using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EIM.Core
{
    /// <summary>
    /// 用户
    /// </summary>
    public class IUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 用户声明
        /// </summary>
        IEnumerable<Claim> Claims { get; }
    }
}
