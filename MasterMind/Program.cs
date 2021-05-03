using System;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MasterMind
{
    class Program
    {
        public const int TOTAL_DIGITS = 4;
        public const int MAX_DIGIT = 6;
        public const char MAX_DIGIT_CHAR = '6';
        public const int MAX_GUESSES = 10;

        private static string ReadInput ()
        {
            string input = Console.ReadLine();
            return input.Trim();
        }

        private static string[] ParseInput(string input)
        {
            string[] values = new string[TOTAL_DIGITS];
            int valueIndex = 0;

            // Parse input to an array. Supports space entered characters, and no space entry. 
            if (input.Contains(" "))
                values = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            else
                for(int i = 0; i < input.Length && valueIndex < TOTAL_DIGITS; i++)
                {
                    if (!Char.IsWhiteSpace(input[i]))
                        values[valueIndex++] = input[i].ToString();
                }

            // Give up if not enough digits.
            if (values.Count() != TOTAL_DIGITS)
                return null;

            // Validate input
            for (int i = 0; i < values.Count(); i++)
            {
                if (values[i].CompareTo("1") < 0 || values[i].CompareTo (MAX_DIGIT_CHAR.ToString()) > 0)
                    return null;
            }
           
            return values;
        }

        private static string[] CreateAnswer()
        {
            var rand = new Random();
            string[] ans = new string[TOTAL_DIGITS];

            for (int i = 0; i < TOTAL_DIGITS; i++)
            {
                ans[i] = (rand.Next(MAX_DIGIT) + 1).ToString();
            }
            return ans;
        }

        static int Main(string[] args)
        {
            string input;

            Console.WriteLine("Welcome to Mastermind.\nTry to guess the four digit number,\nusing the digits 1 through 6.\n");

            var answer = CreateAnswer();

            input = ReadInput();
            var test = ParseInput(input);
            if (test != null)
                Console.WriteLine(String.Join(" ", test));
            else
                Console.WriteLine($"Invalid guess. Enter a {TOTAL_DIGITS} digit number, with digits 1 through {MAX_DIGIT}");

            Console.WriteLine("Answer was {0}", String.Join(" ", answer));
            return 0;
        }
    }
}
