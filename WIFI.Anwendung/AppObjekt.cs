using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt die Grundlage für
    /// WIFI Anwendungsobjekte bereit
    /// und untersützt diese mit 
    /// der Infrastruktur
    /// </summary>
    /// <remarks>Diese Klasse kann
    /// nur als Basisklasse benutzt werden</remarks>
    public abstract class AppObjekt : System.Object, IAppKontext
    {
        /// <summary>
        /// Ruft die Anwendungsinfrastruktur ab
        /// oder legt diese fest
        /// </summary>
        public AppKontext AppKontext { get; set; }

        /// <summary>
        /// Wird ausgelöst, wenn eine Ausnahme aufgetreten ist
        /// </summary>
        public event FehlerAufgetretenEventHandler FehlerAufgetreten;

        /// <summary>
        /// Löst das Ereignis FehlerAufgetreten aus
        /// </summary>
        /// <param name="e">Die Ereignisdaten mit der Ursache</param>
        /// <remarks>Wird FehlerAufgetreten nicht behandelt,
        /// wird die Ausnahme ausgelöst</remarks>
        protected virtual void OnFehlerAufgetreten(FehlerAufgetretenEventArgs e)
        {
            // Version 20201203 Hr. Plaimer
            //                  Damit Fehler sicher in der
            //                  Entwicklung zu einer Reaktion
            //                  führen, abstürzen, wenn
            //                  FehlerAufgetreten nicht behandelt ist

            // Damit beim Multithreading
            // der Garbage Collector das Objekt
            // nicht entfernt, die Speicheradresse kopieren
            var BehandlerKopie = this.FehlerAufgetreten;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, e);
            }
            // 20201203 Hr. Plaimer
#if DEBUG
            else
            {
                throw e.Ursache;
            }
#endif
        }
    }
}
