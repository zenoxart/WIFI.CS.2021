namespace WIFI.CS.Teil2.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Probieren
    /// vom Microsoft TAP bereit.
    /// </summary>
    public class TAPManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Ruft die obere Grenze der 
        /// Wiederholungen ab
        /// </summary><remarks>
        /// Hier die kürzeste Möglichkeit für 
        /// eine schreibgeschützte Eigenschaft benutzt,
        /// ein anonymer Getter</remarks>
        public int Maximum => 1000;

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private bool _ArbeitLäuft;

        /// <summary>
        /// Ruft die aktuelle Anzahl der
        /// bereits berechneten Wiederholungen ab
        /// </summary>
        public bool ArbeitLäuft
        {
            get
            {
                return this._ArbeitLäuft;
            }
            protected set
            {
                this._ArbeitLäuft = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _AktuellerStand;

        /// <summary>
        /// Ruft die aktuelle Anzahl der
        /// bereits berechneten Wiederholungen ab
        /// </summary>
        public int AktuellerStand
        {
            get
            {
                return this._AktuellerStand;
            }
            protected set
            {
                this._AktuellerStand = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _Abbrechen = null;

        /// <summary>
        /// Ermöglicht die laufende Berechnung abzubrechen
        /// </summary><remarks>
        /// Nur zulässig, wenn die aktuelle Arbeit aktuell
        /// läuft</remarks>
        public WIFI.Anwendung.Befehl Abbrechen
        {
            get
            {
                if (this._Abbrechen == null)
                {
                    this._Abbrechen = new WIFI.Anwendung.Befehl(
                        executeMethode: p => this.AbbruchToken.Cancel(),
                        canExecuteMethode: p => this.ArbeitLäuft
                        );


                }

                return this._Abbrechen;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _Starten = null;

        /// <summary>
        /// Ruft den Befehl zum Starten
        /// der Berechnung ab.
        /// </summary><remarks>
        /// Nur zulässig, wenn die aktuelle Arbeit aktuell
        /// nicht läuft</remarks>
        public WIFI.Anwendung.Befehl Starten
        {
            get
            {
                if (this._Starten == null)
                {
                    this._Starten = new WIFI.Anwendung.Befehl(this.ArbeitenAsync, p => !this.ArbeitLäuft);

                }

                return this._Starten;
            }
        }


        /// <summary>
        /// Ruft das Objekt mit der Information 
        /// zum Abbrechen der Arbeit im TAP Thread ab
        /// </summary>
        protected System.Threading.CancellationTokenSource AbbruchToken { get; set; }

        /// <summary>
        /// Führt eine aufwändige Berechnung durch
        /// </summary><param name="commandParameter">Daten der Datenbindung</param>
        protected virtual async void ArbeitenAsync(object commandParameter)
        {
            // Objekt zum Abbrechen des TAP Threads, des Tasks initialisieren
            this.AbbruchToken = new System.Threading.CancellationTokenSource();

            // Die aufwändige Arbeit als anonyme
            // Methode einem TAP Task inkl.
            // der Abbruch-Information geben und starten
            await System.Threading.Tasks.Task.Run(
                 action: () =>
                 {
                     this.ArbeitLäuft = true;
                     this.StartProtokollieren();

                     this.AktuellerStand = 0;

                     for (int i = 0; i < this.Maximum; i++)
                     {
                         this.AktuellerStand++;

                         // weil keine "echte" Arbeit vorliegt, schlafen

                         System.Threading.Thread.Sleep(10);

                         if (this.AbbruchToken.IsCancellationRequested)
                         {
                             // Irgendwer möchte, dass
                             // die Arbeit abgebrochen wird

                             // Vor dem Abbrechen den Aktuellen Stand zurück setzten um den "Schönheitsfehler zu beheben"
                             this.AktuellerStand = 0;

                             break;
                         }
                     }

                     this.EndeProtokollieren();


                 },
                 cancellationToken: this.AbbruchToken.Token
             );

            // Der Oberfläche mitteilen,
            // dass die Schaltfläche wieder
            // deaktiviert bzw. aktiviert werden
            // müssen
            // Damit diese Anweisung erst läuft, wenn
            // der Task fertig ist, muuss beim Task
            // "await" benutzt werden
            this.ArbeitLäuft = false;
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }

    }
}
