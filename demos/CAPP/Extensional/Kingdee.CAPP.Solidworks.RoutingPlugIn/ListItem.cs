using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public class LItem
    {
        private string _id = string.Empty;
        private string _name = string.Empty;
        public LItem(string id, string name)
        {
            _id = id;
            _name = name;
        }
        public override string ToString()
        {
            return this._name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
