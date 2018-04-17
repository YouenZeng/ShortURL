
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ShortUrl
{
  public static class ShortUrl
    {
        //http://www.nowamagic.net/webdesign/webdesign_ShortUrlInTwitter.php
        //Wont distinguish lower case and upper case
        //Use "base32" code to avoid possible ambiguity such as 1 and i and l, o ans 0
        //remove vowel to avoid possible bad meaning words. At last I have to add 0e1a back to make algorithm
        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyz0e1a";
        //00011111
        private static readonly int BaseCode = 0x1F;
        public static string[] GenerateCode(string url)
        {
            string salt = "in salt we trust";

            string hex = CalculateMd5Hash(url + salt);

            string[] result = new string[4];
            //split Md5 hash to 4 string
            for (int i = 0; i < 4; i++)
            {
                string tempString = hex.Substring(i * 8, 8);

                long hexLong = 0x3FFFFFFF & long.Parse(tempString, NumberStyles.HexNumber);

                string output = string.Empty;

                for (int j = 0; j < 6; j++)
                {
                    long index = BaseCode & hexLong;
                    output += Alphabet[(int)index];
                    hexLong = hexLong >> 5;
                }
                result[i] = output;
            }
            return result;
        }
        static string CalculateMd5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

    }
}
