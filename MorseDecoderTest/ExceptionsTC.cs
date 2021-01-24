using Morse;
using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;


namespace MorseDecoderTest
{
    public class EsceptionsTC
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MorseCodeWasNotReceivedException()
        {
            Exception expectedException = MorseException.MorseCodeWasNotReceived;
            string[] arguments = new string[0];
            StartAndAssertException(expectedException, arguments);
        }

        [Test]
        public void EmptyMorseCodeException()
        {
            Exception expectedException = MorseException.EmptyMorseCode;
            string[] possibleReasons = new string[] { "", "\n", " ", "  ", "  \n " };
            foreach (string argument in possibleReasons)
            {
                StartAndAssertException(expectedException, new string[] { argument });
            }
        }

        [Test]
        public void UnexpectedSymbol()
        {
            List<string> expectedSymbols = new List<string> { "-", ".", " " };
            for (int i = char.MinValue; i <= char.MaxValue; i++)
            {
                string unexpectedSymbol = Convert.ToChar(i).ToString();
                if (expectedSymbols.Contains(unexpectedSymbol)) continue;
                string testLine = "- . ... -"; // TEST
                testLine = testLine.Insert(6, unexpectedSymbol);
                Exception expectedException = MorseException.UnexpectedSymbol(unexpectedSymbol);
                StartAndAssertException(expectedException, new string[] { testLine });
            }

        }

        [Test]
        public void TooLongInputException()
        {
            Exception expectedException = MorseException.TooLongInput;
            string reason = String.Concat(Enumerable.Repeat(".", Int16.MaxValue + 1));
            StartAndAssertException(expectedException, new string[] { reason });
        }

        void StartAndAssertException(Exception expectedException, string[] morseCodeArguments)
        {
            try
            {
                Decoder.Main(morseCodeArguments);
                Assert.Fail("Expected exception, but not received");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, expectedException.Message);
            }
        }
    }
}