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

namespace BOT_UDP
{

    public partial class MainWindow : Window
    {
        private int listenPort;
        private UInt32 rndMessage;
        Random rnd = new Random();
        private bool state=false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void START_STOP_Button_Click(object sender, RoutedEventArgs e)
        {
            if (START_STOP_Button.Content.Equals("START"))
            {
                if (String.IsNullOrEmpty(UDP_PORT_TextBox.Text))
                {
                    listenPort = 50000;
                    UDP_PORT_TextBox.AppendText(listenPort.ToString());
                }

                IP_TextBox.IsEnabled = false;
                UDP_PORT_TextBox.IsEnabled = false;
                state = true;
                START_STOP_Button.Content = "STOP";
                StartListener();
            }
            else
            {
                IP_TextBox.IsEnabled = true;
                UDP_PORT_TextBox.IsEnabled = true;
                state = false;
                START_STOP_Button.Content = "START";
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

        private static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        private void StartListener()
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                while (state)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    string msg = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    DataTableGrid.Items.Add(new { timestamp = GetTimestamp(DateTime.Now), sourceIP = groupEP, message = msg});
                    byte[] sendbuf = Encoding.ASCII.GetBytes("ACK! " + RndMsgBlock.Text);
                    s.SendTo(sendbuf, groupEP);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }

        }
    }
}
