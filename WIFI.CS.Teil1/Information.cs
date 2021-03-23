using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Teil1
{
    /// <summary>
    /// Stellt einen Dienst zum
    /// Anzeigen eines Informationstextes bereit
    /// </summary>
    internal class Information : WIFI.Anwendung.AppObjekt, IHauptfensterObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Titel = null;

        /// <summary>
        /// Ruft den Titel der Information ab
        /// </summary>
        /// <remarks>Die erste Zeile aus
        /// der Informationsdatei</remarks>
        public string Titel
        {
            get
            {
                if (this._Titel == null)
                {
                    this.DateiLesen();
                }

                return this._Titel;
            }
            protected set
            {
                if (this._Titel != value)
                {
                    this._Titel = value;
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Text = null;

        /// <summary>
        /// Ruft den Inhalt der Info
        /// ohne Titel ab
        /// </summary>
        public string Text
        {
            get
            {
                if (this._Text == null)
                {
                    this.DateiLesen();
                }

                return this._Text;
            }
            protected set
            {
                if (this._Text != value)
                {
                    this._Text = value;
                }
            }
        }

        /// <summary>
        /// Ruft das Steuerelement ab,
        /// in dem die Information angezeigt werden soll,
        /// oder legt dieses fest
        /// </summary>
        /// <remarks>Hier handelt es sich um
        /// die implizite Eigenschaften Deklaration,
        /// wo der Compiler das Feld verwaltet,
        /// wenn keine zusätzliche Logik notwendig ist</remarks>
        public System.Windows.Forms.Control Besitzer { get; set; }

        /// <summary>
        /// Stellt den Text der Information
        /// in die Windows Zwischenablage
        /// </summary>
        public void Kopieren()
        {
            System.Windows.Forms.Clipboard.SetText(this.Inhalt);
        }

        /// <summary>
        /// Ruft den Titel für den Speichern unter Dialog ab
        /// </summary>
        public string SpeichernUnterTitel
            => WIFI.CS.Teil1.Properties.Resources.InformationSpeichernUnter;

        /// <summary>
        /// Ruft die Dateitypen für den Speichern unter Dialog ab
        /// </summary>
        public string SpeichernUnterTypen
            => WIFI.CS.Teil1.Properties.Resources.InformationSpeichernTypen;

        /// <summary>
        /// Speichert die Information als unformatierten Text
        /// </summary>
        /// <param name="pfad">Vollständige Pfadangabe
        /// der Datei, die zum Speichern benutzt werden soll</param>
        public void SpeichernUnter(string pfad)
        {
            try
            {
                using (var Schreiber = new System.IO.StreamWriter(
                                pfad,
                                append: false,
                                System.Text.Encoding.Default))
                {
                    Schreiber.WriteLine(this.Inhalt);
                }
            }
            catch (System.Exception ex)
            {
                this.OnFehlerAufgetreten(
                    new WIFI.Anwendung.FehlerAufgetretenEventArgs(ex));
            }
        }

        /// <summary>
        /// Ruft das Objekt zum Ausdrucken der Information ab
        /// </summary>
        public System.Drawing.Printing.PrintDocument Druckseite
        //                                              ^-> der Name einer Eigenschaft
        //                                  ^-> Der Datentyp der Eigenschaft
        {
            get // <- die Methode, mit der der Wert der Eigenschaft
                //    zurückgegeben wird, d.h. "abgerufen wird"
            {
                var Ausdruck = new System.Drawing.Printing.PrintDocument();
                //             | --- Objekt, der Wert der Eigenschaft --- |

                Ausdruck.PrintPage += (sender, e) 
                    => this.Zeichnen(
                        e.Graphics, 
                        e.MarginBounds, 
                        ausdruck: true);

                return Ausdruck;
            }
        }

        /// <summary>
        /// Zeichnet den Text der Information
        /// </summary>
        /// <param name="g">System.Drawing.Graphics Objekt,
        /// das zum Zeichnen benutzt werden soll</param>
        /// <remarks>Das Graphics Objekt vom Paint oder
        /// PrintPage Ereignis beziehen, damit das Bild
        /// nicht flackert. Falls der Besitzer Bildlaufleisten
        /// unterstützt, werden diese aktiviert, wenn
        /// der Text nicht Platz hat</remarks>
        public void Zeichnen(System.Drawing.Graphics g)
        {
            this.Zeichnen(
                g,
                this.Besitzer.ClientRectangle,
                ausdruck: false);
        }

        /// <summary>
        /// Zeichnet den Text der Information
        /// </summary>
        /// <param name="g">System.Drawing.Graphics Objekt,
        /// das zum Zeichnen benutzt werden soll</param>
        /// <param name="r">Die zu benutzenden Randeinstellungen</param>
        /// <param name="ausdruck">True, falls die Methode zum Drucken
        /// benutzt wird, sonst False</param>
        /// <remarks>Das Graphics Objekt vom Paint oder
        /// PrintPage Ereignis beziehen, damit das Bild
        /// nicht flackert. Falls der Besitzer Bildlaufleisten
        /// unterstützt, werden diese aktiviert, wenn
        /// der Text nicht Platz hat</remarks>
        protected virtual void Zeichnen(System.Drawing.Graphics g, System.Drawing.Rectangle r, bool ausdruck)
        {
            // Eine Schrift mit fixer Zeichenbreite benutzen,
            // weil es ein unformatierter Text ist
            const string Schriftart = "Courier New";
            const int SchriftGrad = 11;

            using (var Schrift = new System.Drawing.Font(
                        Schriftart, SchriftGrad))
            {
                if (ausdruck)
                {
                    g.DrawString(
                        this.Inhalt, 
                        Schrift, 
                        System.Drawing.Brushes.Black, 
                        r.Left, 
                        r.Top);
                }
                else
                {
                    // Die Ausgabe um die Einstellungen
                    // der Bildlaufleisten korrigieren
                    float dX = 0;
                    float dY = 0;

                    // Prüfen, ob der Besitzer ein
                    // ScrollableControl ist
                    var ScrollableControl = this.Besitzer 
                        as System.Windows.Forms.ScrollableControl;

                    // Falls der Besitzer ein ScrollableControl ist,
                    // die Bildlaufleisten aktivieren und mitteilen,
                    // wie groß der Inhalt ist
                    if (ScrollableControl != null)
                    {
                        ScrollableControl.AutoScroll = true;
                        ScrollableControl.AutoScrollMinSize
                            = g.MeasureString(this.Text, Schrift).ToSize();

                        // Die aktuelle Position der Bildlaufleisten
                        // in die DeltaX und DeltaY Variablen übernehmen
                        dX = ScrollableControl.AutoScrollPosition.X;
                        dY = ScrollableControl.AutoScrollPosition.Y;
                    }

                    g.DrawString(
                        this.Text, 
                        Schrift, 
                        System.Drawing.Brushes.Black, 
                        r.Left + dX, 
                        r.Top + dY);
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Pfad = string.Empty;

        /// <summary>
        /// Ruft den vollständigen Dateinamen
        /// der unformatierten Textdatei mit
        /// dem Inhalt der Information ab
        /// oder legt diesen fest
        /// </summary>
        public string Pfad
        {
            get
            {
                // Wir können den Pfad nicht berechnen
                return this._Pfad;
                // Tipp von Herrn Hummer
                //  In solchen Fällen den String,
                //  weil's ein Verweistyp ist,
                //  IMMER MIT EINEM LEERSTRING INITIALISIEREN
            }

            set
            {
                if (this._Pfad != value)
                {
                    this._Pfad = value;

                    // Es kann sein, dass ein Objekt-Benutzer
                    // den Pfad ändert, obwohl die Datei
                    // früher schon gelesen wurde
                    // Damit unsere gecachte Eigenschaften
                    // in diesem Fall ungültig werden,
                    // die Felder zurücksetzen...
                    this.Titel = null;
                    this.Text = null;
                }
            }
        }

        /// <summary>
        /// Liest den Inhalt der Datei im Pfad
        /// in die Titel und Text Eigenschaften
        /// </summary>
        protected virtual void DateiLesen()
        {
            try
            {
                using (var Datei = new System.IO.StreamReader(
                            this.Pfad,
                            System.Text.Encoding.Default))
                {
                    // Die erste Zeile soll laut Analyse der Titel sein
                    this.Titel = Datei.ReadLine().Trim();

                    // Der Rest ist der Informationstext
                    this.Text = Datei.ReadToEnd().Trim();
                }
            }
            catch (System.Exception ex)
            {
                this.OnFehlerAufgetreten(
                    new WIFI.Anwendung.FehlerAufgetretenEventArgs(ex));

                // Damit keine Schleife entsteht,
                // weil das Zeichnen ja ständig aufgerufen wird,
                // im Fehlerfall leere Texte in die Eigenschaften schreiben
                this.Titel = string.Empty;
                this.Text = string.Empty;
                // Dadurch wird nicht versucht, die
                // fehlerhafte Datei wiederholt zu lesen
            }
        }

        /// <summary>
        /// Ruft die gesamte Information ab
        /// </summary>
        /// <remarks>d.h. der Titel mit einer
        /// Leerzeile und dem Text</remarks>
        // 20210112 Hr. Plaimer: den hardcoded Zeilenvorschub
        //                       durch die System Einstellung ersetzen
        //public string Inhalt => $"{this.Titel}\r\n\r\n{this.Text}";
        public string Inhalt => $"{this.Titel}{Environment.NewLine}{Environment.NewLine}{this.Text}";
    }
}
