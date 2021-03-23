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
            get {
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
                return this._Controller; }
        }

    }
}
