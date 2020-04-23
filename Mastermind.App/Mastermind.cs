using Mastermind.BusinessLogic;
using System;
using System.Collections.Generic;

namespace Mastermind.App
{
    public class Mastermind
    {
        private const int SECRET_SIZE = 4;
        private const int MIN_DIGIT = 1;
        private const int MAX_DIGIT = 6;
        private const int NUMBER_OF_TURNS = 10;

        public static void Main()
        {
            var winningResponse = GetWinningResponse();

            // build the secret
            var random = new Random();
            var secret = new List<int>();
            for (var i = 0; i < SECRET_SIZE; ++i)
            {
                secret.Add(random.Next(MIN_DIGIT, MAX_DIGIT + 1)); // random is an exclusive upper bound
            }

            var game = new MastermindGame(secret);

            Console.WriteLine($"Enter a guess that contains {SECRET_SIZE} digits between {MIN_DIGIT} and {MAX_DIGIT}");
            for (var i = 0; i < NUMBER_OF_TURNS; ++i)
            {
                // validate the guess against the secret
                var guessList = GetPlayerGuess(i + 1);
                var validation = game.ValidateGuess(guessList);
                var validationString = string.Join("", validation);

                Console.WriteLine(validationString);
                if (validationString == winningResponse)
                {
                    Console.WriteLine("You win");
                    Console.WriteLine("Press any key to quit");
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine("You lose");
            Console.WriteLine("Press any key to quit");
            Console.ReadLine();
        }

        private static string GetWinningResponse()
        {
            var winningResponse = new List<char>();
            for (var i = 0; i < SECRET_SIZE; ++i)
            {
                winningResponse.Add(MastermindGame.CORRECT_DIGIT_AND_CORRECT_POSITION);
            }
            return string.Join("", winningResponse);
        }

        private static IList<int> GetPlayerGuess(int guessNumber)
        {

            Console.WriteLine($"Guess #{guessNumber}");
            // keep trying player input until we get something valid
            while (true)
            {
                var input = Console.ReadLine();

                if (input.Length != SECRET_SIZE)
                {
                    Console.WriteLine($"Guess must be {SECRET_SIZE} digits in length");
                    continue;
                }

                var guessList = new List<int>();
                foreach (var c in input)
                {
                    if (!char.IsNumber(c))
                    {
                        Console.WriteLine($"Guess must be only numbers");
                        break;
                    }
                    var number = (int)char.GetNumericValue(c);
                    if (number > MAX_DIGIT || number < MIN_DIGIT)
                    {
                        Console.WriteLine($"Guess must be between {MIN_DIGIT} and {MAX_DIGIT}");
                        break;
                    }
                    guessList.Add(number);
                }

                if (guessList.Count == SECRET_SIZE)
                {
                    return guessList;
                }
            }
        }
    }
}
