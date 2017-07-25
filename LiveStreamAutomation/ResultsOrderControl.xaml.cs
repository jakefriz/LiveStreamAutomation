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
    /// Interaction logic for ResultsOrderControl.xaml
    /// </summary>
    public partial class ResultsOrderControl : UserControl
    {
        public ResultsOrderControl(TeamList tl)
        {
            InitializeComponent();
            TeamList tlSorted = new TeamList();
            tlSorted.Teams = new ObservableCollection<Team>(tl.Teams.OrderByDescending(x => x.ArtisticImpressionScore + x.DifficultyScore + x.ExecutionScore).ToList());
            double rowHeight = 51;
            if (tlSorted.Teams.Count >= 10)
            {
                rowHeight = (720 - 50 - 100 - ((tl.Teams.Count + 3) * 3)) / (tl.Teams.Count + 1);
            }
            int y = 0;
            for (int i = 0; i < tlSorted.Teams.Count; i++)
            {
                y++;
                if (tlSorted.Teams[i] == tlSorted.Teams.First())
                {
                    stkPoolResultsOrder.Children.Add(new TeamListHeader(tlSorted.Teams[i]));
                    stkPoolResultsOrder.Children.Add(new TeamControl(rowHeight));
                }
                
                stkPoolResultsOrder.Children.Add(new TeamControl(rowHeight, tlSorted.Teams[i], y.ToString()));
            }



        }
    }
}