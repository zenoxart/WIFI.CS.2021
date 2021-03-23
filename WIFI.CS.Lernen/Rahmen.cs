using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{
    /// <summary>
    /// Ermöglicht das Erstellen
    /// von Rahmenlinien in der Konsole
    /// </summary>
    public static class Rahmen
    {
        /// <summary>
        /// Ruft das Zeichen für links oben ab
        /// </summary>
        public static char LinksOben
        {
            get
            {
                return '\u250C';
            }
        }

        /// <summary>
        /// Ruft das Zeichen für rechts oben ab
        /// </summary>
        public static char RechtsOben
        {
            get
            {
                return '\u2510';
            }
        }

        /// <summary>
        /// Ruft das Zeichen für rechts unten ab
        /// </summary>
        public static char RechtsUnten
        {
            get
            {
                return '\u2518';
            }
        }

        /// <summary>
        /// Ruft das Zeichen für links unten ab
        /// </summary>
        public static char LinksUnten
        {
            get
            {
                return '\u2514';
            }
        }

        /// <summary>
        /// Ruft das Zeichen für eine waagrechte Linie ab
        /// </summary>
        public static char Horizontal
        {
            get
            {
                return '\u2500';
            }
        }

        /// <summary>
        /// Ruft das Zeichen für eine senkrechte Linie ab
        /// </summary>
        public static char Vertikal
        {
            get
            {
                return '\u2502';
            }
        }
    }
}
