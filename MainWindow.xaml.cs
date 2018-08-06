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
using System.Windows.Threading;

namespace TimeBar
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime start;
        TimeSpan duration;
        TimeBarOptions options;

        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            var args = Environment.GetCommandLineArgs().Skip(1).ToArray();
            try
            {
                var parseResult = CommandLine.Parser.Default.ParseArguments<TimeBarOptions>(args) as CommandLine.Parsed<TimeBarOptions>;
                if (parseResult != null)
                {
                    options = parseResult.Value;

                    this.Width = options.Width;
                    this.Height = options.Height;
                    elapsedCanvas.Background = new SolidColorBrush(options.ColorValueElapsed);
                    remainingCanvas.Background = new SolidColorBrush(options.ColorValueRemaining);
                    frameBorde.BorderBrush = new SolidColorBrush(options.ColorValueBorder);
                    var bgColor = Colors.White;
                    bgColor.A = options.Transparency;
                    start = DateTime.Now;
                    if (options.EndTime != null)
                    {
                        var endTime = DateTime.Parse(options.EndTime);
                        duration = endTime - start;
                    }
                    else
                    {
                        duration = TimeSpan.FromMinutes(options.Minutes);
                    }
                    var check = TimeSpan.MinValue.ToString(options.Format);
                    this.Topmost = options.TopMost;
                }
                else
                {
                    MessageBox.Show($"Option Parse Error: {string.Join(" ", args.Select(x => $"\"{x}\""))}");
                    this.Close();
                }

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(options.UpdateIntervalSec);
                timer.Tick += (sender, e) => {
                    var now = DateTime.Now;
                    var elapsed = now - start;
                    var elapsedWidth = (int)(this.Width * elapsed.Ticks / duration.Ticks);
                    elapsedCanvas.Width = elapsedWidth;
                    var remaining = start + duration - now;
                    string format;
                    if (remaining.Ticks >= 0)
                    {
                        format = options.Format;
                    }
                    else
                    {
                        format = $"\\-{options.Format}";
                    }
                    timeLabel.Content = remaining.ToString(format);
                }; 
                timer.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
