using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Morse
{
    public class Decoder
    {
        // Group 1 - symbol. Group 2 - separator
        static Regex regExRule = new Regex(@"([-\.]+)(\s{3}|\s{1}|$)");

        // Any char except: '-' '.' ' '
        static Regex assertInputStringRule = new Regex(@"[^-. ]");


        static public void Main(string[] args)
        {
            if (args.Length == 0)
                throw MorseException.MorseCodeWasNotReceived;

            string inputString = args[0];
            AssertInputString(inputString);
            string result = decode(inputString);
            Console.WriteLine(result);
        }


        static void AssertInputString(string inputString)
        {
            if (new Regex(@"^\s*$").Match(inputString).Success)
                throw MorseException.EmptyMorseCode;

            if (inputString.Length > Int16.MaxValue)
                throw MorseException.TooLongInput;

            Match match = assertInputStringRule.Match(inputString);
            if (match.Success)
                throw MorseException.UnexpectedSymbol(match.Value);
        }

        static string decode(string inputString)
        {
            Console.WriteLine(inputString);
            List<char> result = new List<char>();

            MatchCollection matches = regExRule.Matches(inputString);

            foreach (Match match in matches)
            {
                string morseSymbol = match.Groups[1].Value;
                char decodedSymbol;
                if (!MorseCodeDictionary.TryGetValue(morseSymbol, out decodedSymbol))
                    decodedSymbol = '*';
                result.Add(decodedSymbol);

                string separator = match.Groups[2].Value;
                if (separator == "   ")
                    result.Add(' ');
            }
            return new string(result.ToArray());
        }

        public static IDictionary<string, char> MorseCodeDictionary =
            new Dictionary<string, char>() {
                 { ".-", 'A'},
                 { "-...", 'B'},
                 { "-.-.", 'C'},
                 { "-..", 'D'},
                 { ".", 'E'},
                 { "..-.", 'F'},
                 { "--.", 'G'},
                 { "....", 'H'},
                 { "..", 'I'},
                 { ".---", 'J'},
                 { "-.-", 'K'},
                 { ".-..", 'L'},
                 { "--", 'M'},
                 { "-.", 'N'},
                 { "---", 'O'},
                 { ".--.", 'P'},
                 { "--.-", 'Q'},
                 { ".-.", 'R'},
                 { "...", 'S'},
                 { "-", 'T'},
                 { "..-", 'U'},
                 { "...-", 'V'},
                 { ".--", 'W'},
                 { "-..-", 'X'},
                 { "-.--", 'Y'},
                 { "--..", 'Z'}
                 };
    }
}
