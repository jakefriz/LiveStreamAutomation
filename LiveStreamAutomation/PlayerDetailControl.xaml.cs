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
    /// Interaction logic for PlayerDetailControl.xaml
    /// </summary>
    public partial class PlayerDetailControl : UserControl
    {
        public PlayerDetailControl(Team team)
        {
            InitializeComponent();

            foreach (Player player in team.Players)
            {
                PlayerControl pc = new PlayerControl(player);
                stkPlayerDetail.Children.Add(pc);
            }
        }
    }
}
