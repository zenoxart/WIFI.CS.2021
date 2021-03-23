using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIFI.CS.Teil1
{
    /// <summary>
    /// Steuert die Anwendung zum Lernen
    /// einer Komponenten-orientierten Windows Forms Anwendung
    /// </summary>
    /// <remarks>Verweis auf WIFI.Anwendung notwendig</remarks>
    static class Anwendung
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// <remarks>Der Zugriffsmodifizierer wird ignoriert.
        /// Damit Windows Möglichkeiten, z. B. Speichern Dialog,
        /// Zwischenablage, ... benutzt werden können, ist das
        /// STAThread-Attribut notwendig</remarks>
        [System.STAThread]
        private static void Main()
        {
            // Die Infrastruktur der Anwendung initialisieren
            var AppKontext = new WIFI.Anwendung.AppKontext();

            // Notwendig, damit Windows Forms
            // den Stil vom Betriebssystem übernimmt
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Die gewünschte Oberflächensprache einstellen
            AppKontext.Sprachen.Einstellen(Properties.Settings.Default.AktuelleSprache);

            bool WiederÖffnen = false;
            do
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture
                    = new System.Globalization.CultureInfo(AppKontext.Sprachen.Aktuell.Code);

                // Sicherstellen, dass die Anwendungssprachen aktuell sind
                AppKontext.Sprachen.Aktualisieren();

                // Ein Anwendungsfenster initialisieren
                // und sicherstellen, dass das Windows Forms Dispose läuft
                using (var Hauptfenster = AppKontext.Produziere<Hauptfenster>())
                {
                    Application.Run(Hauptfenster);

                    // Merken, ob das Fenster wegen
                    // eines Sprachwechsels geschlossen wurde,
                    // damit das Fenster wieder angezeigt wird
                    WiederÖffnen = Hauptfenster.Sprachwechsel;

                }
            } while (WiederÖffnen);

            // Den Code der aktuellen Sprache
            // in die Anwendungskonfiguration schreiben
            Properties.Settings.Default.AktuelleSprache = AppKontext.Sprachen.Aktuell.Code;

            // Die Anwendungskonfiguration speichern
            // (das gilt auch für "Beim Beenden fragen")
            Properties.Settings.Default.Save();

            // Die Infrastruktur herunterfahren
            AppKontext.Fenster.Speichern();
        }
    }
}
