using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Size = System.Drawing.Size;

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
            Console.WriteLine("11.广告查询");
            Console.WriteLine("12.彩金下单");
            Console.WriteLine("13.派奖查询");
            Console.WriteLine("14.二维码生成");
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
                    if((tirsp != null)&&(tirsp.responseData != null))
                    {
                        if (tirsp.responseData.terminalLotteryDtos != null)
                        {
                            if (initDtosItems != null) initDtosItems.Clear();                           
                            foreach (TerminalInitLotteryDtosItem dtos in tirsp.responseData.terminalLotteryDtos)
                            {
                                initDtosItems.Add(dtos);
                            }
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
                    rpod.merOrderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rpod.misc = "";
                    rpod.notifyUrl = "";
                    Console.WriteLine("请输入支付类型(1-支付宝(default),2-微信,3-微信公众号):");
                    string strPayType = Console.ReadLine();
                    if (strPayType == "2") rpod.payType = "02";
                    else if (strPayType == "3") rpod.payType = "03";
                    else rpod.payType = "01";
                    rpod.sendIp = "";
                    rpod.sendMark = "";
                    rpod.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rpod.terminalCode = "0001";
                    rpod.terminalId = "10000";                    
                    rpod.version = "1.0.0";
                    rpod.merOrderId = rpod.terminalId + DateTime.Now.ToString("yyyyMMddHHmmss");

                    List<TerminalPrepOrderLotteryDtosItem> items = new List<TerminalPrepOrderLotteryDtosItem>();
                    TerminalPrepOrderLotteryDtosItem item = new TerminalPrepOrderLotteryDtosItem();
                    int lotteryAmt = 0;//彩票金额
                    foreach (TerminalInitLotteryDtosItem dtos in initDtosItems)
                    {
                        item.boxId = dtos.boxId;
                        item.lotteryAmt = dtos.lotteryAmt;
                        item.lotteryId = dtos.lotteryId;
                        Console.WriteLine("当前票箱ID:" + dtos.boxId + ",票种单价:" + dtos.lotteryAmt);
                        Console.WriteLine("请输入购买张数(不需要购买直接回车):");
                        string lotteryNumStr = Console.ReadLine();
                        if (lotteryNumStr == "" || lotteryNumStr == "0")
                        {
                            item = new TerminalPrepOrderLotteryDtosItem();
                            continue;
                        }
                        short Num = 0;
                        Int16.TryParse(lotteryNumStr, out Num);
                        item.num = Num.ToString();
                        //
                        items.Add(item);
                        lotteryAmt += Num * Convert.ToInt16(dtos.lotteryAmt);
                        item = new TerminalPrepOrderLotteryDtosItem();
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
                    Console.WriteLine("请输入交易订单号:");
                    string merOrderId = Console.ReadLine();
                    rqod.merOrderId = merOrderId;
                    
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
                    rotd.misc = "";
                    rotd.sendIp = "";
                    rotd.sendMark = "";
                    rotd.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rotd.terminalCode = "0001";
                    rotd.terminalId = "10000";                    
                    rotd.version = "1.0.0";
                    Console.WriteLine("请输入原交易订单号:");
                    merOrderId = Console.ReadLine();
                    rotd.merOrderId = merOrderId;

                    List<OutTicketLotteryDtosItem> otitems = new List<OutTicketLotteryDtosItem>();
                    OutTicketLotteryDtosItem otitem = new OutTicketLotteryDtosItem();
                    foreach (TerminalInitLotteryDtosItem dtos in initDtosItems)
                    {
                        otitem.boxId = dtos.boxId;
                        Console.WriteLine("当前票箱【{0}】状态(1-出票成功[default],2 出票异常)",dtos.boxId);
                        string ticketStatus = Console.ReadLine();
                        if (ticketStatus == "2") otitem.ticketStatus = "2";
                        else otitem.ticketStatus = "1";
                        otitem.surplus = "";
                        otitems.Add(otitem);
                        //
                        otitem = new OutTicketLotteryDtosItem();
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
                    Console.WriteLine("请输入彩票序列号(多张以,分隔):");
                    string lotteryno = Console.ReadLine();
                    if (lotteryno == "") lotteryno = "3603790001554250012285104303358";
                    rcpd.lotteryNo = lotteryno;
                    rcpd.misc = "";
                    rcpd.prizeAmt = "";
                    rcpd.prizeStatus = "";
                    rcpd.reqType = "";
                    rcpd.sendIp = "";
                    rcpd.sendMark = "";
                    rcpd.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rcpd.terminalCode = "0001";
                    rcpd.terminalId = "10000";
                    rcpd.userId = "";
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
                    Console.WriteLine("请输入彩票序列号(多张以,分隔):");
                    lotteryno = Console.ReadLine();
                    if (lotteryno == "") lotteryno = "3603790001554250012285104303358";
                    raod.lotteryNo = lotteryno;
                    raod.misc = "";
                    raod.reqType = "02";
                    raod.sendIp = "";
                    raod.sendMark = "";
                    raod.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    raod.terminalCode = "0001";
                    raod.terminalId = "10000";
                    raod.userId = "oB4nYjnoHhuWrPVi2pYLuPjnCaU0";
                    Console.WriteLine("请输入支付方式(1-支付宝,2-微信):");
                    string paytype = Console.ReadLine();
                    if (paytype == "1") paytype = "01";
                    else paytype = "02";
                    raod.payType = paytype;
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
                    rtud.boxStatus = "";
                    foreach (TerminalInitLotteryDtosItem dtos in initDtosItems)
                    {
                        if (rtud.boxStatus == "" || rtud.boxStatus == null)
                        {
                            rtud.boxStatus = dtos.boxId;
                        }
                        else
                        {
                            rtud.boxStatus = "," + dtos.boxId;
                        }
                    }                    
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
                case "11":
                    QueryAdsReq qareq = new QueryAdsReq();
                    QueryAdsRsp qarsp = new QueryAdsRsp();
                    RequestQueryAdsData rqad = new RequestQueryAdsData();
                    rqad.application = "queryAds.Req";
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
                    //广告图片下载/更新配置文件
                    Console.WriteLine("广告图片下载/更新配置文件:");
                    if (qarsp.responseData != null)
                    {
                        if (qarsp.responseData.adsList != null)
                        {
                            if (qarsp.responseData.adsList.Count > 0)
                            {
                                AdsDownloader.AdsDownload(qarsp.responseData.adsList, ".\\adsDownloadDir");
                            }
                        }
                    }
                    //
                    break;
                case "12":
                    ContinueOrderReq coreq = new ContinueOrderReq();
                    ContinueOrderRsp corsp = new ContinueOrderRsp();
                    RequestContinueOrderData rcod = new RequestContinueOrderData();
                    rcod.application = "continueOrder.Req";                    
                    rcod.misc = "";
                    rcod.sendIp = "";
                    rcod.sendMark = "";
                    rcod.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rcod.terminalCode = "0001";
                    rcod.terminalId = "10000";
                    rcod.version = "1.0.0";
                    rcod.merOrderId = rcod.terminalId + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //
                    Console.WriteLine("请输入彩票序列号(多张以,分隔):");
                    lotteryno = Console.ReadLine();
                    if (lotteryno == "") lotteryno = "3603790001554250012285104303358";
                    rcod.lotteryNo = lotteryno;
                    rcod.merOrderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rcod.notifyUrl = "";
                    Console.WriteLine("请输入支付方式(1-支付宝,2-微信):");
                    paytype = Console.ReadLine();
                    if (paytype == "1") paytype = "01";
                    else paytype = "02";
                    rcod.payType = paytype;
                    //
                    List<ContinueOrderLotteryDtosItem> coitems = new List<ContinueOrderLotteryDtosItem>();
                    ContinueOrderLotteryDtosItem coitem = new ContinueOrderLotteryDtosItem();                    
                    lotteryAmt = 0;//彩票金额                    
                    foreach (TerminalInitLotteryDtosItem dtos in initDtosItems)
                    {
                        coitem.boxId = dtos.boxId;
                        coitem.lotteryAmt = dtos.lotteryAmt;
                        coitem.lotteryId = dtos.lotteryId;
                        Console.WriteLine("当前票箱ID:"+dtos.boxId+",票种单价:"+dtos.lotteryAmt);
                        Console.WriteLine("请输入购买张数(不需要购买直接回车):");
                        string lotteryNumStr = Console.ReadLine();
                        if (lotteryNumStr == "")
                        {
                            coitem = new ContinueOrderLotteryDtosItem();
                            continue;
                        }
                        short Num;
                        Int16.TryParse(lotteryNumStr, out Num);
                        coitem.num = Num.ToString();
                        //
                        coitems.Add(coitem);
                        lotteryAmt += Num * Convert.ToInt16(dtos.lotteryAmt);
                        coitem = new ContinueOrderLotteryDtosItem();
                    }
                    rcod.orderAmt = lotteryAmt.ToString();
                    rcod.terminalLotteryDtos = coitems;
                    coreq.requestData = rcod;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(coreq));
                    corsp = TerminalIf.ContinueOrder(coreq);
                    Console.WriteLine(JsonTools.ObjectToJson(corsp));
                    //
                    break;
                case "13":
                    QueryAwardOrderReq qaoreq = new QueryAwardOrderReq();
                    QueryAwardOrderRsp qaorsp = new QueryAwardOrderRsp();
                    RequestQueryAwardOrderData rqaod = new RequestQueryAwardOrderData();
                    rqaod.application = "queryAwardOrder.Req";
                    rqaod.misc = "";
                    rqaod.sendIp = "";
                    rqaod.sendMark = "";
                    rqaod.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rqaod.terminalCode = "0001";
                    rqaod.terminalId = "10000";
                    rqaod.version = "1.0.0";
                    Console.WriteLine("请输入派奖订单号:");
                    string awardOrderId = Console.ReadLine();
                    rqaod.awardOrderId = awardOrderId;

                    qaoreq.requestData = rqaod;
                    //
                    Console.WriteLine(JsonTools.ObjectToJson(qaoreq));
                    qaorsp = TerminalIf.QueryAwardOrder(qaoreq);
                    Console.WriteLine(JsonTools.ObjectToJson(qaorsp));
                    // 
                    break;
                case "14":
                    Console.WriteLine("请输入生成二维码待编码字符串:");
                    string strToEncode = Console.ReadLine();
                    Console.WriteLine("请输入生成二维码每模块像素值(默认20):");
                    string strPixels = Console.ReadLine();
                    short intPixels = 20;
                    Int16.TryParse(strPixels, out intPixels);
                    //
                    string ecclevel = "L"; //L,M,Q,H
                    Bitmap bitmap1 = qrCode.GenerateQrCode(strToEncode, ecclevel, intPixels);
                    string imgFileName1 = ".\\qrCodeImage\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + intPixels.ToString() + ".jpg";
                    bitmap1.Save(imgFileName1, ImageFormat.Jpeg);
                    Console.WriteLine("图片保存为：" + imgFileName1);
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
