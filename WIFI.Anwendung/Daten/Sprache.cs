using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.Daten
{
    /// <summary>
    /// Stellt eine Liste von 
    /// Anwendungssprachen bereit
    /// </summary>
    public class Sprachen : System.Collections.Generic.List<Sprache>
    {
        /// <summary>
        /// Gibt die Sprache mit dem Kürzel zurück
        /// </summary>
        /// <param name="code">Das CultureInfo Kürzel der Sprache</param>
        /// <returns>Null, falls die Sprache nicht existiert</returns>
        public Sprache Suchen(string code)
        {
            return this.Find(s => string.Compare(s.Code, code, ignoreCase: true) == 0);
        }
    }

    /// <summary>
    /// Beschreibt eine Anwendungssprache
    /// </summary>
    public class Sprache : System.Object
    {
        /// <summary>
        /// Ruft das CultureInfo Kürzel 
        /// ab oder legt dieses fest
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Ruft die lesbare Bezeichnung
        /// der Sprache ab oder legt diese fest
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gibt einen Text zurück,
        /// der das aktuelle Objekt beschreibt
        /// </summary>
        public override string ToString()
        {
            return $"{this.GetType().Name}(Code=\"{this.Code}\", Name=\"{this.Name}\")";
        }

    }
}
