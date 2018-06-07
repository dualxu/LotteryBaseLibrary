using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using LotteryBaseLib.TiCaiCut;

namespace LotteryBaseLibTest
{
    class Test_TiCaiCut
    {
        public void TiCaiCut_Test()
        {
            string key;
            Console.WriteLine("");
            Console.WriteLine("LotteryBaseLib TiCaiCut Test");
            TiCaiCut tcc = new TiCaiCut();
            //
            label_menu:
            Console.WriteLine("");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1.打开设备");
            Console.WriteLine("2.设置长度");
            Console.WriteLine("3.查询状态");
            Console.WriteLine("4.切纸");
            Console.WriteLine("5.关闭设备");
            Console.WriteLine("0.退出");
            Console.WriteLine("----------------------------------");

            key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    Console.WriteLine("串口号:");
                    string StrCom = Console.ReadLine();
                    Console.WriteLine("波特率(9600):");
                    string StrBaud = Console.ReadLine();
                    short iBaud = 9600;
                    Int16.TryParse(StrBaud,out iBaud);
                    short RetOpen = tcc.Open(StrCom, iBaud);
                    Console.WriteLine("打开设备："+ RetOpen.ToString());
                    break;
                case "2":
                    Console.WriteLine("长度(5元:101.6,10元:152,20元:202,30元:254):");
                    string StrLength = Console.ReadLine();
                    short iLength = 102;
                    Int16.TryParse(StrLength, out iLength);
                    short RetSetLength = tcc.SetLength(iLength);
                    Console.WriteLine("设置长度：" + RetSetLength.ToString());
                    break;
                case "3":
                    short RetState = tcc.GetState();
                    Console.WriteLine("查询状态：" + RetState.ToString());
                    break;
                case "4":
                    short RetCutPaper = tcc.CutPaper();
                    Console.WriteLine("切纸：" + RetCutPaper.ToString());
                    break;
                case "5":
                    tcc.Close();
                    Console.WriteLine("关闭设备");
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
