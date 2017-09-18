using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESP8266.HelperClass;

namespace ESP8266
{

    
    public partial class Form1 : Form
    {

        private  ATCommand AT=new ATCommand();

        private string strComRead = "";
        private string strComWQrite = "";
        private byte[] byteComRead;

        //创建一个委托，是为访问TextBox控件服务的。
        public delegate void UpdateTxt(string msg,TextBox txtBox);
        //定义一个委托变量
        public UpdateTxt updateTxt;

   
        public Form1()
        {
            InitializeComponent();
            comWorkWay.SelectedIndex = 0;
            comBaudRate.SelectedIndex = 0;
            ScanCom();
            updateTxt = new UpdateTxt(UpdateTxtMethod);
           

        }

        private void ApendText(string msg, TextBox txtbox)
        {
            
        }

        private void BtnScanCom_Click(object sender, EventArgs e)
        {

            ScanCom();
        }

        #region 串口加载

        private void ScanCom()
        {

            foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
            {
                this.comPortName.Items.Add(com);
                comSetPort.Items.Add(com);
            }

            if (comPortName.Items.Count > 0)
            {
                comPortName.SelectedIndex = 0;
                comSetPort.SelectedIndex = 0;
            }
        }

        #endregion

        private void btnOpenCom_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.serialPort1.IsOpen)
                {
                    this.serialPort1.Close();
                }
                else
                {
                    // 设置端口参数
                    this.serialPort1.BaudRate = int.Parse(this.comBaudRate.Text);
                    this.serialPort1.DataBits = 8;
                    this.serialPort1.StopBits = StopBits.One;
                    this.serialPort1.Parity = Parity.None;
                    this.serialPort1.PortName = this.comPortName.Text;
              
                    //打开端口
                    this.serialPort1.Open();
                }
          
                if (this.serialPort1.IsOpen)
                {
                    this.btnOpenCom.Text = "关闭端口";

                }
                else
                {

                    this.btnOpenCom.Text = "打开端口";
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("端口打开失败！" + er.Message, "提示");
            }
        }

        private void btnSetWorkWay_Click(object sender, EventArgs e)
        {

            ComDataTransform(serialPort1, AT.WorkWay(comWorkWay.SelectedIndex + 1));
            
        }

        private void ComDataTransform(SerialPort port,string strWitr)
        {
            if (port.IsOpen)
            {
                txtComWrite.Text += strWitr + Environment.NewLine;
                port.WriteLine(strWitr + Environment.NewLine);
                //port.Write(strWitr);
             
                Thread.Sleep(200);
                txtComRead.Text += port.ReadExisting() + Environment.NewLine;
            }
            else
            {
                MessageBox.Show("请打开串口");
            }
           
        }

        private void tbnClearSend_Click(object sender, EventArgs e)
        {
            txtComWrite.Text = "";
        }

        private void btnClearRece_Click(object sender, EventArgs e)
        {
            txtComRead.Text = "";
        }

        private void btnSetBaudRate_Click(object sender, EventArgs e)
        {
            ComDataTransform(serialPort1, AT.BaudRate);
          
        }

        private void BtnScanCom_Click_1(object sender, EventArgs e)
        {
            ScanCom();
        }

        private void btnSetApIP_Click(object sender, EventArgs e)
        {


            SetAp_IP(txtApIP);
        }

        private void SetAp_IP(TextBox txtBox)
        {
            IPAddress ip;
            if (IPAddress.TryParse(txtBox.Text.Trim(), out ip))
            {
                ComDataTransform(serialPort1, AT.Ap_ip(ip.ToString()));
            }
            else
            {
                MessageBox.Show("请输入正确的ip地址");
            }
        }

        private void tbnClearSend_Click_1(object sender, EventArgs e)
        {
            txtComWrite.Text = "";
        }

        private void btnClearRece_Click_1(object sender, EventArgs e)
        {
            txtComRead.Text = "";
        }

        private void btnReBoot_Click(object sender, EventArgs e)
        {
            ComDataTransform(serialPort1,AT.ReBoot);
        }

        private void btnApStartServer_Click(object sender, EventArgs e)
        {
            int port = 0;
            if (int.TryParse(txtApPort.Text,out port))
            {
                ComDataTransform(serialPort1, AT.Ap_StartServer(port));
            }
            else
            {

                MessageBox.Show("请输入数字");
            }
            
        }

        private void btnApCloseServer_Click(object sender, EventArgs e)
        {
            ComDataTransform(serialPort1, AT.Ap_StopServer);
        }

        private void btnSetMuil_Click(object sender, EventArgs e)
        {
            ComDataTransform(serialPort1, AT.MutiLink);
        }

        private void btnSetStaIp_Click(object sender, EventArgs e)
        {
            SetStationIp(txtStaIp);
        }

        private void SetStationIp(TextBox txtBox)
        {
            IPAddress ip;
            if (IPAddress.TryParse(txtBox.Text.Trim(), out ip))
            {
                ComDataTransform(serialPort1, ip.ToString());
            }
            else
            {
                MessageBox.Show("请输入正确IP地址");

            }
        }

        private void btnApTimeOut_Click(object sender, EventArgs e)
        {
            int timeout = 0;
            if (int.TryParse(txtApTimeOut.Text,out timeout))
            {
                ComDataTransform(serialPort1,timeout.ToString());
            }
            else
            {
                MessageBox.Show("请输入数字");
            }
        }

        private void SetApIP_Click(object sender, EventArgs e)
        {
            SetAp_IP(txtAp_Ip);
        }

        private void SetStaIp_Click(object sender, EventArgs e)
        {
            SetStationIp(txtSta_IP);
        }
        private Socket clientSocket;
        private ManualResetEvent connectDone = new ManualResetEvent(false);

        private void btnTcpConnect_Click(object sender, EventArgs e)
        {
            try
            {
                int serverPort = 0;
                IPAddress ip;
                if (int.TryParse(txtServerPort.Text,out serverPort)&&IPAddress.TryParse(txtTcpSeverIP.Text,out ip))
                {
                    IPEndPoint remoteEP = new IPEndPoint(ip, serverPort);
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallBack), clientSocket);
                    connectDone.WaitOne();
                    Receive(clientSocket);
                }
                else
                {
                    MessageBox.Show("请输入正确的ip地址或者端口号");
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 開始非同步接收主機端資料
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                // 完成连接
                client.EndConnect(ar);

                string str = "已连接到服务器: " + client.RemoteEndPoint.ToString();


                if (txtTcpRece.InvokeRequired)
                {
                    txtTcpRece.BeginInvoke(updateTxt,str , txtTcpRece);
                }
                else
                {
                    txtTcpRece.AppendText(str + Environment.NewLine);
                }

                // 狀態設定為未收到訊號
                connectDone.Set();
            }
            catch (Exception e)
            {
                connectDone.Set();
                MessageBox.Show(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = null;
                state = (StateObject)ar.AsyncState;
                Socket server = state.workSocket;

                // 從主機端讀取資料
                int bytesRead = server.EndReceive(ar);
                // 有資料
                if (bytesRead > 0)
                {
                  
                    String MSG = Encoding.ASCII.GetString(state.buffer);
                    if (txtTcpRece.InvokeRequired)
                    {
                        txtTcpRece.BeginInvoke(updateTxt, MSG, txtTcpRece);
                    }
                    else
                    {
                        txtTcpRece.AppendText(MSG + Environment.NewLine);
                    }
                }
                // 繼續等待主機回傳的資料
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public void UpdateTxtMethod(string msg,TextBox txtBox)
        {
            txtBox.Text+=(msg + Environment.NewLine);
            txtBox.ScrollToCaret();
        }

        private void btnTcpClearSend_Click(object sender, EventArgs e)
        {
            txtSendCmd.Text = "";
        }

        private void btnTcpClearRece_Click(object sender, EventArgs e)
        {
            txtTcpRece.Text = "";
        }

        private void txtTcpSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSendCmd.Text == "")
                {
                    MessageBox.Show("请输入发送信息");
                    return;
                }


                 if (clientSocket!=null )
              
                {

                    if ( clientSocket.Connected)
                    {
                        byte[] byteData = Encoding.ASCII.GetBytes(txtSendCmd.Text.Trim());

                        clientSocket.Send(byteData);

                        txtSendCmd.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("与服务器连接断开,请连接");
                    }
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

