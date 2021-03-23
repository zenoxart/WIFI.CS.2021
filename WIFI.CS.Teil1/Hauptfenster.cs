using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Für HoleLokalisiertenOrdner
using WIFI.Anwendung.Erweiterungen;

namespace WIFI.CS.Teil1
{
    /// <summary>
    /// Stellt die Oberfläche der Anwendung
    /// zum Lernen einer Komponenten-orientierten
    /// Windows Forms Anwendung bereit
    /// </summary>
    /// <remarks>Verweis auf WIFI.Anwendung
    /// und WIFI.Windows.Forms notwendig</remarks>
    public partial class Hauptfenster : WIFI.Windows.Forms.AppFenster
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private bool _Sprachwechsel = false;

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// der angibt, ob das Fenster
        /// wegen eines Sprachwechsels 
        /// geschlossen wurde
        /// </summary>
        public bool Sprachwechsel
        {
            get
            {
                return this._Sprachwechsel;
            }
            private set
            {
                this._Sprachwechsel = value;
            }
        }

        /// <summary>
        /// Initialisiert ein neues Hauptfenster
        /// </summary>
        public Hauptfenster()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Löst das Load-Ereignis aus
        /// </summary>
        /// <param name="e">Keine Ereignisdaten</param>
        /// <remarks>Wird um zusätzliche Initialisierungen,
        /// z. B. das Laden der Anwendungssprachen ergänzt</remarks>
        protected override void OnLoad(EventArgs e)
        {
            // Zuerst einmal das, was sonst
            // gemacht wird, z. B. Fensterposition wiederherstellen
            base.OnLoad(e);

            // Aus der Infrastruktur die Sprachen
            // in die Liste einfüllen
            this.Sprachen.DataSource = this.AppKontext.Sprachen.StandardListe;
            this.Sprachen.DisplayMember = "Name";
            // Damit die Sprache eindeutig erkannt wird,
            // die Schlüsseleigenschaft bekanntgeben...
            this.Sprachen.ValueMember = "Code";
            // Damit in der Liste die aktuelle Sprache ausgewählt ist
            this.Sprachen.SelectedValue = this.AppKontext.Sprachen.Aktuell.Code;

            // Einen anonymen Ereignisbehandler
            // zum Wechseln der Sprache an die Liste anhängen
            this.Sprachen.SelectedValueChanged += (sender, daten) =>
            {
                //2021017   Eine neue Sprache  nur zulassen,
                //          wenn's eine andere als die aktuelle Sprache ist

                // Die ausgewählte Sprache in die Infrastruktur übernehmen
                //this.AppKontext.Sprachen.Aktuell = this.Sprachen.SelectedItem
                //            as WIFI.Anwendung.Daten.Sprache;
                var NeueSprache = this.Sprachen.SelectedItem
                            as WIFI.Anwendung.Daten.Sprache;

                // Hier wird der Code der Sprache case sensitiv geprüft
                // Das ist zulässig, weil der Benutzer nirgendwo in
                // der Awendung den Code ändern kann
                if (this.AppKontext.Sprachen.Aktuell.Code != NeueSprache.Code)
                {
                    this.AppKontext.Sprachen.Aktuell = NeueSprache;
                    //--20210107

                    // Einstellen, dass wir wegen
                    // eines Sprachwechsels geschlossen werden
                    this.Sprachwechsel = true;

                    // Weil die Sprache geändert wurde,
                    // das Fenster schließen
                    this.Close();
                }
            };

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private IHauptfensterObjekt _AktuellesObjekt = null;

        /// <summary>
        /// Ruft das aktuell angezeigte Objekt ab
        /// oder legt dieses fest
        /// </summary>
        /// <remarks>Als Standardwert wird eine Begrüßung initialisiert.
        /// Wird das AktuelleObjekt neu eingestellt, muss
        /// der Besitzer manuell aktualisiert werden</remarks>
        private IHauptfensterObjekt AktuellesObjekt
        {
            get
            {
                if (this._AktuellesObjekt == null)
                {
                    // Hier wird bewusst die Eigenschaft
                    // und nicht das Feld verwendet.
                    // Dadurch wird implizit der Setter aufgerufen
                    // und dieser schreibt die Informationen
                    // in die Oberfläche (rekursiv)
                    this.AktuellesObjekt = this.AppKontext.Produziere<Begrüßung>();

                    // Hinweis: Hier wird das Paint Ereignis
                    //          automatisch vom System aufgerufen,
                    //          weil es das erste Objekt ist

                    // die neue Begrüßung dem Anwendungsverlauf hinzufügen
                    this.Verlauf.Hinterlegen(this._AktuellesObjekt);
                }

                return this._AktuellesObjekt;
            }
            set
            {
                if (this._AktuellesObjekt != value)
                {
                    this._AktuellesObjekt = value;

                    // Die Daten vom neuen Objekt
                    // in die Oberfläche schreiben
                    this.Titel.Text = this._AktuellesObjekt.Titel;

                    // Dem Objekt mitteilen, 
                    // wo es sich zeichnen soll
                    this._AktuellesObjekt.Besitzer = this.Inhalt;

                    // Hinweis: das Inhalt-Steuerelement
                    //          wird nicht automatisch neu gezeichnet
                    //          Das wollen wir hier aber auch nicht,
                    //          weil sonst beim Starten der
                    //          Anwendung die Begrüßung 2x gezeichnet wird

                    // Eventuell von früheren Objekten
                    // hinterlassene Bildlaufleisten abschalten
                    this.Inhalt.AutoScroll = false;
                }
            }
        }

        /// <summary>
        /// Stellt den Inhalt vom aktuellen Objekt
        /// in die Windows Zwischenablage
        /// </summary>
        private void Kopieren(object sender, EventArgs e)
        {
            this.AktuellesObjekt.Kopieren();
        }

        /// <summary>
        /// Zeigt ein Dialogfenster zum Auswählen
        /// eines Dateinamens und speichert das aktuelle Objekt
        /// </summary>
        private void SpeichernUnter(object sender, EventArgs e)
        {
            using (var Dialog = new System.Windows.Forms.SaveFileDialog())
            {
                Dialog.Title = this.AktuellesObjekt.SpeichernUnterTitel;
                Dialog.Filter = this.AktuellesObjekt.SpeichernUnterTypen;

                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    this.AktuellesObjekt.SpeichernUnter(Dialog.FileName);
                }
            }
        }

        /// <summary>
        /// Zeigt eine Druckvorschau für das aktuelle Objekt
        /// </summary>
        private void SeitenansichtÖffnen(object sender, EventArgs e)
        {
            //using (var Dialog = new WIFI.Windows.Forms.PrintPreviewDialogEx())
            //                      ^-> hier wird unser AppKontext im Dialog
            //                          nicht initialisiert
            using (var Dialog = this.AppKontext.Produziere<WIFI.Windows.Forms.PrintPreviewDialogEx>())
            {
                // Damit der Fenster-Manager einen Namen
                // zum Wiederfinden hat...
                Dialog.Name = "Druckvorschau";

                // Im Gegensatz zum SaveFileDialog
                // wird das Icon nicht automatisch übernommen...
                Dialog.Icon = this.Icon;

                // Dem Dialog die Seitenansicht geben
                Dialog.Document = this.AktuellesObjekt.Druckseite;

                // Anzeigen
                Dialog.ShowDialog();

            }
        }

        /// <summary>
        /// Sorgt dafür, dass sich das aktuelle Objekt neu zeichnet
        /// </summary>
        /// <param name="sender">Verweis auf das Objekt,
        /// das diesen Behandler aufruft</param>
        /// <param name="e">Ereignisdaten mit dem Graphics Objekt</param>
        private void AktuellesObjektZeichnen(object sender, PaintEventArgs e)
        {
            this.AktuellesObjekt.Zeichnen(e.Graphics);
        }

        /// <summary>
        /// Erzwingt ein Neuzeichnen vom Objekt,
        /// wenn sich die Fenstergröße geändert hat
        /// </summary>
        private void AktuellesObjektAktualisieren(object sender, EventArgs e)
        {
            this.Inhalt.Refresh();
        }

        /// <summary>
        /// Abhängig vom Sender die gewünschte Information anzeigen
        /// </summary>
        /// <remarks>Die Dateien werden in einem
        /// Unterordner "Information" des Programmverzeichnisses gesucht</remarks>
        private void InformationZeigen(object sender, EventArgs e)
        {
            var Sender = sender as System.Windows.Forms.LinkLabel;

            if (Sender != null)
            {
                string Dateiname = null;

                switch (Sender.Name)
                {
                    case "AuswahlAllgemein":
                        Dateiname = "Allgemein.txt";
                        break;
                    case "AuswahlStruktur":
                        Dateiname = "Struktur.txt";
                        break;
                    case "AuswahlBinär":
                        Dateiname = "Binär.txt";
                        break;
                    case "AuswahlFall":
                        Dateiname = "Fallentscheidung.txt";
                        break;
                    case "AuswahlZählen":
                        Dateiname = "Zählen.txt";
                        break;
                    case "AuswahlAbweisen":
                        Dateiname = "Abweisen.txt";
                        break;
                    case "AuswahlDurchlaufen":
                        Dateiname = "Durchlaufen.txt";
                        break;
                }

                if (Dateiname != null)
                {
                    var Pfad = System.IO.Path.Combine(
                        this.AppKontext.Anwendungspfad,
                        "Information");

                    Pfad = System.IO.Path.Combine(
                        Pfad.HoleLokalisiertenOrdner(),
                        Dateiname);

                    // Damit die gleiche Information 
                    // nicht mehrmals geöffnet wird,
                    // nur, wenn das aktuelle Objekt
                    // ein anderes ist

                    var Info = this.AktuellesObjekt as Information;

                    if (Info == null || Info.Pfad != Pfad)
                    {
                        Info = this.AppKontext.Produziere<Information>();

                        Info.Pfad = Pfad;

                        this.AktuellesObjekt = Info;
                        this.Verlauf.Hinterlegen(Info);

                        // Ein Neuzeichnen vom Steuerelement erzwingen
                        this.Inhalt.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Schließt das Fenster und
        /// beendet damit die Anwendung
        /// </summary>
        private void Beenden(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// LÖst das FormClosing Ereignis aus
        /// </summary>
        /// <param name="e">Ereignisdaten mit
        /// dem Grund des Schließens und der 
        /// Möglichkeit, das Schließen abzubrechen</param>
        /// <remarks>Hier wird der Benutzer gefragt,
        /// ob er wirklich beenden möchte. Wenn NEIN wird
        /// das Schließen abgebrochen</remarks>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Falls kein Sprachwechsel und BeimBeendenFragen aktiv ist,
            // fragen, ob wirklich beendet werden soll
            if (!this.Sprachwechsel && this.BeimBeendenFragen.Checked)
            {
                var Antwort = System.Windows.Forms.MessageBox
                    .Show(
                    Properties.Resources.BeendenFrage,
                    Properties.Resources.BeendenTitel,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                e.Cancel = Antwort == DialogResult.No;
            }

            // Das machen, was sonst gemacht wird
            // (z. B. die Fensterposition speichern)
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Verlauf _Verlauf = null;

        /// <summary>
        /// Ruft den Dienst zum
        /// Zurück- und Vorwärtsblättern
        /// in der Anwendung ab
        /// </summary>
        protected WIFI.Anwendung.Verlauf Verlauf
        {
            get
            {
                if (this._Verlauf == null)
                {
                    this._Verlauf = new WIFI.Anwendung.Verlauf();

                    // Wichtig, die Ereignis-Behandler
                    // dürfen nur EINMAL zugewiesen werden,
                    // genau hier nach dem Initialisieren

                    this._Verlauf.ZurückMöglich += (sender, e) => this.Zurück.Enabled = true;
                    this._Verlauf.VorwärtsMöglich += (sender, e) => this.Vorwärts.Enabled = true;
                    this._Verlauf.KeinZurück += (sender, e) => this.Zurück.Enabled = false;
                    this._Verlauf.KeinVorwärts += (sender, e) => this.Vorwärts.Enabled = false;
                }

                return this._Verlauf;
            }
        }

        /// <summary>
        /// Abhängig vom Sender das Zurück oder Vorwärts
        /// Objekt holen und zum aktuellen Objekt machen
        /// </summary>
        private void ImVerlaufBewegen(object sender, EventArgs e)
        {
            var Symbol = sender as Button;

            if (Symbol != null)
            {

                object Verlaufsobjekt = null;

                switch (Symbol.Name)
                {
                    case "Zurück":
                        Verlaufsobjekt = this.Verlauf.HoleZurückObjekt();
                        break;

                    case "Vorwärts":
                        Verlaufsobjekt = this.Verlauf.HoleVorwärtsObjekt();
                        break;
                }

                var HauptfensterObjekt
                    = Verlaufsobjekt as IHauptfensterObjekt;

                if (HauptfensterObjekt != null)
                {
                    this.AktuellesObjekt = HauptfensterObjekt;
                    this.Inhalt.Refresh();
                }
            }
        }
    }
}
