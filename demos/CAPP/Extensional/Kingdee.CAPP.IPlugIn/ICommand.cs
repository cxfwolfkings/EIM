using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.IPlugIn
{
    public interface ICommand
    {
        void Excute(string commandName);
    }
}
