
namespace WIFI.CS.Teil1
{
    partial class Hauptfenster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hauptfenster));
            this.Kopfbereich = new System.Windows.Forms.Panel();
            this.Titel = new System.Windows.Forms.Label();
            this.Vorwärts = new System.Windows.Forms.Button();
            this.ImageListVerlauf = new System.Windows.Forms.ImageList(this.components);
            this.Zurück = new System.Windows.Forms.Button();
            this.Sprachen = new System.Windows.Forms.ComboBox();
            this.Menüzeile = new System.Windows.Forms.ToolStrip();
            this.MenüAktionen = new System.Windows.Forms.ToolStripDropDownButton();
            this.AktionenKopieren = new System.Windows.Forms.ToolStripMenuItem();
            this.AktionenSpeichernUnter = new System.Windows.Forms.ToolStripMenuItem();
            this.AktionenLinie01 = new System.Windows.Forms.ToolStripSeparator();
            this.AktionenSeitenansicht = new System.Windows.Forms.ToolStripMenuItem();
            this.AktionenLinie02 = new System.Windows.Forms.ToolStripSeparator();
            this.AktionenBeenden = new System.Windows.Forms.ToolStripMenuItem();
            this.Auswahl = new System.Windows.Forms.Panel();
            this.BeimBeendenFragen = new System.Windows.Forms.CheckBox();
            this.AuswahlDurchlaufen = new System.Windows.Forms.LinkLabel();
            this.AuswahlAbweisen = new System.Windows.Forms.LinkLabel();
            this.AuswahlZählen = new System.Windows.Forms.LinkLabel();
            this.AuswahlFall = new System.Windows.Forms.LinkLabel();
            this.AuswahlBinär = new System.Windows.Forms.LinkLabel();
            this.AuswahlStruktur = new System.Windows.Forms.LinkLabel();
            this.AuswahlAllgemein = new System.Windows.Forms.LinkLabel();
            this.Inhalt = new System.Windows.Forms.Panel();
            this.Kopfbereich.SuspendLayout();
            this.Menüzeile.SuspendLayout();
            this.Auswahl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Kopfbereich
            // 
            this.Kopfbereich.Controls.Add(this.Titel);
            this.Kopfbereich.Controls.Add(this.Vorwärts);
            this.Kopfbereich.Controls.Add(this.Zurück);
            this.Kopfbereich.Controls.Add(this.Sprachen);
            resources.ApplyResources(this.Kopfbereich, "Kopfbereich");
            this.Kopfbereich.Name = "Kopfbereich";
            // 
            // Titel
            // 
            resources.ApplyResources(this.Titel, "Titel");
            this.Titel.Name = "Titel";
            // 
            // Vorwärts
            // 
            resources.ApplyResources(this.Vorwärts, "Vorwärts");
            this.Vorwärts.FlatAppearance.BorderSize = 0;
            this.Vorwärts.ImageList = this.ImageListVerlauf;
            this.Vorwärts.Name = "Vorwärts";
            this.Vorwärts.UseVisualStyleBackColor = true;
            this.Vorwärts.Click += new System.EventHandler(this.ImVerlaufBewegen);
            // 
            // ImageListVerlauf
            // 
            this.ImageListVerlauf.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListVerlauf.ImageStream")));
            this.ImageListVerlauf.TransparentColor = System.Drawing.Color.Yellow;
            this.ImageListVerlauf.Images.SetKeyName(0, "Zurück.bmp");
            this.ImageListVerlauf.Images.SetKeyName(1, "Vorwärts.bmp");
            // 
            // Zurück
            // 
            resources.ApplyResources(this.Zurück, "Zurück");
            this.Zurück.FlatAppearance.BorderSize = 0;
            this.Zurück.ImageList = this.ImageListVerlauf;
            this.Zurück.Name = "Zurück";
            this.Zurück.UseVisualStyleBackColor = true;
            this.Zurück.Click += new System.EventHandler(this.ImVerlaufBewegen);
            // 
            // Sprachen
            // 
            resources.ApplyResources(this.Sprachen, "Sprachen");
            this.Sprachen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Sprachen.FormattingEnabled = true;
            this.Sprachen.Name = "Sprachen";
            // 
            // Menüzeile
            // 
            this.Menüzeile.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Menüzeile.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menüzeile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenüAktionen});
            resources.ApplyResources(this.Menüzeile, "Menüzeile");
            this.Menüzeile.Name = "Menüzeile";
            // 
            // MenüAktionen
            // 
            this.MenüAktionen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenüAktionen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AktionenKopieren,
            this.AktionenSpeichernUnter,
            this.AktionenLinie01,
            this.AktionenSeitenansicht,
            this.AktionenLinie02,
            this.AktionenBeenden});
            resources.ApplyResources(this.MenüAktionen, "MenüAktionen");
            this.MenüAktionen.Name = "MenüAktionen";
            // 
            // AktionenKopieren
            // 
            this.AktionenKopieren.Name = "AktionenKopieren";
            resources.ApplyResources(this.AktionenKopieren, "AktionenKopieren");
            this.AktionenKopieren.Click += new System.EventHandler(this.Kopieren);
            // 
            // AktionenSpeichernUnter
            // 
            this.AktionenSpeichernUnter.Name = "AktionenSpeichernUnter";
            resources.ApplyResources(this.AktionenSpeichernUnter, "AktionenSpeichernUnter");
            this.AktionenSpeichernUnter.Click += new System.EventHandler(this.SpeichernUnter);
            // 
            // AktionenLinie01
            // 
            this.AktionenLinie01.Name = "AktionenLinie01";
            resources.ApplyResources(this.AktionenLinie01, "AktionenLinie01");
            // 
            // AktionenSeitenansicht
            // 
            this.AktionenSeitenansicht.Name = "AktionenSeitenansicht";
            resources.ApplyResources(this.AktionenSeitenansicht, "AktionenSeitenansicht");
            this.AktionenSeitenansicht.Click += new System.EventHandler(this.SeitenansichtÖffnen);
            // 
            // AktionenLinie02
            // 
            this.AktionenLinie02.Name = "AktionenLinie02";
            resources.ApplyResources(this.AktionenLinie02, "AktionenLinie02");
            // 
            // AktionenBeenden
            // 
            this.AktionenBeenden.Name = "AktionenBeenden";
            resources.ApplyResources(this.AktionenBeenden, "AktionenBeenden");
            this.AktionenBeenden.Click += new System.EventHandler(this.Beenden);
            // 
            // Auswahl
            // 
            this.Auswahl.Controls.Add(this.BeimBeendenFragen);
            this.Auswahl.Controls.Add(this.AuswahlDurchlaufen);
            this.Auswahl.Controls.Add(this.AuswahlAbweisen);
            this.Auswahl.Controls.Add(this.AuswahlZählen);
            this.Auswahl.Controls.Add(this.AuswahlFall);
            this.Auswahl.Controls.Add(this.AuswahlBinär);
            this.Auswahl.Controls.Add(this.AuswahlStruktur);
            this.Auswahl.Controls.Add(this.AuswahlAllgemein);
            resources.ApplyResources(this.Auswahl, "Auswahl");
            this.Auswahl.Name = "Auswahl";
            // 
            // BeimBeendenFragen
            // 
            resources.ApplyResources(this.BeimBeendenFragen, "BeimBeendenFragen");
            this.BeimBeendenFragen.Checked = global::WIFI.CS.Teil1.Properties.Settings.Default.BeimBeendenFragen;
            this.BeimBeendenFragen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BeimBeendenFragen.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::WIFI.CS.Teil1.Properties.Settings.Default, "BeimBeendenFragen", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BeimBeendenFragen.Name = "BeimBeendenFragen";
            this.BeimBeendenFragen.UseVisualStyleBackColor = true;
            // 
            // AuswahlDurchlaufen
            // 
            this.AuswahlDurchlaufen.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.AuswahlDurchlaufen, "AuswahlDurchlaufen");
            this.AuswahlDurchlaufen.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AuswahlDurchlaufen.LinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlDurchlaufen.Name = "AuswahlDurchlaufen";
            this.AuswahlDurchlaufen.TabStop = true;
            this.AuswahlDurchlaufen.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlDurchlaufen.Click += new System.EventHandler(this.InformationZeigen);
            // 
            // AuswahlAbweisen
            // 
            this.AuswahlAbweisen.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.AuswahlAbweisen, "AuswahlAbweisen");
            this.AuswahlAbweisen.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AuswahlAbweisen.LinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlAbweisen.Name = "AuswahlAbweisen";
            this.AuswahlAbweisen.TabStop = true;
            this.AuswahlAbweisen.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlAbweisen.Click += new System.EventHandler(this.InformationZeigen);
            // 
            // AuswahlZählen
            // 
            this.AuswahlZählen.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.AuswahlZählen, "AuswahlZählen");
            this.AuswahlZählen.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AuswahlZählen.LinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlZählen.Name = "AuswahlZählen";
            this.AuswahlZählen.TabStop = true;
            this.AuswahlZählen.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlZählen.Click += new System.EventHandler(this.InformationZeigen);
            // 
            // AuswahlFall
            // 
            this.AuswahlFall.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.AuswahlFall, "AuswahlFall");
            this.AuswahlFall.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AuswahlFall.LinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlFall.Name = "AuswahlFall";
            this.AuswahlFall.TabStop = true;
            this.AuswahlFall.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlFall.Click += new System.EventHandler(this.InformationZeigen);
            // 
            // AuswahlBinär
            // 
            this.AuswahlBinär.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.AuswahlBinär, "AuswahlBinär");
            this.AuswahlBinär.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AuswahlBinär.LinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlBinär.Name = "AuswahlBinär";
            this.AuswahlBinär.TabStop = true;
            this.AuswahlBinär.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlBinär.Click += new System.EventHandler(this.InformationZeigen);
            // 
            // AuswahlStruktur
            // 
            this.AuswahlStruktur.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.AuswahlStruktur, "AuswahlStruktur");
            this.AuswahlStruktur.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AuswahlStruktur.LinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlStruktur.Name = "AuswahlStruktur";
            this.AuswahlStruktur.TabStop = true;
            this.AuswahlStruktur.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlStruktur.Click += new System.EventHandler(this.InformationZeigen);
            // 
            // AuswahlAllgemein
            // 
            this.AuswahlAllgemein.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.AuswahlAllgemein, "AuswahlAllgemein");
            this.AuswahlAllgemein.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AuswahlAllgemein.LinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlAllgemein.Name = "AuswahlAllgemein";
            this.AuswahlAllgemein.TabStop = true;
            this.AuswahlAllgemein.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.AuswahlAllgemein.Click += new System.EventHandler(this.InformationZeigen);
            // 
            // Inhalt
            // 
            this.Inhalt.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.Inhalt, "Inhalt");
            this.Inhalt.Name = "Inhalt";
            this.Inhalt.Paint += new System.Windows.Forms.PaintEventHandler(this.AktuellesObjektZeichnen);
            this.Inhalt.Resize += new System.EventHandler(this.AktuellesObjektAktualisieren);
            // 
            // Hauptfenster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.Inhalt);
            this.Controls.Add(this.Auswahl);
            this.Controls.Add(this.Menüzeile);
            this.Controls.Add(this.Kopfbereich);
            this.Name = "Hauptfenster";
            this.Kopfbereich.ResumeLayout(false);
            this.Menüzeile.ResumeLayout(false);
            this.Menüzeile.PerformLayout();
            this.Auswahl.ResumeLayout(false);
            this.Auswahl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Kopfbereich;
        private System.Windows.Forms.ComboBox Sprachen;
        private System.Windows.Forms.Button Zurück;
        private System.Windows.Forms.ImageList ImageListVerlauf;
        private System.Windows.Forms.Button Vorwärts;
        private System.Windows.Forms.Label Titel;
        private System.Windows.Forms.ToolStrip Menüzeile;
        private System.Windows.Forms.ToolStripDropDownButton MenüAktionen;
        private System.Windows.Forms.ToolStripMenuItem AktionenKopieren;
        private System.Windows.Forms.ToolStripMenuItem AktionenSpeichernUnter;
        private System.Windows.Forms.ToolStripSeparator AktionenLinie01;
        private System.Windows.Forms.ToolStripMenuItem AktionenSeitenansicht;
        private System.Windows.Forms.ToolStripSeparator AktionenLinie02;
        private System.Windows.Forms.ToolStripMenuItem AktionenBeenden;
        private System.Windows.Forms.Panel Auswahl;
        private System.Windows.Forms.Panel Inhalt;
        private System.Windows.Forms.LinkLabel AuswahlDurchlaufen;
        private System.Windows.Forms.LinkLabel AuswahlAbweisen;
        private System.Windows.Forms.LinkLabel AuswahlZählen;
        private System.Windows.Forms.LinkLabel AuswahlFall;
        private System.Windows.Forms.LinkLabel AuswahlBinär;
        private System.Windows.Forms.LinkLabel AuswahlStruktur;
        private System.Windows.Forms.LinkLabel AuswahlAllgemein;
        private System.Windows.Forms.CheckBox BeimBeendenFragen;
    }
}