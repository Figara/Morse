using Morse;
using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;


namespace MorseDecoderTest
{
    class FunctionalTC
    {
        StringWriter output;

        List<string> allMorseSymbols = new List<string>(Decoder.MorseCodeDictionary.Keys);
        List<char> allDecodedSymbols = new List<char>(Decoder.MorseCodeDictionary.Values);

        Dictionary<string, string> correctDecodeWithKnownSymbols;

        [SetUp]
        public void Setup()
        {
            correctDecodeWithKnownSymbols = new Dictionary<string, string>
            {
                { ".... . .-.. .-.. ---   .-- --- .-. .-.. -..", "HELLO WORLD"}, // just two words
                { ".... .  .-.. .-.. ---      .-- --- .-. .-.. -..", "HELLO WORLD"}, // Extra spaces
                { "....", "H"}, // One symbol
                { String.Join("   ", allMorseSymbols), String.Join(" ", allDecodedSymbols)} // Splited by 3 spaces (decoder decode 3 morse spaces as 1 space)
            };

            output = new StringWriter();
            Console.SetOut(output);
        }

        [TearDown]
        public void Teardown()
        {
            output.Close();
        }

        [Test]
        public void CorrectChars()
        {
            foreach (var pair in correctDecodeWithKnownSymbols)
            {
                string decodedString = StartAndGetAnswer(pair.Key);
                Assert.AreEqual(decodedString, pair.Value);
            }
        }

        [Test]
        public void UnknownChars()
        {
            List<string> allMorseSymbolsWithForgoten = new List<string>(allMorseSymbols);
            List<char> allDecodedSymbolsWithForgoten = new List<char>(allDecodedSymbols);
            Dictionary<string, string> correctDecodeWitUnknownSymbols = new Dictionary<string, string>(correctDecodeWithKnownSymbols);
            foreach (char c in "HLWD") // first,last,double symbols
            {
                allDecodedSymbolsWithForgoten[allDecodedSymbolsWithForgoten.FindIndex(el => el == c)] = '*';
                var pair = Decoder.MorseCodeDictionary.Where(p => p.Value == c).ToList().First();
                Decoder.MorseCodeDictionary.Remove(pair.Key);
                foreach (string key in correctDecodeWitUnknownSymbols.Keys.ToArray())
                {
                    correctDecodeWitUnknownSymbols[key] = correctDecodeWitUnknownSymbols[key].Replace(c, '*');
                }
            }

            foreach (var pair in correctDecodeWitUnknownSymbols)
            {
                string decodedString = StartAndGetAnswer(pair.Key);
                Assert.AreEqual(decodedString, pair.Value);
            }
        }

        private string StartAndGetAnswer(string key)
        {
            Decoder.Main(new string[] { key });
            string[] allLines = output.ToString().Split("\n");
            string answer = allLines[allLines.Length - 2].Trim();
            return answer;
        }
    }
}
