using System.Windows;
using System.Windows.Input;

namespace WIFI.CS.Teil2
{
    /// <summary>
    /// Interaktionslogik für Einstellungsfenster.xaml
    /// </summary>
    public partial class Einstellungsfenster : Window
    {
        public Einstellungsfenster()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Ruft das Design aus den Ressourcen oder legt dieses fest
        /// </summary>
        public bool DunklesDesign
        {
            get { return Properties.Settings.Default.DunklesDesign; }
            set { Properties.Settings.Default.DunklesDesign = value; }
        }

        private void SchließenMitEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
