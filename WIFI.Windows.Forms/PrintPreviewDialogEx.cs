using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIFI.Anwendung;

namespace WIFI.Windows.Forms
{
    /// <summary>
    /// Stellt einen Windows Forms PrintPreviewDialog
    /// mit der WIFI Infrastruktur bereit
    /// </summary>
    /// <remarks>Verweis auf WIFI.Anwendung notwendig</remarks>
    public class PrintPreviewDialogEx : System.Windows.Forms.PrintPreviewDialog, WIFI.Anwendung.IAppKontext
    {
        /// <summary>
        /// Ruft die Anwendungsinfrastruktur
        /// ab oder legt diese fest
        /// </summary>
        public AppKontext AppKontext { get; set; }

        /// <summary>
        /// Löst das Load Ereignis aus
        /// </summary>
        /// <param name="e">Keine Ereignisdaten</param>
        protected override void OnLoad(EventArgs e)
        {
            // Am Anfang das machen,
            // was sonst gemacht wird
            base.OnLoad(e);

            // Jetzt noch die Fensterposition wiederherstellen,
            // wenn wir nicht im Designer vom Studio sind,
            // weil im Designer unsere Infrastruktur fehlt
            if (!this.DesignMode)
            {
                // Eine vorhandene FensterInfo holen
                var FensterInfo = this.AppKontext.Fenster.Abrufen(this.Name);

                // Wenn diese vorhanden ist
                if (FensterInfo != null)
                {
                    // Die Positionsdaten nur, wenn welche vorhanden sind

                    this.Left = FensterInfo.Links ?? this.Left;
                    this.Top = FensterInfo.Oben ?? this.Top;

                    // Die Breite und Höhe nur, falls veränderbar
                    if (this.FormBorderStyle == FormBorderStyle.Sizable)
                    {
                        this.Width = FensterInfo.Breite ?? this.Width;
                        this.Height = FensterInfo.Höhe ?? this.Height;
                    }

                    // Nur den Fensterzustand maximiert wiederherstellen,
                    // alles andere normal, damit Benutzer ein Fenster
                    // nicht übersehen

                    this.WindowState = (FormWindowState)FensterInfo.Zustand == FormWindowState.Maximized ?
                        FormWindowState.Maximized : FormWindowState.Normal;
                }
            }
        }

        /// <summary>
        /// Löst das FormClosing Ereignis aus
        /// </summary>
        /// <param name="e">Ereignisdaten mit der Ursache des Schließens</param>
        /// <remarks>Wird um das Speichern der Fensterposition ergänzt</remarks>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Am Anfang die Position hinterlegen

            var FensterInfo = new WIFI.Anwendung.Daten.Fenster
            {
                Name = this.Name,
                Zustand = (int)this.WindowState
            };

            // Die Positionsdaten nur, wenn
            // es ein normales Fenster ist, weil
            // sonst die Werte keinen Sinn ergeben
            if (this.WindowState == FormWindowState.Normal)
            {
                // Immer links und oben
                FensterInfo.Links = this.Left;
                FensterInfo.Oben = this.Top;

                // Die Breite und Höhe nur,
                // wenn die Fenstergröße änderbar ist
                if (this.FormBorderStyle == FormBorderStyle.Sizable)
                {
                    FensterInfo.Breite = this.Width;
                    FensterInfo.Höhe = this.Height;
                }
            }

            // Die Fensterinfo in der Infrastruktur hinterlegen
            this.AppKontext.Fenster.Hinterlegen(FensterInfo);

            // Zum Schluss noch das machen,
            // was sonst gemacht wird
            base.OnFormClosing(e);
        }
    }
}
