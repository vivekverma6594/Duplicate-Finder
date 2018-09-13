using System;
using System.Collections.Generic;
using DuplicateFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonix;

namespace DuplicateFinderTest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string[] Words = new[] { "Spotify", "Spotfy", "Sputfi", "Spotifi" };

        readonly DoubleMetaphone _generator = new DoubleMetaphone();

        [TestMethod]
        public void TestMethod1()
        {
            string[] keys2 = new string[Words.Length];
            for (int n = 0; n < Words.Length; n++)
            {
                keys2[n] = _generator.BuildKey(Words[n]);
            }

            Dictionary<int, string> testWord = new Dictionary<int, string>();
            testWord.Add(1, "Spotify");
            testWord.Add(2, "Spotfy");
            testWord.Add(3, "Sputfi");
            testWord.Add(4, "Spotifi");

            MainWindow mainWindow = new MainWindow();
            string[] testkeys =   new List<string>(mainWindow.KeyGenerator(testWord).Values).ToArray();
            CollectionAssert.AreEqual(keys2, testkeys);
        }
    }
}








