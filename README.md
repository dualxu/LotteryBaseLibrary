# LotteryBaseLib
----
**目录**
---
- docs：相关文档
- reference：参考文档或源码
- source：源代码，包括库及库测试

**LotteryBaseLib库**
---
LotteryBaseLib包括库项目及测试项目

- CashPrize: 扫描仪、打孔设备接口(已弃用但代码保留)
- TerminalIf:终端通讯接口
- TiCaiCut： 切纸器设备接口
- Scanner:   扫描设备接口

**环境**
---
- Visual Studio 2012
- .net Framework 4.5

CashPrize和TiCaiCut,ZT_ScannerSE2102接口需先在系统下使用regsvr32.exe注册对应.ocx控件。

**扫描设备接口:Scanner**
---
<pre><code>
		/// <summary>
        /// 打开扫描设备
        /// </summary>
        /// <param name="iPort">端口号，例如1表示串口1</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int OpenDevice(short iPort);

        /// <summary>
        /// 关闭扫描设备
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int CloseDevice();

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <param name="version">版本号或错误信息</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int GetVersion(out string version);

        /// <summary>
        /// 启动扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int Startup();

        /// <summary>
        /// 停止扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int Stop();

        /// <summary>
        /// 获取扫描数据
        /// </summary>
        /// <param name="BarCode">成功时返回扫描到的条形码</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int GetData(out string BarCode);
</code></pre>

**扫描仪、打孔设备接口接口:CashPrize(已弃用但代码保留)**
---

<pre><code>

        /// <summary>
        /// 打开打孔设备
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="iBaud">波特率,9600</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int OpenPunchDev(short iPort, int iBaud);


        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public int CloseDev();


        /// <summary>
        /// 打开扫描设备
        /// </summary>
        /// <param name="iPort">端口号，例如1表示串口1</param>
        /// <param name="iBaud">波特率,115200</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int OpenScanDev(short iPort, int iBaud);


        /// <summary>
        /// 复位打孔设备
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int PunchReset();


        /// <summary>
        /// 查询打孔设备状态
        /// </summary>
        /// <param name="state1">打孔设备状态1</param>
        /// <param name="state2">打孔设备状态2</param>
        /// <param name="state3">打孔设备状态3</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int GetPunchState(out short state1,out short state2, out short state3);


        /// <summary>
        /// 是否有彩票
        /// </summary>
        /// <param name="ticketstate">彩票状态:85 有，78没有</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public int IsThereAticket(out short ticketstate);


        /// <summary>
        /// 启动扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int StartScan();


        /// <summary>
        /// 停止扫描
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>


        /// <summary>
        /// 获取扫描数据
        /// </summary>
        /// <param name="BarCode">成功时返回扫描到的条形码</param>
        /// <returns>返回：0-成功，其他失败</returns>

        /// <summary>
        /// 打孔
        /// </summary>
        /// <returns>返回：0-成功，其他失败</returns>
        public int Punch();

</code></pre>

**终端通讯接口：TerminalIf**
---
<pre><code>
    /// <summary>
    /// TerminalIf接口定义
    /// </summary>
    public interface ITerminalIfHandler
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="terminalinitreq">初始化请求</param>
        /// <returns></returns>
        TerminalInitRsp TerminalInit(TerminalInitReq terminalinitreq);

        /// <summary>
        /// 预下单
        /// </summary>
        /// <param name="preporderreq">预下单请求</param>
        /// <returns></returns>
        PrepOrderRsp PrepOrder(PrepOrderReq preporderreq);

        /// <summary>
        /// 交易查询
        /// </summary>
        /// <param name="queryorderreq">交易查询请求</param>
        /// <returns></returns>
        QueryOrderRsp QueryOrder(QueryOrderReq queryorderreq);

        /// <summary>
        /// 出票状态更新
        /// </summary>
        /// <param name="outticketreq">出票状态更新请求</param>
        /// <returns></returns>
        OutTicketRsp OutTicket(OutTicketReq outticketreq);

        /// <summary>
        /// 终端兑奖
        /// </summary>
        /// <param name="cashprizereq">终端兑奖请求</param>
        /// <returns></returns>
        CashPrizeRsp CashPrize(CashPrizeReq cashprizereq);

        /// <summary>
        /// 派奖
        /// </summary>
        /// <param name="awardorderreq">派奖请求</param>
        /// <returns></returns>
        AwardOrderRsp AwardOrder(AwardOrderReq awardorderreq);

        /// <summary>
        /// 终端状态同步
        /// </summary>
        /// <param name="terminalupdatereq"></param>
        /// <returns></returns>
        TerminalUpdateRsp TerminalUpdate(TerminalUpdateReq terminalupdatereq);

        /// <summary>
        /// 广告查询
        /// </summary>
        /// <param name="terminalinitreq"></param>
        /// <returns></returns>
        QueryAdsRsp QueryAds(QueryAdsReq terminalinitreq);
    }
</code></pre>

**切纸器设备接口：TiCaiCut**
---

<pre><code>
		/// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="sPort">端口号,例如"1"表示串口1</param>
        /// <param name="iBPS">波特率，9600</param>
        /// <returns>返回：0-成功，其他失败</returns>
        public short Open(string sPort, short iBPS);


        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="length">长度，5元:101.6,10元:152,20元:202,30元:254</param>
        /// <returns></returns>
        public short SetLength(short length);


        /// <summary>
        /// 查询状态
        /// </summary>
        /// <returns></returns>
        public short GetState();


        /// <summary>
        /// 切纸
        /// </summary>
        /// <returns></returns>
        public short CutPaper();


        /// <summary>
        /// 关闭设备
        /// </summary>
        public void Close();

</code></pre>


