using System.Collections.Generic;

namespace Lottery.Models
{
    public class SettingModel
    {
        public string Count { get; set; }

        public IList<User> Users { get; set; }
    }
}