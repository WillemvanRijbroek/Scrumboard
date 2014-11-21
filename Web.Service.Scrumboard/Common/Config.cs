using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;

namespace ScrumboardWebService.Common
{
    public class Config
    {
        public static String ConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["scrumboard"].ToString();
            }
        }
    }
}