using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{
    /// <summary>
    /// Stellt Information über
    /// das Lotto-Spiel in einem Land bereit
    /// </summary>
    public class LottoLand
    {
        /// <summary>
        /// Ruft die Bezeichnung des 
        /// Landes ob oder legt diese fest
        /// </summary>
        /// <remarks>Das ist die implizite
        /// Eigenschaftendeklaration. Nur für
        /// Ausnahmefälle. Grundsätzlich ein
        /// eigenes Feld deklarieren</remarks>
        public string Name { get; set; }

        /// <summary>
        /// Ruft die Anzahl der Zahlen
        /// bei einem Tipp ab oder legt diese fest
        /// </summary>
        public int AnzahlZahlenTipp { get; set; }

        /// <summary>
        /// Ruft die höchste Lottozahl im Land
        /// ab oder legt diese fest
        /// </summary>
        public int HöchsteZahl { get; set; }
    }
}
