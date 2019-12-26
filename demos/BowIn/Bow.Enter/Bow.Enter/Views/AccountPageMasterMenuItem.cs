using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Enter.Views
{

    public class AccountPageMasterMenuItem
    {
        public AccountPageMasterMenuItem()
        {
            TargetType = typeof(AccountPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}