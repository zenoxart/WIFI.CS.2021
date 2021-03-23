using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.Controller
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen 
    /// und Schreiben von Sprachen 
    /// im Xml-Format bereit
    /// </summary>
    internal class SprachenXmlController : WIFI.Anwendung.Generisch.XmlController<Daten.Sprachen>
    {
        /// <summary>
        /// Gibt die Liste der Sprachen
        /// aus den Ressourcen zurück
        /// </summary>
        public Daten.Sprachen HoleStandardListe()
        {
            var Sprachen = new Daten.Sprachen();
            var Xml = new System.Xml.XmlDocument();

            Xml.LoadXml(WIFI.Anwendung.Properties.Resources.Sprachen);

            foreach (System.Xml.XmlNode e in Xml.DocumentElement.ChildNodes)
            {
                Sprachen.Add(new Daten.Sprache
                {
                    Code = e.Attributes["code"].Value,
                    Name = e.Attributes["name"].Value
                });
            }

            return Sprachen;
        }
    }
}
