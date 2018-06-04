using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MyExam.Common
{
    public static class CommonHelper
    {
        public static string CalcMD5(this string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }
        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" +
                    computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }
        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" +
                    computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }

        public static string GenerateCaptchaCode(int len)
        {
            char[] data = { 'a', 'c', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'p', 'r', 's', 'w', 'x', 'y', 'z', '2', '3', '4', '5', '7', '8' };
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < len; i++)
            {
                sb.Append(data[rd.Next(data.Length)]);
            }
            return sb.ToString();
        }
        public static string CreateVerifyCode(int len)
        {
            char[] data = { 'a', 'c', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 's', 't', 'w', 'x', 'y' };
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = rand.Next(data.Length);//[0,data.length)
                char ch = data[index];
                sb.Append(ch);
            }
            //勤测！
            return sb.ToString();
        }
        public static string CreateVerifyPassWord(int len)
        {
            string chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string chars2 = "0123456789";
            string chars3 = "abcdefghijklmnopqrstuvwxyz";
            string chars4 = "!@#$%^&*()";
            string chars5 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^&*()";
            if (len < 5)
            {
                throw new ArgumentException("参数异常，必须大于3");
            }
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            sb.Append(chars1[rand.Next(chars1.Length)]);
            sb.Append(chars2[rand.Next(chars2.Length)]);
            sb.Append(chars3[rand.Next(chars3.Length)]);
            sb.Append(chars4[rand.Next(chars4.Length)]);
            for (int i = 0; i < len - 4; i++)
            {
                sb.Append(chars5[rand.Next(chars5.Length)]);
            }
            return sb.ToString();
        }
    }
}
