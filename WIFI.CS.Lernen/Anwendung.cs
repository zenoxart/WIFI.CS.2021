using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{
    /// <summary>
    /// Steuert die Anwendung zum Lernen
    /// der Objektorientierten Programmierung
    /// mit C# und dem .Net Framework
    /// </summary>
    internal class Anwendung : Entwicklungsbasis
    {
        /// <summary>
        /// Stellt den Haupteinstiegspunkt bereit.
        /// </summary>
        /// <remarks>Dieser muss Main heißen
        /// und statisch sein, d.h. der Klasse
        /// und nicht dem Objekt gehören</remarks>
        private static void Main()
        {

            Anwendung.ZeigeTitel();
            Anwendung.BenutzerBegrüßen();
            Anwendung.ZeigeMenü();

            // Eine Klasse als Objekt benutzen
            // (wenn die Mitglieder nicht statisch sind)

            // 1. Speicher für das Objekt reservieren
            //    (genaugenommen die Speicheradresse)
            Algorithmus ObjektAlgo = null;

            // 2. Die Klasse initialisieren und 
            //    die Speicheradresse merken
            ObjektAlgo = new Algorithmus();

            // 3. Die nicht statischen 
            //    Mitglieder benutzen

            // Der Benutzer soll entscheiden,
            // was er sehen möchte...
            /*
            ObjektAlgo.ZeigeSequenz();
            ObjektAlgo.ZeigeBinär();
            //Damit der gecachte Zufallsgenerator sichtbar wird,
            //noch einmal
            ObjektAlgo.ZeigeBinär();
            ObjektAlgo.ZeigeFall();
            ObjektAlgo.ZeigeZählen();
            */

            const string Prompt = "C#> ";
            //                        ^-> ein Leer, damit
            //                            zur Eingabe ein Abstand ist
            const string Fehleingabe = "Eingabe nicht erkannt! ? für Hilfe";

            var Auswahl = string.Empty; // = ""

            do
            {
                Anwendung.Schreibe(Prompt, AusgabeModus.NormalInZeile);
                Auswahl = System.Console.ReadLine().Trim();

                switch (Auswahl)
                {
                    case "Hallo":
                    case "1":
                        ObjektAlgo.ZeigeSequenz();
                        break;
                    case "if":
                    case "2":
                        ObjektAlgo.ZeigeBinär();
                        break;
                    case "switch":
                    case "3":
                        ObjektAlgo.ZeigeFall();
                        break;
                    case "for":
                    case "foreach":
                    case "4":
                        ObjektAlgo.ZeigeZählen();
                        break;
                    case "while":
                    case "5":
                        ObjektAlgo.ZeigeAbweisen();
                        break;
                    case "do":
                    case "6":
                        ObjektAlgo.ZeigeDurchlaufen();
                        break;

                    // Damit beim Beenden oder bei keiner Eingabe
                    // der Fehler nicht angezeigt wird
                    case "":
                    case "9":
                        break;

                    case "?":
                        Anwendung.ZeigeMenü();
                        break;

                    default:
                        Anwendung.Schreibe(Fehleingabe, AusgabeModus.Fehler);
                        break;
                }

            } while (Auswahl != "9");

            // 4. Den nicht mehr benötigten
            //    Speicher für die Freigabe 
            //    kennzeichnen
            ObjektAlgo = null;

#if DEBUG
            // NUR ZU TESTZWECKEN!!!
            // NIE IN ECHTANWENDUNGEN
            // -> manuelle Garbage Collection
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();

            //Damit die Konsole offen bleibt...
            Anwendung.Schreibe("Die Eingabetaste zum Beenden tippen");
            System.Console.ReadLine();
#endif
        }

        /// <summary>
        /// Zeigt in einem Rechteck 
        /// den Anwendungstitel an
        /// </summary>
        /// <remarks>Demonstriert Möglichkeiten
        /// der .Net System.String Klasse</remarks>
        private static void ZeigeTitel()
        {

            //Version 20201103 Der Titel wird jetzt zentriert

            Anwendung.Schreibe("Anwendung.ZeigeTitel startet...", debug: true);

            // Speicher reservieren
            const string Titel = "Einführung in die objektorientierte Programmierung mit C# und .Net";
            const string AusgabeMuster = "{0}{1}{2}{3}{4}{3}{5}{1}{6}";

            int Innenbreite = System.Console.WindowWidth - 2;

            // Ausgabe berechnen
            var Ergebnis = string.Format(
                AusgabeMuster,
                Rahmen.LinksOben,                               // {0}
                // Die nächste Anweisung nur erlaubt,
                // weil System.String kein Dispose() hat
                new string(Rahmen.Horizontal, Innenbreite),     // {1}
                Rahmen.RechtsOben,                              // {2}
                Rahmen.Vertikal,                                // {3}
                // 20201103
                // Titel.PadRight(Innenbreite),                 // {4}
                Titel.PadLeft((Innenbreite - Titel.Length)/2 + Titel.Length)
                        .PadRight(Innenbreite),
                // ----
                Rahmen.LinksUnten,                              // {5}
                Rahmen.RechtsUnten                              // {6}
                );

            // Ergebnis anzeigen
            Anwendung.Schreibe(Ergebnis);

            Anwendung.Schreibe("Anwendung.ZeigeTitel beendet.", debug: true);
        }

        /// <summary>
        /// Fragt den Benutzer nach seinen Namen
        /// und begrüßt ihn passend zur aktuellen Stunde
        /// </summary>
        /// <remarks>Sollte der Benutzer seinen Namen
        /// nicht angeben, wird das Login Kürzel benutzt</remarks>
        private static void BenutzerBegrüßen()
        {
            Anwendung.Schreibe("Anwendung.BenutzerBegrüßen startet...", debug: true);

            // Speicherreservierungen
            const string Frage = "Wie heißen Sie? ";
            //                                   ^-> damit zur Antwort
            //                                       ein Abstand ist

            // Den Benutzer nach seinen Namen fragen
            Anwendung.Schreibe(Frage, AusgabeModus.NormalInZeile);
            var Benutzer = System.Console.ReadLine().Trim();

            // Falls keine Eingabe vorhanden war,
            // das Login aus dem Betriebssytem
            if (Benutzer == string.Empty)
            {
                Benutzer = System.Environment.UserName;
            }

            // Ausgabe berechnen und anzeigen
            Anwendung.Schreibe(
                Algorithmus.HoleBegrüßung(System.DateTime.Now.Hour)
                    .Replace("!", ", " + Benutzer + "!"));

            Anwendung.Schreibe("Anwendung.BenutzerBegrüßen beendet.", debug: true);
        }

        /// <summary>
        /// Internes Feld für den gecacheten
        /// Text der ZeigeMenü Methode
        /// </summary>
        private static string _ZeigeMenüText = null;

        /// <summary>
        /// Listet die Anwendungsmöglichkeiten auf
        /// </summary>
        private static void ZeigeMenü()
        {
            Anwendung.Schreibe("Anwendung.ZeigeMenü startet...", debug: true);

            if (Anwendung._ZeigeMenüText == null)
            {
                // Weil keine längeren Texte mit + verkettet werden sollen
                var Text = new System.Text.StringBuilder();
                // Hier wurde Punkt 1 und 2 bei Objekten
                // in einer Anweisung geschrieben.
                // Nur erlaubt, wenn das Objekt sofort benötigt wird

                Text.AppendLine("Algorithmen Bausteine");
                Text.AppendLine();
                Text.AppendLine(" 1 Sequenz");
                Text.AppendLine(" 2 Binärentscheidung");
                Text.AppendLine(" 3 Fallentscheidung");
                Text.AppendLine(" 4 Zählschleife");
                Text.AppendLine(" 5 Abweiseschleife");
                Text.AppendLine(" 6 Durchlaufeschleife");
                Text.AppendLine();
                Text.AppendLine(" 9 Beenden");

                Anwendung._ZeigeMenüText = Text.ToString();
                Anwendung.Schreibe(
                    "ZeigeMenü-Text berechnet und gecachet...", 
                    debug: true);
            
            }

            Anwendung.Schreibe(Anwendung._ZeigeMenüText);

            Anwendung.Schreibe("Anwendung.ZeigeMenü beendet.", debug: true);
        }

    }
}
