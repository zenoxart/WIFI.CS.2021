using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.Daten
{
    /// <summary>
    /// Stellt eine Auflistung von
    /// Fenster-Objekten bereit
    /// </summary>
    /// <remarks>Hier wird eine generische 
    /// Liste typisiert</remarks>
    public class Fensterliste : System.Collections.Generic.List<Fenster>
    {
        /// <summary>
        /// Gibt das Fensterobjekt mit
        /// dem gewünschen Namen zurück
        /// </summary>
        /// <param name="name">Bezeichnung des Fensters</param>
        /// <returns>Null, falls das Fenster nicht existiert</returns>
        public Fenster Suchen(string name)
        {
            return this.Find(f => string.Compare(f.Name, name, ignoreCase: true) == 0);
        }
    }

    /// <summary>
    /// Stellt Daten über eine Fensterposition bereit
    /// </summary>
    /// <remarks>Hier handelt es sich um ein
    /// Datentransferobjekt (DTO)</remarks>
    public class Fenster : System.Object
    {
        /// <summary>
        /// Ruft den Namen des Fensters
        /// ab oder legt diesen fest
        /// </summary>
        /// <remarks>Diese Information wird 
        /// als Schlüssel zum Wiederfinden benutzt</remarks>
        public string Name { get; set; }
        // -> Verweisdatentyp "class", Standard null

        /// <summary>
        /// Ruft den Fensterzustand Normal,
        /// Maximiert oder Minimiert ab
        /// oder legt diesen fest.
        /// </summary>
        /// <remarks>Integer deshalb, damit
        /// das Fenster für Windows Forms
        /// und WPF benutzt werden kann</remarks>
        public int Zustand { get; set; }
        // -> Wertdatentyp "struct", Standard Zahl 0

        // Folgende Eigenschaften enthalten
        // nur gültige Werte, wenn der Zustand normal ist

        // So zu schreiben, uninteressant
        //public System.Nullable<int> Links { get; set; }

        /// <summary>
        /// Ruft die linke Position ab
        /// oder legt diese fest
        /// </summary>
        /// <remarks>Nur bei Zustand Normal</remarks>
        public int? Links { get; set; }
        // -> nullable Werttyp

        /// <summary>
        /// Ruft die obere Position ab
        /// oder legt diese fest
        /// </summary>
        /// <remarks>Nur bei Zustand Normal</remarks>
        public int? Oben { get; set; }
        // -> nullable Werttyp

        /// <summary>
        /// Ruft die Breite des Fensters ab
        /// oder legt diese fest
        /// </summary>
        /// <remarks>Nur bei Zustand Normal</remarks>
        public int? Breite { get; set; }
        // -> nullable Werttyp

        /// <summary>
        /// Ruft die Höhe des Fensters ab
        /// oder legt diese fest
        /// </summary>
        /// <remarks>Nur bei Zustand Normal</remarks>
        public int? Höhe { get; set; }
        // -> nullable Werttyp

        /// <summary>
        /// Gibt eine Zeichenfolge zurück,
        /// die das aktuelle Fenster beschreibt
        /// </summary>
        public override string ToString()
        {
            //return base.ToString();

            return $"{this.GetType().Name}(Name=\"{this.Name}\")";
        }
    }
}
