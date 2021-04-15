using System;

namespace WIFI.CS.Teil2.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten
    /// der Anwendungspunkte bereit
    /// </summary>
    public class AufgabenManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.AufgabenXmlController _Controller = null;

        /// <summary>
        /// Ruft den Dienst zum Laden der
        /// Anwendungspunkte ab
        /// </summary>
        private Models.AufgabenXmlController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    this._Controller = this.AppKontext.Produziere<Models.AufgabenXmlController>();
                }

                return this._Controller;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        /// <remarks>Da sich die Aufgaben innerhalb einer Anwendung nicht ändern,
        /// wird hier statisch gearbeitet</remarks>
        private static Models.Aufgaben _Liste = null;

        /// <summary>
        /// Ruft die aktuell unterstützten
        /// Anwendungspunkte ab.
        /// </summary>
        public Models.Aufgaben Liste
        {
            get
            {
                if (AufgabenManager._Liste == null
                    // Die Daten auch holen, wenn sie noch nicht bereitstehen
                    // Das passiert, wenn das Holen länger dauert und die
                    // Benutzer bereits ein neues Fenster geöffnet haben
                    || AufgabenManager._Liste[0].Name == Properties.Texte.DatenHolen)
                {
                    // Hier wäre die Oberfläche während des Holens blockiert
                    //this._Liste = this.Controller.HoleAusRessourcen();

                    // Damit Benutzer sehen, dass etwas passiert...
                    AufgabenManager._Liste = new Models.Aufgaben
                    {
                        new Models.Aufgabe {
                            Name=Properties.Texte.DatenHolen,
                            Symbol="6"
                        }
                    };


                    this.InitialisiereAufgabenAsync();
                }

                return AufgabenManager._Liste;
            }
            protected set
            {
                AufgabenManager._Liste = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Füllt die Liste mit den Aufgaben
        /// asynchron in einem eigenen Thread
        /// </summary>
        protected virtual async void InitialisiereAufgabenAsync()
        {
            // Damit die Thread-Methode nicht mehrmals gestartet wird
            if (this.InitialisiereAufgabenLäuft)
            {
                return;
            }

            await System.Threading.Tasks.Task.Run(
                () =>
                {
                    this.InitialisiereAufgabenLäuft = true;
                    // SO NIE, nicht mit dem Feld!!!
                    //this._Liste = this.Controller.HoleAusRessourcen();
                    // a) Damit WPF mitbekommt, dass sich die Liste
                    //    geändert hat, wird PropertyChanged benötigt
                    // b) Weil kein Thread in die Daten von einem
                    //    anderen Thread greifen darf, nicht hier
                    //    das PropertyChanged. Das muss in der Eigenschaft sein!

                    this.StartProtokollieren();


                    this.Liste = this.Controller.HoleAusRessourcen();

                    this.EndeProtokollieren();
                    this.InitialisiereAufgabenLäuft = false;
                    this.KreisProzessbarSichtbarkeit = false;
                }
                );

            // Nachdem die Liste abgerufen wurde,
            // die zuletzt benutzte Aufgabe einstellen

            // Die Einstellung aus der Konfiguration
            // in einer Variable abkürzen und beim
            // Benutzen sauber prüfen, ob der Wert
            // im gültigen Bereich. Spezialbenutzer
            // könnten herumgepfuscht haben
            var i = Properties.Settings.Default.IndexAktuelleAufgabe;

            if (this.Liste != null && i >= 0 && i < this.Liste.Count)
            {
                this.AktuelleAufgabe = this.Liste[i];
            }

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.Aufgabe _AktuelleAufgabe = null;

        /// <summary>
        /// Ruft den im Cache der Anwendung benutzten
        /// Schlüssel für die aktuelle Aufgabe ab
        /// </summary>
        private string SchlüsselAktuelleAufgabe => this.GetType().FullName + ".AktuelleAufgabe";



        /// <summary>
        /// Ruft den aktuellen Anwendungspunkt ab, oder legt diesen fest
        /// </summary>
        public Models.Aufgabe AktuelleAufgabe
        {
            get
            {
                // Wenn die private Eigenschaft nicht null ist
                if (this._AktuelleAufgabe == null)
                {

                    // Suche im Cache nach dem Schlüssel STRING
                    if (this.AppKontext.Cache.ContainsKey(this.SchlüsselAktuelleAufgabe))
                    {
                        this._AktuelleAufgabe = this.AppKontext.Cache[this.SchlüsselAktuelleAufgabe]
                            as Models.Aufgabe;
                    }

                }

                return this._AktuelleAufgabe;
            }
            set
            {

                if (this._AktuelleAufgabe != value)
                {
                    this._AktuelleAufgabe = value;
                    OnPropertyChanged();

                    // Damit ist der aktive Viewer ungültig
                    this.AppKontext.Cache[
                        this.SchlüsselAktuelleAufgabe
                        ] = this.AktuelleAufgabe;

                    // Außerdem die Aufgabe in der Konfiguration
                    // hinterlegen, damit diese bei einem Neustart
                    // wieder ausgewählt werden kann
                    // Index aus den Ressourcen geladen
                    Properties.Settings.Default.IndexAktuelleAufgabe
                        = this.Liste.IndexOf(this._AktuelleAufgabe);

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
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.Collections.Hashtable _VorhandeneViewer = null;

        /// <summary>
        /// Ruft den Cache mit bereits initialisierten
        /// Aufgaben-Viewer ab
        /// </summary>
        protected System.Collections.Hashtable VorhandeneViewer
        {
            get
            {
                if (this._VorhandeneViewer == null)
                {
                    this._VorhandeneViewer = new System.Collections.Hashtable(this.Liste.Count);
                    this.AppKontext.Protokoll.Eintragen(
                        $"{this} hat den Cache für die Viewer initialisiert...",
                        WIFI.Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);
                }

                return this._VorhandeneViewer;
            }
        }

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// ob das Initialisieren der Aufgaben 
        /// aktuell läuft oder nicht
        /// </summary>
        private bool InitialisiereAufgabenLäuft { get; set; }

        /// <summary>
        /// Für die aktuelle Aufgabe den
        /// Arbeitsbereich initialisieren
        /// und in der AktiverViewer Eigenschaft
        /// hinterlegen
        /// </summary><remarks>
        /// Bereits produzierte Viewer werden gecachet</remarks>
        protected virtual void InitialisiereAktivenViewer()
        {

            // Das Initialisieren ist nur sinnvoll,
            // wenn a) eine AktuelleAufgabe vorhanden ist
            // und b) die Liste mit den Aufgaben abgerufen wurde

            if (this.AktuelleAufgabe != null &&
                this.AktuelleAufgabe.Name != Properties.Texte.DatenHolen)
            {


                this.StartProtokollieren();

                object Viewer = null;

                // Prüfen, ob der Viewer bereits vorhanden ist
                if (this.VorhandeneViewer.ContainsKey(this.AktuelleAufgabe.ViewerName))
                {
                    Viewer = this.VorhandeneViewer[this.AktuelleAufgabe.ViewerName];
                }

                // Falls nicht, erstellen und merken
                else
                {

                    try
                    {
                        Viewer = System.Activator.CreateInstance(
                            Type.GetType(this.AktuelleAufgabe.ViewerName));
                    }
                    catch (Exception ex)
                    {
                        Viewer = new object();

                        var FehlerBeschreibung =
                            new System.Exception(
                                $"Keine {this.AktuelleAufgabe.ViewerName} Klasse gefunden!",
                                ex);

                        this.OnFehlerAufgetreten(new WIFI.Anwendung.FehlerAufgetretenEventArgs(FehlerBeschreibung));
                    }
                    this.VorhandeneViewer.Add(this.AktuelleAufgabe.ViewerName, Viewer);

                }
                this.AktiverViewer = Viewer;
                this.EndeProtokollieren();
            }


        }


        /// <summary>
        /// Ruft die Sichbarkeit der Prozessbar ab
        /// oder setzt diese
        /// </summary>
        public bool? KreisProzessbarSichtbarkeit
        {
            get
            {


                if (this.AppKontext._KreisProzessbarSichtbarkeit == null)
                {
                    this.AppKontext._KreisProzessbarSichtbarkeit = true;
                }

                return this.AppKontext._KreisProzessbarSichtbarkeit;
            }
            set
            {
                this.AppKontext._KreisProzessbarSichtbarkeit = value;
                OnPropertyChanged();
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



    }
}
