using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung.Erweiterungen
{
    /// <summary>
    /// Stellt Erweiterungsmethoden bereit,
    /// mit denen die AssemblyInfo 
    /// ausgelesen werden kann
    /// </summary>
    public static class AssemblyInfo
    {
        /// <summary>
        /// Gibt die Einstellung vom AssemblyCompanyAttribute zurück
        /// </summary>
        /// <param name="fürObjekt">Verweis auf das Objekt,
        /// bei dem die Erweiterung benutzt wird</param>
        public static string HoleFirmenname(this object fürObjekt)
        {
            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetCustomAttributes(
                    typeof(System.Reflection.AssemblyCompanyAttribute),
                    inherit: true);

            if (Daten.Length > 0)
            {
                return ((System.Reflection.AssemblyCompanyAttribute)Daten[0]).Company;
            }

            // Falls kein CompanyAttribute vorhanden ist
            return string.Empty;
        }

        /// <summary>
        /// Gibt die Einstellung vom AssemblyTitleAttribute zurück
        /// </summary>
        /// <param name="fürObjekt">Verweis auf das Objekt,
        /// bei dem die Erweiterung benutzt wird</param>
        public static string HoleTitel(this object fürObjekt)
        {
            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetCustomAttributes(
                    typeof(System.Reflection.AssemblyTitleAttribute),
                    inherit: true);

            if (Daten.Length > 0)
            {
                return ((System.Reflection.AssemblyTitleAttribute)Daten[0]).Title;
            }

            // Falls kein TitleAttribute vorhanden ist
            return string.Empty;
        }

        /// <summary>
        /// Gibt die Assembly Version zurück
        /// </summary>
        /// <param name="fürObjekt">Verweis auf das Objekt,
        /// bei dem die Erweiterung benutzt wird</param>
        public static string HoleVersion(this object fürObjekt)
        {

            // Warum System.Reflection.AssemblyVersionAttribute
            // nicht wie die anderen Attribute funktionieren,
            // weiß der Trainer ned

            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetName().Version;

            return $"{Daten.Major}.{Daten.Minor}";
        }

        /// <summary>
        /// Gibt die Einstellung vom AssemblyCopyrightAttribute zurück
        /// </summary>
        /// <param name="fürObjekt">Verweis auf das Objekt,
        /// bei dem die Erweiterung benutzt wird</param>
        public static string HoleCopyright(this object fürObjekt)
        {
            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetCustomAttributes(
                    typeof(System.Reflection.AssemblyCopyrightAttribute),
                    inherit: true);

            if (Daten.Length > 0)
            {
                return ((System.Reflection.AssemblyCopyrightAttribute)Daten[0]).Copyright;
            }

            return string.Empty;
        }
    }
}
