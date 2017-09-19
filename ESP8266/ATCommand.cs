using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ESP8266
{
    public class ATCommand
    {
        /// <summary>
        /// 设置工作模式，1：station，2：AP，3：Ap兼Station
        /// </summary>
        /// <param name="way">1：station，2：AP，3：Ap兼Station</param>
        /// <returns></returns>
        public string WorkWay(int way)
        {
            return "AT+CWMODE=" + way;

        }

        public readonly string BaudRate = "AT+CIOBAUD=";

        public string Ap_ip(string ip)
        {
            return "AT+CIPAP=" + "\"" + ip + "\"";
        }


        public string Sta_ip(string ip)
        {
            return "AT+CIPSTA=" + "\"" + ip + "\"";
        }

        //AT+CWJAP="ESP8266","0123456789"
        private const string _JionWifi = "AT+CWJAP=";

        #region Station模式下接入wifi
        public string Sta_JionWifi(string WifiName, string WifiPwd)
        {

            return _JionWifi + WifiName + "," + WifiPwd;

        }
        #endregion

        //AT+CWSAP="ESP8266","0123456789",11,0
        private const string SetWifiName = "AT+CWSAP=";

        public string Ap_SetWifi(string WifiName, string WifiPwd)
        {

            return SetWifiName + WifiName + "," + WifiPwd + "," + 11 + "," + "0";
        }

        /// <summary>
        /// AT+CWDHCP=0,1,  0为Ap，1为打开
        /// </summary>
        public string DHCP = "AT+CWDHCP";

        /// <summary>
        /// AT+CWAUTOCONN=1  设置sta 开机自动链接，1开，0关
        /// </summary>
        public string Sta_AutoJion = "AT+CWAUTOCONN";


        //AT+CIPSERVER=1,5000 mode：0-关闭server模式，1-开启server模式 5000：端口号
        //多路连接模式下（AT+CIPMUX=1），才能开启TCP服务器
        //关闭server模式需要重启
        private string _Ap_StartServer = "AT+CIPSERVER=1";

        public string Ap_StartServer(int port)
        {
            return _Ap_StartServer + "," + port;
        }

        public readonly string Ap_StopServer = "AT+CIPSERVER=0";


        /// <summary>
        /// AT+CIPSTO=180,服务器超时时间
        /// </summary>
        private string _Ap_ServerTimeOut = "AT+CIPSTO=";
        /// <summary>
        /// AT+CIPSTO=180,服务器超时时间
        /// </summary>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public string Ap_ServerTimeOut(int timeOut)
        {

            return _Ap_ServerTimeOut + timeOut;

        }

        public readonly string MutiLink = "AT+CIPMUX=1";
        public readonly string SingleLink = "AT+CIPMUX=0";

       
      

        public readonly string ReBoot = "AT+RST";

        /// <summary>
        /// AT+CIPMODE=0 非穿透模式
        /// </summary>
        public readonly string PenetrateNot = "AT+CIPMODE=0";
        /// <summary>
        /// AT+CIPMODE=1 穿透模式
        /// </summary>
        public readonly string Penetrate = "AT+CIPMODE=1";

        public readonly string QuitPenetrate = "+++";

        /// <summary>
        /// 查询服务超时时间
        /// </summary>
        public readonly string QueryTimeout = "AT+CIPSTO?";


        public string TcpConnect(int port, IPAddress ip)
        {
            //AT + CIPSTART = "TCP","192.168.1.100",5000

            return "AT+CIPSTART=\"TCP\"," + "\"" + ip.ToString() + "\"" + "," + port;
        }

        public string UDPConnect(int RemoPort, int LocalPort, IPAddress ip)
        {
            //"AT+CIPSTART="UDP","255.255.255.255",5000,5000"
            return "AT+CIPSTART=\"UDP\"," + "\"" + ip + "\"," + RemoPort + "," + LocalPort;

        }

        public string CloseConnect(int id)
        {

            return "AT+CIPCLOSE=" + id;
        }
        /// <summary>
        /// AT+CIFSR 查询自身IP地址
        /// </summary>
        public readonly string QuerySelfIp = "AT+CIFSR";

        public readonly string Version = "AT+GMR";
        public readonly string ScanWifi = "AT+CWLAP";
        public readonly string CloseWifi = "AT+CWQAP";

        public readonly string OpenDog = "AT+CSYSWDTENABLE";
        public readonly string CloseDog = "AT+CSYSWDTDISABLE";
        public readonly string ClearDog = "AT+CSYSWDTCLEAR";

        public readonly string QueryTimeOut = "AT+CIPSTO?";
        public readonly string QueryWorkWAy = "AT+CWMODE?";

        public readonly string QueryJionDevice = "AT+CWLIF";

        public string SetWifi(string name, string pwd, int encrpt)
        {
            //"AT+CWSAP="ESP8266","0123456789",11,0"

            return "AT+CWSAP=\"" + name + "\",\"" + pwd + "\"" + ",11," + encrpt;
        }

        public string JoinWifi(string name, string pwd)
        {
            //AT+CWJAP="ESP8266","0123456789"
            return "AT+CWJAP=\"" + name + "\",\"" + pwd + "\"";
        }

    }
}
