using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Zum Auslesen vom Copyright
using WIFI.Anwendung.Erweiterungen;

namespace WIFI.CS.Teil1
{
    /// <summary>
    /// Stellt einen Dienst zum Zeichnen
    /// einer Begrüßung bereit
    /// </summary>
    internal class Begrüßung : WIFI.Anwendung.AppObjekt, IHauptfensterObjekt
    {
        /// <summary>
        /// Ruft den Titel der Begrüßung ab
        /// </summary>
        /// <remarks>Aktuell die kürzeste Möglichkeit
        /// für eine schreibgeschützte Eigenschaft, 
        /// ein anonymer Getter</remarks>
        public string Titel => WIFI.CS.Teil1.Properties.Resources.BegrüßungTitel;

        /// <summary>
        /// Ruft das Steuerelement ab,
        /// in dem sich die Begrüßung zeichnen soll,
        /// oder legt dieses fest
        /// </summary>
        /// <remarks>Hier handelt es sich um
        /// die implizite Eigenschaften Deklaration,
        /// wo der Compiler das Feld verwaltet,
        /// wenn keine zusätzliche Logik notwendig ist</remarks>
        public System.Windows.Forms.Control Besitzer { get; set; }

        /// <summary>
        /// Stellt ein Bild der Begrüßung
        /// in die Windows Zwischenablage
        /// </summary>
        /// <remarks>Als Größe wird die Größe
        /// vom Besitzersteuerelement herangezogen</remarks>
        public void Kopieren()
        {
            using (var Bild = new System.Drawing.Bitmap(
                this.Besitzer.ClientRectangle.Width,
                this.Besitzer.ClientRectangle.Height))
            {
                using (var Graphics = System.Drawing.Graphics.FromImage(Bild))
                {

                    // Damit der Hintergrund nicht Transparent ist...
                    Graphics.Clear(this.Besitzer.BackColor);

                    this.Zeichnen(Graphics);
                }


                System.Windows.Forms.Clipboard.SetImage(Bild);

            }
        }

        /// <summary>
        /// Ruft den Titel für den Speichern unter Dialog ab
        /// </summary>
        public string SpeichernUnterTitel
            => WIFI.CS.Teil1.Properties.Resources.BegrüßungSpeichernUnter;

        /// <summary>
        /// Ruft die Dateitypen für den Speichern unter Dialog ab
        /// </summary>
        public string SpeichernUnterTypen
            => WIFI.CS.Teil1.Properties.Resources.BegrüßungSpeichernTypen;

        /// <summary>
        /// Speichert ein PNG Bitmap der Begrüßung
        /// </summary>
        /// <param name="pfad">Vollständige Pfadangabe
        /// der Datei, die zum Speichern benutzt werden soll</param>
        /// <remarks>Als Größe wird die Größe vom Besitzer benutzt</remarks>
        public void SpeichernUnter(string pfad)
        {
            using (var Bild = new System.Drawing.Bitmap(
                        this.Besitzer.ClientRectangle.Width,
                        this.Besitzer.ClientRectangle.Height))
            {
                using (var Graphics = System.Drawing.Graphics.FromImage(Bild))
                {

                    // Damit der Hintergrund nicht Transparent ist...
                    Graphics.Clear(this.Besitzer.BackColor);

                    this.Zeichnen(Graphics);
                }

                try
                {
                    Bild.Save(pfad, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (System.Exception ex)
                {
                    this.OnFehlerAufgetreten(new WIFI.Anwendung.FehlerAufgetretenEventArgs(ex));
                }
            }
        }

        /// <summary>
        /// Ruft das Objekt zum Ausdrucken eines Bildes der Begrüßung ab
        /// </summary>
        public System.Drawing.Printing.PrintDocument Druckseite
        {
            get
            {
                var Ausdruck = new System.Drawing.Printing.PrintDocument();

                // Ereignisbehandler
                // Ausdruck.BeginPrint += ... notwendig, bei mehrseitigen Ausdrucken
                //                            zum Vorbereiten, z. B. Seitenzähler-Feld, ...

                Ausdruck.PrintPage += (sender, e) => this.Zeichnen(e.Graphics, e.MarginBounds);

                // Ausdruck.EndPrint += ... notwendig, wenn nach dem Druck
                //                          "zusammengeräumt" werden muss
                return Ausdruck;
            }
        }

        /// <summary>
        /// Zeichnet ein Bild der Begrüßung
        /// </summary>
        /// <param name="g">System.Drawing.Graphics Objekt,
        /// das zum Zeichnen benutzt werden soll</param>
        /// <remarks>Das Graphics Objekt vom Paint oder
        /// PrintPage Ereignis beziehen, damit das Bild
        /// nicht flackert</remarks>
        public void Zeichnen(System.Drawing.Graphics g)
        {
            this.Zeichnen(g, this.Besitzer.ClientRectangle);
        }

        /// <summary>
        /// Zeichnet ein Bild der Begrüßung
        /// </summary>
        /// <param name="g">System.Drawing.Graphics Objekt,
        /// das zum Zeichnen benutzt werden soll</param>
        /// <param name="r">Die zu benutzenden Randeinstellungen</param>
        /// <remarks>Das Graphics Objekt vom Paint oder
        /// PrintPage Ereignis beziehen, damit das Bild
        /// nicht flackert</remarks>
        protected virtual void Zeichnen(System.Drawing.Graphics g, System.Drawing.Rectangle r)
        {
            const string SchriftOben = "Arial";
            const int SchriftgradOben = 12;
            const string SchriftMitte = "Times New Roman";
            const int SchriftgradMitte = 16;
            const string SchriftUnten = "Arial Narrow";
            const int SchriftgradUnten = 10;

            float LogoY = 0;

            #region Oben der Titel
            using (var Schrift = new System.Drawing.Font(SchriftOben, SchriftgradOben))
            {
                var Platzbedarf = g.MeasureString(Properties.Resources.BegrüßungTitel, Schrift);

                float Y = r.Top;

                // Mit einer Zeile Abstand das Logo
                LogoY = r.Top + 2 * Platzbedarf.Height;

                float X = r.Left + (r.Width - Platzbedarf.Width) / 2;

                g.DrawString(
                    Properties.Resources.BegrüßungTitel,
                    Schrift,
                    System.Drawing.Brushes.DarkBlue,
                    X,
                    Y);
            }
            #endregion Oben der Titel

            #region C# Logo


            // Weil DrawImage das Bild in einer
            // nicht gewünschten Größe zeichnet,
            // das Bild aus den Ressourcen skalieren
            float Skalierung = 0.8f;

            using (var Bild = new System.Drawing.Bitmap(
                Properties.Resources.siSharp, 
                (int)(Properties.Resources.siSharp.Width * Skalierung), 
                (int)(Properties.Resources.siSharp.Height * Skalierung)))
            {
                float X = r.Left + (r.Width - Bild.Width) / 2;
                // Die Y Koordinate wird im Zeichnen vom Titel berechnet

                g.DrawImage(Bild, X, LogoY);
            }

            #endregion C# Logo

            #region In der Mitte der Name der Anwendung
            using (var Schrift = new System.Drawing.Font(SchriftMitte, SchriftgradMitte))
            {
                var Platzbedarf = g.MeasureString(Properties.Resources.BegrüßungZeile1, Schrift);

                float Y = r.Top + r.Height / 2 - Platzbedarf.Height;
                float X = r.Left + (r.Width - Platzbedarf.Width) / 2;

                g.DrawString(
                    Properties.Resources.BegrüßungZeile1,
                    Schrift,
                    System.Drawing.Brushes.Black,
                    X,
                    Y);

                Platzbedarf = g.MeasureString(Properties.Resources.BegrüßungZeile2, Schrift);

                Y = r.Top + r.Height / 2;
                X = r.Left + (r.Width - Platzbedarf.Width) / 2;

                g.DrawString(
                    Properties.Resources.BegrüßungZeile2,
                    Schrift,
                    System.Drawing.Brushes.Black,
                    X,
                    Y);
            }
            #endregion In der Mitte der Name der Anwendung

            #region Unten das Copyright
            using (var Schrift = new System.Drawing.Font(SchriftUnten, SchriftgradUnten))
            {
                var Copyright = this.HoleCopyright();
                var Platzbedarf = g.MeasureString(Copyright, Schrift);

                float Y = r.Top + r.Height - Platzbedarf.Height;
                float X = r.Left + r.Width - Platzbedarf.Width;

                g.DrawString(
                    Copyright,
                    Schrift,
                    System.Drawing.Brushes.DarkGray,
                    X,
                    Y);
            }
            #endregion Unten das Copyright
        }
    }
}
