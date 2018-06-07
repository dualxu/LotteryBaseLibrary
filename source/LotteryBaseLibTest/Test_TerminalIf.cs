using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using LotteryBaseLib.TerminalIf;
using LotteryBaseLib.Public;

namespace LotteryBaseLibTest
{
    class Test_TerminalIf
    {
        public void TerminalIf_Test()
        {
            string key;
            Console.WriteLine("");
            Console.WriteLine("LotteryBaseLib TerminalIf Test");
            TerminalIf.Set_Debug_Flag();
            //
            //string publickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDE2gnaEwJzGUInakWWB98aRJeOoT3mCksX8ON/4NXuw+0BGeNGQgD0RrM2Oy0YFpG3zqMWLpASir3sSQTt8ea9B8kl3WsIe6N/GtUEmAS+kYN24La1qhzPfqUY+y8X1NgoakTficElc7kxT3VwqcH/ebvRApZuiziyOGw8GsxzcwIDAQAB";
            //string privatekey = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAMTaCdoTAnMZQidqRZYH3xpEl46hPeYKSxfw43/g1e7D7QEZ40ZCAPRGszY7LRgWkbfOoxYukBKKvexJBO3x5r0HySXdawh7o38a1QSYBL6Rg3bgtrWqHM9+pRj7LxfU2ChqRN+JwSVzuTFPdXCpwf95u9EClm6LOLI4bDwazHNzAgMBAAECgYEAq/H8WwTxxdHRTBZys+sqQIqbi5ViOPbSwxXB0ih1FbsD4UtYjz0GEllTHtKvv/Ou0svm/nArnlacMLFTYfhDX10tzwA4nMtAewvI/jus+fgSCj8JZjdUI+vTkULU5WFcb0DLAuRyxsFGUG+vKhxUR18zQzofRngxTt5Gy4RFGIECQQD99Gpmn0GkbNzOEWfzkat7JxhnVkri8EtJ5P6fvQlIn3WeTpotMi/+RB45rFj1MNjL1WY27RGGTKto8Dgj9/WzAkEAxm/kZ7ayE+gAWXSa2JsHcAP7nr3oYUg4KgmqxRm3QxCTypHszORp2fu1xtWDJKEXNV9HuW+XYZwaRkadviYrQQJBAO4UNav/oYqEhHyr1MiDyD+sZzR5sbsPi4W7KPqYPhvXYm0HQ4MbieLV+YAYE03KfXSamzjjB4rgVdILYpZV4AECQDSYD3eVqpkwEnejOi9S16POynAGcYLnO0uZCFP5PuNdj25PQu4DVDLcTg+HI50fvSD+QepaM0tBro0VxlVRlIECQHKWNKJuo9OwuFqGuPVxXqGT45oUVKbcyRBHC+0Vy2HRRy7xXZNAbH8o9ELjNwJB/TxBgQAvZt3F6eqVN+z06ko=";
            string publickey = PublicLib.publicky;
            string privatekey = PublicLib.privatekey;
            string data2sign = "";
            string signeddata = "";
            //
            Random rd = new Random();
            int OrderId = rd.Next(1, 100000);
            //
            List<TerminalInitLotteryDtosItem> initDtosItems = new List<TerminalInitLotteryDtosItem>();
            //
            label_menu:
            Console.WriteLine("");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1.签名");
            Console.WriteLine("2.验签");
            Console.WriteLine("3.初始化");
            Console.WriteLine("4.预下单");
            Console.WriteLine("5.交易查询");
            Console.WriteLine("6.出票状态更新");
            Console.WriteLine("7.终端兑奖");
            Console.WriteLine("8.派奖");
            Console.WriteLine("9.终端状态同步");
            Console.WriteLine("a.广告查询");
            Console.WriteLine("0.退出");
            Console.WriteLine("----------------------------------");

            key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    Console.WriteLine("待签名数据:");
                    data2sign = Console.ReadLine();
                    signeddata = SHA1WithRSA.sign(data2sign, privatekey, "UTF-8");
                    Console.WriteLine("签名数据:"+ signeddata);
                    break;
                case "2":
                    Console.WriteLine("待签名数据:");
                    data2sign = Console.ReadLine();
                    Console.WriteLine("签名数据:");
                    signeddata = Console.ReadLine();
                    bool result = SHA1WithRSA.verify(data2sign, signeddata, publickey, "UTF-8");
                    Console.WriteLine("验签结果:"+ result);
                    break;
                case "3":
                    TerminalInitReq tireq = new TerminalInitReq();
                    TerminalInitRsp tirsp = new TerminalInitRsp();
                    RequestInitData ridaa = new RequestInitData();
                    ridaa.application = "terminalInit.Req";
                    ridaa.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    ridaa.terminalCode = "0001";
                    ridaa.terminalId = "10000";
                    ridaa.version = "1.0.0";
                    ridaa.misc = "";
                    ridaa.sendIp = "";
                    ridaa.sendMark = "";                  

                    tireq.requestData = ridaa;
                    Console.WriteLine(JsonTools.ObjectToJson(tireq));

                    tirsp = TerminalIf.TerminalInit(tireq);
                    if (tirsp.responseData.terminalLotteryDtos != null)
                    {
                        if (initDtosItems != null) initDtosItems.Clear();
                        foreach(TerminalInitLotteryDtosItem dtos in tirsp.responseData.terminalLotteryDtos)
                        {
                            initDtosItems.Add(dtos);
                        }
                    }
                    Console.WriteLine(JsonTools.ObjectToJson(tirsp));
                    //                    
                    break;
                case "4":
                    //;
                    PrepOrderRsp porsp = new PrepOrderRsp();
                    PrepOrderReq poreq = new PrepOrderReq();
                    RequestPrepOrderData rpod = new RequestPrepOrderData();
                    rpod.application = "prepOrder.Req";
                    rpod.merOrderId = OrderId.ToString();
                    rpod.misc = "";
                    rpod.notifyUrl = "";                    
                    rpod.payType = "01";
                    rpod.sendIp = "";
                    rpod.sendMark = "";
                    rpod.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rpod.terminalCode = "0001";
                    rpod.terminalId = "10000";                    
                    rpod.version = "1.0.0";
                    List<TerminalPrepOrderLotteryDtosItem> items = new List<TerminalPrepOrderLotteryDtosItem>();
                    TerminalPrepOrderLotteryDtosItem item = new TerminalPrepOrderLotteryDtosItem();
                    int lotteryNum = 2;//彩票张数
                    int lotteryAmt = 0;//彩票金额
                    foreach (TerminalInitLotteryDtosItem dtos in initDtosItems)
                    {
                        item.boxId = dtos.boxId;
                        item.lotteryAmt = dtos.lotteryAmt;
                        item.lotteryId = dtos.lotteryId;
                        item.num = lotteryNum.ToString();
                        //
                        items.Add(item);
                        lotteryAmt += lotteryNum * Convert.ToInt16(dtos.lotteryAmt);
                    }
                    rpod.orderAmt = lotteryAmt.ToString();
                    rpod.terminalLotteryDtos = items;
                    poreq.requestData = rpod;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(poreq));
                    porsp = TerminalIf.PrepOrder(poreq);
                    Console.WriteLine(JsonTools.ObjectToJson(porsp));
                    // 
                    break;
                case "5":
                    QueryOrderReq qoreq = new QueryOrderReq();
                    QueryOrderRsp qorsp = new QueryOrderRsp();
                    RequestQueryOrderData rqod = new RequestQueryOrderData();
                    rqod.application = "queryOrder.Req";
                    rqod.misc = "";
                    rqod.sendIp = "";
                    rqod.sendMark = "";
                    rqod.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rqod.terminalCode = "0001";
                    rqod.terminalId = "10000";
                    rqod.version = "1.0.0";
                    rqod.merOrderId = OrderId.ToString();
                    
                    qoreq.requestData = rqod;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(qoreq));
                    qorsp = TerminalIf.QueryOrder(qoreq);
                    Console.WriteLine(JsonTools.ObjectToJson(qorsp));
                    // 
                    break;
                case "6":
                    OutTicketReq otreq = new OutTicketReq();
                    OutTicketRsp otrsp = new OutTicketRsp();
                    RequestOutTicketData rotd = new RequestOutTicketData();
                    rotd.application = "outTicket.Req";
                    rotd.merOrderId = OrderId.ToString();
                    rotd.misc = "";
                    rotd.sendIp = "";
                    rotd.sendMark = "";
                    rotd.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rotd.terminalCode = "0001";
                    rotd.terminalId = "10000";                    
                    rotd.version = "1.0.0";
                    List<OutTicketLotteryDtosItem> otitems = new List<OutTicketLotteryDtosItem>();
                    OutTicketLotteryDtosItem otitem = new OutTicketLotteryDtosItem();
                    foreach (TerminalInitLotteryDtosItem dtos in initDtosItems)
                    {
                        otitem.boxId = dtos.boxId;
                        otitem.ticketStatus = "1";
                        otitems.Add(otitem);
                    }
                    rotd.terminalLotteryDtos = otitems;
                    otreq.requestData = rotd;                    
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(otreq));
                    otrsp = TerminalIf.OutTicket(otreq);
                    Console.WriteLine(JsonTools.ObjectToJson(otrsp));
                    //
                    break;
                case "7":
                    CashPrizeReq cpreq = new CashPrizeReq();
                    CashPrizeRsp cprsp = new CashPrizeRsp();
                    RequestCashPrizeData rcpd = new RequestCashPrizeData();
                    rcpd.application = "cashPrize.Req";
                    rcpd.awardStatus = "";
                    rcpd.cashType = "0";
                    rcpd.lotteryNo = "3603790001554250559443096279598";
                    rcpd.misc = "";
                    rcpd.prizeAmt = "50000";
                    rcpd.prizeStatus = "1";
                    rcpd.reqType = "02";
                    rcpd.sendIp = "";
                    rcpd.sendMark = "";
                    rcpd.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rcpd.terminalCode = "0001";
                    rcpd.terminalId = "10000";
                    rcpd.userId = "oB4nYjnoHhuWrPVi2pYLuPjnCaU0";
                    rcpd.version = "1.0.0";
                    
                    cpreq.requestData = rcpd;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(cpreq));
                    cprsp = TerminalIf.CashPrize(cpreq);
                    Console.WriteLine(JsonTools.ObjectToJson(cprsp));
                    //                    
                    break;
                case "8":
                    AwardOrderReq aoreq = new AwardOrderReq();
                    AwardOrderRsp aorsp = new AwardOrderRsp();
                    RequestAwardOrderData raod = new RequestAwardOrderData();
                    raod.application = "awardOrder.Req";
                    raod.lotteryNo = "3603790001554250559443096279598";
                    raod.misc = "";
                    raod.reqType = "02";
                    raod.sendIp = "";
                    raod.sendMark = "";
                    raod.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    raod.terminalCode = "0001";
                    raod.terminalId = "10000";
                    raod.userId = "oB4nYjnoHhuWrPVi2pYLuPjnCaU0";
                    raod.version = "1.0.0";
                    
                    aoreq.requestData = raod;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(aoreq));
                    aorsp = TerminalIf.AwardOrder(aoreq);
                    Console.WriteLine(JsonTools.ObjectToJson(aorsp));
                    //
                    break;
                case "9":
                    TerminalUpdateReq tureq = new TerminalUpdateReq();
                    TerminalUpdateRsp tursp = new TerminalUpdateRsp();
                    RequestTerminalUpdateData rtud = new RequestTerminalUpdateData();
                    rtud.application = "terminalUpdate.Req";
                    rtud.boxStatus = "1,2,3,4";
                    rtud.misc = "";
                    rtud.reqType = "02";
                    rtud.sendIp = "";
                    rtud.sendMark = "";
                    rtud.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rtud.status = "00";
                    rtud.terminalCode = "0001";
                    rtud.terminalId = "10000";
                    rtud.version = "1.0.0";
                    
                    tureq.requestData = rtud;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(tureq));
                    tursp = TerminalIf.TerminalUpdate(tureq);
                    Console.WriteLine(JsonTools.ObjectToJson(tursp));
                    //
                    break;
                case "a":
                    QueryAdsReq qareq = new QueryAdsReq();
                    QueryAdsRsp qarsp = new QueryAdsRsp();
                    RequestQueryAdsData rqad = new RequestQueryAdsData();
                    rqad.application = "queryAds.Req";
                    rqad.misc = "";
                    rqad.sendIp = "";
                    rqad.sendMark = "";
                    rqad.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rqad.terminalCode = "0001";
                    rqad.terminalId = "10000";
                    rqad.version = "1.0.0";
                    //
                    qareq.requestData = rqad;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(qareq));
                    qarsp = TerminalIf.QueryAds(qareq);
                    Console.WriteLine(JsonTools.ObjectToJson(qarsp));
                    //
                    break;               
                case "0":
                    goto label_exit;
                default:
                    goto label_menu;
            }
            goto label_menu;
        //
        label_exit:
            return;
        }
    }
}
