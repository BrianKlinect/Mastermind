using Mastermind.BusinessLogic;
using System;
using System.Collections.Generic;

namespace Mastermind.App
{
    public class Mastermind
    {
        /// <summary>
        /// Plays a game of mastermind
        /// Uses the console to ask for guesses and gives the responses
        /// </summary>
        public static void Main()
        {
            var winningResponse = GetWinningResponse();

            // build the secret
            var random = new Random();
            var secret = new List<int>();
            for (var i = 0; i < MastermindGame.SECRET_SIZE; ++i)
            {
                secret.Add(random.Next(MastermindGame.MIN_DIGIT, MastermindGame.MAX_DIGIT + 1)); // random is an exclusive upper bound
            }

            var game = new MastermindGame(secret);

            Console.WriteLine($"Enter a guess that contains {MastermindGame.SECRET_SIZE} digits between {MastermindGame.MIN_DIGIT} and {MastermindGame.MAX_DIGIT}");
            for (var i = 0; i < MastermindGame.NUMBER_OF_TURNS; ++i)
            {
                Console.WriteLine($"Guess #{i + 1}");

                // validate the guess against the secret
                var guessList = GetPlayerGuess();
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
            Console.WriteLine($"The secret was {string.Join("", secret)}");
            Console.WriteLine("Press any key to quit");
            Console.ReadLine();
        }

        /// <summary>
        ///  helper method to generate a ++++ string so that we can tell the user they won
        /// </summary>
        private static string GetWinningResponse()
        {
            var winningResponse = new List<char>();
            for (var i = 0; i < MastermindGame.SECRET_SIZE; ++i)
            {
                winningResponse.Add(MastermindGame.CORRECT_DIGIT_AND_CORRECT_POSITION);
            }
            return string.Join("", winningResponse);
        }

        /// <summary>
        /// Prompts the player
        /// Handle console input from the player
        /// Validates the input is in the correct format
        /// </summary>
        /// <returns>A list of the digits the player entered</returns>
        private static IList<int> GetPlayerGuess()
        {
            // keep trying player input until we get something valid
            while (true)
            {
                var input = Console.ReadLine();

                if (input.Length != MastermindGame.SECRET_SIZE)
                {
                    Console.WriteLine($"Guess must be {MastermindGame.SECRET_SIZE} digits in length");
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
                    if (number > MastermindGame.MAX_DIGIT || number < MastermindGame.MIN_DIGIT)
                    {
                        Console.WriteLine($"Guess must be between {MastermindGame.MIN_DIGIT} and {MastermindGame.MAX_DIGIT}");
                        break;
                    }
                    guessList.Add(number);
                }

                if (guessList.Count == MastermindGame.SECRET_SIZE)
                {
                    return guessList;
                }
            }
        }
    }
}
