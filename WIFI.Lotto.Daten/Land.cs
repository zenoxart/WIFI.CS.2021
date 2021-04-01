using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Lotto.Daten
{

    /// <summary>
    /// Stellt eine Liste für unterstützte Lottoländer bereit
    /// </summary>
    public class Länder : System.Collections.ObjectModel.ObservableCollection<Land>
    {

    }

    /// <summary>
    /// Stellt Information für ein 
    /// unterstütztes Lottoland bereit
    /// </summary>
    public class Land : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Name = string.Empty;

        /// <summary>
        /// Ruft die Bezeichnung des Landes ab
        /// oder legt diese fest
        /// </summary>
        [WIFI.Anwendung.Daten.InToString]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (value != this._Name)
                {
                    this._Name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _ISO2 = string.Empty;

        /// <summary>
        /// Ruft das internationale Länderkürzel
        /// des Landes ab oder legt dieses fest
        /// </summary>
        [WIFI.Anwendung.Daten.InToString]
        public string ISO2
        {
            get
            {
                return this._ISO2;
            }
            set
            {
                if (value != this._ISO2)
                {
                    this._ISO2 = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _HöchsteZahl;

        /// <summary>
        /// Ruft die Höchste der Lottozahlen ab
        /// oder legt diese fest
        /// </summary>
        [WIFI.Anwendung.Daten.InToString]
        public int HöchsteZahl
        {
            get { return this._HöchsteZahl; }
            set {

                if (value != this._HöchsteZahl)
                {
                    this._HöchsteZahl = value;
                    this.OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int _AnzahlZahlen;

        /// <summary>
        /// Ruft die Anzahl der Lottozahlen ab
        /// oder legt diese fest
        /// </summary>
        [WIFI.Anwendung.Daten.InToString]
        public int AnzahlZahlen
        {
            get { return this._AnzahlZahlen; }
            set
            {

                if (value != this._AnzahlZahlen)
                {
                    this._AnzahlZahlen = value;
                    this.OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Beschreibung = string.Empty;

        /// <summary>
        /// Ruft die lokalisierte Beschreibung des Lottosystem des Landes ab
        /// oder legt diese fest
        /// </summary>
        [WIFI.Anwendung.Daten.InToString]
        public string Beschreibung
        {
            get { return this._Beschreibung; }
            set {

                if (value != this._Beschreibung)
                {

                    this._Beschreibung = value;
                    this.OnPropertyChanged();
                } }
        }

    }
}
