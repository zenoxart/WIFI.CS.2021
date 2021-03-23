using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Erweitert die Infrastruktur um
    /// Dienste für Datenbank Anwendungen
    /// </summary>
    public class DatenbankAppKontext : WIFI.Anwendung.AppKontext
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ProtokollManager _Protokoll = null;

        /// <summary>
        /// Ruft das Anwendungsprotokoll ab
        /// </summary>
        public ProtokollManager Protokoll
        {
            get
            {
                if (this._Protokoll == null)
                {
                    this._Protokoll = this.Produziere<ProtokollManager>();
                    this._Protokoll.Eintragen(
                            $"{this._Protokoll} wurde produziert",
                            Daten.ProtokollEintragTyp.NeueInstanz);
                }

                return this._Protokoll;
            }
        }

        /// <summary>
        /// Gibt ein initialisiertes Anwendungsobjekt zurück
        /// </summary>
        /// <typeparam name="T">Gibt den Typ des benötigten Anwendungsobjekts an</typeparam>
        /// <returns>Ein Objekt, bei dem die AppKontext Eigenschaft eingestellt ist</returns>
        /// <remarks>Im Anwendungsprotokoll wird automatisch
        /// ein Eintrag erstellt, dass eine neue Instanz initialisiert wurde</remarks>
        public override T Produziere<T>()
        {
            // Zuerst einmal das machen,
            // was sonst gemacht wird
            var NeuesAppObjekt = base.Produziere<T>();

            // Im Protokoll eintragen, 
            // dass ein neues Anwendungsobjekt
            // erstellt wurde. Damit keine Rekursion
            // eintritt, mit Ausnahme vom Protokoll
            if (!(NeuesAppObjekt is ProtokollManager))
            {
                this.Protokoll.Eintragen(
                    $"{NeuesAppObjekt} wurde produziert",
                    Daten.ProtokollEintragTyp.NeueInstanz);

                // Bei Anwendungsobjekten dafür sorgen,
                // dass die Ursache von FehlerAufgetreten
                // im Protokoll steht

                var AppObjekt = NeuesAppObjekt as WIFI.Anwendung.AppObjekt;
                if (AppObjekt != null)
                {
                    AppObjekt.FehlerAufgetreten
                        // --- Ereignisbehandler
                        += (sender, e) => this.Protokoll.Eintragen(
                            $"{AppObjekt} verursachte eine Ausnahme:\r\n{e.Ursache.Message}",
                            Daten.ProtokollEintragTyp.Fehler);
                    // ----------------------

                    this.Protokoll.Eintragen($"{AppObjekt} behandelt FehlerAufgetreten...");
                }
            }

            // Neues Objekt liefern...
            return NeuesAppObjekt;
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.Collections.Hashtable _Cache = null;

        /// <summary>
        /// Ruft den Cache der Anwendung ab
        /// </summary>
        public System.Collections.Hashtable Cache
        {
            get
            {
                if (this._Cache == null)
                {
                    this._Cache = new System.Collections.Hashtable();
                    this.Protokoll.Eintragen(
                        "Die Anwendung hat den Cache initialisiert...",
                        Daten.ProtokollEintragTyp.NeueInstanz);

                }

                return this._Cache;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private bool _EnthältFehler;

        /// <summary>
        /// Ruft einen Warheitswert ab ob ein Fehler enthalten ist 
        /// oder legt fest
        /// </summary>
        public bool EnthältFehler
        {
            get { return this._EnthältFehler; }
            set { this._EnthältFehler = value; }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft der Prozessbar
        /// </summary>
        public bool? _KreisProzessbarSichtbarkeit = null;

        /// <summary>
        /// Internes Feld, gibt an wie viele Einstellungsfenster geöffnet sind
        /// </summary>
        public int OffeneEinstellungsFenster = 0;

        /// <summary>
        /// Ruft die Adresse des zu benutzenden SQL-Servers ab oder legt diese fest
        /// </summary>
        /// <remarks>
        /// Entweder die IP oder die DNS-Adresse.
        /// Sollte es sich u einen lokalen Sql Server 
        /// handeln, "(LocalDB)MSSQLLocalDB" verwenden.
        /// In diesem Fall muss DatenbankPfad eingestellt sein
        /// </remarks>
        public string SqlServer
        {
            get; set;
        }

        /// <summary>
        /// Zitat Trainer: "Sind wir nicht Päpstlicher als der Paps selbst"
        /// 
        /// Ruft den vollständigen Speicherort einer lokal angehängten Sql Server Datenbankdatei ab oder legt diesen fest
        /// </summary>
        /// <remarks>
        /// Diese Einstellung leer lassen,
        /// wenn es sich um eine angehängte Sql Server 
        /// Datenbank handelt. In Diesem Fall darf
        /// die SqlServer Eigenschaft nicht "(LocalDB)MSSQLLocalDB" sein.
        /// </remarks>
        public string DatenbankPfad
        {
            get; set;
        }

        /// <summary>
        /// Ruft den Namen der Sql Server Datenbank ab
        /// oder legt diesen fest
        /// </summary>
        /// <remarks>
        /// Falls mit "(LocalDB)MSSQLLocalDB" gearbeitet wird inkl. der Dateierweiterung
        /// </remarks>
        public string DatenbankName
        {
            get; set;
        }



    }
}
