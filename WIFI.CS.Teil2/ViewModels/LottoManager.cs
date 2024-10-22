﻿using System;
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
            get
            {

                if (this._Ziehungen == null)
                {
                    this.ZiehungenInitialisieren();
                }
                return this._Ziehungen;
            }
            protected set
            {

                this._Ziehungen = value;
                this.OnPropertyChanged();

                // Damit ist der TagDerZiehung ungültig
                this.TagDerZiehung = null;
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


            if (this.Ziehungen.Length > 0)
            {
                this.TagDerZiehung = this.Ziehungen[0];
            }
            //this.EndeProtokollieren();


        }

        /// <summary>
        /// Ruft einen Wahrheitswert ab, oder legt fest
        /// ob die Anwendung das dunkle Design 
        /// benutzen soll oder nicht
        /// </summary>
        /// 20210406 
        /// Weil die View auf DataKontext gebunden ist
        /// und Anwendung.DunklesDesign nicht sieht,
        /// diese Eigenschaft auch hier
        public bool DunklesDesign
        {
            get
            {
                return Properties.Settings.Default.DunklesDesign;
            }

            set
            {
                if (value != Properties.Settings.Default.DunklesDesign)
                {
                    Properties.Settings.Default.DunklesDesign = value;

                    // Es wird davon ausgegangen, dass die 
                    // Einstellung im Main() gespeichert wird

                }
                // Weil die Anwendung mehere Fenster haben kann,
                // egal ob der Wert geändert wurde oder nicht...
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.DateTime? _TagDerZiehung;

        /// <summary>
        /// Ruft das Datum der Ziehung ab, von dem 
        /// die Zahlen benötigt werden, 
        /// oder legt diese fest
        /// </summary>
        public System.DateTime? TagDerZiehung
        {
            get { return this._TagDerZiehung; }
            set
            {

                if (this._TagDerZiehung != value)
                {
                    this._TagDerZiehung = value;
                    this.OnPropertyChanged();


                    this.AktuelleZiehung = null;

                    // Dem CommandManager mitteilen,
                    // das die "CanExecute" Methoden
                    // neu geprüft werden
                    System.Windows.Input.CommandManager.InvalidateRequerySuggested();
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Lotto.Daten.LottoZiehung _AktuelleZiehung = null;

        /// <summary>
        /// Ruft die Zahlen einer Ziehung für 
        /// das aktuelle Land an eim Tag ab
        /// </summary>
        public WIFI.Lotto.Daten.LottoZiehung AktuelleZiehung
        {
            get { return this._AktuelleZiehung; }
            protected set
            {
                this._AktuelleZiehung = value;
                this.OnPropertyChanged();

            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.Befehl _HoleZiehung = null;

        /// <summary>
        /// Ruft den Befehl zum Abrufen der
        /// Zahlen einer Zeihung für das aktuelle Land
        /// für einen Tag ab
        /// </summary>
        public WIFI.Anwendung.Befehl HoleZiehung
        {
            get
            {

                if (this._HoleZiehung == null)
                {
                    this._HoleZiehung = new WIFI.Anwendung.Befehl(
                            async p =>  this.AktuelleZiehung = 
                                        await this.Controller.HoleZiehungAsync(
                                            this.AktuellesLand, 
                                            this.TagDerZiehung.Value)
                            ,
                            p => this.AktuellesLand != null && this.TagDerZiehung != null
                        );
                }
                return this._HoleZiehung;
            }
            set { _HoleZiehung = value; }
        }

    }
}
