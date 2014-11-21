using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrumBoard.ScrumboardService;
using System.Net;
using System.Security.Principal;

namespace ScrumBoard.Common
{
    public class ServiceConn
    {
        static ScrumboardSoapClient client;

        public static ScrumboardSoapClient getClient()
        {
            if (client == null)
                client = new ScrumboardSoapClient();
            return client;
        }
        
    }
}
