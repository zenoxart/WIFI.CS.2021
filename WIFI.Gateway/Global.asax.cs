using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WIFI.Gateway
{
    /// <summary>
    /// Steuert das WIFI REST API Gateway
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Eine Pflichtmethode, die vom Webserver aufgerufen wird,
        /// wenn die Anwendung zu laufen beginnt
        /// </summary>
        /// <remarks>
        /// Im leerlauf wird ca. alle 20 Minuten die Anwendung vom Server beendet. 
        /// Sollte also kein Zugriff auf unsere REST Schnittstelle sein,
        /// beginnt die Anwendung von vorne.</remarks>
        protected void Application_Start()
        {
            // Firmenframework-AppKontext hochfahren
            var Infrastruktur = new WIFI.Anwendung.DatenbankAppKontext();


            Infrastruktur.Protokoll.Pfad = System.Web.Configuration.WebConfigurationManager.AppSettings["Protokollpfad"];

            if (Infrastruktur.Protokoll.Pfad != string.Empty)
            {
                // MapPath löst den Pfad mit ~ zum Physischen auf
                Infrastruktur.Protokoll.Pfad = this.Server.MapPath(Infrastruktur.Protokoll.Pfad);
            }

            // Die Einstellngen aus der Web.Config übernehmen
            // Das AppSettings ist eine NamedValueCollection (ähnlich wie ein Hashtable oder ein Dictionary)
            Infrastruktur.SqlServer = System.Web.Configuration.WebConfigurationManager.AppSettings["SqlServer"];
            Infrastruktur.DatenbankName = System.Web.Configuration.WebConfigurationManager.AppSettings["DatenbankName"];



            Infrastruktur.DatenbankPfad = System.Web.Configuration.WebConfigurationManager.AppSettings["DatenbankPfad"];
            
            // Wir mache aus dem Relativen Pfad (~/app_data) den vollständigen (z.B.: https://localhost/8080/app_data) und setzen es auf die selbe Variable
            Infrastruktur.DatenbankPfad = this.Server.MapPath(Infrastruktur.DatenbankPfad);

            // this.Context.con
            GlobalConfiguration.Configuration.Properties.TryAdd(Infrastruktur.GetType().FullName, Infrastruktur);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
