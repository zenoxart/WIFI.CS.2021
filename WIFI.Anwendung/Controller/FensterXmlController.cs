using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.Controller
{
    /// <summary>
    /// Stellt einen Dienst zum Schreiben
    /// und Lesen einer Fensterliste im 
    /// Xml-Format bereit
    /// </summary>
    internal class FensterXmlController : WIFI.Anwendung.Generisch.XmlController<Daten.Fensterliste>
    {

    }

    // 20201210
    // Wurde durch einen generischen Controller ersetzt
    /*
    /// <summary>
    /// Stellt einen Dienst zum Schreiben
    /// und Lesen einer Fensterliste im 
    /// Xml-Format bereit
    /// </summary>
    internal class FensterXmlController : AppObjekt
    {
        /// <summary>
        /// Serialisiert die Daten der
        /// Liste in die angegebene Datei
        /// im Xml Format
        /// </summary>
        /// <param name="liste">Die zu speichernden Daten</param>
        /// <param name="inPfad">Die vollständige Pfadangabe der Xml Datei</param>
        /// <exception cref="System.Exception">Tritt auf, wenn das Serialisieren nicht funktioniert hat</exception>
        public void Speichern(Daten.Fensterliste liste, string inPfad)
        {
            var Serialisierer = new System.Xml.Serialization.XmlSerializer(liste.GetType());

            using (var Schreiber = new System.IO.StreamWriter(inPfad))
            {
                Serialisierer.Serialize(Schreiber, liste);
            }
        }

        /// <summary>
        /// Gibt die deserialisierten Xml Daten
        /// aus der Datei zurück
        /// </summary>
        /// <param name="ausPfad">Die vollständige Pfadangabe der Xml Datei</param>
        /// <exception cref="System.Exception">Tritt auf, wenn das Deserialisieren nicht funktioniert hat</exception>
        public Daten.Fensterliste Laden(string ausPfad)
        {
            var Serialisierer = new System.Xml.Serialization.XmlSerializer(typeof(Daten.Fensterliste));

            using (var Leser = new System.IO.StreamReader(ausPfad))
            {
                return (Daten.Fensterliste)Serialisierer.Deserialize(Leser);
            }
        }
    }
    */
}
