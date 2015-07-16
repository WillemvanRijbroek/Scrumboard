using ScrumBoard.ScrumboardService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrumBoard.Common
{
    public abstract class Cache
    {
        private ScrumboardSoapClient client = ServiceConn.getClient();

        /// <summary>
        /// Updates the cache with any changes applied on the server
        /// </summary>
        public abstract void update();

        /// <summary>
        /// Clears all cached content
        /// </summary>
        public abstract void clear();

        /// <summary>
        /// Gets the scrumboard service client running on the server
        /// </summary>
        protected ScrumboardSoapClient ServiceClient { get { return client; } }
    }
}
