using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryBaseLib.TerminalIf
{
    #region 初始化
    /// <summary>
    /// 初始化请求数据
    /// </summary>
    public class RequestInitData
    {
        /// <summary>
        /// 应用名称，terminalInit.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 终端类型，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 初始化请求
    /// </summary>
    public class TerminalInitReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 初始化请求数据
        /// </summary>
        public RequestInitData requestData { get; set; }
    }

    /// <summary>
    /// 票箱和彩票数据
    /// </summary>
    public class TerminalInitLotteryDtosItem
    {
        /// <summary>
        /// 票箱ID
        /// </summary>
        public string boxId { get; set; }
        /// <summary>
        /// 彩票ID
        /// </summary>
        public string lotteryId { get; set; }
        /// <summary>
        /// 彩票名称
        /// </summary>
        public string lotteryName { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string lotteryImg { get; set; }
        /// <summary>
        /// 彩票单价，单位分
        /// </summary>
        public string lotteryAmt { get; set; }
        /// <summary>
        /// 剩余
        /// </summary>
        public string surplus { get; set; }
        /// <summary>
        /// 票箱状态
        /// 1 正常
        /// 2 无票
        /// 3 故障
        /// </summary>
        public string boxStatus { get; set; }
    }

    /// <summary>
    /// 初始化应答数据
    /// </summary>
    public class ResponseInitData
    {
        /// <summary>
        /// 应用名称,terminalInit.Rsp
        /// </summary>
        public string application { get; set; }        
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 登录后返回的SESSIONID,C
        /// 保留动态密钥，暂时无用
        /// </summary>
        public string sessionKey { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 终端状态，设备状态
        /// 0 待激活
        /// 1 已激活
        /// 2 待维修
        /// 3 已暂停
        /// 4 设备无票
        /// </summary>
        public string terminalStatus { get; set; }
        /// <summary>
        /// 广告地址1，C，当设备更新状态为00时返回
        /// </summary>
        public string img1 { get; set; }
        /// <summary>
        /// 广告地址2，C，当设备更新状态为00时返回
        /// </summary>
        public string img2 { get; set; }
        /// <summary>
        /// 广告地址3，C，当设备更新状态为00时返回
        /// </summary>
        public string img3 { get; set; }
        /// <summary>
        /// 票箱和彩票数据列表，C，当设备更新状态为00时返回
        /// </summary>
        public List<TerminalInitLotteryDtosItem> terminalLotteryDtos { get; set; }
        /// <summary>
        /// 更新状态
        /// 00 不更新
        /// 01 强制更新
        /// 02 非强制更新
        /// </summary>
        public string updateStatus { get; set; }
        /// <summary>
        /// 更新地址，C，如更新状态不为0则返回URL
        /// </summary>
        public string updateAddress { get; set; }
        /// <summary>
        /// 附加信息，C，如更新状态不为0则返回更新描述
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 初始化应答
    /// </summary>
    public class TerminalInitRsp
    {
        /// <summary>
        /// 初始化应答数据
        /// </summary>
        public ResponseInitData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 预下单
    /// <summary>
    /// 预下单票箱
    /// </summary>
    public class TerminalPrepOrderLotteryDtosItem
    {
        /// <summary>
        /// 票箱ID
        /// </summary>
        public string boxId { get; set; }
        /// <summary>
        /// 彩种
        /// </summary>
        public string lotteryId { get; set; }
        /// <summary>
        /// 张数
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 单价，单位分
        /// </summary>
        public string lotteryAmt { get; set; }        
    }

    /// <summary>
    /// 预下单请求数据
    /// </summary>
    public class RequestPrepOrderData
    {
        /// <summary>
        /// 应用名称,prepOrder.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号,默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 终端类型，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string merOrderId { get; set; }
        /// <summary>
        /// 订单时间,YYYYMMDDHHmmss
        /// </summary>
        public string merOrderTime { get; set; }
        /// <summary>
        /// 订单总金额，单位分
        /// </summary>
        public string orderAmt { get; set; }  
        /// <summary>
        /// 预下单票箱列表
        /// </summary>
        public List<TerminalPrepOrderLotteryDtosItem> terminalLotteryDtos { get; set; }
        /// <summary>
        /// 支付类型
        /// 01 支付宝
        /// 02 微信
        /// 03 微信公众号支付
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 通知地址，C
        /// </summary>
        public string notifyUrl { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }

    }

    /// <summary>
    /// 预下单请求
    /// </summary>
    public class PrepOrderReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 预下单请求数据
        /// </summary>
        public RequestPrepOrderData requestData { get; set; }
    }

    /// <summary>
    /// 预下单应答数据
    /// </summary>
    public class ResponsePrepOrderData
    {
        /// <summary>
        /// 应用名称,prepOrder.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，R
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R
        /// </summary>
        public string version { get; set; }        
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号，R
        /// </summary>
        public string merOrderId { get; set; }        
        /// <summary>
        /// 支付信息，C
        /// </summary>
        public string qrCode { get; set; }
        /// <summary>
        /// 附加信息，C
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }       
    }

    /// <summary>
    /// 预下单应答
    /// </summary>
    public class PrepOrderRsp
    {
        /// <summary>
        /// 预下单应答数据
        /// </summary>
        public ResponsePrepOrderData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion
    
    #region 交易查询
    /// <summary>
    /// 交易查询请求数据
    /// </summary>
    public class RequestQueryOrderData
    {
        /// <summary>
        /// 应用名称，queryOrder.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 终端类型，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string merOrderId { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 交易查询请求
    /// </summary>
    public class QueryOrderReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 交易查询请求数据
        /// </summary>
        public RequestQueryOrderData requestData { get; set; }
    }

    /// <summary>
    /// 交易查询应答数据
    /// </summary>
    public class ResponseQueryOrderData
    {
        /// <summary>
        /// 应用名称,queryOrder.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号，R
        /// </summary>
        public string merOrderId { get; set; }
        /// <summary>
        /// 订单时间，YYYYMMDDHHmmss
        /// </summary>
        public string merOrderTime { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public string orderAmt { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string orderDesc { get; set; }
        /// <summary>
        /// 订单状态
        /// 0 未处理
        /// 1 成功
        /// 2 失败
        /// 3 处理中
        /// </summary>
        public string orderStatus { get; set; }
        /// <summary>
        /// 支付类型
        /// 01 支付宝
        /// 02 微信
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 附加信息，C
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }        
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 交易查询应答
    /// </summary>
    public class QueryOrderRsp
    {
        /// <summary>
        /// 交易查询应答数据
        /// </summary>
        public ResponseQueryOrderData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 出票状态更新
    /// <summary>
    /// 出票状态更新请求数据
    /// </summary>
    public class RequestOutTicketData
    {
        /// <summary>
        /// 应用名称，outTicket.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 终端类型，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string merOrderId { get; set; }
        /// <summary>
        /// 票箱和彩票数据列表
        /// </summary>
        public List<OutTicketLotteryDtosItem> terminalLotteryDtos { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 出票状态更新请求
    /// </summary>
    public class OutTicketReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 出票状态更新请求数据
        /// </summary>
        public RequestOutTicketData requestData { get; set; }
    }

    /// <summary>
    /// 票箱和彩票数据
    /// </summary>
    public class OutTicketLotteryDtosItem
    {
        /// <summary>
        /// 票箱ID
        /// </summary>
        public string boxId { get; set; }
        /// <summary>
        /// 出票状态
        /// 1 出票成功
        /// 2 出票异常
        /// </summary>
        public string ticketStatus { get; set; }
        /// <summary>
        /// 票箱余票
        /// </summary>
        public string surplus { get; set; }
    }

    /// <summary>
    /// 出票状态更新应答数据
    /// </summary>
    public class ResponseOutTicketData
    {
        /// <summary>
        /// 应用名称,outTicket.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号，R
        /// </summary>
        public string merOrderId { get; set; }
        /// <summary>
        /// 票箱和彩票数据列表
        /// </summary>
        public List<OutTicketLotteryDtosItem> terminalLotteryDtos { get; set; }
        /// <summary>
        /// 附加信息，C，如更新状态不为0则返回更新描述
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 出票状态更新应答
    /// </summary>
    public class OutTicketRsp
    {
        /// <summary>
        /// 出票状态更新应答数据
        /// </summary>
        public ResponseOutTicketData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 终端兑奖
    /// <summary>
    /// 终端兑奖请求数据
    /// </summary>
    public class RequestCashPrizeData
    {
        /// <summary>
        /// 应用名称，cashPrize.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 请求类型
        /// 01 公众号
        /// 02 终端
        /// </summary>
        public string reqType { get; set; }
        /// <summary>
        /// 终端类型，C，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，C
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 彩票序列号
        /// </summary>
        public string lotteryNo { get; set; }
        /// <summary>
        /// 用户ID，微信 openid
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 兑奖类型
        /// 0：扫码
        /// 1：输入
        /// </summary>
        public string cashType { get; set; }
        /// <summary>
        /// 中奖状态
        /// 0 未兑奖
        /// 1 中奖
        /// 2 未中奖
        /// </summary>
        public string prizeStatus { get; set; }
        /// <summary>
        /// 中奖金额,单位分
        /// </summary>
        public string prizeAmt { get; set; }
        /// <summary>
        /// 派奖状态，C
        /// </summary>
        public string awardStatus { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 终端兑奖请求
    /// </summary>
    public class CashPrizeReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 初始化请求数据
        /// </summary>
        public RequestCashPrizeData requestData { get; set; }
    }

    /// <summary>
    /// 终端兑奖应答数据
    /// </summary>
    public class ResponseCashPrizeData
    {
        /// <summary>
        /// 应用名称,cashPrize.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 彩票序列号,R
        /// </summary>
        public string lotteryNo { get; set; }
        /// <summary>
        /// 用户ID，微信 openid
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 兑奖类型
        /// 0：扫码
        /// 1：输入
        /// </summary>
        public string cashType { get; set; }
        /// <summary>
        /// 中奖状态
        /// 0 未兑奖
        /// 1 中奖
        /// 2 未中奖
        /// </summary>
        public string prizeStatus { get; set; }
        /// <summary>
        /// 中奖金额,单位分
        /// </summary>
        public string prizeAmt { get; set; }
        /// <summary>
        /// 派奖状态，C
        /// </summary>
        public string awardStatus { get; set; }
        /// <summary>
        /// 附加信息，C，如更新状态不为0则返回更新描述
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 终端兑奖应答
    /// </summary>
    public class CashPrizeRsp
    {
        /// <summary>
        /// 终端兑奖应答数据
        /// </summary>
        public ResponseCashPrizeData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 派奖
    /// <summary>
    /// 派奖请求数据
    /// </summary>
    public class RequestAwardOrderData
    {
        /// <summary>
        /// 应用名称，awardOrder.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 请求类型
        /// 01 公众号
        /// 02 终端
        /// </summary>
        public string reqType { get; set; }
        /// <summary>
        /// 终端类型，C，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，C
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 用户ID，微信openid
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 彩票序列号，多张以，分隔
        /// </summary>
        public string lotteryNo { get; set; }
        /// <summary>
        /// 支付类型
        /// 01 支付宝
        /// 02 微信
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 派奖请求
    /// </summary>
    public class AwardOrderReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 派奖请求数据
        /// </summary>
        public RequestAwardOrderData requestData { get; set; }
    }

    /// <summary>
    /// 派奖应答数据
    /// </summary>
    public class ResponseAwardOrderData
    {
        /// <summary>
        /// 应用名称,awardOrder.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 用户ID，R,微信openid
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 彩票序列号，R,多张以，分隔
        /// </summary>
        public string lotteryNo { get; set; }
        /// <summary>
        /// 支付类型,R
        /// 01 支付宝
        /// 02 微信
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 派奖金额，单位分
        /// </summary>
        public string awardAmt { get; set; }
        /// <summary>
        /// 派奖二维码，C，请求类型为02终端时返回
        /// </summary>
        public string awardUrl { get; set; }
        /// <summary>
        /// 派奖订单号
        /// </summary>
        public string awardOrderId { get; set; }
        /// <summary>
        /// 附加信息，C
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 派奖应答
    /// </summary>
    public class AwardOrderRsp
    {
        /// <summary>
        /// 派奖应答数据
        /// </summary>
        public ResponseAwardOrderData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 终端状态同步
    /// <summary>
    /// 终端状态同步请求数据
    /// </summary>
    public class RequestTerminalUpdateData
    {
        /// <summary>
        /// 应用名称，terminalUpdate.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 请求类型
        /// 01 公众号
        /// 02 终端
        /// </summary>
        public string reqType { get; set; }
        /// <summary>
        /// 终端类型，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 终端状态
        /// 00 正常
        /// 01 设备故障
        /// 02 票箱无票
        /// 03 票箱故障
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 票箱信息，C
        /// 如终端状态为02，03上送1,2,3,4 用，号分割
        /// </summary>
        public string boxStatus { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 终端状态同步请求
    /// </summary>
    public class TerminalUpdateReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 终端状态同步请求数据
        /// </summary>
        public RequestTerminalUpdateData requestData { get; set; }
    }

    /// <summary>
    /// 终端状态同步应答数据
    /// </summary>
    public class ResponseTerminalUpdateData
    {
        /// <summary>
        /// 应用名称,terminalUpdate.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 附加信息，C，如更新状态不为0则返回更新描述
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 终端状态同步应答
    /// </summary>
    public class TerminalUpdateRsp
    {
        /// <summary>
        /// 终端状态同步应答数据
        /// </summary>
        public ResponseTerminalUpdateData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 广告查询
    /// <summary>
    /// 广告查询请求数据
    /// </summary>
    public class RequestQueryAdsData
    {
        /// <summary>
        /// 应用名称，queryAds.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 终端类型，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string terminalId { get; set; }
    }

    /// <summary>
    /// 广告查询请求
    /// </summary>
    public class QueryAdsReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 广告查询请求数据
        /// </summary>
        public RequestQueryAdsData requestData { get; set; }
    }

    /// <summary>
    /// 广告数据
    /// </summary>
    public class QueryAdsLotteryDtosItem
    {
        /// <summary>
        /// 广告ID
        /// </summary>
        public string adsId { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string adsName { get; set; }
        /// <summary>
        /// 广告类别，1上屏；2中屏；3下屏；4屏保
        /// </summary>
        public string adsKind { get; set; }
        /// <summary>
        /// 广告类型，1图片，2视频
        /// </summary>
        public string adsType { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string filePath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string fileSize { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string beginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 播放时间，单位：秒
        /// </summary>
        public string playTime { get; set; }
        /// <summary>
        /// 播放顺序
        /// </summary>
        public string playSeq { get; set; }
        /// <summary>
        /// 下载模式
        /// 1 远程下载
        /// 2 U盘更新
        /// </summary>
        public string downloadMode { get; set; }
    }

    /// <summary>
    /// 广告查询应答数据
    /// </summary>
    public class ResponseQueryAdsData
    {
        /// <summary>
        /// 应用名称,queryAds.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 广告列表
        /// </summary>
        public List<QueryAdsLotteryDtosItem> adsList { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 广告查询应答
    /// </summary>
    public class QueryAdsRsp
    {
        /// <summary>
        /// 广告查询应答数据
        /// </summary>
        public ResponseQueryAdsData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 彩金下单
    /// <summary>
    /// 彩金下单请求数据
    /// </summary>
    public class RequestContinueOrderData
    {
        /// <summary>
        /// 应用名称，continueOrder.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 终端类型，C，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，C
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string merOrderId { get; set; }
        /// <summary>
        /// 订单时间，YYYYMMDDHHmmss
        /// </summary>
        public string merOrderTime { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public string orderAmt { get; set; }
        /// <summary>
        /// 彩金下单票箱数据列表
        /// </summary>
        public List<ContinueOrderLotteryDtosItem> terminalLotteryDtos { get; set; }
        /// <summary>
        /// 支付类型，如奖金剩余需要派奖，则为派奖方式；如奖金不足则为支付方式；
        ///  01 支付宝
        ///  02 微信
        ///  03 微信公众号支付
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 中奖彩票序列号，多张以,分隔
        /// </summary>
        public string lotteryNo { get; set; }
        /// <summary>
        /// 通知地址，C
        /// </summary>
        public string notifyUrl { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 彩金下单票箱数据
    /// </summary>
    public class ContinueOrderLotteryDtosItem
    {
        /// <summary>
        /// 票箱ID
        /// </summary>
        public string boxId { get; set; }
        /// <summary>
        /// 彩种
        /// </summary>
        public string lotteryId { get; set; }
        /// <summary>
        /// 张数
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string lotteryAmt { get; set; }
    }

    /// <summary>
    /// 彩金下单请求
    /// </summary>
    public class ContinueOrderReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 彩金下单请求数据
        /// </summary>
        public RequestContinueOrderData requestData { get; set; }
    }

    /// <summary>
    /// 彩金下单应答数据
    /// </summary>
    public class ResponseContinueOrderData
    {
        /// <summary>
        /// 应用名称,continueOrder.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 订单编号，R
        /// </summary>
        public string merOrderId { get; set; }
        /// <summary>
        /// 订单时间，R
        /// </summary>
        public string merOrderTime { get; set; }
        /// <summary>
        /// 订单总金额，R
        /// </summary>
        public string orderAmt { get; set; }
        /// <summary>
        /// 彩金下单票箱列表数据，R
        /// </summary>
        public List<ContinueOrderLotteryDtosItem> terminalLotteryDtos { get; set; }
        /// <summary>
        /// 支付类型，R
        /// 如奖金剩余需要派奖，则为派奖方式；如奖金不足则为支付方式；
        /// 01 支付宝
        /// 02 微信
        /// 03 微信公众号支付
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 中奖彩票序列号，R，多张以,分隔
        /// </summary>
        public string lotteryNo { get; set; }
        /// <summary>
        /// 派奖金额，C，单位分
        /// </summary>
        public string awardAmt { get; set; }
        /// <summary>
        /// 派奖订单号,C
        /// </summary>
        public string awardOrderId { get; set; }
        /// <summary>
        /// 支付信息，C，如奖金不足需支付，则返回支付二维码；二维码串 应答码00时返回
        /// </summary>
        public string qrCode { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public string payAmt { get; set; }
        /// <summary>
        /// 派奖信息，C，如奖金剩余需要派奖，返回派奖二维码
        /// </summary>
        public string awardUrl { get; set; }
        /// <summary>
        /// 附加信息，C
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 彩金下单应答
    /// </summary>
    public class ContinueOrderRsp
    {
        /// <summary>
        /// 彩金下单应答数据
        /// </summary>
        public ResponseContinueOrderData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion

    #region 派奖查询
    /// <summary>
    /// 派奖查询请求数据
    /// </summary>
    public class RequestQueryAwardOrderData
    {
        /// <summary>
        /// 应用名称，queryAwardOrder.Req
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间，发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 请求IP,C
        /// </summary>
        public string sendIp { get; set; }
        /// <summary>
        /// 请求坐标，C
        /// </summary>
        public string sendMark { get; set; }
        /// <summary>
        /// 终端类型，0001 振通设备 0002 小设备
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 派奖订单号
        /// </summary>
        public string awardOrderId { get; set; }
        /// <summary>
        /// 自定义保留域，C
        /// </summary>
        public string misc { get; set; }
    }

    /// <summary>
    /// 派奖查询请求
    /// </summary>
    public class QueryAwardOrderReq
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 派奖查询请求数据
        /// </summary>
        public RequestQueryAwardOrderData requestData { get; set; }
    }

    /// <summary>
    /// 派奖查询应答数据
    /// </summary>
    public class ResponseQueryAwardOrderData
    {
        /// <summary>
        /// 应用名称,queryAwardOrder.Rsp
        /// </summary>
        public string application { get; set; }
        /// <summary>
        /// 发送时间,R,发送报文的时间，格式为：YYYYMMDDhhmmss
        /// </summary>
        public string sendTime { get; set; }
        /// <summary>
        /// 接口版本号，R，默认1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 终端编号，R
        /// </summary>
        public string terminalId { get; set; }
        /// <summary>
        /// 派奖订单号，R
        /// </summary>
        public string awardOrderId { get; set; }
        /// <summary>
        /// 派奖总金额，分
        /// </summary>
        public string awardAmt { get; set; }
        /// <summary>
        /// 派奖状态
        /// 0 未处理
        /// 1 成功
        /// 2 失败
        /// </summary>
        public string awardStatus { get; set; }
        /// <summary>
        /// 派奖类型
        /// 01 支付宝
        /// 02 微信
        /// 03 奖金
        /// 04 代付
        /// </summary>
        public string payType { get; set; }
        /// <summary>
        /// 附加信息，C
        /// </summary>
        public string msgExt { get; set; }
        /// <summary>
        /// 自定义保留域，R
        /// </summary>
        public string misc { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }

    /// <summary>
    /// 派奖查询应答
    /// </summary>
    public class QueryAwardOrderRsp
    {
        /// <summary>
        /// 派奖查询应答数据
        /// </summary>
        public ResponseQueryAwardOrderData responseData { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 应答码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 应答码描述
        /// </summary>
        public string respDesc { get; set; }
    }
    #endregion
}
