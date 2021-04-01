using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Teil2.ViewModels
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Lotto-Daten in der Anwendung bereit
    /// </summary>
    public class LottoManager : WIFI.Anwendung.ViewModelAppObjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.ILottoController _Controller = null;

        /// <summary>
        /// Ruft den Dienst zum Lesen und Schreiben
        /// von Lottodaten ab
        /// </summary>
        private Models.ILottoController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    if (WIFI.CS.Teil2.Properties.Settings.Default.OfflineModus)
                    {
                        this._Controller = this.AppKontext.Produziere<Models.LottoOfflineController>();
                    }
                    else
                    {
                        // Online Modus
                        this._Controller = this.AppKontext.Produziere<Models.LottoWebController>();
                    }
                }
                return this._Controller;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Lotto.Daten.Länder _Länder = null;

        /// <summary>
        /// Ruft die unterstützten LottoLänder ab
        /// </summary>
        public WIFI.Lotto.Daten.Länder Länder
        {
            get
            {

                if (this._Länder == null)
                {
                    this._Länder = new Lotto.Daten.Länder();
                    this._Länder.Add(
                        new Lotto.Daten.Land { Name = Properties.Texte.DatenHolen });

                    this.LänderInitialisierenAsync();
                }
                return this._Länder;
            }
            protected set
            {
                    this._Länder = value;
                    this.OnPropertyChanged();   
            }
        }

        /// <summary>
        /// Initialisiert die Länder Eigenschaft
        /// mit den Informationen vom Controller
        /// </summary>
        protected virtual async void LänderInitialisierenAsync()
        {
            this.StartProtokollieren();
            this.Länder = await this.Controller.HoleLänderAsync();

            if (this.Länder != null && this.Länder.Count > 0)
            {
                // weil wir in einem Thread laufen,
                //nicht mit den Feldern, immer mit den Eigenschaften arbeiten

                this.AktuellesLand = this.Länder[0];
            }
            this.EndeProtokollieren();
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Lotto.Daten.Land _AktuellesLand = null;

        /// <summary>
        /// Ruft das Land ab, in dem aktuell gespielt wird,
        /// oder legt dieses fest.
        /// </summary>
        public WIFI.Lotto.Daten.Land AktuellesLand
        {
            get
            {
                return this._AktuellesLand;
            }
            set
            {
                if (this._AktuellesLand != value)
                {
                    this._AktuellesLand = value;
                    this.OnPropertyChanged();


                    // Wenn das Land gewechselt wird,
                    // sind die Ziehungen ungültig
                    this.Ziehungen = null;
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.DateTime[] _Ziehungen = null;

        /// <summary>
        /// Ruft die Tage mit einer Ziehung
        /// für das aktuelle Land ab
        /// </summary>
        public System.DateTime[] Ziehungen
        {
            get {

                if (this._Ziehungen == null)
                {
                    this.ZiehungenInitialisieren();
                }
                return this._Ziehungen; }
            protected set
            {

                this._Ziehungen = value;
                this.OnPropertyChanged();

            }
        }

        /// <summary>
        /// Initialisiert die Länder Eigenschaft
        /// mit den Informationen vom Controller
        /// </summary>
        protected virtual async void ZiehungenInitialisieren()
        {

            if (this.AktuellesLand == null) return;
            
                //this.StartProtokollieren();

                this.Ziehungen = await this.Controller.HoleZiehungenAsync(this.AktuellesLand);

                //this.EndeProtokollieren();
            
        }

    }
}
