using System;
using System.Collections.Generic;
using System.Linq;
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

        public const string BaudRate = "AT+CIOBAUD=";
   
        public const string Ap_ip = "AT+CIPAP=";
        public const string Sta_ip = "AT+CIPSTA=";
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
            return _Ap_StartServer + port;
        }

        private string _Ap_StopServer = "AT+CIPSERVER=0";

        public string Ap_StopServer(int port)
        {
            return _Ap_StopServer + port;
        }
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

        public const string MutiLink = "AT+CIPMUX=1";
        public const string SingleLink = "AT+CIPMUX=0";

        /// <summary>
        /// AT+CIFSR 查询自身IP地址
        /// </summary>
        public const string QueryIP = "AT+CIFSR";

        public const string Reset = "AT+RST";

        /// <summary>
        /// AT+CIPMODE=0 非穿透模式
        /// </summary>
        public const string Penetrate = "AT+CIPMODE=0";
        /// <summary>
        /// AT+CIPMODE=1 穿透模式
        /// </summary>
        public const string PenetrateNot = "AT+CIPMODE=1";

        /// <summary>
        /// 查询服务超时时间
        /// </summary>
        public const string QueryTimeout= "AT+CIPSTO?";
    }
}
