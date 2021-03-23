using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.Daten
{
    /// <summary>
    /// Beschreibt die Art eines Protokolleintrags
    /// </summary>
    public enum ProtokollEintragTyp
    {
        /// <summary>
        /// Beschreibt, dass ein neues Objekt initialisiert wurde
        /// </summary>
        NeueInstanz,
        /// <summary>
        /// Beschreibt einen Hinweis
        /// </summary>
        Normal,
        /// <summary>
        /// Kennzeichnet einen Hinweis, der zu beachten ist
        /// </summary>
        Warnung,
        /// <summary>
        /// Kennzeichnet einen Eintrag wegen einer Ausnahme
        /// </summary>
        Fehler
    }

    /// <summary>
    /// Stellt eine typsichere Auflistung von
    /// Protokolleinträgen bereit, die für die
    /// WPF Datenbindung benutzt werden kann
    /// </summary>
    public class ProtokollEinträge 
        : System.Collections.ObjectModel.ObservableCollection<ProtokollEintrag>
    {

    }

    /// <summary>
    /// Stellt die Daten eines
    /// Protolleintrags bereit
    /// </summary>
    public class ProtokollEintrag : DatenBasis
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private DateTime _Zeitstempel = DateTime.Now;

        /// <summary>
        /// Ruft den Zeitpunkt des Erstellens
        /// ab oder legt diese fest
        /// </summary>
        public DateTime Zeitstempel
        {
            get
            {
                return this._Zeitstempel;
            }
            set
            {
                this._Zeitstempel = value;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ProtokollEintragTyp _Typ = ProtokollEintragTyp.Normal;

        /// <summary>
        /// Ruft die Art des Eintrags ab,
        /// oder legt diese fest
        /// </summary>
        [InToString(2)]
        public ProtokollEintragTyp Typ
        {
            get
            {
                return this._Typ;
            }
            set
            {
                this._Typ = value;
            }
        }

        /// <summary>
        /// Interne Feld für die Eigenschaft
        /// </summary>
        /// <remarks>Hr. Humer: Weil String ein
        /// Verweistyp ist, unbedingt mit String.Empty
        /// anstelle von null initialisieren, damit
        /// andere beim Zugriff sicher nicht abstürzen</remarks>
        private string _Text = string.Empty;

        /// <summary>
        /// Ruft die Information von diesem Eintrag
        /// ab oder legt diese fest
        /// </summary>
        [InToString(1)]
        public string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
            }
        }
    }
}
