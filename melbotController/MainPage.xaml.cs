using System.Net.Sockets;
using System.Text;
using System.Net;


namespace melbotController
{
    public partial class MainPage : ContentPage
    {
        Socket sock;
        IPEndPoint ep;

        string[] command = {"0", "1", "0", "1"};

        public MainPage()
        {
            InitializeComponent();

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            ep = new IPEndPoint(IPAddress.Parse("192.168.1.74"), 5000);

        }

        

        private void left_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            leftDisp.Text = ((int)(left.Value)).ToString();

            command[0] = ((int)(Math.Abs(left.Value))).ToString("D3");

            command[1]= left.Value < 0 ? "0" : "1";

            sendCommand();
        }

        private void left_DragCompleted(object sender, EventArgs e)
        {
            left.Value = 0;
        }


        private void right_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            rightDisp.Text = ((int)(right.Value)).ToString();

            command[2] = ((int)(Math.Abs(right.Value))).ToString("D3");

            command[3] = right.Value < 0 ? "0" : "1";

            sendCommand();

        }


        private void right_DragCompleted(object sender, EventArgs e)
        {
            right.Value = 0;
        }

        private void IP_Completed(object sender, EventArgs e)
        {
            string IP = IpPort.Text.Split(',')[0];
            string port = IpPort.Text.Split(',')[1];

            IPAddress broadcast = IPAddress.Parse(IP);
            ep = new IPEndPoint(broadcast, 5000);
        }



        void sendCommand()
        {
            string commands = string.Format("{0},{1},{2},{3}", command[0], command[1], command[2], command[3]);            

            byte[] sendbuf = Encoding.ASCII.GetBytes(commands);

            sock.SendTo(sendbuf, ep);
        }


        //Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        //IPAddress broadcast = IPAddress.Parse("10.51.238.62");

        //byte[] sendbuf = Encoding.ASCII.GetBytes("test");
        //IPEndPoint ep = new IPEndPoint(broadcast, 5000);

        //s.SendTo(sendbuf, ep);

        //Console.WriteLine("Message sent to the broadcast address");

    }

}
