using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{
    /// <summary>
    /// Stellt die Methode dar, die das FehlerAufgetreten Ereignis behandelt.
    /// </summary>
    /// <param name="sender">Immer der erste Parameter. Wer ruft den Behandler auf.</param>
    /// <param name="e">Immer der zweite Parameter mit den Daten für den Behandler.</param>
    public delegate void FehlerAufgetretenEventHandler(object sender, FehlerAufgetretenEventArgs e);

    /// <summary>
    /// Stellt die Daten für das FehlerAufgetreten Ereignis bereit.
    /// </summary>
    /// <remarks>Solche Klassen müssen immer auf EventArgs enden
    /// und System.EventArgs erweitern.</remarks>
    public class FehlerAufgetretenEventArgs : System.EventArgs
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private System.Exception _Ursache = null;

        /// <summary>
        /// Ruft das Exception Objekt ab, das den Fehler beschreibt.
        /// </summary>
        /// <remarks>Die Daten für diese schreibgeschützte
        /// Eigenschaften kommen über den Konstruktor</remarks>
        public System.Exception Ursache
        {
            get
            {
                return this._Ursache;
            }
        }

        /// <summary>
        /// Initialisiert ein FehlerAufgetretenEventArgs Objekt.
        /// </summary>
        /// <param name="ursache">Die Ausnahme, die den Fehler beschreibt.</param>
        public FehlerAufgetretenEventArgs(System.Exception ursache)
        {
            this._Ursache = ursache;
        }
    }
}
