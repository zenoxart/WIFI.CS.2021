using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Teil1
{
    /// <summary>
    /// Stellt Eigenschaften und Methoden bereit,
    /// damit ein Objekt im Hauptfenster als
    /// aktuelles Objekt benutzt werden kann
    /// </summary>
    interface IHauptfensterObjekt
    {
        /// <summary>
        /// Ruft den Titel des Objekts ab
        /// </summary>
        string Titel { get; }

        /// <summary>
        /// Ruft das Steuerelement ab,
        /// in dem sich das Objekt zeichnen soll,
        /// oder legt dieses fest
        /// </summary>
        /// <remarks>Damit kann das Hauptfenster-Objekt
        /// auf das Steuerelement vom Hauptfenster zugreifen,
        /// in dem es sich zeichnen soll</remarks>
        System.Windows.Forms.Control Besitzer { get; set; }

        /// <summary>
        /// Stellt den Inhalt vom Objekt
        /// in die Windows Zwischenablage
        /// </summary>
        void Kopieren();

        /// <summary>
        /// Ruft den Titel für den Speichern Unter Dialog ab
        /// </summary>
        string SpeichernUnterTitel { get; }

        /// <summary>
        /// Ruft die Dateitypen für den Speichern Unter Dialog ab
        /// </summary>
        string SpeichernUnterTypen { get; }

        /// <summary>
        /// Schreibt den Inhalt des Objekts
        /// in die angegebene Datei
        /// </summary>
        /// <param name="pfad">Vollständige Pfadangabe
        /// der Datei, die zum Speichern benutzt werden soll</param>
        void SpeichernUnter(string pfad);

        /// <summary>
        /// Ruft das Objekt zum Ausdrucken ab
        /// </summary>
        System.Drawing.Printing.PrintDocument Druckseite { get; }

        /// <summary>
        /// Zeichnet den Inhalt des Objekts in der Graphics
        /// </summary>
        /// <param name="g">System.Drawing.Graphics Objekt,
        /// das zum Zeichnen benutzt werden soll</param>
        /// <remarks>Das Graphics Objekt vom Paint oder
        /// PrintPage Ereignis beziehen, damit das Bild
        /// nicht flackert</remarks>
        void Zeichnen(System.Drawing.Graphics g);
    }
}
