using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
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
        public Form1()
        {
            InitializeComponent();
            comWorkWay.SelectedIndex = 0;
            comBaudRate.SelectedIndex = 0;
            ScanCom();
            
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

        private void btnTcpConnect_Click(object sender, EventArgs e)
        {

        }
    }
}

