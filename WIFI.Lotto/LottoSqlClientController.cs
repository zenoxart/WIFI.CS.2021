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
                                    HöchsteZahl = Convert.ToInt32(DatenLeser["Höchstanzahl"]),
                                    // Ergänzung der Beschreibung von der Datenbank beim Laden
                                    Beschreibung = DatenLeser["Beschreibung"].ToString()
                                }) ;
                        }
                    }
                }


            }

            this.EndeProtokollieren();
            return Länder;
        }

        /// <summary>
        /// Gibt die Tage mit Ziehung eines Landes zurück
        /// </summary>
        /// <param name="land">Der ISO2 Code des Landes</param>
        /// <returns>Ein Array mit den Tagen einer Ziehung</returns>
        public System.DateTime[] HoleZiehungen(string land)
        {
            this.StartProtokollieren();
            var Tage = new System.Collections.Generic.List<System.DateTime>();

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleZiehungen",Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("@land", land);

                    Befehl.Prepare();

                    Verbindung.Open();

                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (Leser.Read())
                        {
                            Tage.Add((DateTime)Leser["Datum"]);
                        }
                    }
                }
            }

                this.EndeProtokollieren();
            return Tage.ToArray();
        }

        /// <summary>
        /// Gibt die Daten einer Ziehung eines Landes an einem Tag zurück
        /// </summary>
        /// <param name="land">Der ISO2 Code des Landes</param>
        /// <returns>Ein Array mit den Tagen einer Ziehung</returns>
        public WIFI.Lotto.Daten.LottoZiehung HoleZiehung(string land,DateTime datum)
        {
            this.StartProtokollieren();
            var Ziehungen = new WIFI.Lotto.Daten.LottoZiehung { Datum= datum };

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleZiehung", Verbindung))
                {
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    Befehl.Parameters.AddWithValue("@land", land);
                    Befehl.Parameters.AddWithValue("@datum", datum);

                    Befehl.Prepare();

                    Verbindung.Open();

                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (Leser.Read()){
                            Ziehungen.Zahl1 = (int)Leser["Zahl1"];
                            Ziehungen.Zahl2 = (int)Leser["Zahl2"];
                            Ziehungen.Zahl3 = (int)Leser["Zahl3"];
                            Ziehungen.Zahl4 = (int)Leser["Zahl4"];
                            Ziehungen.Zahl5 = (int)Leser["Zahl5"];
                            Ziehungen.Zahl6 = (int)Leser["Zahl6"];
                            Ziehungen.ZusatzZahl = (int)Leser["Zusatzzahl"];
                        }
                        else 
                            Ziehungen = null;
                    }
                }
            }

            this.EndeProtokollieren();
            return Ziehungen;
        }
    }
}
