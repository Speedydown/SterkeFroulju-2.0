using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WZWVAPI;

namespace SterkeFroulju
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitDataBase();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private static void InitDataBase()
        {
            AppConfig.Instance.DatabaseConnections.Add("Server=MYSQL5012.Smarterasp.net;Database=db_9b4757_sterkef;Uid=9b4757_sterkef;Pwd=Geheim1!;");
            AppConfig.Instance.CurrentDatabaseConnection = AppConfig.Instance.DatabaseConnections.Count - 1;
            AppConfig.Instance.EnalbleMail = false;
        }
    }
}
