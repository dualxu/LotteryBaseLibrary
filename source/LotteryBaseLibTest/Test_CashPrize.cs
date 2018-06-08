using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using LotteryBaseLib.CashPrize;

namespace LotteryBaseLibTest
{
    public class Test_CashPrize
    {
        public void CashPrize_Test()
        {
            string key;
            Console.WriteLine("");
            Console.WriteLine("LotteryBaseLib CashPrize Test");
            CashPrize cp = new CashPrize();
            //
            label_menu:
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("*****已弃用接口*****");
            Console.WriteLine("1.打开打孔设备");
            Console.WriteLine("2.打开扫描设备");
            Console.WriteLine("3.复位打孔设备");
            Console.WriteLine("4.查询打孔设备状态");
            Console.WriteLine("5.是否有彩票");
            Console.WriteLine("6.启动扫描");
            Console.WriteLine("7.获取扫描数据(条形码)");
            Console.WriteLine("8.停止扫描");
            Console.WriteLine("9.打孔");
            Console.WriteLine("a.关闭设备");
            Console.WriteLine("0.退出");
            Console.WriteLine("----------------------------------------------------------------------------");

            key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    Console.WriteLine("串口号:");
                    string StrCom = Console.ReadLine();
                    short iCom = 1;
                    Int16.TryParse(StrCom, out iCom);
                    Console.WriteLine("波特率(9600):");
                    string StrBaud = Console.ReadLine();
                    short iBaud = 9600;
                    Int16.TryParse(StrBaud,out iBaud);
                    int RetOpenPunchDev = cp.OpenPunchDev(iCom,iBaud);
                    Console.WriteLine("OpenPunchDev:" + RetOpenPunchDev.ToString());
                    break;
                case "2":
                    Console.WriteLine("串口号:");
                    StrCom = Console.ReadLine();
                    iCom = 1;
                    Int16.TryParse(StrCom, out iCom);
                    Console.WriteLine("波特率(115200):");
                    StrBaud = Console.ReadLine();
                    iBaud = 9600;
                    Int16.TryParse(StrBaud, out iBaud);
                    int RetOpenScanDev = cp.OpenScanDev(iCom, iBaud);
                    Console.WriteLine("OpenScanDev:" + RetOpenScanDev.ToString());
                    break;
                case "3":
                    int RetPunchReset = cp.PunchReset();
                    Console.WriteLine("PunchReset:" + RetPunchReset.ToString());
                    break;
                case "4":
                    short state1, state2, state3;
                    int RetGetPunchState = cp.GetPunchState(out state1, out state2, out state3);
                    Console.WriteLine("GetPunchState:" + RetGetPunchState.ToString());
                    if (RetGetPunchState == 0)
                    {
                        Console.WriteLine("state1:" + state1.ToString() + ",state2:" + state2.ToString() + ",state3:" + state3.ToString());
                    }
                    break;
                case "5":
                    short ticketstate;
                    int RetIsThereAticket = cp.IsThereAticket(out ticketstate);
                    Console.WriteLine("IsThereAticket:" + RetIsThereAticket.ToString());
                    if (RetIsThereAticket == 0)
                    {
                        Console.WriteLine("ticketstate:" + ticketstate.ToString());
                    }
                    break;
                case "6":
                    int RetStartScan = cp.StartScan();
                    Console.WriteLine("StartScan:" + RetStartScan.ToString());
                    break;
                case "7":
                    string barcode = "";
                    int RetGetScanData = cp.GetScanData(out barcode);
                    Console.WriteLine("GetScanData:" + RetGetScanData.ToString());
                    if (RetGetScanData == 0)
                    {
                        Console.WriteLine("BarCode:" + barcode);
                    }
                    break;
                case "8":
                    int RetStopScan = cp.StopScan();
                    Console.WriteLine("StopScan:" + RetStopScan.ToString());
                    break;
                case "9":
                    int RetPunch = cp.Punch();
                    Console.WriteLine("Punch:" + RetPunch.ToString());
                    break;
                case "a":
                    int RetCloseDev = cp.CloseDev();
                    Console.WriteLine("CloseDev:" + RetCloseDev.ToString());
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
