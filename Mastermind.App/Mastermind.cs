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

            for (var i = 0; i < NUMBER_OF_TURNS; ++i)
            {
                // todo: get the player input
                var playerInput = "1111";

                // validate the guess against the secret
                var guessList = new List<int>();
                foreach (var c in playerInput)
                {
                    guessList.Add((int)char.GetNumericValue(c));
                }
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
    }
}
