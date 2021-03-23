using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{ // <- hier beginnt eine Gültigkeit

    /// <summary>
    /// Stellt für jeden Baustein eines
    /// Lösungsverfahrens ein Beispiel bereit
    /// </summary>
    internal class Algorithmus : Entwicklungsbasis
    { // <- hier beginnt eine Gültigkeit

        /// <summary>
        /// Gibt den "Hallo Welt!" Text aus
        /// </summary>
        public void ZeigeSequenz()
        {
            Algorithmus.Schreibe("Algorithmus.ZeigeSequenz startet...", debug: true);

            // 1. Speicher reservieren
            // ---- kommt später aus Ressourcen (ab Teil 1)
            const string Info = "Hallo Welt!";
            // --------------------------------

            // 2. Den Text ausgeben
            Algorithmus.Schreibe(Info);

            Algorithmus.Schreibe("Algorithmus.ZeigeSequenz beendet.", debug: true);
        }

        /// <summary>
        /// Eine zufällige Ganzzahl berechnen
        /// und ausgeben, ob die Zahl unter
        /// oder über einer Grenze liegt
        /// </summary>
        public void ZeigeBinär()
        {
            //Version
            // 20201027 Die Grenze wird genau gemeldet

            Algorithmus.Schreibe("Algorithmus.ZeigeBinär startet...", debug: true);

            // Speicher reservieren
            // ---- später aus Ressourcen
            const string ErgebnisKleiner = "Die Zahl {0} ist kleiner der Grenze {1}!";

            // 20201027
            // const string ErgebnisGrößerGleich = "Die Zahl {0} ist größer oder gleich der Grenze {1}!";
            const string ErgebnisGrößer = "Die Zahl {0} ist größer der Grenze {1}!";
            const string ErgebnisGleich = "Die Zahl {0} ist die Grenze!";

            // ----------------------------------

            // ---- später aus der Anwendungskonfiguration
            const int Untergrenze = 1;
            const int Obergrenze = 100;
            const int Grenze = 50;
            //-------------------------------------------

            // Eine zufällige Ganzzahl im 
            // gewünschten Bereich berechnen
            int Zufallszahl = this.Zufallsgenerator
                .Next(Untergrenze, Obergrenze + 1);

            // 20201027
            //string Ergebnis = ErgebnisGrößerGleich;
            string Ergebnis = ErgebnisGleich;

            // Prüfen, ob die Zahl unter der Grenze
            if (Zufallszahl < Grenze)
            {
                Ergebnis = ErgebnisKleiner;
            }
            // 20201027 - Kommentar hinfällig
            /*
            // bzw. über der Grenze liegt
            // wegen der Initialisierung 
            // Ergebnis = ErgebnisGrößerGleich
            // nicht notwendig
            */

            // 20201027 neue Zusatzprüfung
            else if (Zufallszahl > Grenze)
            {
                Ergebnis = ErgebnisGrößer;
            }

            // Ergebnis mitteilen
            Algorithmus.Schreibe(
                string.Format(
                    Ergebnis,
                    Zufallszahl, // {0}
                    Grenze       // {1}
                    ));

            Algorithmus.Schreibe("Algorithmus.ZeigeBinär beendet.", debug: true);
        }

        /// <summary>
        /// Passend zu einer zufälligen
        /// Stunde einen Begrüßungstext anzeigeen
        /// </summary>
        public void ZeigeFall()
        {
            Algorithmus.Schreibe("Algorithmus.ZeigeFall startet...", debug: true);

            // Speicher reservieren
            const string ErgebnisMuster = "{0} Es ist {1} Uhr...";

            // Zufällige Stunde berechnen
            int ZufälligeStunde = this.Zufallsgenerator.Next(24);

            // Das Ergebnis mitteilen
            var Ergebnis = string.Format(
                ErgebnisMuster, 
                Algorithmus.HoleBegrüßung(ZufälligeStunde), // {0}
                ZufälligeStunde                             // {1}
                );
            Algorithmus.Schreibe(Ergebnis);

            Algorithmus.Schreibe("Algorithmus.ZeigeFall beendet.", debug: true);
        }

        /// <summary>
        /// Internes Feld für die Ausgabe
        /// der ZeigeZählen Methode
        /// </summary>
        private string _ZeigeZählenCache = null;

        // 20201103 Durch eine Variante mit Cachen ersetzt

        /*
        /// <summary>
        /// Für jede mögliche Stunde
        /// von 0 bis 23 die Begrüßung
        /// aus ZeigeFall testen
        /// </summary>
        public void ZeigeZählen()
        {
            Algorithmus.Schreibe("Algorithmus.ZeigeZählen startet...", debug: true);

            // Speicher reservieren
            const string AusgabeMuster = "{0} Es ist {1} Uhr...";

            // Für jede mögliche Stunde
            // das Ergebnis von HoleBegrüßung anzeigen
            for (int i = 0; i < 24; i++)
            {
                Algorithmus.Schreibe(
                    string.Format(
                        AusgabeMuster, 
                        Algorithmus.HoleBegrüßung(i),   // {0}
                        i                               // {1}
                        ));
            }

            Algorithmus.Schreibe("Algorithmus.ZeigeZählen beendet.", debug: true);
        }
        */

        /// <summary>
        /// Für jede mögliche Stunde
        /// von 0 bis 23 die Begrüßung
        /// aus ZeigeFall testen
        /// </summary>
        public void ZeigeZählen()
        {
            //Version 20201103 Mit Cachen

            Algorithmus.Schreibe("Algorithmus.ZeigeZählen startet...", debug: true);

            if (this._ZeigeZählenCache == null)
            {
                // Speicher reservieren
                const string AusgabeMuster = "{0} Es ist {1} Uhr...";

                var Text = new System.Text.StringBuilder();

                // Für jede mögliche Stunde
                // das Ergebnis von HoleBegrüßung anzeigen
                for (int i = 0; i < 24; i++)
                {
                    Text.AppendLine(
                        string.Format(
                            AusgabeMuster,
                            Algorithmus.HoleBegrüßung(i),   // {0}
                            i                               // {1}
                            ));
                }

                // Den berechneten Text in den Cache
                this._ZeigeZählenCache = Text.ToString();
                Algorithmus.Schreibe(
                    "Algorithmus.ZeigeZählen hat das Ergebnis berechnet und gecacht...", 
                    AusgabeModus.Debug);

            }

            // Das gecachte Ergebnis ausgeben
            Algorithmus.Schreibe(this._ZeigeZählenCache);

            Algorithmus.Schreibe("Algorithmus.ZeigeZählen beendet.", debug: true);
        }

        /// <summary>
        /// Den Inhalt einer unformatieren
        /// Textdatei lesen und als 
        /// Fließtext ausgeben
        /// </summary>
        /// <remarks>Die Datei muss "Wörter.txt"
        /// heißen und im Anwendungsverzeichnis liegen</remarks>
        public void ZeigeAbweisen()
        {
            Algorithmus.Schreibe("Algorithmus.ZeigeAbweisen startet...", debug: true);

            // Speicher reservieren
            const string Dateiname = "Wörter.txt";
            var Pfad = System.IO.Path.Combine(this.Anwendungspfad, Dateiname);

            var Text = new Textdatei();

            // Falls die Datei nicht vorhanden ist,
            // das den Benutzern mitteilen
            Text.FehlerAufgetreten += FehlerMelden;
            
            Text.Pfad = Pfad;

            Algorithmus.Schreibe(Text.HoleFließtext(50));

            Algorithmus.Schreibe("Algorithmus.ZeigeAbweisen beendet.", debug: true);
        }

        /// <summary>
        /// Schreibt den Text der Ursache
        /// des Fehlers in die Konsole
        /// </summary>
        /// <param name="sender">Immer das 1. Argument bei Ereignisbehandlern</param>
        /// <param name="e">Immer das 2. Argument mit den Ereignisdaten</param>
        /// <remarks>Solche Methoden heißen Ereignis-Behandler</remarks>
        private void FehlerMelden(object wurscht, FehlerAufgetretenEventArgs daten)
        {
            Algorithmus.Schreibe(daten.Ursache.Message, AusgabeModus.Fehler);
        }

        /// <summary>
        /// Internes Feld für das Objekt
        /// zum Lotto Spielen im ZeigeDurchlaufen
        /// </summary>
        private Lotto _Lotto = null;

        /// <summary>
        /// Ruft das Objekt zum Lotto Spielen ab
        /// </summary>
        protected Lotto Lotto
        {
            get
            {
                if (this._Lotto == null)
                {
                    this._Lotto = new Lotto();
                    Algorithmus.Schreibe(
                        "Algorithmus hat Lotto initialisiert und gecachet...", 
                        AusgabeModus.Debug);
                }

                return this._Lotto;
            }
        }

        /// <summary>
        /// Zeigt einen Lotto Quicktipp an
        /// </summary>
        public void ZeigeDurchlaufen()
        {
            Algorithmus.Schreibe("Algorithmus.ZeigeDurchlaufen startet...", debug: true);

            var Text = new System.Text.StringBuilder();

            // Zufällig eines der vorhandenen Lottoländer auswählen
            this.Lotto.Land = this.Lotto.Länder[this.Zufallsgenerator.Next(this.Lotto.Länder.Length)];

            Text.AppendLine($"{this.Lotto.Land.Name}: Lotto {this.Lotto.Land.AnzahlZahlenTipp} aus {this.Lotto.Land.HöchsteZahl}");
            
            //Mit der Spezialzählschleife
            //jede Zahl in den StringBuilder stellen
            foreach (int Zahl in this.Lotto.ErmittleQuicktipp())
            {
                Text.Append(Zahl.ToString().PadLeft(3));
            }

            Algorithmus.Schreibe(Text.ToString());

            Algorithmus.Schreibe("Algorithmus.ZeigeDurchlaufen beendet.", debug: true);
        }

        /// <summary>
        /// Gibt passend zur Stunde einen Begrüßungstext zurück.
        /// </summary>
        /// <param name="fürStunde">Eine Ganzzahl zwischen 0 und 23</param>
        /// <returns>Entweder "Guten Morgen!", "Mahlzeit!", 
        /// "Guten Abend!" oder "Grüß Gott!"</returns>
        /// <remarks>Das eigentliche Beispiel für die Fallentscheidung</remarks>
        public static string HoleBegrüßung(int fürStunde)
        {

            // Speicher reservieren
            const string GrußAmMorgen = "Guten Morgen!";
            const string GrußZuMittag = "Mahlzeit!";
            const string GrußAmAbend = "Guten Abend!";
            const string GrußStandard = "Grüß Gott!";

            string Ergebnis = GrußStandard;

            // Die Stunde zuordnen

            switch (fürStunde)
            {
                case int s when s > 4 && s < 12:
                    Ergebnis = GrußAmMorgen;
                    break;

                case int s when s > 11 && s < 15:
                    Ergebnis = GrußZuMittag;
                    break;

                case int s when s > 17 && s < 23:
                    Ergebnis = GrußAmAbend;
                    break;

                //default: wegen
                //         der Initialisierung
                //         nicht notwendig
            }

            // Ergebnis zurückgeben
            return Ergebnis;
        }

    } // <- hier endet die Gültigkeit

} // <- hier endet die Gültigkeit
