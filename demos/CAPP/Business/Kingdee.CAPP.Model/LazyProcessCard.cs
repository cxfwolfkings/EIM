using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: Lazy load process card
 *******************************/

namespace Kingdee.CAPP.Model
{
    public class LazyProcessCard:ProcessCard
    {
        private CardsXML _cardXML;
        public override CardsXML Card
        {
            get
            {
                if (_cardXML == null)
                {
                    if (LazyCardXMLLoader != null)
                    {
                        _cardXML = LazyCardXMLLoader(this.ID);
                    }
                    else
                    {
                        _cardXML = null;
                    }
                }
                return _cardXML;
            }
            set
            {
                base.Card = value;
            }
        }

        public Func<Guid, CardsXML> LazyCardXMLLoader
        { get; set; }
    }
}
