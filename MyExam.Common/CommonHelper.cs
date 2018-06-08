using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        /// <summary>
        ///  JWT加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JWTEnCode(string str)
        {

            var payload = DeserializeStringToDictionary<string,object>(str);
            

            var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";//不要泄露
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
            
        }
        /// <summary>
        /// JWT解密
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        public static string JWTDeCode(string token)
        {
            var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";//不要泄露
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                var json = decoder.Decode(token, secret, verify: true);
                return json;
            }
            catch (TokenExpiredException)
            {
                throw new Exception("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw new Exception("Token has invalid signature");
            }
        }



        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;

        }





    }
}
