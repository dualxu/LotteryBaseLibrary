using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AxZT_SCANNERSE2102Lib;
using LotteryBaseLib.Public;

namespace LotteryBaseLib.Scanner
{
    /// <summary>
    /// 扫描仪ScannerSE2102类
    /// 调用逻辑：
    /// 1.OpenDevice()
    /// 2.Startup()
    /// 3.循环执行GetData()直到获取到扫描数据然后Stop（）停止扫描 结束；或 直接执行 Stop() 停止扫描
    /// 4.再从第2步开始，或第5步关闭设备
    /// 5.CloseDevice()
    /// </summary>
    public class Scanner
    {
        AxZT_ScannerSE2102 _scan = null;
        int m_bStartup = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Scanner()
        {
            _scan = new AxZT_ScannerSE2102();
            //
            _scan.BeginInit();
            _scan.CreateControl();
            _scan.EndInit();          
        }

        /// <summary>
        /// 打开扫描设备
        /// </summary>
        /// <param name="iPort">端口号，例如1表示串口1</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int OpenDevice(short iPort)
        {
            string strCom = "COM" + iPort.ToString() + ":115200:N:8:1";
            int ret = _scan.OpenDevice(strCom);
            if (ret != 0) _scan.CloseDevice();
            return ret;
        }

        /// <summary>
        /// 关闭扫描设备
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int CloseDevice()
        {
            return _scan.CloseDevice();
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <param name="version">版本号或错误信息</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int GetVersion(out string version)
        {
            string retStr = "";
            retStr = _scan.GetVersion();
            if (retStr.Length < 1)
            {
                version = _scan.ErrMsg;
                return -1;
            }
            version =  retStr;
            return 0;
        }

        /// <summary>
        /// 启动扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int Startup()
        {
            int ret = _scan.Startup();
            if (ret == 0)
            {
                m_bStartup = 1;
            }
            else
            {
                m_bStartup = 0;
            }
            return ret;
        }

        /// <summary>
        /// 停止扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int Stop()
        {
            m_bStartup = 0;
            return _scan.Stop();
        }

        /// <summary>
        /// 获取扫描数据
        /// </summary>
        /// <param name="BarCode">成功时返回扫描到的条形码</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int GetData(out string BarCode)
        {
            if (m_bStartup != 1)
            {
                BarCode = "";
                return -1;
            }

            string strData = _scan.GetData();
            if (strData.Length < 1)
            {
                BarCode = "";
                return -2;
            }
            m_bStartup = 0;
            BarCode = strData;
            return 0;
        }
    }
}

