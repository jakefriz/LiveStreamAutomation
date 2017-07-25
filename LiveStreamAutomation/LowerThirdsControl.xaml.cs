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
    /// Interaction logic for LowerThirdsControl.xaml
    /// </summary>
    public partial class LowerThirdsControl : UserControl
    {
        System.Timers.Timer timer = null;
        public LowerThirdsControl(Team t)
        {
            InitializeComponent();

            stckNames.Children.Clear();
            Viewbox vbLine1 = new Viewbox();
            vbLine1.Height = 65;
            vbLine1.Margin = new Thickness(5);
            vbLine1.HorizontalAlignment = HorizontalAlignment.Left;
            TextBlock txtLine1 = new TextBlock();
            txtLine1.Name = "txtHeader";
            txtLine1.Text = t.Event + " " + t.Division + " " + t.Round;
            if (!string.IsNullOrEmpty(t.Pool))
            {
                txtLine1.Text += " Pool: " + t.Pool;
            }
            txtLine1.FontFamily = new FontFamily("Segoe UI");
            //txtLine1.Margin = new Thickness(5);
            vbLine1.Child = txtLine1;
            stckNames.Children.Add(vbLine1);

            Viewbox vbLine2 = new Viewbox();
            vbLine2.Height = 65;
            vbLine2.Margin = new Thickness(5);
            vbLine2.HorizontalAlignment = HorizontalAlignment.Left;
            TextBlock txtLine2 = new TextBlock();
            txtLine2.Name = "txtPLayers";
            int x = 0;
            foreach (var item in t.Players)
            {
                x++;
                txtLine2.Text = txtLine2.Text + item.Name;
                if (x < t.Players.Count)
                {
                    txtLine2.Text += ", ";
                }

            }
            txtLine2.FontFamily = new FontFamily("Segoe UI");
            //txtLine1.Margin = new Thickness(5);
            vbLine2.Child = txtLine2;
            stckNames.Children.Add(vbLine2);

            switch (t.State)
            {
                case TeamStates.None:
                    break;
                case TeamStates.JudgesReady:
                    Visible(true);
                    break;
                case TeamStates.Begin:
                    Visible(true);
                    Visible(false, 10000);
                    break;
                case TeamStates.Stopped:
                    Visible(true);
                    Visible(false);
                    break;
                case TeamStates.Finished:
                    Visible(true);
                    Visible(false, 10000);
                    break;
                case TeamStates.ScoresRecorded:

                    break;
                default:
                    break;
            }
        }

        private void Visible(bool visible, double interval = 1)
        {
            Dispatcher.Invoke((Action)delegate ()
            {
                if (visible)
                {
                    grdLowerThird.Visibility = Visibility.Visible;
                }
                else
                {
                    if (timer == null)
                    {
                        timer = new System.Timers.Timer(interval);
                        timer.Elapsed += Timer_Elapsed;
                        timer.Enabled = true;
                        timer.Start();
                    }
                }
            });
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke((Action)delegate ()
            {
                grdLowerThird.Visibility = Visibility.Hidden;
            });
            timer.Stop();
            timer = null;
        }

    }
}
