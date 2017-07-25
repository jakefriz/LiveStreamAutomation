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
    /// Interaction logic for Team.xaml
    /// </summary>
    public partial class TeamControl : UserControl
    {
        public TeamControl(double rowHeight = 51, Team t = null, string teamNumber = "")
        {
            InitializeComponent();

            grdTop.RowDefinitions[0].Height = new GridLength(rowHeight);

            if (t != null)
            {
                if (String.IsNullOrEmpty(teamNumber))
                {
                    txtPlayOrder.Text = t.TeamNumber.ToString();
                }
                else
                {
                    txtPlayOrder.Text = teamNumber;
                }
                
                string teamInfo = string.Empty;
                foreach (var p in t.Players)
                {
                    teamInfo += p.Name;
                    if (p != t.Players.Last())
                    {
                        teamInfo += ", ";
                    }
                }
                txtTeamInfo.Text = teamInfo;
                if (t.ArtisticImpressionScore != 0 && t.DifficultyScore != 0 && t.ExecutionScore != 0)
                {
                    txtAIScore.Text = t.ArtisticImpressionScore.ToString();
                    txtDiffScore.Text = t.DifficultyScore.ToString();
                    txtExScore.Text = t.ExecutionScore.ToString();
                    txtTotalScore.Text = (t.ExecutionScore + t.DifficultyScore + t.ArtisticImpressionScore).ToString();
                    grdTop.Background = Brushes.LightGray;
                }
            }
            else
            {
                txtPlayOrder.Text = "ORDR";
                txtPlayOrder.FontWeight = FontWeights.Bold;
                txtTeamInfo.Text = "Team";
                txtTeamInfo.FontWeight = FontWeights.Bold;
                txtAIScore.Text = "AI";
                txtTeamInfo.FontWeight = FontWeights.Bold;
                txtDiffScore.Text = "DIFF";
                txtDiffScore.FontWeight = FontWeights.Bold;
                txtExScore.Text = "EX";
                txtExScore.FontWeight = FontWeights.Bold;
                txtTotalScore.Text = "SUM";
                txtTotalScore.FontWeight = FontWeights.Bold;
            }
            
        }
    }
}
