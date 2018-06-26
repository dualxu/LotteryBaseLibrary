using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using Size = System.Drawing.Size;
using QRCoder;
using LotteryBaseLib.Public;

namespace LotteryBaseLib.TerminalIf
{
    /// <summary>
    /// 二维码生成类
    /// </summary>
    public class qrCode
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="strToEncode">待编码数据字符串</param>
        /// <param name="ecclevel">ECC校验等级,L/M/Q/H,默认L</param>
        /// <param name="pixelsPerModule">每模块像素值,默认20</param>
        /// <returns>返回Bitmap或异常时为null</returns>
        public static Bitmap GenerateQrCode(string strToEncode, string eccLevel, int pixelsPerModule)
        {
            try
            {
                PublicLib.logger.Info("GenerateQrCode:strToEncode=" + strToEncode + ",eccLevel=" + eccLevel + ",pixelsPerModule=" + pixelsPerModule.ToString());

                if (pixelsPerModule <= 0) pixelsPerModule = 20;

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.ECCLevel lvl = QRCodeGenerator.ECCLevel.L;
                if (eccLevel == "H") lvl = QRCodeGenerator.ECCLevel.H;
                else if (eccLevel == "M") lvl = QRCodeGenerator.ECCLevel.M;
                else if (eccLevel == "Q") lvl = QRCodeGenerator.ECCLevel.Q;
                else lvl = QRCodeGenerator.ECCLevel.L;

                QRCodeData qrCodeData = qrGenerator.CreateQrCode(strToEncode, lvl);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(pixelsPerModule);

                //Set color by using Color-class types
                //Bitmap qrCodeImage1 = qrCode.GetGraphic(pixelsPerModule, Color.DarkRed, Color.PaleGreen, true);

                //Set color by using HTML hex color notation
                //Bitmap qrCodeImage2 = qrCode.GetGraphic(pixelsPerModule, "#000ff0", "#0ff000");
            
                //The another overload enables you to render a logo/image in the center of the QR code.
                //Bitmap qrCodeImage3 = qrCode.GetGraphic(pixelsPerModule, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));

                return qrCodeImage;
            }
            catch (Exception ex)
            {
                PublicLib.logger.Error("qrCode->GenerateQrCode:" + ex.Message);
                return null;
            }
        }
    }
}
