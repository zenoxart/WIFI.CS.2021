using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.CS.Teil2.Models
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen und Schreiben
    /// der Lotto Daten über die REST Schnittstelle
    /// vom WIFI.Gateway bereit
    /// </summary>
    internal class LottoWebController : WIFI.Anwendung.ViewModelAppObjekt,ILottoController
    {
        /// <summary>
        /// Gibt die unterstützten Lottoländer zurück
        /// </summary>

        public async System.Threading.Tasks.Task<WIFI.Lotto.Daten.Länder> HoleLänderAsync()
        {
            const string Adresse = "{0}Lotto";

            using (var Antwort = 
                        await this.HttpClient.GetAsync(
                                String.Format(
                                    Adresse, 
                                    Properties.Settings.Default.UrlGatewayAPI
                                    )
                                )
                   )
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Lotto.Daten.Länder>(AntwortText);
            }
        }


        /// <summary>
        /// Gibt die Tage mit einer Ziehung zurück
        /// </summary>

        public async System.Threading.Tasks.Task<System.DateTime[]> HoleZiehungenAsync(WIFI.Lotto.Daten.Land land)
        {
            const string Adresse = "{0}lotto?iso2land={1}";

            using (var Antwort =
                        await this.HttpClient.GetAsync(
                                String.Format(
                                    Adresse,
                                    Properties.Settings.Default.UrlGatewayAPI,
                                    land.ISO2
                                    )
                                )
                   )
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<System.DateTime[]>(AntwortText);
            }
        }
        /// <summary>
        /// Gibt die eine Ziehung durch Land und Datum zurück
        /// </summary>

        public async System.Threading.Tasks.Task<WIFI.Lotto.Daten.LottoZiehung> HoleZiehungAsync(WIFI.Lotto.Daten.Land land, DateTime dateTime)
        {
            const string Adresse = "{0}lotto?iso2land={1}&datum={2}";

            using (var Antwort =
                        await this.HttpClient.GetAsync(
                                String.Format(
                                    Adresse,
                                    Properties.Settings.Default.UrlGatewayAPI,
                                    land.ISO2,
                                    dateTime.ToString("yyyy/MM/dd")
                                    )
                                )
                   )
            {
                var AntwortText = await Antwort.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<WIFI.Lotto.Daten.LottoZiehung>(AntwortText);
            }
        }
    }
}
