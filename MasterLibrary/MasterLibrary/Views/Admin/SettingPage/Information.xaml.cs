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

namespace MasterLibrary.Views.Admin.SettingPage
{
    /// <summary>
    /// Interaction logic for Information.xaml
    /// </summary>
    public partial class Information : Page
    {
        public TextBox txb1;
        public TextBox txb2;
        public TextBox txb3;

        public Information()
        {
            InitializeComponent();
            txb1 = txbname;txb2 = txbusername; txb3 = txbemail;
        }
       
    }
}
