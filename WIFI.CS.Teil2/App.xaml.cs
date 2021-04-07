using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

// Für die Erweiterungsmethoden
// zum Holen von AssemblyInfos
using WIFI.Anwendung.Erweiterungen;

namespace WIFI.CS.Teil2
{
    /// <summary>
    /// Steuert die WIFI Client WPF Anwendung
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Stellt den Haupteinstiegspunkt bereit
        /// </summary>
        /// <remarks>Für das eigene Main muss
        /// bei WPF "ApplicationDefinition" 
        /// bei App.xaml auf "Page" umgestellt werden</remarks>
        [System.STAThread]
        static void Main()
        {

            // die erweiterte Infrastruktur hochfahren
            var AppKontext = new WIFI.Anwendung.DatenbankAppKontext();

            // Dafür sorgen, dass die DatenBasis einen Protokolleintrag
            // erstellt, wenn ein Typ für ToString() analysiert wurde
            WIFI.Anwendung.Daten.DatenBasis.NeuerTypWurdeAnalysiert 
                += (sender, e) 
                => AppKontext.Protokoll.Eintragen(
                    $"WIFI.Anwendung.Daten.DatenBasis hat einen neuen Typ {e.NewObject} für ToString() analysiert...", 
                    Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);

            var Protokolleintrag = new WIFI.Anwendung.Daten.ProtokollEintrag
            {
                Typ = Anwendung.Daten.ProtokollEintragTyp.NeueInstanz,
                Text = $"{AppKontext.HoleTitel()} beginnt zu laufen..."
            };

            AppKontext.Protokoll.Eintragen(Protokolleintrag);

            // Aktiviert das automatische Komprimieren, wenn die Settings-Property aus der Anwendung auf true gesetzt ist
            AppKontext.Protokoll.AutomatischKomprimieren = WIFI.CS.Teil2.Properties.Settings.Default.ProtokollKomprimieren;

            // Lade aus den Settings das Aktuelle Ressourcedictionary
            // AppKontext.RessourceDictionary = 

            // die zuletzt benutzte Sprache einstellen
            // Falls die Konfiguration keinen Wert enthält,
            // die Sprache vom Betriebssystem benutzen
            var LetzterSprachcode = WIFI.CS.Teil2.Properties.Settings
                .Default.CodeAktuelleSprache;
            if (LetzterSprachcode.Length == 0)
            {
                // Vermutlich ein neuer Benutzer
                // Als Standardsprache die Sprache
                // vom Betriebssytem

                // Die Anwendung unterscheidet keine Sub-Kulturen,
                // kein "de-AT", ..., nur die Hauptsprache ISO2
                LetzterSprachcode = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            }
            AppKontext.Sprachen.Einstellen(LetzterSprachcode);

            // in der Anwendung diese Sprache einstellen
            System.Globalization.CultureInfo.CurrentUICulture
                = new System.Globalization.CultureInfo(AppKontext.Sprachen.Aktuell.Code);

            // Sollte die Zahlenformatierung nicht vom Betriebsystem
            // gesteuert werden, die Kultur der Oberfläche auch für die
            // Zahlenformatierung benutzen
            if (!WIFI.CS.Teil2.Properties.Settings.Default.ZahlenformatVonOS)
            {
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.CurrentUICulture;
                
                AppKontext.Protokoll.Eintragen(
                    "Die Zahlenformatierung wird von der Anwendung gesteuert", 
                    Anwendung.Daten.ProtokollEintragTyp.Warnung);
            }


            //System.Globalization.CultureInfo.CurrentUICulture.NumberFormat = new System.Globalization.NumberFormatInfo();
            //System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.FullDateTimePattern = DateTime.Now.GetDateTimeFormats("");

            // Den Cache vom Sprachen-Manager neu initialisieren
            AppKontext.Sprachen.Aktualisieren();
            // Weil "Aktualisieren" die Aktuelle Sprache  
            // nicht mitübersetzt / initialisiert
            // (wäre ein Todo im WIFI.Anwendung aus dem Teil 1)
            AppKontext.Sprachen.Einstellen(AppKontext.Sprachen.Aktuell.Code);

            // Damit das eigene ViewModel intialisieren
            var VM = AppKontext.Produziere<ViewModels.Anwendung>();

            // Damit die WPF Ressourcen geladen werden,
            // die WPF Anwendung intialisieren
            // (Damit System.Windows.Application.Current
            //  initialisiert wird)
            var WpfApp = new WIFI.CS.Teil2.App();
            WpfApp.InitializeComponent();

            // Damit das Protokoll für die WPF Anwendung threadsicher wird,
            // den Dispatcher der Anwendung mitteilen
            AppKontext.Protokoll.Dispatcher = WpfApp.Dispatcher;

            // Das ViewModel mit einem Xaml Fenster verbinden,
            // dem ViewModel die View mitteilen
            VM.Starten<WIFI.CS.Teil2.Hauptfenster>();

            // Die Benutzer-Konfiguration speichern:
            // - die Anwendungssprache
            WIFI.CS.Teil2.Properties.Settings.Default.CodeAktuelleSprache
                = AppKontext.Sprachen.Aktuell.Code;

            WIFI.CS.Teil2.Properties.Settings.Default.Save();

            // die Infrastruktur herunterfahren
            AppKontext.Fenster.Speichern();

        }
    }
}
