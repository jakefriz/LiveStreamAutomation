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

namespace LiveStreamAutomation
{
    /// <summary>
    /// Interaction logic for TeamListHeader.xaml
    /// </summary>
    public partial class TeamListHeader : UserControl
    {
        public TeamListHeader(Team t)
        {
            InitializeComponent();

            txtRow1.Text = t.Event;
            txtRow2.Text = t.Division + " " + t.Round;
            if (!String.IsNullOrEmpty(t.Pool))
            {
                txtRow2.Text += " - Pool " + t.Pool;
            }
        }
    }
}
