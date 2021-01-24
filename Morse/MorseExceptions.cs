using System;
using System.Collections.Generic;
using System.Text;

namespace Morse
{
    public struct MorseException
    {
        public static ArgumentException MorseCodeWasNotReceived 
        {
            get => new ArgumentException("Morse code was expected as first argument, but not reseived");
        }
        public static ArgumentException EmptyMorseCode
        {
            get => new ArgumentException("Empty Morse code was received");
        }
        public static Exception TooLongInput 
        { 
            get => new ArgumentException($"Received too long Morse code. Please enter less than {Int16.MaxValue} symbols"); 
        }
        public static ArgumentException UnexpectedSymbol(string symbol = "\'UnknownSymbol\'")
        {
            return new ArgumentException($"Unexpected symbol: ", symbol);
        }
    }
}
