using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Lernen
{
    /// <summary>
    /// Steuert, wie die Schreibe Methode
    /// den Text anzeigen soll
    /// </summary>
    public enum AusgabeModus
    {
        /// <summary>
        /// Text für die Benutzer mit Zeilenumbruch
        /// </summary>
        Normal,

        /// <summary>
        /// Text für die Benutzer ohne Zeilenumbruch
        /// </summary>
        NormalInZeile,

        /// <summary>
        /// Text für Entwicklungszwecke
        /// </summary>
        Debug,

        /// <summary>
        /// Text in anderer Farbe für Hinweise
        /// </summary>
        Fehler
    }

    /// <summary>
    /// Unterstützt sämtliche WIFI.CS Objekte
    /// mit einer Grundfunktionalität
    /// </summary>
    /// <remarks>Diese Klasse kann nur 
    /// als Basisklasse benutzt werden</remarks>
    public abstract class Entwicklungsbasis : System.Object
    {

        // Grundprinzip der Objektorientierten Programmierung
        // Die Datenkapselung
        // Alle Daten sind privat!

        // Hier ist der Klassen-Gültigkeitsbereich
        // Alle Variablen hier werden als Felder bezeichnet

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        /// <remarks>Mit "static" wird sichergestellt,
        /// dass alle Objekte nur mit genau einem
        /// Zufallsgenerator arbeiten. Das heißt in
        /// der Fachsprache "Singleton". Notwendig,
        /// damit die Gleichverteilung gewährleistet ist</remarks>
        private static System.Random _Zufallsgenerator = null;

        /// <summary>
        /// Ruft den anwendungsweiten Zufallsgenerator ab.
        /// </summary>
        protected System.Random Zufallsgenerator
        {
            get
            {
                if (Entwicklungsbasis._Zufallsgenerator == null)
                {
                    //Beim ersten Zugriff just-in-time initialisieren

                    Entwicklungsbasis._Zufallsgenerator = new System.Random();

                    //Folgender Protokolleintrag darf
                    //genau einmal angezeigt werden (wegen static)
                    Entwicklungsbasis.Schreibe(
                        "Die Anwendung hat den Zufallsgenerator intialisiert...",
                        debug: true);
                }

                return Entwicklungsbasis._Zufallsgenerator;
            }
        }

        /// <summary>
        /// Gibt den Text in der Konsole aus
        /// </summary>
        /// <param name="text">Die Information die für 
        /// die Benutzer bestimmt ist</param>
        /// <remarks>Statisch, damit die Methode
        /// auch in Main() genutzt werden kann</remarks>
        protected static void Schreibe(string text)
        {
            //Entwicklungsbasis.Schreibe(text, false);

            //Benannte Parameter benutzen:

            Entwicklungsbasis.Schreibe(text, debug: false);
        }

        /// <summary>
        /// Gibt den Text in der Konsole aus
        /// </summary>
        /// <param name="text">Die Information, die
        /// ausgegeben werden soll</param>
        /// <param name="modus">Steuert, wie
        /// der Text angezeigt werden soll</param>
        protected static void Schreibe(string text, AusgabeModus modus)
        {
            switch (modus)
            {
                case AusgabeModus.Normal:
                    Entwicklungsbasis.Schreibe(text, debug: false);
                    break;
                case AusgabeModus.NormalInZeile:
                    System.Console.Write(text);
                    break;
                case AusgabeModus.Debug:
                    Entwicklungsbasis.Schreibe(text, debug: true);
                    break;
                case AusgabeModus.Fehler:
                    // Die neue Farbe einstellen
                    System.Console.ForegroundColor = ConsoleColor.Yellow;

                    // Den Text ausgeben
                    System.Console.WriteLine(text);

                    // Die Farbe wiederherstellen
                    System.Console.ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Gibt den Text in der Konsole aus
        /// </summary>
        /// <param name="text">Die Information, die
        /// ausgegeben werden soll</param>
        /// <param name="debug">True, falls die
        /// Information nur zum Entwickeln dient,
        /// sonst für die Benutzer</param>
        protected static void Schreibe(string text, bool debug)
        {
            if (debug)
            {

                //NUR IN DER ENTWICKLERVERSION
#if DEBUG

                //Protokolleinträge dunkelgrau

                // Die neue Farbe einstellen
                System.Console.ForegroundColor = ConsoleColor.DarkGray;

                // Den Text ausgeben
                System.Console.WriteLine(text);

                // Die Farbe wiederherstellen
                System.Console.ResetColor();
#endif
            }
            else
            {
                //Für die Benutzer
                System.Console.WriteLine(text);
            }
        }

        /// <summary>
        /// Initialisiert ein neues Objekt
        /// </summary>
        /// <remarks>Hier handelt es sich
        /// um den Konstruktor</remarks>
        public Entwicklungsbasis()
        {
            Entwicklungsbasis.Schreibe($"Ein Objekt \"{this.GetType().Name}\" lebt...", debug: true);

            //HIER SCHLECHT
            //  a) Es wird die Startgeschwindigkeit verzögert
            //  b) Es steht nicht fest, ob das Objekt 
            //     überhaupt benötigt wird
            //  Finger weg von den Konstruktoren!

            //Entwicklungsbasis._Zufallsgenerator = new System.Random();

            //Richtlinie
            //  Objekte in der Eigenschaft
            //  beim ersten Zugriff just-in-time 
            //  initialisieren und cachen
        }

#if DEBUG

        /// <summary>
        /// Zerstört ein Objekt
        /// </summary>
        /// <remarks>Hier handelt es sich
        /// um den Destruktor</remarks>
        ~Entwicklungsbasis()
        {
            Entwicklungsbasis.Schreibe($"Ein Objekt \"{this.GetType().Name}\" ist tot.", debug: true);
        }

#endif

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Anwendungspfad = null;

        /// <summary>
        /// Ruft das Verzeichnis ab,
        /// aus dem die Anwendung gestartet wurde
        /// </summary>
        protected string Anwendungspfad
        {
            // 20201105 Die Eigenschaft wird jetzt gecachet
            get
            {
                /*
                return System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetEntryAssembly().Location
                    );
                */
                if (this._Anwendungspfad == null)
                {
                    this._Anwendungspfad 
                        = System.IO.Path.GetDirectoryName(
                                    System.Reflection.Assembly
                                        .GetEntryAssembly().Location
                            );
                    Entwicklungsbasis.Schreibe(
                        "Die Anwendung hat das Anwendungsverzeichnis ermittelt und gecachet...", 
                        AusgabeModus.Debug);
                }

                return this._Anwendungspfad;
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn eine Ausnahme aufgetreten ist
        /// </summary>
        /// <remarks>Das "event" Schlüsselwort ist nur
        /// notwendig, damit ein Blitz angezeigt wird</remarks>
        public event FehlerAufgetretenEventHandler FehlerAufgetreten;

        /// <summary>
        /// Löst das Ereignis FehlerAufgetreten aus
        /// </summary>
        /// <param name="ausnahme">Die Ausnahme, die
        /// den Fehler beschreibt</param>
        protected void OnFehlerAufgetreten(System.Exception ausnahme)
        {
            if (this.FehlerAufgetreten != null)
            {
                this.FehlerAufgetreten(
                    this, 
                    new FehlerAufgetretenEventArgs(ausnahme));
            }
        }
    }
}
