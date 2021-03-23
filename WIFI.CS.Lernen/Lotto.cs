using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{

    /// <summary>
    /// Ermöglicht das Berechnen eines
    /// Quicktipps für unterstützte Länder
    /// </summary>
    /// <remarks>Diese Klasse kann nicht 
    /// als Basisklasse benutzt werden.</remarks>
    public sealed class Lotto : Entwicklungsbasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private LottoLand[] _Länder = null;

        /// <summary>
        /// Ruft die Liste mit den
        /// unterstützten Ländern ab
        /// </summary>
        /// <remarks>Der Inhalt kann über 
        /// die Lotto-Länder.csv Datei konfiguriert werden</remarks>
        public LottoLand[] Länder
        {
            get
            {
                if (this._Länder == null)
                {
                    // Mit Hilfe unserer TextDatei Klasse
                    // die Zeilen der "Lotto-Länder.csv" lesen
                    var Konfiguration = new Textdatei();
                    Konfiguration.Pfad 
                        = System.IO.Path.Combine(
                            this.Anwendungspfad, 
                            "Lotto-Länder.csv");

                    // Ein Datenfeld (Array) für die gefundenen
                    // Länder initialisieren
                    this._Länder = new LottoLand[Konfiguration.Zeilen.Count];

                    // In einer Zählschleife jede Konfigurationszeile
                    // entsprechend vom Komma trennen und damit
                    // ein LottoLand Objekt initialisieren
                    for (int i = 0; i < this._Länder.Length; i++)
                    {
                        var Info = Konfiguration.Zeilen[i].ToString().Split(',');

                        this._Länder[i] = new LottoLand
                        {
                            Name = Info[0],
                            AnzahlZahlenTipp = int.Parse(Info[1]),
                            HöchsteZahl = int.Parse(Info[2])
                        };
                    }

                    Lotto.Schreibe(
                        $"Lotto.Länder mit {this._Länder.Length} Ländern initialisiert...", 
                        AusgabeModus.Debug);
                }

                return this._Länder;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private LottoLand _Land = null;

        /// <summary>
        /// Ruft das Land ab, in dem gespielt wird, oder legt dieses fest.
        /// </summary>
        /// <remarks>Standardwert ist das erste Land der Konfiguration</remarks>
        public LottoLand Land
        {
            get
            {
                if (this._Land == null)
                {
                    this._Land = this.Länder[0];
                }
                return this._Land;
            }
            set
            {
                this._Land = value;
            }
        }


        /// <summary>
        /// Gibt für das eingestellte Land
        /// Zahlen eines Quicktipps zurück.
        /// </summary>
        /// <returns>Ein Datenfeld mit der Anzahl
        /// der Zahlen des Landes.</returns>
        public int[] ErmittleQuicktipp()
        {
            //Ergebnisarray initialisieren
            var Ergebnis = new int[this.Land.AnzahlZahlenTipp];
            //      ^-> ein statisches Array
            //          kann im Umfang nicht mehr verändert werden

            //Die Anzahl der berechneten Zahlen merken
            int AnzahlZahlen = 0;

            //Weil Zufall-Max exklusiv ist
            int Obergrenze = this.Land.HöchsteZahl + 1;

            //in einer Durchlaufschleife
            do
            {
                //eine neue Zahl berechnen
                var NeueZahl = this.Zufallsgenerator.Next(1, Obergrenze);
                
                //ab der zweiten Zahl prüfen,
                //ob diese schon vorhanden ist
                bool Gefunden = false;
                int i = 0;

                while (i < AnzahlZahlen && !Gefunden)
                {
                    if (Ergebnis[i] == NeueZahl)
                    {
                        Gefunden = true;
                    }
                    else
                    {
                        i++;
                    }
                }

                //Die neue Zahl nur merken,
                //wenn sie noch nicht vorhanden ist
                if (!Gefunden)
                {
                    Ergebnis[AnzahlZahlen] = NeueZahl;
                    AnzahlZahlen++;
                }

                //bis die Anzahl der Zahlen erreicht ist
            } while (AnzahlZahlen < this.Land.AnzahlZahlenTipp);

            //Ergebnis sortieren
            System.Array.Sort(Ergebnis);

            //Ergebnis zurückgeben
            return Ergebnis;
        }
    }
}
