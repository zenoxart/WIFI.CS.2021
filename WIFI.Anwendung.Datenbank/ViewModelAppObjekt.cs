using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt die Grundlage für WIFI ViewModels
    /// bereit und untersützt diese mit der Infrastruktur
    /// </summary>
    public abstract class ViewModelAppObjekt
        : WIFI.Anwendung.AppObjekt, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt
        /// einer Eigenschaft ändert
        /// </summary>
        /// <remarks>Wird von WPF benötigt, damit
        /// die Oberfläche automatisch aktualisiert wird.
        /// WPF prüft das Interface INotifyPropertyChanged</remarks>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="eigenschaft">Der Name der Eigenschaft,
        /// deren Wert geändert wurde</param>
        /// <remarks>Sollte der Name der Eigenschaft nicht angegeben
        /// werden, wird der Name vom Aufrufer benutzt</remarks>
        protected virtual void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName]string eigenschaft = "")
        {
            // Wegen des Multithreadings mit einer 
            // Kopie vom Ereignisbehandler arbeiten
            var BehandlerKopie = this.PropertyChanged;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(
                    this, 
                    new System.ComponentModel.PropertyChangedEventArgs(eigenschaft));
            }
        }

        /// <summary>
        /// Ruft die erweiterte Infrastruktur
        /// ab oder legt diese fest
        /// </summary>
        /// <remarks>Als Feld wird der AppKontext
        /// der Basisklasse benutzt. Durch diese
        /// Eigenschaft erspart man sich das
        /// spätere ständige Casten.</remarks>
        public new DatenbankAppKontext AppKontext
        {
            get
            {
                return base.AppKontext as DatenbankAppKontext;
            }
            set
            {
                //Erlaubt, weil DatenbankAppKontext
                //AppKontext erweitert...
                base.AppKontext = value;
            }
        }

        /// <summary>
        /// Internes Hilfsfeld für die
        /// IstBeschäftigt Eigenschaft
        /// </summary>
        private int _IstBeschäftigtZähler = 0;

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// der angibt, ob die Anwendung
        /// aktuelle Berechnungen durchführt
        /// oder anderweitig beschäftigt ist
        /// </summary>
        /// <remarks>Der Inhalt der Eigenschaft
        /// wird mit StartProtokollieren und
        /// EndeProtokollieren gesetzt</remarks><remarks>
        /// Mit der IstBeschäftigtSynchronisieren Methode 
        /// kann ein anderes ViewModel informiert werden.</remarks>
        public bool IstBeschäftigt
        {
            get
            {
                return this._IstBeschäftigtZähler > 0;
            }
            private set
            {
                if (value)
                {
                    this._IstBeschäftigtZähler++;
                }
                else
                {
                    this._IstBeschäftigtZähler--;
                }

                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Schaltet IstBeschäftigt ein und erstellt
        /// einen Protokolleintrag, dass die Methode
        /// zu laufen beginnt
        /// </summary>
        /// <param name="methodenName">Name der Methode,
        /// deren Start protokolliert werden soll</param>
        /// <remarks>Sollte der Methodenname nicht angegeben
        /// sein, wird automatisch der Name der 
        /// aufrufenden Methode benutzt</remarks>
        protected virtual void StartProtokollieren(
            [System.Runtime.CompilerServices.CallerMemberName]
            string methodenName = "")
        {
            this.IstBeschäftigt = true;
            this.AppKontext.Protokoll.Eintragen($"{this}.{methodenName} beginnt zu laufen...");
        }

        /// <summary>
        /// Schaltet IstBeschäftigt aus und erstellt
        /// einen Protokolleintrag, dass die Methode
        /// beendet ist
        /// </summary>
        /// <param name="methodenName">Name der Methode,
        /// deren Ende protokolliert werden soll</param>
        /// <remarks>Sollte der Methodenname nicht angegeben
        /// sein, wird automatisch der Name der 
        /// aufrufenden Methode benutzt</remarks>
        protected virtual void EndeProtokollieren(
            [System.Runtime.CompilerServices.CallerMemberName]
            string methodenName = "")
        {
            this.AppKontext.Protokoll.Eintragen($"{this}.{methodenName} beendet.");
            this.IstBeschäftigt = false;
        }

        /// <summary>
        /// Stellt sicher, dass eine Änderung
        /// von IstBeschäftigt auch in dem anderen
        /// ViewModel geändert wird
        /// </summary>
        /// <param name="mitViewModel">ViewModel, wo die 
        /// IstBeschäftigt Einstellung ebenfalls sichtbar
        /// sein soll</param>
        public virtual void IstBeschäftigtSynchronisieren(ViewModelAppObjekt mitViewModel)
        {

            this.PropertyChanged += (sender,e) => { 
                if(e.PropertyName == "IstBeschäftigt")
                {
                    mitViewModel.IstBeschäftigt = this.IstBeschäftigt;
                }
            };

            this.AppKontext.Protokoll.Eintragen(
                $"{this} teilt IstBeschäftigt {mitViewModel} mit...");
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static System.Net.Http.HttpClient _HttpClient = null;

        /// <summary>
        /// Ruft den Dienst zum aufrufen von Http-Aufrufen ab.
        /// Das Header-Feld "Accept-Language" wird auf die aktuelle Anwendungssprache eingestellt
        /// </summary><remarks>
        /// Es handelt sich um ein Singleton-Objekt
        /// also beim ersten Zugriff wird es initialisiert und über 
        /// die Lebensdauer der Anwendung wiederverwendet</remarks>
        protected System.Net.Http.HttpClient HttpClient
        {
            get
            {
                if (ViewModelAppObjekt._HttpClient == null)
                {
                    // Initialisieren
                    ViewModelAppObjekt._HttpClient = new System.Net.Http.HttpClient();

                    // Protokoll
                    this.AppKontext.Protokoll.Eintragen(
                        $"{this} hat den anwendungsweiten HttpClient initialisiert...",
                        Daten.ProtokollEintragTyp.NeueInstanz);

                    // Code einstellen
                    ViewModelAppObjekt._HttpClient.DefaultRequestHeaders.Add(
                        "Accept-Language",
                        this.AppKontext.Sprachen.Aktuell.Code);

                    // Protokoll
                    this.AppKontext.Protokoll.Eintragen(
                        $"\"Accept-Language\" auf {this.AppKontext.Sprachen.Aktuell.Code} festgelegt",
                        Daten.ProtokollEintragTyp.Normal);
                }

                return ViewModelAppObjekt._HttpClient;
            }
        }
    }
}
