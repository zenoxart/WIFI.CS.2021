using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen
    /// einer unformatierten Textdatei
    /// bereit und ermöglicht 
    /// verschiedene Ausgaben
    /// </summary>
    internal class Textdatei : Entwicklungsbasis
    {
        /// <summary>
        /// Internes Feld für die
        /// Pfad-Eigenschaft
        /// </summary>
        private string _Pfad = string.Empty;

        /// <summary>
        /// Ruft den vollständigen Pfad der Datei ab
        /// oder legt diesen fest.
        /// </summary>
        public string Pfad
        {
            get
            {
                // Wird vom Compiler zu string GetPfad()
                //                      ================
                return this._Pfad;
            }
            set
            {
                // Wird vom Compiler zu void SetPfad(string ???)
                //                      ========================
                // Der Parameter wird vom Compiler "value" genannt,
                // ein C# Schlüsselwort

                this._Pfad = value;

                Textdatei.Schreibe(
                    $"Textdatei.Pfad=\"{this._Pfad}\"",
                    AusgabeModus.Debug);

                // Weil ein neuer Pfad vorhanden ist,
                // muss die Datei neu gelesen werden.
                // Dazu den Cache für die Zeilen löschen
                this._Zeilen = null;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.Collections.ArrayList _Zeilen = null;

        /// <summary>
        /// Ruft die Zeilen der Datei ab
        /// </summary>
        /// <remarks>Beim ersten Zugriff oder wenn sich der Pfad ändert
        /// wird die Datei automatisch gelesen.</remarks>
        public System.Collections.ArrayList Zeilen
        {
            get
            {
                if (this._Zeilen == null)
                {
                    this._Zeilen = new System.Collections.ArrayList();

                    Textdatei.Schreibe(
                                $"Textdatei hat die Liste für die Zeilen initialisiert...",
                                AusgabeModus.Debug);

                    this.Lesen();
                }

                return this._Zeilen;
            }
        }


        /// <summary>
        /// Liest den Inhalt der Datei
        /// zeilenweise in die Zeilen Eigenschaft
        /// </summary>
        /// <remarks>Das eigentliche Beispiel
        /// für die Abweiseschleife</remarks>
        protected void Lesen()
        {
            Textdatei.Schreibe("Textdatei.Lesen startet...", AusgabeModus.Debug);

            // Weil der StreamReader Konstruktor Ausnahmen auslöst...
            try
            {
                //Damit Dispose() automatisch aufgerufen wird
                using (var Leser = new System.IO.StreamReader(this.Pfad,System.Text.Encoding.Default))
                {

                   while (!Leser.EndOfStream)
                    {
                        this.Zeilen.Add(Leser.ReadLine().Trim());
                    }

                } //<- hier wird Dispose() und damit Close() automatisch aufgerufen

                Textdatei.Schreibe(
                    $"Textdatei hat {this.Zeilen.Count} Zeilen gefunden...", 
                    AusgabeModus.Debug);
            }
            catch (System.Exception ex)
            {
                //Im Fehlerfall den Code vom Objekt-Besitzer aufrufen,
                //also "das Ereignis auslösen", 
                //geerbt von der Entwicklungsbasis
                this.OnFehlerAufgetreten(ex);
            }

            Textdatei.Schreibe("Textdatei.Lesen beendet.", AusgabeModus.Debug);
        }

        /// <summary>
        /// Gibt den Inhalt der Datei als linksbündigen
        /// Text mit der angegebenen maximalen Zeilenlänge zurück.
        /// </summary>
        /// <param name="maxZeilenlänge">Stellt die maximale Anzahl von Zeichen in einer Zeilen ein.</param>
        public string HoleFließtext(int maxZeilenlänge)
        {

            Textdatei.Schreibe(
                "Textdatei.HoleFließtext startet...", 
                AusgabeModus.Debug);

            // Speicher reservieren
            var Text = new System.Text.StringBuilder();
            int AktuelleZeilenlänge = 0;

            //Jedes Element in den Wörter abarbeiten
            foreach (string Wort in this.Zeilen)
            {

                //Leere Wörter sollen einen Absatz ergeben...
                if (Wort == string.Empty)
                {
                    //Die aktuelle Zeile beenden
                    Text.AppendLine();
                    //Für den Absatz...
                    Text.AppendLine();
                    AktuelleZeilenlänge = 0;
                }
                else
                {
                    //Prüfen, ob das aktuelle Wort noch Platz hat
                    //Falls nicht eine neue Zeile beginnen
                    if (AktuelleZeilenlänge + Wort.Length + 1 > maxZeilenlänge)
                    {
                        Text.AppendLine();
                        AktuelleZeilenlänge = 0;
                    }

                    //Wort mit Leerzeichen schreiben
                    Text.Append(Wort + " ");
                    /*
                    AktuelleZeilenlänge = AktuelleZeilenlänge + Wort.Length + 1; 
                    */
                    AktuelleZeilenlänge += Wort.Length + 1;
                }
            }

            Textdatei.Schreibe("Textdatei.HoleFließtext beendet.", AusgabeModus.Debug);

            return Text.ToString().TrimEnd();
        }
    }
}
