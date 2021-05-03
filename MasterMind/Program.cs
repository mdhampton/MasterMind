using System;
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
            int tries = 0;

            Console.WriteLine("Welcome to Mastermind.\nTry to guess the four digit number,\nusing the digits 1 through 6.\n");

            var answer = CreateAnswer();

            while (tries < MAX_GUESSES)
            {
                input = ReadInput();
                var test = ParseInput(input);
                if (test != null)
                {
                    bool match = false;
                    var result = CheckWithAnswer(answer, test, ref match);

                    tries++;
                    if (match)
                    {
                        Console.WriteLine("Solved! Answer found after {1} is {0}", string.Join(" ", answer), tries);
                        return tries;
                    }
                    Console.WriteLine("{0} Try #{1}", string.Join(" ", result), tries);
                }
                else
                { 
                    Console.WriteLine($"Invalid guess. Enter a {TOTAL_DIGITS} digit number, with digits 1 through {MAX_DIGIT}");
                }
            }
            if (tries > 0)
                Console.WriteLine("Failed to get answer after {0}. Answer was {1}", MAX_GUESSES, string.Join(" ", answer));
            else
                Console.WriteLine("Answer was {0}", string.Join(" ", answer));
            return 0;
        }

        private static string ReadInput()
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
                for (int i = 0; i < input.Length; i++)
                {
                    if (valueIndex >= TOTAL_DIGITS)
                        return null;
                    if (!Char.IsWhiteSpace(input[i]))
                        values[valueIndex++] = input[i].ToString();
                }

            // Give up if not enough digits.
            if (values.Count() != TOTAL_DIGITS)
                return null;

            // Validate input
            for (int i = 0; i < values.Count(); i++)
            {
                if (values[i] == null || values[i].CompareTo("1") < 0 || values[i].CompareTo(MAX_DIGIT_CHAR.ToString()) > 0)
                    return null;
            }
            return values; 
        }

        /*
         * Possible failure: Not specified, and can't remember. If a digit is in the correct location, 
         * and is also guessed as an incorrect guess in another position, it will show as - 
         * Not specified in orginal spec, so it will show as a valid guess. 
         * 
         */
        private static string[] CheckWithAnswer(string[] answer, string[] test, ref bool match)
        {
            string[] results = new string[TOTAL_DIGITS];
            // sanity tests
            if (answer.Count() != TOTAL_DIGITS)
                throw new ArgumentOutOfRangeException($"Answer doen't have {TOTAL_DIGITS} digits, but has {answer.Count()} digits.");
            if (test.Count() != TOTAL_DIGITS)
                throw new ArgumentOutOfRangeException($"Test doen't have {TOTAL_DIGITS} digitis, but has {test.Count()} digits.");

            for (int i = 0; i < TOTAL_DIGITS; i++)
            {
                if (test[i] == answer[i])
                    results[i] = "+";
                else if (answer.Contains(test[i]))
                    results[i] = "-";
                else
                    results[i] = " ";
            }
            if (!(results.Contains("-") || results.Contains(" ")))
                match = true;

            return results;
        }
    }
}

