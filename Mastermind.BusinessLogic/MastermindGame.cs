using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.BusinessLogic
{
    /// <summary>
    /// Class to encapsulate the game logic of mastermind
    /// A secret value exists (provided in constructor for unit testing compatibility)
    /// Guesses are made, and each time an output of the digits in the correct location, and of digits in the wrong location are sent back
    /// </summary>
    public class MastermindGame
    {
        // game constants
        // these could go into a settings screen or configuration file to give the user a more customizable experience
        public const char CORRECT_DIGIT_AND_CORRECT_POSITION = '+';
        public const char CORRECT_DIGIT_AND_WRONG_POSITION = '-';
        public const int SECRET_SIZE = 4;
        public const int MIN_DIGIT = 1;
        public const int MAX_DIGIT = 6;
        public const int NUMBER_OF_TURNS = 10;

        private IList<int> _secret;
        private IList<int> _secretCounts;

        /// <summary>
        /// Starts the game. The secret is provided for unit testing compatibility
        /// </summary>
        /// <param name="secret"></param>
        public MastermindGame(IList<int> secret)
        {
            _secret = secret;
            _secretCounts = GetCountsOfEachDigitInList(_secret);
        }

        /// <summary>
        /// Takes a guess from the player, and returns the accuracy of the guess
        /// If no digits are correct, an empty list is returned
        /// </summary>
        /// <param name="guess">A list of the digits in the guess</param>
        /// <returns>A list of symbols telling the user how well they did</returns>
        public IList<char> ValidateGuess(IList<int> guess)
        {
            // check first for the number of digits in the correct position
            int digitsInCorrectPosition = GetNumberOfDigitsInCorrectPosition(guess);

            // now count how many digits are in the wrong position
            // subtract the number of correct positions off of the number of wrong positions (we double counted them)
            var digitsInWrongPosition = GetNumberOfDigitsThatExistInBothGuessAndSecret(guess);
            digitsInWrongPosition -= digitsInCorrectPosition;

            return GetResponseList(digitsInCorrectPosition, digitsInWrongPosition);
        }

        /// <summary>
        /// Gets the list of symbols telling the player how accurate the guess was
        /// Show the symbol for correct position, then the symbol for incorrect position
        /// </summary>
        /// <param name="digitsInCorrectPosition"></param>
        /// <param name="digitsInWrongPosition"></param>
        /// <returns></returns>
        private static List<char> GetResponseList(int digitsInCorrectPosition, int digitsInWrongPosition)
        {
            var responseList = new List<char>();
            for (var i = 0; i < digitsInCorrectPosition; ++i)
            {
                responseList.Add(CORRECT_DIGIT_AND_CORRECT_POSITION);
            }
            for (var i = 0; i < digitsInWrongPosition; ++i)
            {
                responseList.Add(CORRECT_DIGIT_AND_WRONG_POSITION);
            }

            return responseList;
        }

        private int GetNumberOfDigitsInCorrectPosition(IList<int> guess)
        {
            var digitsInCorrectPosition = 0;
            for (var i = 0; i < _secret.Count; ++i)
            {
                if (_secret[i] == guess[i])
                {
                    ++digitsInCorrectPosition;
                }
            }

            return digitsInCorrectPosition;
        }

        private int GetNumberOfDigitsThatExistInBothGuessAndSecret(IList<int> guess)
        {
            var guessCounts = GetCountsOfEachDigitInList(guess);

            var returnValue = 0;
            for (var i = 0; i < guessCounts.Count; ++i)
            {
                // include however many of this digit was guessed
                // but only if there are that many secret digits
                returnValue += Math.Min(guessCounts[i], _secretCounts[i]);
            }
            return returnValue;
        }

        private IList<int> GetCountsOfEachDigitInList(IList<int> list)
        {
            var countList = new List<int>();
            for (var i = MIN_DIGIT; i <= MAX_DIGIT; ++i)
            {
                countList.Add(list.Count(x => x == i));
            }
            return countList;
        }
    }
}
