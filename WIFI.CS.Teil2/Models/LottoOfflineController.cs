using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
