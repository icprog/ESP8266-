using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
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
            }

            if (comPortName.Items.Count > 0)
            {
                comPortName.SelectedIndex = 0;

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

            DataTransform(serialPort1, AT.WorkWay(comWorkWay.SelectedIndex + 1));
            
        }

        private void DataTransform(SerialPort port,string strWitr)
        {
            txtComWrite.Text += strWitr+Environment.NewLine;
            port.WriteLine(strWitr + Environment.NewLine);
            //port.Write(strWitr);
            Thread.Sleep(20);
            txtComRead.Text +=  port.ReadExisting()+ Environment.NewLine;
           
        }

        private void tbnClearSend_Click(object sender, EventArgs e)
        {
            txtComWrite.Text = "";
        }

        private void btnClearRece_Click(object sender, EventArgs e)
        {
            txtComRead.Text = "";
        }
    }
}

