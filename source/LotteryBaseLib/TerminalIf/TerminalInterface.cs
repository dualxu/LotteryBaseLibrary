using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryBaseLib.TerminalIf
{
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
}
