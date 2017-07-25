using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PlayOrderControl.xaml
    /// </summary>
    public partial class PlayOrderControl : UserControl
    {
        public PlayOrderControl(TeamList tl)
        {
            InitializeComponent();
            TeamList tlSorted = new TeamList();
            tlSorted.Teams = new ObservableCollection<Team>(tl.Teams.OrderBy(x => x.TeamNumber).ToList());

            double   rowHeight = 51;
            if (tlSorted.Teams.Count >= 10)
            {
                rowHeight = (720 - 50 - 100 - ((tl.Teams.Count + 3)*3)) / (tl.Teams.Count + 1);             
            }
            int y = 0;
            foreach (var t in tlSorted.Teams)
            {
                y++;
                if (t == tlSorted.Teams.First())
                {
                    stkPoolPlayOrder.Children.Add(new TeamListHeader(t));
                    stkPoolPlayOrder.Children.Add(new TeamControl(rowHeight));
                }
                t.TeamNumber = y;
                stkPoolPlayOrder.Children.Add(new TeamControl(rowHeight, t));
            }


        }
    }
}
