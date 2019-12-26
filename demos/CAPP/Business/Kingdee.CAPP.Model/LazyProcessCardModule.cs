using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: Lazy load ProcessCardModule
 *******************************/

namespace Kingdee.CAPP.Model
{
    public class LazyProcessCardModule:ProcessCardModule
    {
        private CardsXML cardModule;
        public override CardsXML  CardModule
        {
	        get 
	        {
                if (cardModule == null)
                {
                    if (CardModuleLazyLoader != null)
                    {
                        cardModule = CardModuleLazyLoader(this.Id);
                    }
                    else
                    {
                        cardModule = null;
                    }
                }

                return cardModule;
	        }
	          set 
	        { 
		        base.CardModule = value;
	        }
        }
        public Func<Guid, CardsXML> CardModuleLazyLoader
        { get; set; }
    }
}
