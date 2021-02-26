using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MsgTransTest
{
    public class Msg
    {
        private readonly EProtocol protocol;
        private readonly ETopService topService;
        private readonly int lowService;
        private readonly int length;
        private readonly XmlDocument content;
        public Msg(EProtocol p, ETopService ts, int ls, XmlDocument c)
        {
            protocol = p;
            topService = ts;
            lowService = ls;
            content = c;
            length = c.InnerXml.Length;
        }

        public EProtocol GetProtocol()
        {
            return protocol;
        }

        public ETopService GetTopService()
        {
            return topService;
        }

        public int GetLowService()
        {
            return lowService;
        }

        public int GetLength()
        {
            return length;
        }

        public XmlDocument GetContent()
        {
            return content;
        }
    }

}
