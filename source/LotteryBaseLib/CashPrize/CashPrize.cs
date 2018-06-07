using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AxCASHPRIZELib;
using LotteryBaseLib.Public;

namespace LotteryBaseLib.CashPrize
{
    /// <summary>
    /// 扫描仪、打孔设备类
    /// </summary>
    public class CashPrize
    {
        AxCashPrize _cp = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CashPrize()
        {
            _cp = new AxCashPrize();
            //
            _cp.BeginInit();
            _cp.CreateControl();
            _cp.EndInit();           
        }

        /// <summary>
        /// 打开打孔设备
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="iBaud">波特率,9600</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int OpenPunchDev(short iPort, int iBaud)
        {
            int ret = _cp.OpenPunchDev(iPort, iBaud);
            if(ret != 0) _cp.CloseDev();
            return ret;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public int CloseDev()
        {
            return _cp.CloseDev();
        }

        /// <summary>
        /// 打开扫描设备
        /// </summary>
        /// <param name="iPort">端口号，例如1表示串口1</param>
        /// <param name="iBaud">波特率,115200</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int OpenScanDev(short iPort, int iBaud)
        {
            int ret = _cp.OpenScanDev(iPort, iBaud);
            if (ret != 0) _cp.CloseDev();
            return ret;
        }

        /// <summary>
        /// 复位打孔设备
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int PunchReset()
        {
            return _cp.PunchReset();
        }

        /// <summary>
        /// 查询打孔设备状态
        /// </summary>
        /// <param name="state1">打孔设备状态1</param>
        /// <param name="state2">打孔设备状态2</param>
        /// <param name="state3">打孔设备状态3</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int GetPunchState(out short state1,out short state2, out short state3)
        {
            state1 = _cp.punchState1;
            state2 = _cp.punchState2;
            state3 = _cp.punchState3;
            return _cp.GetPunchState();
        }

        /// <summary>
        /// 是否有彩票
        /// </summary>
        /// <param name="ticketstate">彩票状态:85 有，78没有</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int IsThereAticket(out short ticketstate)
        {
            ticketstate = _cp.ticketState;
            return _cp.IsThereALotteryTicket();
        }

        /// <summary>
        /// 启动扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int StartScan()
        {
            return _cp.StartScan();
        }

        /// <summary>
        /// 停止扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int StopScan()
        {
            return _cp.StopScan();
        }

        /// <summary>
        /// 获取扫描数据
        /// </summary>
        /// <param name="BarCode">成功时返回扫描到的条形码</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int GetScanData(out string BarCode)
        {
            BarCode = _cp.BarCode;
            return _cp.GetScanData();
        }

        /// <summary>
        /// 打孔
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int Punch()
        {
            return _cp.Punch();
        }


    }
}
