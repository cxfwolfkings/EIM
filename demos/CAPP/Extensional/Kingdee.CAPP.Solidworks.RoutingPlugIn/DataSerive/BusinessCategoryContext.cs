using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    /// <summary>
    /// BusinessCategory data context
    /// </summary>
    public class BusinessCategoryContext: DataContext
    {
        public BusinessCategoryContext(string connection) : base(connection) { }
        public Table<BusinessCategory> BusinessCategories;
    }
}
