using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LotteryBaseLibTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string key;
            var version = Assembly.GetEntryAssembly().GetName().Version;
            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
                                TimeSpan.TicksPerSecond * 2 * version.Revision)); /* seconds since midnight,(multiply by 2 to get original) */

            Console.WriteLine("LotteryBaseLib Test Utility");
            Console.WriteLine("xux@2018");
            Console.WriteLine("最后更新: " + buildDateTime);
            //
            label_menu:
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("1.CashPrize       (已废弃)扫描仪、打孔设备");
            Console.WriteLine("2.TiCaiCut                彩票切纸器");
            Console.WriteLine("3.Scanner                 扫描设备");
            Console.WriteLine("4.TerminalIf              终端接口");
            Console.WriteLine("0.退出");
            Console.WriteLine("--------------------------------------------------------------------");

            key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    Test_CashPrize t1 = new Test_CashPrize();
                    t1.CashPrize_Test();
                    break;
                case "2":
                    Test_TiCaiCut t2 = new Test_TiCaiCut();
                    t2.TiCaiCut_Test();
                    break;
                case "3":
                    Test_Scanner t3 = new Test_Scanner();
                    t3.Scanner_Test();
                    break;
                case "4":
                    Test_TerminalIf t4 = new Test_TerminalIf();
                    t4.TerminalIf_Test();
                    break;                   
                case "0":
                    goto label_exit;
                default:
                    goto label_menu;
            }
            goto label_menu;
        //
        label_exit:
            Console.WriteLine("Press any key to exit(except POWER)......");
            Console.ReadLine();
            return;
        }
    }

}
