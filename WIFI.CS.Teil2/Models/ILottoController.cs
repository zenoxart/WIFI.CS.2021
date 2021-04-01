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
    /// der Lotto Daten über die REST Schnittstelle
    /// vom WIFI.Gateway bereit
    /// </summary>
    internal interface ILottoController
    {
        /// <summary>
        /// Gibt die unterstützten Lottoländer zurück.
        /// </summary>
        /// <returns></returns>
        System.Threading.Tasks.Task<WIFI.Lotto.Daten.Länder> HoleLänderAsync();


        /// <summary>
        /// Gibt die Tage mit einer Ziehung zurück
        /// </summary>
        System.Threading.Tasks.Task<DateTime[]> HoleZiehungenAsync(Land land);
    }
}
