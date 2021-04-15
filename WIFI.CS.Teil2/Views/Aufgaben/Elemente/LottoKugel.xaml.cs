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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WIFI.CS.Teil2
{
    /// <summary>
    /// Interaktionslogik für LottoKugel.xaml
    /// </summary>
    public partial class LottoKugel : UserControl
    {
        /// <summary>
        /// Initialisiert ein neues LottoKugel-Objekt
        /// </summary>
        public LottoKugel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ruft die Nummer dieser Kugel ab
        /// oder legt diese fest
        /// </summary>
        public int? Zahl
        {
            get { return this.GetValue(ZahlProperty) as int?; }
            set { this.SetValue(ZahlProperty, value) ; }
        }

        /// <summary>
        /// Bezeichnet die LottoKugel.Zahl Abhängigkeitseigenschaft
        /// </summary>
        public static readonly DependencyProperty ZahlProperty = DependencyProperty.Register("Zahl", typeof(int?), typeof(LottoKugel));
    }
}
