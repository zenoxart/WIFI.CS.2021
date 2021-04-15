using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIFI.Lotto.Daten;

namespace WIFI.CS.Teil2.Models
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen und Schreiben 
    /// von Lottodaten direk über WIFI.Lotto bereit
    /// </summary> <remarks>
    /// WIFI.Lotto wird dynamisch geladen,
    /// damit die Assembly aus dem Projektordner 
    /// entfernt werden kann. Wenn nur online gearbeitet wird</remarks>
    internal class LottoOfflineController : WIFI.Anwendung.ViewModelAppObjekt, ILottoController
    {
        /// <summary>
        /// Gibt die unterstützten Lottoländer zurück.
        /// </summary>
        /// <returns></returns>
        public System.Threading.Tasks.Task<WIFI.Lotto.Daten.Länder> HoleLänderAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gibt die Tage mit der Ziehung zurück
        /// </summary>
        public Task<DateTime[]> HoleZiehungenAsync(Land land)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gibt die eine Ziehung durch Land und Datum zurück
        /// </summary>

        public Task<LottoZiehung> HoleZiehungAsync(Land land, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
