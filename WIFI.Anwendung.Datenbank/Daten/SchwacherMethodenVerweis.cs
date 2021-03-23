using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.Daten
{
    /// <summary>
    /// Ermöglicht das Kapseln einer Methode
    /// ohne den Garbage Collector an der 
    /// Freigabe des Besitzers zu hindern
    /// </summary>
    public class SchwacherMethodenVerweis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.WeakReference _Methode;

        /// <summary>
        /// Ruft die gekapselte Methode ab
        /// </summary><remarks>
        /// Null, falls der Besitzer nicht mehr existiert</remarks>
        public System.Action Methode
        {
            get
            {
                if (this._Methode.IsAlive)
                {

                    return this._Methode.Target as System.Action;
                }

                return null;
            }

        }


        /// <summary>
        /// Initialisiert ein SchwacherMethodenVerweis Objekt
        /// </summary>
        /// <param name="methode">Speicheradresse der Methode,
        /// die als WeakReference gekapselt werden soll,
        /// damit der Garbage Collector nicht am Freigeben 
        /// des Besitzers gehindert wird</param>
        public SchwacherMethodenVerweis(System.Action methode)
        {
            // Bis hierher ist der Besitzer der Methode für
            // den Garbage Collector gesperrt
            this._Methode = new WeakReference(methode);

            // Ab jetzt kann der Garbage Collecotr
            // den Besitzer der Methode entfernen
        }


    }

    /// <summary>
    /// Stellt eine Auflistung von 
    /// SchwacherMethodenVerweis Objekten bereit
    /// </summary>
    public class SchwacherMethodenVerweisListe : System.Collections.Generic.List<SchwacherMethodenVerweis>
    {

    }



}
