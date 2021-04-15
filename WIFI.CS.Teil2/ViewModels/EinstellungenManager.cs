using System;

namespace WIFI.CS.Teil2.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// der Einstellungspunkte bereit
    /// </summary>
    public class EinstellungenManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.EinstellungenXmlController _Controller = null;

        /// <summary>
        /// Ruft den Dienst zum Laden der
        /// Anwendungspunkte ab
        /// </summary>
        private Models.EinstellungenXmlController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    this._Controller = this.AppKontext.Produziere<Models.EinstellungenXmlController>();
                }

                return this._Controller;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        /// <remarks>Da sich die Einstellungen innerhalb einer Anwendung nicht ändern,
        /// wird hier statisch gearbeitet</remarks>
        private static Models.Einstellungen _Liste = null;

        /// <summary>
        /// Ruft die aktuell unterstützten
        /// Einstellungspunkte ab.
        /// </summary>
        public Models.Einstellungen EinstellungenListe
        {
            get
            {
                if (EinstellungenManager._Liste == null
                    // Die Daten auch holen, wenn sie noch nicht bereitstehen
                    // Das passiert, wenn das Holen länger dauert und die
                    // Benutzer bereits ein neues Fenster geöffnet haben
                    || EinstellungenManager._Liste[0].Name == Properties.Texte.DatenHolen)
                {
                    // Hier wäre die Oberfläche während des Holens blockiert
                    //this._Liste = this.Controller.HoleAusRessourcen();

                    // Damit Benutzer sehen, dass etwas passiert...
                    EinstellungenManager._Liste = new Models.Einstellungen
                    {
                        new Models.Einstellung {
                            Name=Properties.Texte.DatenHolen,
                            Symbol="6"
                        }
                    };


                    this.InitialisiereEinstellungenAsync();
                }

                return EinstellungenManager._Liste;
            }
            protected set
            {
                EinstellungenManager._Liste = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// ob das Initialisieren der Einstellungen 
        /// aktuell läuft oder nicht
        /// </summary>
        private bool InitialisiereEinstellungenLäuft { get; set; }

        /// <summary>
        /// Füllt die Liste mit den Einstellungen
        /// asynchron in einem eigenen Thread
        /// </summary>
        protected virtual async void InitialisiereEinstellungenAsync()
        {
            // Damit die Thread-Methode nicht mehrmals gestartet wird
            if (this.InitialisiereEinstellungenLäuft)
            {
                return;
            }

            await System.Threading.Tasks.Task.Run(
                () =>
                {
                    this.InitialisiereEinstellungenLäuft = true;
                    // SO NIE, nicht mit dem Feld!!!
                    //this._Liste = this.Controller.HoleAusRessourcen();
                    // a) Damit WPF mitbekommt, dass sich die Liste
                    //    geändert hat, wird PropertyChanged benötigt
                    // b) Weil kein Thread in die Daten von einem
                    //    anderen Thread greifen darf, nicht hier
                    //    das PropertyChanged. Das muss in der Eigenschaft sein!

                    this.StartProtokollieren();


                    this.EinstellungenListe = this.Controller.HoleAusRessourcen();

                    this.EndeProtokollieren();
                    this.InitialisiereEinstellungenLäuft = false;
                }
                );

            // Nachdem die Liste abgerufen wurde,
            // die zuletzt benutzte Aufgabe einstellen

            // Die Einstellung aus der Konfiguration
            // in einer Variable abkürzen und beim
            // Benutzen sauber prüfen, ob der Wert
            // im gültigen Bereich. Spezialbenutzer
            // könnten herumgepfuscht haben
            var i = Properties.Settings.Default.IndexAktuelleEinstellung;

            if (this.EinstellungenListe != null && i >= 0 && i < this.EinstellungenListe.Count)
            {
                this.AktuelleEinstellung = this.EinstellungenListe[i];
            }

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.Collections.Hashtable _VorhandeneViewer = null;

        /// <summary>
        /// Ruft den Cache mit bereits initialisierten
        /// Einstellungen-Viewer ab
        /// </summary>
        protected System.Collections.Hashtable VorhandeneViewer
        {
            get
            {
                if (this._VorhandeneViewer == null)
                {
                    this._VorhandeneViewer = new System.Collections.Hashtable(this.EinstellungenListe.Count);
                    this.AppKontext.Protokoll.Eintragen(
                        $"{this} hat den Cache für die Viewer initialisiert...",
                        WIFI.Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);
                }

                return this._VorhandeneViewer;
            }
        }

        /// <summary>
        /// Ruft das Design aus den Ressourcen ab, 
        /// oder setzt dieses
        /// </summary>
        public bool DunklesDesign
        {
            get { return Properties.Settings.Default.DunklesDesign; }
            set
            {
                Properties.Settings.Default.DunklesDesign = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.Einstellung _AktuelleEinstellung = null;


        /// <summary>
        /// Ruft den im Cache der Anwendung benutzten
        /// Schlüssel für die aktuelle Aufgabe ab
        /// </summary>
        private string SchlüsselAktuelleEinstellung => this.GetType().FullName + ".AktuelleEinstellung";

        /// <summary>
        /// Ruft den aktuellen Anwendungspunkt ab, oder legt diesen fest
        /// </summary>
        public Models.Einstellung AktuelleEinstellung
        {
            get
            {
                // Wenn die private Eigenschaft nicht null ist
                if (this._AktuelleEinstellung == null)
                {

                    // Suche im Cache nach dem Schlüssel STRING
                    if (this.AppKontext.Cache.ContainsKey(this.SchlüsselAktuelleEinstellung))
                    {
                        this._AktuelleEinstellung = this.AppKontext.Cache[this.SchlüsselAktuelleEinstellung]
                            as Models.Einstellung;
                    }

                }

                return this._AktuelleEinstellung;
            }
            set
            {

                if (this._AktuelleEinstellung != value)
                {
                    this._AktuelleEinstellung = value;
                    OnPropertyChanged();

                    // Damit ist der aktive Viewer ungültig
                    this.AppKontext.Cache[
                        this.SchlüsselAktuelleEinstellung
                        ] = this.AktuelleEinstellung;

                    // Außerdem die Aufgabe in der Konfiguration
                    // hinterlegen, damit diese bei einem Neustart
                    // wieder ausgewählt werden kann
                    // Index aus den Ressourcen geladen
                    Properties.Settings.Default.IndexAktuelleEinstellung
                        = this.EinstellungenListe.IndexOf(this._AktuelleEinstellung);

                    this.AktiverViewer = null;
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private object _AktiverViewer;

        /// <summary>
        /// Ruft den Arbeitsbereich der
        /// aktuellen Aufgabe ab
        /// </summary>
        public object AktiverViewer
        {
            get
            {
                if (this._AktiverViewer == null)
                {
                    this.InitialisiereAktivenViewer();

                }

                return this._AktiverViewer;
            }
            protected set
            {
                this._AktiverViewer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Für die aktuelle Einstellung den
        /// Einstellungsbereich initialisieren
        /// und in der AktiverViewer Eigenschaft
        /// hinterlegen
        /// </summary><remarks>
        /// Bereits produzierte Viewer werden gecachet</remarks>
        protected virtual void InitialisiereAktivenViewer()
        {

            // Das Initialisieren ist nur sinnvoll,
            // wenn a) eine AktuelleAufgabe vorhanden ist
            // und b) die Liste mit den Aufgaben abgerufen wurde

            if (this.AktuelleEinstellung != null &&
                this.AktuelleEinstellung.Name != Properties.Texte.DatenHolen)
            {


                this.StartProtokollieren();

                object Viewer = null;

                // Prüfen, ob der Viewer bereits vorhanden ist
                if (this.VorhandeneViewer.ContainsKey(this.AktuelleEinstellung.ViewerName))
                {
                    Viewer = this.VorhandeneViewer[this.AktuelleEinstellung.ViewerName];
                }

                // Falls nicht, erstellen und merken
                else
                {

                    try
                    {
                        Viewer = System.Activator.CreateInstance(
                            Type.GetType(this.AktuelleEinstellung.ViewerName));
                    }
                    catch (Exception ex)
                    {
                        Viewer = new object();

                        var FehlerBeschreibung =
                            new System.Exception(
                                $"Keine {this.AktuelleEinstellung.ViewerName} Klasse gefunden!",
                                ex);

                        this.OnFehlerAufgetreten(new WIFI.Anwendung.FehlerAufgetretenEventArgs(FehlerBeschreibung));
                    }
                    this.VorhandeneViewer.Add(this.AktuelleEinstellung.ViewerName, Viewer);

                }
                this.AktiverViewer = Viewer;
                this.EndeProtokollieren();
            }


        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.CS.Teil2.ViewModels.Anwendung _Anwendung;

        /// <summary>
        /// Stellt den Haupt-Appkontext als Property zur Verfügung
        /// </summary>
        public WIFI.CS.Teil2.ViewModels.Anwendung Anwendung
        {
            get { return this._Anwendung; }
            set { this._Anwendung = value; }
        }

    }
}
