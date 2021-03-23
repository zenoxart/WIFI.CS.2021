using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Kapselt eine Methode und
    /// kann in WPF für die Datenbindung
    /// benutzt werden
    /// </summary>
    public class Befehl : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Wird ausgelöst, wenn die Methode zum
        /// Prüfen der Zulässigkeits des Befehls
        /// ein anderes Ergebnis als früher liefert
        /// </summary>
        /// <remarks>Wird mit dem WPF CommandManager
        /// RequerySuggested gekoppelt. Diese Variante
        /// hat der Trainer von einem Microsoft Blog</remarks>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                System.Windows.Input.CommandManager.RequerySuggested += value;
            }
            remove
            {
                System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Gibt einen Wahrheitswert zurück,
        /// der angibt, ob der Befehl aktuell
        /// zulässig oder nicht
        /// </summary>
        /// <param name="parameter">Daten der Datenbindung</param>
        /// <returns>True, falls keine Methode
        /// zum Prüfen der Zulässigkeit vorhanden ist,
        /// sonst das Ergebnis der Prüfmethode</returns>
        public bool CanExecute(object parameter)
        {
            return 
                this._CanExecuteMethode == null ? 
                true : this._CanExecuteMethode(parameter);
        }

        /// <summary>
        /// Führt die gekapselte Methode aus
        /// </summary>
        /// <param name="parameter">Daten der Datenbindung</param>
        public void Execute(object parameter)
        {
            this._ExecuteMethode(parameter);
        }

        /// <summary>
        /// Internes Feld für die Methode, die
        /// in diesem Befehl-Objekt gekapselt ist
        /// </summary>
        private System.Action<object> _ExecuteMethode = null;

        /// <summary>
        /// Internes Feld für die Methode, die
        /// prüft, ob der Befehl aktuell zulässig ist
        /// oder nicht
        /// </summary>
        private System.Predicate<object> _CanExecuteMethode = null;

        /// <summary>
        /// Initialisiert ein neues Befehl-Objekt
        /// </summary>
        /// <param name="executeMethode">Verweis auf die Methode,
        /// die ausgeführt werden soll, wenn der Befehl benutzt wird</param>
        public Befehl(System.Action<object> executeMethode) 
            : this(executeMethode, canExecuteMethode: null)
        {

        }

        /// <summary>
        /// Initialisiert ein neues Befehl-Objekt
        /// </summary>
        /// <param name="executeMethode">Verweis auf die Methode,
        /// die ausgeführt werden soll, wenn der Befehl benutzt wird</param>
        /// <param name="canExecuteMethode">Verweis auf die Methode,
        /// die prüft, ob der Befehl aktuell zulässig ist</param>
        public Befehl(
            System.Action<object> executeMethode, 
            System.Predicate<object> canExecuteMethode)
        {
            this._ExecuteMethode = executeMethode;
            this._CanExecuteMethode = canExecuteMethode;
        }
    }
}
