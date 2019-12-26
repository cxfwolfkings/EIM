using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public delegate void TestEventHandler(SelectedEventArgs e);
    public class EventHelper
    {
        private EventHelper()
        { 
        
        }
        private static EventHelper _instance = null;
        public static EventHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EventHelper();
            }
            return _instance;
        }

        public event TestEventHandler Testing = null;

        protected virtual void OnTesting(SelectedEventArgs e)
        {
            if (Testing != null)
            {
                Testing(e);
            }
        }

        public void RasieTesting(SelectedEventArgs e)
        {
            OnTesting(e);
        }
    }
    public class SelectedEventArgs: EventArgs
    {
        public string ComponentName
        {
            get;
            set;
        }
        public string SketchName
        {
            get;
            set;
        }
    }
}
