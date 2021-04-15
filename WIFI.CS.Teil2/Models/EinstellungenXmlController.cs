using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Teil2.Models
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen und
    /// Schreiben von WIFI.CS.Teil2 Einstellungpunkten
    /// im Xml Format bereit.
    /// </summary>
    internal class EinstellungenXmlController : WIFI.Anwendung.Generisch.XmlController<Einstellungen>
    {
        /// <summary>
        /// Gibt die Standardeinstellungen aus
        /// den Anwendungsressourcen zurück.
        /// </summary>
        public Einstellungen HoleAusRessourcen()
        {
            var Xml = new System.Xml.XmlDocument();
            Xml.LoadXml(WIFI.CS.Teil2.Properties.Resources.Einstellungen);

            var Einstellungen = new Einstellungen();

            foreach (System.Xml.XmlNode a in Xml.DocumentElement.ChildNodes)
            {
                Einstellungen.Add(
                    new Einstellung
                    {
                        Name = a.Attributes["name"].Value,
                        Symbol = a.Attributes["symbol"].Value,
                        ViewerName = a.Attributes["view"].Value
                    }
                    );
            }

            return Einstellungen;
        }
    }
}
