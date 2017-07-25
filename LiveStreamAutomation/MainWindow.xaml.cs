using System;
using System.Windows;
using Microsoft.Owin.Hosting;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace LiveStreamAutomation
{
    public enum UIChoice
    {
        LowerThird,
        None,
        PlayOrder,
        ResultsOrder,
        PlayerDetail
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow root;
        public UIChoice _uiChouice = UIChoice.LowerThird;
        private IDisposable restApi = null;
        private string baseAddress = "http://+:9000/";
        public TeamList _teamList = new TeamList();
        public Team _lastReceivedTeam;
        public MainWindow()
        {
            root = this;
            InitializeComponent();

            Closed += MainWindow_Closed;

            Loaded += MainWindow_Loaded;
        }

        internal void WriteNames(Team team)
        {
            if (team == null)
            {
                team = _lastReceivedTeam;
            }
            if (team == null)
            {
                try
                {
                    team = _teamList.Teams[0];
                }
                catch
                {
                    return;
                }
                
            }
            TeamList teamListToShow = new TeamList();
            var teamsToShow = _teamList.Teams.Where(
                x => x.Division == team.Division &&
                x.Event == team.Event &&
                x.Pool == team.Pool &&
                x.Round == team.Round
                );
            foreach (var t in teamsToShow)
            {
                teamListToShow.UpdateTeam(t);
            }
            switch (_uiChouice)
            {
                case UIChoice.LowerThird:
                    if (team != null)
                    {
                        Dispatcher.Invoke((Action)delegate ()
                        {
                            LowerThirdsControl lc = new LowerThirdsControl(team);
                            grdEverything.Children.Clear();
                            grdEverything.Children.Add(lc);
                        });
                    }
                    break;
                case UIChoice.None:
                    Dispatcher.Invoke((Action)delegate ()
                    {
                        grdEverything.Children.Clear();
                    });
                    break;
                case UIChoice.PlayOrder:
                    Dispatcher.Invoke((Action)delegate ()
                    {
                        PlayOrderControl po = new PlayOrderControl(teamListToShow);
                        grdEverything.Children.Clear();
                        grdEverything.Children.Add(po);
                    });
                    break;
                case UIChoice.ResultsOrder:
                    Dispatcher.Invoke((Action)delegate ()
                    {
                        ResultsOrderControl po = new ResultsOrderControl(teamListToShow);
                        grdEverything.Children.Clear();
                        grdEverything.Children.Add(po);
                    });
                    break;
                case UIChoice.PlayerDetail:
                    Dispatcher.Invoke((Action)delegate ()
                    {
                        PlayerDetailControl pdc = new PlayerDetailControl(team);
                        grdEverything.Children.Clear();
                        grdEverything.Children.Add(pdc);
                    });
                    break;
                default:
                    break;
            }

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Start OWIN host
            // netsh http add urlacl url=http://+:9000/ user=%COMPUTERNAME%\%USERNAME%
            restApi = WebApp.Start<Startup>(url: baseAddress);

            //ResultsOrderControl poc = new ResultsOrderControl(MakeTestData(10,4));
            //PlayOrderControl poc = new PlayOrderControl(MakeTestData(10, 4));
            //grdEverything.Children.Add(poc);
        }

        public void UpdateTeam(Team team)
        {
            _lastReceivedTeam = team;
            _teamList.UpdateTeam(team);
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (restApi != null)
            {
                restApi.Dispose();
            }
        }
        public void DisableAll()
        {

        }

        public void EnableLowerThird()
        {

        }

        public void EnablePlayOrder()
        {

        }

        public void EnableScores()
        {

        }

        public void EnableTeamDetail()
        {

        }
    }

    public class LiveStreamAutomationContext : DbContext
    {
        public DbSet<TeamControl> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

        }
    }
}
