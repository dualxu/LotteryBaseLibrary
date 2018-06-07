using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AxTICAICUTLib;
using LotteryBaseLib.Public;

namespace LotteryBaseLib.TiCaiCut
{   
    /// <summary>
    /// 切纸器设备类
    /// </summary>
    public partial class TiCaiCut
    {
        AxTiCaiCut _tcc = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TiCaiCut()
        {
            _tcc = new AxTiCaiCut();
            //
            _tcc.BeginInit();
            _tcc.CreateControl();
            _tcc.EndInit();
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="sPort">端口号,例如"1"表示串口1</param>
        /// <param name="iBPS">波特率，9600</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public short Open(string sPort, short iBPS)
        {
            string strcom = "COM" + sPort;
            short ret = _tcc.Open(strcom, iBPS);
            if (ret != 0) _tcc.Close();
            return ret;
        }

        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="length">长度，5元:101.6,10元:152,20元:202,30元:254</param>
        /// <returns></returns>
        public short SetLength(short length)
        {
            return _tcc.SetLeng(length);
        }

        /// <summary>
        /// 查询状态
        /// </summary>
        /// <returns></returns>
        public short GetState()
        {
            return _tcc.GetState();
        }

        /// <summary>
        /// 切纸
        /// </summary>
        /// <returns></returns>
        public short CutPaper()
        {
            return _tcc.CutPaper();
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public void Close()
        {
            _tcc.Close();
        }
    }
}
