using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using LotteryBaseLib.Scanner;

namespace LotteryBaseLibTest
{
    public class Test_Scanner
    {
        public void Scanner_Test()
        {
            string key;
            Console.WriteLine("");
            Console.WriteLine("LotteryBaseLib Scanner Test");
            Scanner cp = new Scanner();
        //
        label_menu:
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("1.打开扫描设备");
            Console.WriteLine("2.获取版本号");
            Console.WriteLine("3.启动扫描");
            Console.WriteLine("4.获取扫描数据(条形码)");
            Console.WriteLine("5.停止扫描");
            Console.WriteLine("6.关闭设备");
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
                    int RetOpenDevice = cp.OpenDevice(iCom);
                    Console.WriteLine("OpenDevice:" + RetOpenDevice.ToString());
                    break;
                case "2":
                    string strVersion = "";
                    int RetGetVersion = cp.GetVersion(out strVersion);
                    if (RetGetVersion != 0)
                    {
                        Console.WriteLine("GetVersion:" + RetGetVersion + ",ErrMsg:" + strVersion);
                    }
                    else
                    {
                        Console.WriteLine("GetVersion:" + strVersion);
                    }
                    break;
                case "3":
                    int RetStartup = cp.Startup();
                    Console.WriteLine("Startup:" + RetStartup.ToString());
                    break;
                case "4":
                    string barcode = "";
                    int RetGetData = cp.GetData(out barcode);
                    Console.WriteLine("GetData:" + RetGetData.ToString());
                    if (RetGetData == 0)
                    {
                        Console.WriteLine("GetData,BarCode:" + barcode);
                    }
                    else
                    {
                        Console.WriteLine("GetData,ErrMsg:" + barcode);
                    }
                    break;
                case "5":
                    int RetStop = cp.Stop();
                    Console.WriteLine("Stop:" + RetStop.ToString());
                    break;
                case "6":
                    int RetCloseDevice = cp.CloseDevice();
                    Console.WriteLine("CloseDevice:" + RetCloseDevice.ToString());
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
