using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.SqlClient
{
    /// <summary>
    /// Stellt die Grundlage [Basisklasse] für eine Klasse bereit,
    /// die Daten von einem Sql Server liest bzw. schreibt.
    /// </summary>
    public abstract class Controller : ViewModelAppObjekt
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private static string _ConnectionString = null;

        /// <summary>
        /// Ruft den ConnectionString ab, der
        /// zum Verbinden zur Sql Server Datenbank benutzt wird
        /// </summary>
        /// <remarks>
        /// Es handelt sich um einen singleton Wert,
        /// d.h. während der Laufzeit ändert wich die Eigenschaft nicht.
        /// Die Information zum Berechnen wird aus der Infrastruktur bezogen</remarks>
        protected string ConnectionString
        {
            get
            {
                if (Controller._ConnectionString == null)
                {
                    

                    var CB = new System.Data.SqlClient.SqlConnectionStringBuilder();

                    // Auf alle Fälle den Server einstellen
                    CB.DataSource = this.AppKontext.SqlServer;

                    // Abhängig davon, ob es eine angehängte Datebank ist oder nicht
                    if (this.AppKontext.DatenbankPfad != null 
                        && this.AppKontext.DatenbankPfad != string.Empty)
                    {
                        // es ist eine dynamisch angehängte Datenbank
                        CB.AttachDBFilename = System.IO.Path.Combine(
                            this.AppKontext.DatenbankPfad,
                            this.AppKontext.DatenbankName);
                    }
                    else
                    {
                        // es ist eine fix angehängte Datenbank
                        // auf einem Sql Server
                        CB.InitialCatalog = this.AppKontext.DatenbankName;
                    }

                    CB.IntegratedSecurity = true;

                    //CB.UserID=...
                    //CB.Password=...

                    Controller._ConnectionString = CB.ConnectionString;
                    this.AppKontext.Protokoll.Eintragen(
                        $"{this} hat den ConnectionString für die Datenbank berechnet und gecached."
                        );
                }
                return Controller._ConnectionString;
            }
        }
    }
}
