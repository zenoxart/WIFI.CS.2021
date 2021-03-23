using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// der Anwendungsfenster bereit
    /// </summary>
    public class FensterManager : AppObjekt
    {

        // 20210204 Der FensterManager speichert auch
        //          die Anzahl der Bildschirme bei einer
        //          Fensterposition, damit ein Fenster
        //          bei fehlendem Bildschirm nicht
        //          am falschen geöffnet werden
        //
        //          TODO: Auch die Auflösung mitspeichern
        //                und beim Wiederherstellen prüfen

        /// <summary>
        /// Liefert die Anzahl der Bildschirme
        /// im Rahmen von GetSystemMetrics
        /// </summary>
        private const int SM_CMONITORS = 80;

        /// <summary>
        /// Gibt spezifische Systeminformationen zurück
        /// </summary>
        /// <param name="info">Code der benötigten Information</param>
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        protected static extern int GetSystemMetrics(int info);

        /// <summary>
        /// Ruft ein Schlüsseltext ab, der
        /// die Anzahl der aktuellen Bildschirme enthält
        /// </summary>
        /// <remarks>Die Info ist nicht gecachet,
        /// weil sich die Anzahl der Monitore
        /// dynamsich ändern kann</remarks>
        protected string MonitorSchlüssel
        {
            get
            {
                // NICHT CACHEN!!!
                return $"_M{FensterManager.GetSystemMetrics(SM_CMONITORS)}";
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Daten.Fensterliste _Liste = null;

        /// <summary>
        /// Ruft die verwalteten Fenster ab
        /// </summary>
        /// <remarks>Die Liste wird mit den
        /// Daten der im Xml-Format gespeicherten
        /// Fensterliste intialisiert</remarks>
        protected WIFI.Anwendung.Daten.Fensterliste Liste
        {
            get
            {
                if (this._Liste == null)
                {
                    this._Liste = this.Lesen();
                }

                return this._Liste;
            }
        }

        /// <summary>
        /// Fügt dem Fenstermanager ein Fenster hinzu
        /// </summary>
        /// <param name="fenster">Objekt mit der Fensterposition</param>
        public void Hinterlegen(WIFI.Anwendung.Daten.Fenster fenster)
        {

            // 20210204 Um mit mehreren Bildschirmen besser 
            //          umgehen zu können, zusätzlich die
            //          Anzahl der Monitore berücksichtigen
            fenster.Name += this.MonitorSchlüssel;
            //---

            // Prüfen, ob das Fenster bereits vorhanden ist
            var f = this.Liste.Suchen(fenster.Name);

            // Falls nicht, das Fenster hinterlegen
            if (f == null)
            {
                this.Liste.Add(fenster);
            }
            else
            {
                // sonst den Zustand und
                // die Positionsdaten aktualisieren

                // Weil Fenster ein Verweistyp ist,
                // wird hier direkt mit dem Objekt
                // in der Liste gearbeitet

                f.Zustand = fenster.Zustand;

                // Neue Positionsdaten aber nur
                // übernehmen, wenn welche vorhanden sind
                // Fad: eine herkömmliche Binärentscheidung
                if (fenster.Links != null)
                {
                    f.Links = fenster.Links;
                }

                // Besser: eine Binärentscheidung in
                //         einer Anweisung mit ? :
                //         Entspricht der WENN() Funktion in Excel
                f.Oben = fenster.Oben != null ? fenster.Oben : f.Oben;

                // Noch schöner: Der NULL Überprüfungsoperator ??
                f.Breite = fenster.Breite ?? f.Breite;
                f.Höhe = fenster.Höhe ?? f.Höhe;
            }
        }

        /// <summary>
        /// Gibt das Objekt mit der Fensterposition zurück
        /// </summary>
        /// <param name="fensterName">Bezeichnung vom Fenster,
        /// dessen Positionsdaten benötigt werden</param>
        /// <returns>Null, falls das Fenster nicht gefunden wurde</returns>
        public WIFI.Anwendung.Daten.Fenster Abrufen(string fensterName)
        {
            // 20210204 Um mit mehreren Bildschirmen besser 
            //          umgehen zu können, zusätzlich die
            //          Anzahl der Monitore berücksichtigen
            //return this.Liste.Suchen(fensterName);
            return this.Liste.Suchen(fensterName + this.MonitorSchlüssel);
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        /// <remarks>Statisch, weil sich der Benutzer
        /// während der Laufzeit nicht ändern kann</remarks>
        private static string _StandardPfad = null;

        /// <summary>
        /// Ruft das Verzeichnis ab, in dem
        /// die Fensterliste standardmäßig gespeichert wird.
        /// </summary>
        /// <remarks>Im AppData\Local der Anwendung</remarks>
        public string StandardPfad
        {
            get
            {
                if (FensterManager._StandardPfad == null)
                {
                    FensterManager._StandardPfad
                        = System.IO.Path.Combine(
                            this.AppKontext.LokalerDatenpfad,
                            "Fenster.xml");
                }

                return FensterManager._StandardPfad;
            }
        }

        /// <summary>
        /// Schreibt die Fensterliste als
        /// Xml-Datei im Anwendungsdaten-Verzeichnis
        /// </summary>
        /// <remarks>Sollte das Speichern nicht möglich sein,
        /// wird FehlerAufgetreten mit der Ursache ausgelöst</remarks>
        public void Speichern()
        {
            try
            {
                this.Controller.Speichern(this.Liste, this.StandardPfad);
            }
            catch (System.Exception ex)
            {
                this.OnFehlerAufgetreten(new FehlerAufgetretenEventArgs(ex));
            }
        }

        /// <summary>
        /// Holt aus dem Anwendungsdaten-Verzeichnis
        /// eine früher im Xml-Format gespeicherte
        /// Fensterliste und gibt diese zurück
        /// </summary>
        protected WIFI.Anwendung.Daten.Fensterliste Lesen()
        {
            try
            {
                return this.Controller.Laden(this.StandardPfad);
            }
            catch (System.Exception ex)
            {
                // Passiert bei einem neuen Benutzer
                // In diesem Fall den Fehler ignorieren

                var VerbesserteAusnahme = new System.Exception(
                    "Kann bei einem neuen Benutzer ignoriert werden!", ex);

                this.OnFehlerAufgetreten(new FehlerAufgetretenEventArgs(VerbesserteAusnahme));

                // Damit nicht null zurückgegeben wird
                return new Daten.Fensterliste();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Controller.FensterXmlController _Controller = null;

        /// <summary>
        /// Ruft den Dienst zum Speichern bzw. Lesen
        /// der Fensterliste im Xml Format ab
        /// </summary>
        private Controller.FensterXmlController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    this._Controller = this.AppKontext
                        .Produziere<WIFI.Anwendung.Controller.FensterXmlController>();
                }

                return this._Controller;
            }
        }

    }
}
