using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Lotto.Daten
{
    /// <summary>
    /// Stellt die Daten für die Tip-Reihen eines Lottoscheins bereit
    /// </summary>
    public class LottoTips : System.Collections.ObjectModel.ObservableCollection<LottoTip>
    {

    }
    /// <summary>
    /// Stellt die Daten für eine Tippreihe bereit
    /// </summary>
    public class LottoTip : WIFI.Anwendung.Daten.DatenBasis
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Zahl1 = 0;

        /// <summary>
        /// Ruft die Zahl ab, oder 
        /// legt diese fest
        /// </summary>
        public int Zahl1
        {
            get { return this._Zahl1; }
            set
            {

                if (this._Zahl1 != value)
                {
                    this._Zahl1 = value;
                    this.OnPropertyChanged();

                }
            }
        }
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Zahl2 = 0;

        /// <summary>
        /// Ruft die Zahl ab, oder 
        /// legt diese fest
        /// </summary>
        public int Zahl2
        {
            get { return this._Zahl2; }
            set
            {

                if (this._Zahl2 != value)
                {
                    this._Zahl2 = value;
                    this.OnPropertyChanged();

                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Zahl3 = 0;

        /// <summary>
        /// Ruft die Zahl ab, oder 
        /// legt diese fest
        /// </summary>
        public int Zahl3
        {
            get { return this._Zahl3; }
            set
            {

                if (this._Zahl3 != value)
                {
                    this._Zahl3 = value;
                    this.OnPropertyChanged();

                }
            }
        }
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Zahl4 = 0;

        /// <summary>
        /// Ruft die Zahl ab, oder 
        /// legt diese fest
        /// </summary>
        public int Zahl4
        {
            get { return this._Zahl4; }
            set
            {

                if (this._Zahl4 != value)
                {
                    this._Zahl4 = value;
                    this.OnPropertyChanged();

                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Zahl5 = 0;

        /// <summary>
        /// Ruft die Zahl ab, oder 
        /// legt diese fest
        /// </summary>
        public int Zahl5
        {
            get { return this._Zahl5; }
            set
            {

                if (this._Zahl5 != value)
                {
                    this._Zahl5 = value;
                    this.OnPropertyChanged();

                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _Zahl6 = 0;

        /// <summary>
        /// Ruft die Zahl ab, oder 
        /// legt diese fest
        /// </summary>
        public int Zahl6
        {
            get { return this._Zahl6; }
            set
            {

                if (this._Zahl6 != value)
                {
                    this._Zahl6 = value;
                    this.OnPropertyChanged();

                }
            }
        }


    }

    /// <summary>
    /// Stellt die Daten für eine LottoZiehung bereit
    /// </summary>
    public class LottoZiehung : LottoTip
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _ZusatzZahl = 0;

        /// <summary>
        /// Ruft die Zahl ab, oder 
        /// legt diese fest
        /// </summary>
        public int ZusatzZahl
        {
            get { return this._ZusatzZahl; }
            set
            {

                if (this._ZusatzZahl != value)
                {
                    this._ZusatzZahl = value;
                    this.OnPropertyChanged();

                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Land _Land;

        /// <summary>
        /// Ruft das Land, zu dem diese Ziehung gehört
        /// ab oder legt dieses fest
        /// </summary>
        public Land Land
        {
            get { return this._Land; }
            set
            {

                if (this._Land == value)
                {
                    this._Land = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DateTime? _Datum = null;

        /// <summary>
        /// Ruft den Tag der Ziehung ab 
        /// oder legt diesen fest
        /// </summary>
        public DateTime? Datum
        {
            get { return this._Datum; }
            set
            {
                if (this._Datum != value)
                {
                    this._Datum = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
