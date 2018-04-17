using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ShortUrl.Test
{
    [TestClass]
    public class ShortUrlTest
    {
        [TestMethod]
        public void CanGenerateUrl()
        {
            //arrange
            string url = "https://www.example.com/a/?key=qwerty";

            //act
            string[] result = ShortUrl.GenerateCode(url);

            //arrange
            Assert.IsNotNull(result);
            Assert.IsNotNull(result[0]);
        }

        [TestMethod]
        public void CanGenerateSameCodeForSameUrl()
        {
            //arrange
            string url = "https://www.example.com/a/?key=qwerty";
            string url2 = "https://www.example.com/a/?key=qwerty";

            //act
            string[] result = ShortUrl.GenerateCode(url);
            string[] result2 = ShortUrl.GenerateCode(url2);

            //arrange

            Assert.AreEqual(result[0], result2[0]);

        }

        [TestMethod]
        public void CanGenerateSameCodeForDifferentUrl()
        {
            //arrange
            string url = "https://www.example.com/a/?key=qwerty";
            string url2 = "https://www.example.com/a/?key=qwerty1";

            //act
            string[] result = ShortUrl.GenerateCode(url);
            string[] result2 = ShortUrl.GenerateCode(url2);

            //arrange

            Assert.AreNotEqual(result[0], result2[0]);

        }

        [TestMethod]
        public void CanGenerateMore5MWithoutDuplicate()
        {
            //arrange
            int maxCount = 500 * 100 * 100;
            //act
            int count = TestDulplicate(maxCount);

            //arrange
            Assert.IsTrue(count>=maxCount);
        }

        private int TestDulplicate(int maxCount)
        {
            HashSet<string> hs = new HashSet<string>();
            int count = 0;
            while (true)
            {
                //a random url
                string baseUrl = "https://www.example.com?key=" + Guid.NewGuid().ToString("N");
                string[] arr = ShortUrl.GenerateCode(baseUrl);
                count++;
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(count);
                    if (hs.Add(arr[i]) == false)
                    {
                        if (i == 3)
                        {
                            return count;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (count > maxCount)
                {
                    return count;
                }
            }
        }
    }
}
