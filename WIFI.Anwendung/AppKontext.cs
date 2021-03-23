using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Die Erweiterungen für die AssemblyInfo aktivieren
using WIFI.Anwendung.Erweiterungen;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt die Infrastruktur 
    /// einer WIFI Anwendung bereit
    /// </summary>
    /// <remarks>Hier handelt es sich um 
    /// Xml Dokumentationskommentar</remarks>
    public class AppKontext : System.Object
    {
        // (das ist C/C++ Kommenatar
        //  automatich am Zeilenende beendet)


        // Deklarationen auf Klassenebene
        // sind IMMER PRIVAT und heißen "Felder"

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private FensterManager _Fenster = null;

        /// <summary>
        /// Ruft den Dienst zum Verwalten
        /// der Anwendungsfenster ab
        /// </summary>
        public FensterManager Fenster
        {
            get
            {
                if (this._Fenster == null)
                {
                    /*
                    this._Fenster = new FensterManager();
                    this._Fenster.AppKontext = this;
                    */
                    // Dazu gibt's eine eigene Methode
                    this._Fenster = this.Produziere<FensterManager>();
                }

                return this._Fenster;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private SprachenManager _Sprachen = null;

        /// <summary>
        /// Ruft den Dienst zum Verwalten
        /// der Anwendungssprachen ab
        /// </summary>
        public SprachenManager Sprachen
        {
            get
            {
                if (this._Sprachen == null)
                {
                    this._Sprachen = this.Produziere<SprachenManager>();
                }

                return this._Sprachen;
            }
        }

        /// <summary>
        /// Gibt ein initialisiertes Anwendungsobjekt zurück
        /// </summary>
        /// <typeparam name="T">Gibt den Typ
        /// des benötigten Anwendungsobjekts an</typeparam>
        /// <returns>Ein Objekt, bei dem
        /// die AppKontext Eigenschaft eingestellt ist</returns>
        public virtual T Produziere<T>() where T: IAppKontext, new()
        {
            var NeuesObjekt = new T();

            // Die Infrastruktur im neuen Objekt einstellen
            NeuesObjekt.AppKontext = this;

            // Im Ausgabefenster vom Studio
            // einen Produktionshinweis hinterlassen
            System.Console.WriteLine($"{NeuesObjekt} wurde produziert...");

            // Falls es ein Anwendungsobjekt ist,
            // einen Ereignis-Behandler an FehlerAufgetreten anhängen,
            // der in der Konsole einen Hinweis ausgibt
            var AppObjekt = NeuesObjekt as AppObjekt;
            if (AppObjekt != null)
            {
                // Hier die Technik der "anonymen Methoden" benutzt
                AppObjekt.FehlerAufgetreten 
                    += (sender, e) => System.Console.WriteLine(
                        $"ERROR: Ausname \"{e.Ursache.Message}\" in Objekt {AppObjekt}");
            }

            // TODO: Hier weitere Produktionsschritte einfügen

            return NeuesObjekt;
        }

        /// <summary>
        /// Internes Feld für die gecachte Eigenschaft
        /// </summary>
        /// <remarks>Kann sich während der Sitzung nicht ändern</remarks>
        private static string _LokalerDatenpfad = null;

        /// <summary>
        /// Ruft das lokale Anwendungsdatenverzeichnis ab
        /// </summary>
        /// <remarks>Es wird sichergestellt, dass 
        /// das Verzeichnis existiert</remarks>
        public string LokalerDatenpfad
        {
            // 20210128 Absturz behoben, wenn im
            //          Firmenname oder im Titel
            //          nicht erlaubt Zeichen vorkommen,
            //          z. B. "/" oder "&"
            get
            {
                if (AppKontext._LokalerDatenpfad == null)
                {
                    AppKontext._LokalerDatenpfad = System.IO.Path.Combine(
                        // Basisverzeichnis %user%\AppData\Local
                        System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                        // Firmenname anhängen
                        // 20210128
                        //, this.HoleFirmenname()
                        , this.HoleFirmenname().Replace('/', '_').Replace('&', '_')
                        // Anwendungsname anhängen
                        // 20210128
                        //, this.HoleTitel()
                        , this.HoleTitel().Replace('/', '_').Replace('&', '_')
                        // Version
                        , this.HoleVersion()
                        );
                }

                // Sicherstellen, dass der Pfad vorhanden ist
                System.IO.Directory.CreateDirectory(AppKontext._LokalerDatenpfad);

                return AppKontext._LokalerDatenpfad;
            }
        }

        /// <summary>
        /// Internes Feld für die gecachte Eigenschaft
        /// </summary>
        /// <remarks>Kann sich während der Sitzung nicht ändern</remarks>
        private static string _Datenpfad = null;

        /// <summary>
        /// Ruft das roaming Anwendungsdatenverzeichnis ab
        /// </summary>
        /// <remarks>Es wird sichergestellt, dass 
        /// das Verzeichnis existiert</remarks>
        public string Datenpfad
        {
            // 20210128 Absturz behoben, wenn im
            //          Firmenname oder im Titel
            //          nicht erlaubt Zeichen vorkommen,
            //          z. B. "/" oder "&"
            get
            {
                if (AppKontext._Datenpfad == null)
                {
                    AppKontext._Datenpfad = System.IO.Path.Combine(
                        // Basisverzeichnis %user%\AppData\Roaming
                        System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                        // Firmenname anhängen
                        // 20210128
                        //, this.HoleFirmenname()
                        , this.HoleFirmenname().Replace('/','_').Replace('&','_')
                        // Anwendungsname anhängen
                        // 20210128
                        //, this.HoleTitel()
                        ,this.HoleTitel().Replace('/', '_').Replace('&', '_')
                        // Version
                        , this.HoleVersion()
                        );
                }

                // Sicherstellen, dass der Pfad vorhanden ist
                System.IO.Directory.CreateDirectory(AppKontext._Datenpfad);

                return AppKontext._Datenpfad;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Anwendungspfad = null;

        /// <summary>
        /// Ruft das Verzeichnis ab,
        /// aus dem die Anwendung gestartet wurde
        /// </summary>
        public string Anwendungspfad
        {
            get
            {
                if (this._Anwendungspfad == null)
                {
                    this._Anwendungspfad
                        = System.IO.Path.GetDirectoryName(
                                    System.Reflection.Assembly
                                        .GetEntryAssembly().Location
                            );
                }

                return this._Anwendungspfad;
            }
        }
    }
}
