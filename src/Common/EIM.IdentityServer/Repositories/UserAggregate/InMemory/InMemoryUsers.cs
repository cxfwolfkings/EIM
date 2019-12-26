using EIM.IdentityServer.Models;
using NodaTime;
using System.Collections.Generic;

namespace EIM.IdentityServer.Repositories.UserAggregate.InMemory
{
    public static class InMemoryUsers
    {
        public static List<User> Users = new List<User>
        {
            User.Hydrate(1,"bob", Instant.FromUtc(2017,1,24,6,6), true),
            User.Hydrate(2,"alice", Instant.FromUtc(2017,1,24,6,6), true),
        };
    }
}
