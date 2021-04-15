using System;
using System.Windows.Controls;

namespace WIFI.CS.Teil2
{
    /// <summary>
    /// Interaktionslogik für ProtokollView.xaml
    /// </summary>
    public partial class ProtokollView : UserControl
    {
        /// <summary>
        /// Initialisiert ein neues ProtokollView Objekt
        /// </summary>
        public ProtokollView()
        {
            InitializeComponent();



            // Verweis auf das aktuelle ViewModel
            WIFI.Anwendung.ViewModelAppObjekt ViewModel = null;

            // Speicheradresse der Rückrufmethode 
            System.Action ZumLetztenEintragBlättern = new Action(
                        () =>
                        {

                            if (this.Liste.Items.Count > 0)
                            {
                                this.Liste.ScrollIntoView(this.Liste.Items[this.Liste.Items.Count - 1]);
                            }
                        });

            // Die geladene Liste soll im Anwendungsprotokoll
            // eine Rückrufmethode hinterlegen, mit der ein neuer
            // Eintrag sichtbar gemacht wird
            this.Liste.Loaded += (sender, e) =>
            {
                ViewModel = this.DataContext as WIFI.Anwendung.ViewModelAppObjekt;

                if (ViewModel != null)
                {
                    ViewModel.AppKontext.Protokoll.RückrufBuchen(ZumLetztenEintragBlättern);
                }
            };

            // Wird die Liste entfernt, muss der 
            // Rückruf storniert werden

            this.Liste.Unloaded += (sender, e) =>
            {
                if (ViewModel != null)
                {
                    ViewModel.AppKontext.Protokoll.RückrufStornieren(ZumLetztenEintragBlättern);

                }
            };
        }
    }
}
