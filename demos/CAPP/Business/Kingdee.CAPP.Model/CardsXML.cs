using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
/*******************************
 * Created By franco
 * Description: Card Module Content
 *******************************/


namespace Kingdee.CAPP.Model
{
    [Serializable]
    [XmlRoot("CardsXML")]
    public class CardsXML
    {
        /// <summary>
        /// 多个卡片
        /// </summary>
        [XmlElementAttribute(ElementName = "Card", IsNullable = false)]
        public Card[] Cards { get; set; }        
    }
}
