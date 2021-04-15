namespace WIFI.CS.Teil2.Models
{
    /// <summary>
    /// Stellt eine Auflistung an Einstellungsbereichen bereit
    /// </summary>
    public class Einstellungen : System.Collections.ObjectModel.ObservableCollection<Einstellung>
    {

    }

    /// <summary>
    /// Beschreibt einen Einstellungsbereich
    /// </summary>
    public class Einstellung : WIFI.Anwendung.Daten.DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Symbol = string.Empty;

        /// <summary>
        /// Ruft den Pfad der Png ab,
        /// das als Symbol für den Anwendungsbereich
        /// benutzt werden soll, oder legt diese fest
        /// </summary>
        public string Symbol
        {
            get
            {
                return this._Symbol;
            }
            set
            {
                if (this._Symbol != value)
                {
                    this._Symbol = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Name = string.Empty;

        /// <summary>
        /// Ruft die Bezeichnung des Anwendungsbereichs
        /// ab oder legt diese fest
        /// </summary>
        [WIFI.Anwendung.Daten.InToString(1)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _ViewName = null;

        /// <summary>
        /// Ruft die Typ-Bezeichnung des 
        /// Viewers für diese Aufgabe ab
        /// oder legt diese fest
        /// </summary>
        public string ViewerName
        {
            get
            {
                return this._ViewName;
            }

            set
            {
                if (this._ViewName != value)
                {
                    this._ViewName = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
