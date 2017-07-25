using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
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
using LiveStreamAutomation;

namespace Client
{
	public class NoCharSetJsonMediaTypeFormatter : JsonMediaTypeFormatter
	{
		public override void SetDefaultContentHeaders(Type type, System.Net.Http.Headers.HttpContentHeaders headers, System.Net.Http.Headers.MediaTypeHeaderValue mediaType)
		{
			base.SetDefaultContentHeaders(type, headers, mediaType);
			headers.ContentType.CharSet = "";
		}
	}

	public static class HttpClientExtensions
	{
		public static async Task<HttpResponseMessage> PostAsJsonWithNoCharSetAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return await client.PostAsync(requestUri, value, new NoCharSetJsonMediaTypeFormatter());
		}
	}

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _baseAddress = "http://localhost:9000/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private LiveStreamAutomation.TeamList MakeTestData(int numOfTeams, int numOfTeamsWithScores)
        {
            string Event = "FPAW 2017";
            string Division = "Open Pairs";
            string Pool = "A";
            string Round = "Semifinals";
            LiveStreamAutomation.TeamList tl = new LiveStreamAutomation.TeamList();
            List <LiveStreamAutomation.Player> playerList = MakePlayerList();
            Random rnd = new Random();
            int x = 0;
            int y = 1;
            for (int i = 0; i < numOfTeams; i++)
            {
                LiveStreamAutomation.Team t = new LiveStreamAutomation.Team();
                t.Event = Event;
                t.Division = Division;
                t.Pool = Pool;
                t.Round = Round;
                t.TeamNumber = (i + 1);
                
                List<Player> teamPlayers = new List<Player>();
                teamPlayers.Add(playerList[x]);
                teamPlayers.Add(playerList[y]);
                t.Players = teamPlayers;
                y++;
                if (y >= playerList.Count)
                {
                    x++;
                    y = x + 1;                    
                }
                
                if (i <= numOfTeamsWithScores)
                {
                    t.ArtisticImpressionScore = rnd.Next(1, 10);
                    t.DifficultyScore = rnd.Next(1, 10);
                    t.ExecutionScore = rnd.Next(1, 10);
                }
                tl.UpdateTeam(t);

            }
            return tl;
        }

        private List<LiveStreamAutomation.Player> MakePlayerList()
        {
            List<LiveStreamAutomation.Player> playerList = new List<LiveStreamAutomation.Player>();

            LiveStreamAutomation.Player p = new LiveStreamAutomation.Player();
            p.HomeCity = "Portland, Oregon";
            p.HomeCountry = "USA";
            p.Name = "Matt Gauthier";
            p.Rank = 15;
            playerList.Add(p);

            LiveStreamAutomation.Player p1 = new LiveStreamAutomation.Player();
            p1.HomeCity = "Portland, Oregon";
            p1.HomeCountry = "USA";
            p1.Name = "Jake Gauthier";
            p1.Rank = 99;
            playerList.Add(p1);

            LiveStreamAutomation.Player p2 = new LiveStreamAutomation.Player();
            p2.HomeCity = "Portland, Oregon";
            p2.HomeCountry = "USA";
            p2.Name = "Lisa Hunrichs";
            p2.Rank = 20;
            playerList.Add(p2);

            LiveStreamAutomation.Player p3 = new LiveStreamAutomation.Player();
            p3.HomeCity = "Portland, Oregon";
            p3.HomeCountry = "USA";
            p3.Name = "Lori Daniels";
            p3.Rank = 32;
            playerList.Add(p3);

            LiveStreamAutomation.Player p4 = new LiveStreamAutomation.Player();
            p4.HomeCity = "Seattle, Washington";
            p4.HomeCountry = "USA";
            p4.Name = "Ryan Young";
            p4.Rank = 1;
            playerList.Add(p4);

            LiveStreamAutomation.Player p5 = new LiveStreamAutomation.Player();
            p5.HomeCity = "Seattle, Washingon";
            p5.HomeCountry = "USA";
            p5.Name = "Randy Silvey";
            p5.Rank = 2;
            playerList.Add(p5);

            LiveStreamAutomation.Player p6 = new LiveStreamAutomation.Player();
            p6.HomeCity = "New York, New York";
            p6.HomeCountry = "USA";
            p6.Name = "James Wiseman";
            p6.Rank = 3;
            playerList.Add(p6);

            LiveStreamAutomation.Player p7 = new LiveStreamAutomation.Player();
            p7.HomeCity = "New York, New York";
            p7.HomeCountry = "USA";
            p7.Name = "Daneil O'Neil";
            p7.Rank = 4;
            playerList.Add(p7);

            LiveStreamAutomation.Player p8 = new LiveStreamAutomation.Player();
            p8.HomeCity = "Triest";
            p8.HomeCountry = "Italy";
            p8.Name = "Fabio Sanna";
            p8.Rank = 27;
            playerList.Add(p8);

            return playerList;
        }

        private LiveStreamAutomation.Team getTeamData(LiveStreamAutomation.TeamStates state)
        {
            LiveStreamAutomation.Team t = new LiveStreamAutomation.Team();
            t.Division = txtDivision.Text;
            t.Event = txtEvent.Text;
            t.Pool = txtPool.Text;
            t.Round = txtRound.Text;
            t.TeamNumber = int.Parse(txtTeamNumber.Text);
            t.ArtisticImpressionScore = float.Parse(txtAIScore.Text);
            t.DifficultyScore = float.Parse(txtDIFFScore.Text);
            t.ExecutionScore = float.Parse(txtEXSore.Text);
            if (!String.IsNullOrEmpty(txtPlayer1.Text))
            {
                t.Players.Add(new LiveStreamAutomation.Player() { Name = txtPlayer1.Text });
            }
            if (!String.IsNullOrEmpty(txtPlayer2.Text))
            {
                t.Players.Add(new LiveStreamAutomation.Player() { Name = txtPlayer2.Text });
            }
            if (!String.IsNullOrEmpty(txtPlayer3.Text))
            {
                t.Players.Add(new LiveStreamAutomation.Player() { Name = txtPlayer3.Text });
            }
            t.State = state;
            return t;
        }

        private void CallWebServiceTeam(LiveStreamAutomation.TeamStates state)
        {
            HttpClient client = new HttpClient();
			Task<HttpResponseMessage> t = client.PostAsJsonAsync(_baseAddress + "api/teams", getTeamData(state));

			t.ContinueWith(task =>
            {
                try
                {
                    var response = task.Result;
                    var json = response.Content.ReadAsStringAsync().Result;
                    //LiveStream returnedLiveStream = JsonConvert.DeserializeObject<LiveStream>(json);
                    //l?.Invoke(returnedLiveStream);
                }
                catch { }
                finally
                {
                    //a?.Invoke();
                }
            });
        }

        private void CallWebServiceTeamList(LiveStreamAutomation.TeamList teamList)
        {
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> t = client.PostAsJsonAsync(_baseAddress + "api/TeamList", teamList);

            t.ContinueWith(task =>
            {
                try
                {
                    var response = task.Result;
                    var json = response.Content.ReadAsStringAsync().Result;
                    //LiveStream returnedLiveStream = JsonConvert.DeserializeObject<LiveStream>(json);
                    //l?.Invoke(returnedLiveStream);
                }
                catch { }
                finally
                {
                    //a?.Invoke();
                }
            });
        }

        private void CallWebServiceUI(int ui)
        {
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> t = client.GetAsync(_baseAddress + "api/Ui/" + ui);

            t.ContinueWith(task =>
            {
                try
                {
                    var response = task.Result;
                    var json = response.Content.ReadAsStringAsync().Result;
                    //LiveStream returnedLiveStream = JsonConvert.DeserializeObject<LiveStream>(json);
                    //l?.Invoke(returnedLiveStream);
                }
                catch { }
                finally
                {
                    //a?.Invoke();
                }
            });
        }

        private void btnReady_Click(object sender, RoutedEventArgs e)
        {

            CallWebServiceTeam(LiveStreamAutomation.TeamStates.JudgesReady);

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceTeam(LiveStreamAutomation.TeamStates.Begin);

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceTeam(LiveStreamAutomation.TeamStates.Finished);
        }

        private void btnStopNow_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceTeam(LiveStreamAutomation.TeamStates.Stopped);
        }

        private void btn3rd_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceUI(0);
        }

        private void btnNone_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceUI(1);
        }

        private void btnOrdr_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceUI(2);
        }

        private void btnResults_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceUI(3);
        }

        private void btnPlayerDetail_Click(object sender, RoutedEventArgs e)
        {
            CallWebServiceUI(4);
        }

        private void btnTestData_Click(object sender, RoutedEventArgs e)
        {
            LiveStreamAutomation.TeamList teamList = MakeTestData(10, 4);
            CallWebServiceTeamList(teamList);
            txtAIScore.Text = teamList.Teams[0].ArtisticImpressionScore.ToString();
            txtDIFFScore.Text = teamList.Teams[0].DifficultyScore.ToString();
            txtDivision.Text = teamList.Teams[0].Division;
            txtEvent.Text = teamList.Teams[0].Event;
            txtEXSore.Text = teamList.Teams[0].ExecutionScore.ToString();
            txtPlayer1.Text = (teamList.Teams[0].Players.Count < 1) ? string.Empty : teamList.Teams[0].Players[0].Name;
            txtPlayer2.Text = (teamList.Teams[0].Players.Count < 2) ? string.Empty : teamList.Teams[0].Players[1].Name;
            txtPlayer3.Text = (teamList.Teams[0].Players.Count < 3) ? string.Empty : teamList.Teams[0].Players[2].Name;
            txtPool.Text = teamList.Teams[0].Pool;
            txtRound.Text = teamList.Teams[0].Round;
            txtTeamNumber.Text = teamList.Teams[0].TeamNumber.ToString();
        }
    }
}