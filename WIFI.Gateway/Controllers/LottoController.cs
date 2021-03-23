using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WIFI.Gateway.Controllers
{
    /// <summary>
    /// Stellt ein REST Api zum Abrufen
    /// der unterstützten Lotto-Länder bereit
    /// </summary>
    public class LottoController : ApiController
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Anwendung.DatenbankAppKontext _AppKontext = null;

        /// <summary>
        /// Ruft die Infrastruktur ab
        /// </summary><remarks>
        /// Es wird davon ausgegangenm dass beim hochfahren
        /// der Web Amwendung in den Eigenschaftender Configuration
        /// der WIFI Anwendungkontext hinterlegt wurde</remarks>
        protected WIFI.Anwendung.DatenbankAppKontext AppKontext
        {
            get
            {
                if (this._AppKontext == null)
                {
                    this._AppKontext = this.Configuration.Properties[typeof(WIFI.Anwendung.DatenbankAppKontext).FullName]
                        as WIFI.Anwendung.DatenbankAppKontext;
                }
                return this._AppKontext;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private WIFI.Lotto.LottoSqlClientController _Controller;

        /// <summary>
        /// Ruft den Dienst zum Lesen oder Schreiben von Information aus der SQLServer-Datenbank ab
        /// </summary>
        public WIFI.Lotto.LottoSqlClientController Controller
        {
            get
            {

                if (this._Controller == null)
                {
                    this._Controller = this.AppKontext.Produziere<WIFI.Lotto.LottoSqlClientController>();
                }
                return this._Controller;
            }
        }


        /// <summary>
        /// Gibt die unterstrützen Länder zurück
        /// </summary>
        public WIFI.Lotto.Daten.Länder Get()
        {
            return this.Controller.HoleLänder(
                // Der Sprachcode aus dem Header
                // Im Header liegt der Sprachcode durch die
                // (Aktuelle Sprache).Code aus der Infrastruktur
                this.Request.Headers.AcceptLanguage.ToString()
                );
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}