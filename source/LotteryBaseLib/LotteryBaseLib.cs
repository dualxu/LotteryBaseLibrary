using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

//参考：https://www.cnblogs.com/czytcn/p/8005788.html
//拷贝.ocx文件到VS安装目录下VC文件夹
//注册Dll
    //regsvr32 TiCaiCut.ocx
    //regsvr32 CashPrize.ocx
//生成axDll
    //D:\Applications\Microsoft Visual Studio 11.0\VC>aximp TiCaiCut.ocx
    //Generated Assembly: D:\Applications\Microsoft Visual Studio 11.0\VC\TICAICUTLib.dll
    //Generated Assembly: D:\Applications\Microsoft Visual Studio 11.0\VC\AxTICAICUTLib.dll

    //D:\Applications\Microsoft Visual Studio 11.0\VC>aximp CashPrize.ocx
    //Generated Assembly: D:\Applications\Microsoft Visual Studio 11.0\VC\CASHPRIZELib.dll
    //Generated Assembly: D:\Applications\Microsoft Visual Studio 11.0\VC\AxCASHPRIZELib.dll
//添加引用axDll
    //using AxCASHPRIZELib;
    //using AxTICAICUTLib;

namespace LotteryBaseLib.Public
{
    /// <summary>
    /// LotteryBaseLib公共类
    /// </summary>
    public class PublicLib
    {
        /// <summary>
        /// NLog日志类
        /// </summary>
        public static Logger logger = LogManager.GetLogger("LotteryBaseLib");

        /// <summary>
        /// 测试用私钥
        /// </summary>
        public static string privatekey = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAMTaCdoTAnMZQidqRZYH3xpEl46hPeYKSxfw43/g1e7D7QEZ40ZCAPRGszY7LRgWkbfOoxYukBKKvexJBO3x5r0HySXdawh7o38a1QSYBL6Rg3bgtrWqHM9+pRj7LxfU2ChqRN+JwSVzuTFPdXCpwf95u9EClm6LOLI4bDwazHNzAgMBAAECgYEAq/H8WwTxxdHRTBZys+sqQIqbi5ViOPbSwxXB0ih1FbsD4UtYjz0GEllTHtKvv/Ou0svm/nArnlacMLFTYfhDX10tzwA4nMtAewvI/jus+fgSCj8JZjdUI+vTkULU5WFcb0DLAuRyxsFGUG+vKhxUR18zQzofRngxTt5Gy4RFGIECQQD99Gpmn0GkbNzOEWfzkat7JxhnVkri8EtJ5P6fvQlIn3WeTpotMi/+RB45rFj1MNjL1WY27RGGTKto8Dgj9/WzAkEAxm/kZ7ayE+gAWXSa2JsHcAP7nr3oYUg4KgmqxRm3QxCTypHszORp2fu1xtWDJKEXNV9HuW+XYZwaRkadviYrQQJBAO4UNav/oYqEhHyr1MiDyD+sZzR5sbsPi4W7KPqYPhvXYm0HQ4MbieLV+YAYE03KfXSamzjjB4rgVdILYpZV4AECQDSYD3eVqpkwEnejOi9S16POynAGcYLnO0uZCFP5PuNdj25PQu4DVDLcTg+HI50fvSD+QepaM0tBro0VxlVRlIECQHKWNKJuo9OwuFqGuPVxXqGT45oUVKbcyRBHC+0Vy2HRRy7xXZNAbH8o9ELjNwJB/TxBgQAvZt3F6eqVN+z06ko=";
        
        /// <summary>
        /// 测试用公钥
        /// </summary>
        public static string publicky = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDE2gnaEwJzGUInakWWB98aRJeOoT3mCksX8ON/4NXuw+0BGeNGQgD0RrM2Oy0YFpG3zqMWLpASir3sSQTt8ea9B8kl3WsIe6N/GtUEmAS+kYN24La1qhzPfqUY+y8X1NgoakTficElc7kxT3VwqcH/ebvRApZuiziyOGw8GsxzcwIDAQAB";
    }
}
