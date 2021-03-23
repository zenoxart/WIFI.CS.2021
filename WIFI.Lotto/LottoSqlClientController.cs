using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Lotto
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen und Schreiben von benötigten 
    /// Informationen aus bzw. in eine SQL Server Datenbank bereit
    /// </summary>
    public class LottoSqlClientController : WIFI.Anwendung.SqlClient.Controller
    {
        /// <summary>
        /// gibt die Liste mit den Unterstützten Ländern aus der Datenbank ab
        /// </summary>
        /// <param name="sprache">CulturInfo Kürzel der Sprache, 
        /// mit der die Namen der länder geliefert werden 
        /// sollen [Sprach-Code]</param>
        /// <remarks>Hier sieht man das Standardschema,
        /// wie mit C# und .Net auf einen Sql Server 
        /// zugegriffen wird. 
        /// Es gibt kein schnelleres Verfahren.</remarks>
        public WIFI.Lotto.Daten.Länder HoleLänder(string sprache)
        {

            this.StartProtokollieren();

            var Länder = new WIFI.Lotto.Daten.Länder();

            // Schema F zum Zugreifen auf eine Sql Server Datenbank

            // 1. Eine Verbindung
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {
                // 2. Ein Befehl
                //                              Der SqlCommand kann direkt auch stored Procedures,
                //                              wenn das richtige Enum gesetzt wird [CommandType]
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleLottoLänder",Verbindung))
                {

                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("@sprache", sprache);

                    Befehl.Prepare();

                    // ÖFFNE SPÄT, SCHLIESSE FRÜH
                    Verbindung.Open();

                    // 3. Nur bei Select ein Leser [DataReader]
                    using (var DatenLeser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (DatenLeser.Read())
                        {
                            
                            Länder.Add(
                                new WIFI.Lotto.Daten.Land
                                {
                                    ISO2 = DatenLeser["Kurzbezeichnung"].ToString(),
                                    Name = DatenLeser["Übersetzung 1"].ToString(),
                                    AnzahlZahlen = Convert.ToInt32(DatenLeser["Zahlenanzahl"]),
                                    HöchsteZahl = Convert.ToInt32(DatenLeser["Höchstanzahl"])
                                });
                        }
                    }
                }


            }

            this.EndeProtokollieren();
            return Länder;
        }
    }
}
