using System;

namespace RSA
{
    public class Program
    {
        static void Main(string[] arg)
        {

            /**RSA加密测试,RSA中的密钥对通过SSL工具生成，生成命令如下：
             * 1 生成RSA私钥：
             * openssl genrsa -out rsa_private_key.pem 1024
             *2 生成RSA公钥
             * openssl rsa -in rsa_private_key.pem -pubout -out rsa_public_key.pem
             *
             * 3 将RSA私钥转换成PKCS8格式
             * openssl pkcs8 -topk8 -inform PEM -in rsa_private_key.pem -outform PEM -nocrypt -out rsa_pub_pk8.pem
             *
             * 直接打开rsa_private_key.pem和rsa_pub_pk8.pem文件就可以获取密钥对内容，获取密钥对内容组成字符串时，注意将换行符删除
             * */

            string publickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDzOqfNunFxFtCZPlq7fO/jWwjqmTvAooVBB4y87BizSZ9dl/F7FpAxYc6MmX2TqivCvvORXgdlYdFWAhzXOnIUv9OGG///WPLe9TMs9kIwAZ/APUXauvC01oFLnYkzwPlAh0tQ1Au9arTE/OG1V1dKgf8BXHLPhKL4BmGBEUZBtQIDAQAB";
            string privatekey = "MIICeQIBADANBgkqhkiG9w0BAQEFAASCAmMwggJfAgEAAoGBAPM6p826cXEW0Jk+Wrt87+NbCOqZO8CihUEHjLzsGLNJn12X8XsWkDFhzoyZfZOqK8K+85FeB2Vh0VYCHNc6chS/04Yb//9Y8t71Myz2QjABn8A9Rdq68LTWgUudiTPA+UCHS1DUC71qtMT84bVXV0qB/wFccs+EovgGYYERRkG1AgMBAAECgYEA2PmnPdgnYKnolfvQ9tXiLaBFGPpvGk4grz0r6FB5TF7N4rErwxECunq0xioaowK4HPc40qHd2SvkkWQ7FCjYIDsnMk1oOhxNKn0J3FG0n5Cg1/dFai4eoXHs/nKn3SVZ8YZC1T2cMtN2srectLqNqhB8aQEe8xmykyUlUpg/qmECQQD9vkwjUotG5oUUrOj6etcB4WcdyyH0FtThKgyoJUDwgBv6lGGzWyFJEREvp47IgV+FgC7zeP2mL4MhgnD3tNCZAkEA9WRrjOLBNc379XZpoDsH7rZjobVvhnTrEuRDx/whqZ+vk64EPrEW81XYh647bAbJlFn2jPhY+IUHkrxFEFT/fQJBAMoLNOULXQtfkqgb5odMONeue0Ul8itB4tBHgzyALW1TFPQ6InGGJsLfbCfd67uMCFts7fXAaXhibK/KBdm3iEECQQChwVAjzlUN4nnzk9qMhFz2PcPvFGovd2J9UXpcmRaXeWuDLXIe4Rz/ydaxmWgSDWdTIvoicpIzP31+fBwKZ/0BAkEAy0bh4weKmYF29//rK0sxmY8RtqkQeFrwWbqx1daa1w0DfWlNSvy47zyW1G5/AdZU6JSpXxlxdlM/HSDw+v7kcA==";

            //加密字符串
            string data = "yibao";

            Console.WriteLine("加密前字符串内容：" + data);
            //加密
            string encrypteddata = SHA1WithRSA.encryptData(data, publickey, "UTF-8");
            Console.WriteLine("加密后的字符串为：" + encrypteddata);
            Console.WriteLine("解密后的字符串内容：" + SHA1WithRSA.decryptData(encrypteddata, privatekey, "UTF-8"));

            Console.WriteLine("***********");

            //解密
            string endata = "LpnnvnfA72VnyjboX/OsCPO6FOFXeEnnsKkI7aAEQyVAPfCTfQ43ZYVZVqnADDPMW7VhBXJWyQMAGw2Fh9sS/XLHmO5XW94Yehci6JrJMynePgtIiDysjNA+UlgSTC/MlResNrBm/4MMSPvq0qLwScgpZDynhLsVZk+EQ6G8wgA=";
            string datamw = SHA1WithRSA.decryptData(endata, privatekey, "UTF-8");
            Console.WriteLine("静态加密后的字符串为：" + endata);
            Console.WriteLine("解密后的字符串内容：" + datamw);

            //签名
            string signdata = "YB010000001441234567286038508081299";
            Console.WriteLine("\n签名前的字符串内容：" + signdata);
            string sign = SHA1WithRSA.sign(signdata, privatekey, "UTF-8");
            Console.WriteLine("签名后的字符串：" + sign);

            //验签
            string ysigndata = "YB010000001441234567286038508081299";
            string qm = sign;//签名
            Console.WriteLine("验签前的字符串内容：" + ysigndata);
            bool ysign = SHA1WithRSA.verify(ysigndata, qm, publickey, "UTF-8");
            Console.WriteLine("验签后的字符串：" + ysign);

            //测试签名（公钥）和验签（私钥）
            Console.WriteLine("\n测试签名（公钥）和验签（私钥）:");
            privatekey = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAMTaCdoTAnMZQidqRZYH3xpEl46hPeYKSxfw43/g1e7D7QEZ40ZCAPRGszY7LRgWkbfOoxYukBKKvexJBO3x5r0HySXdawh7o38a1QSYBL6Rg3bgtrWqHM9+pRj7LxfU2ChqRN+JwSVzuTFPdXCpwf95u9EClm6LOLI4bDwazHNzAgMBAAECgYEAq/H8WwTxxdHRTBZys+sqQIqbi5ViOPbSwxXB0ih1FbsD4UtYjz0GEllTHtKvv/Ou0svm/nArnlacMLFTYfhDX10tzwA4nMtAewvI/jus+fgSCj8JZjdUI+vTkULU5WFcb0DLAuRyxsFGUG+vKhxUR18zQzofRngxTt5Gy4RFGIECQQD99Gpmn0GkbNzOEWfzkat7JxhnVkri8EtJ5P6fvQlIn3WeTpotMi/+RB45rFj1MNjL1WY27RGGTKto8Dgj9/WzAkEAxm/kZ7ayE+gAWXSa2JsHcAP7nr3oYUg4KgmqxRm3QxCTypHszORp2fu1xtWDJKEXNV9HuW+XYZwaRkadviYrQQJBAO4UNav/oYqEhHyr1MiDyD+sZzR5sbsPi4W7KPqYPhvXYm0HQ4MbieLV+YAYE03KfXSamzjjB4rgVdILYpZV4AECQDSYD3eVqpkwEnejOi9S16POynAGcYLnO0uZCFP5PuNdj25PQu4DVDLcTg+HI50fvSD+QepaM0tBro0VxlVRlIECQHKWNKJuo9OwuFqGuPVxXqGT45oUVKbcyRBHC+0Vy2HRRy7xXZNAbH8o9ELjNwJB/TxBgQAvZt3F6eqVN+z06ko=";
            publickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDE2gnaEwJzGUInakWWB98aRJeOoT3mCksX8ON/4NXuw+0BGeNGQgD0RrM2Oy0YFpG3zqMWLpASir3sSQTt8ea9B8kl3WsIe6N/GtUEmAS+kYN24La1qhzPfqUY+y8X1NgoakTficElc7kxT3VwqcH/ebvRApZuiziyOGw8GsxzcwIDAQAB";
            //私钥签名：YsZrRTh/XWCs8NjIzgP6C9zs0T8hwRWXsSsxx3+cMf8w6WOMtfher27sV68nKQlNj3hzkmXIgo9tN/Dl3j7fcJCmZ8JR4f5Dp3V/FnZF5CAOLdVPYMgNlmhH2povVfN+mCmzxDYKWVgiBHBe7HsHYECrNjO81PuU9Cu9UzOH17s=
            //待签名数据：a

            //签名
            signdata = "a";
            Console.WriteLine("签名前的字符串内容：" + signdata);
            sign = SHA1WithRSA.sign(signdata, privatekey, "UTF-8");
            Console.WriteLine("签名后的字符串：" + sign);

            //验签
            ysigndata = "a";
            qm = sign;//签名
            Console.WriteLine("验签前的字符串内容：" + ysigndata);
            ysign = SHA1WithRSA.verify(ysigndata, qm, publickey, "UTF-8");
            Console.WriteLine("验签后的字符串：" + ysign);

            //再验签
            ysigndata = "a";
            qm = "YsZrRTh/XWCs8NjIzgP6C9zs0T8hwRWXsSsxx3+cMf8w6WOMtfher27sV68nKQlNj3hzkmXIgo9tN/Dl3j7fcJCmZ8JR4f5Dp3V/FnZF5CAOLdVPYMgNlmhH2povVfN+mCmzxDYKWVgiBHBe7HsHYECrNjO81PuU9Cu9UzOH17s=";//签名
            Console.WriteLine("\n验签前的字符串内容：" + ysigndata);
            ysign = SHA1WithRSA.verify(ysigndata, qm, publickey, "UTF-8");
            Console.WriteLine("验签后的字符串：" + ysign);

            Console.ReadLine();
        }
    }
}
