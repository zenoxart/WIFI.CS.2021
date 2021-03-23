using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
