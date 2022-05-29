using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
using System.Timers;
using System.Windows.Threading;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;

namespace BOT_UDP
{

    public partial class MainWindow : Window
    {
        BackgroundWorker backgroundWorker1;
        int listenPort;
        string ip;
        UInt32 rndMessage;
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void START_STOP_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                IPandPort();
                IP_TextBox.IsEnabled = false;
                UDP_PORT_TextBox.IsEnabled = false;
                START_STOP_Button.Content = "STOP";

                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                IP_TextBox.IsEnabled = true;
                UDP_PORT_TextBox.IsEnabled = true;
                START_STOP_Button.Content = "START";

                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }

        private void IPandPort()
        {
            if (String.IsNullOrEmpty(IP_TextBox.Text))
            {
                ip = IPAddress.Any.ToString();
            }
            else
            {
                ip = IP_TextBox.Text;
            }

            if (!String.IsNullOrEmpty(UDP_PORT_TextBox.Text) && int.Parse(UDP_PORT_TextBox.Text) >= 5000 && int.Parse(UDP_PORT_TextBox.Text) <= 60000)
            {
                listenPort = int.Parse(UDP_PORT_TextBox.Text);
            }
            else
            {
                listenPort = 50000;
            }
        }

        private void RenderMessage(object sender, EventArgs e)
        {
            rndMessage = Convert.ToUInt32(rnd.Next());
            RndMsgBlock.Text = Convert.ToString(rndMessage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(RenderMessage);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private static String GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        }

        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            UdpClient listener = new UdpClient(listenPort);
            //IPAddress.Any IPAddress.Parse(ip)
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(ip), listenPort);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                while (true)
                {
                    if (backgroundWorker1.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        byte[] bytes = listener.Receive(ref groupEP);
                        string msg = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                        byte[] sendbuf = Encoding.ASCII.GetBytes("ACK! " + rndMessage);
                        s.SendTo(sendbuf, groupEP);
                        backgroundWorker1.ReportProgress(10,msg);
                    }
                }
            }
            catch (SocketException a)
            {
                Console.WriteLine(a);
            }
            finally
            {
                listener.Close();
                s.Close();
                e.Cancel = true;
            }
        }

        // This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataTableGrid.Items.Add(new { timestamp = GetTimestamp(), sourceIP = ip, message = e.UserState as String} );
        }
    }
}
