using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.BusinessLogic
{
    public class MastermindGame
    {
        public const char CORRECT_DIGIT_AND_CORRECT_POSITION = '+';
        public const char CORRECT_DIGIT_AND_WRONG_POSITION = '-';
        public const int SECRET_SIZE = 4;
        public const int MIN_DIGIT = 1;
        public const int MAX_DIGIT = 6;
        public const int NUMBER_OF_TURNS = 10;

        private IList<int> _secret;
        private IList<int> _secretCounts;

        public MastermindGame(IList<int> secret)
        {
            _secret = secret;
            _secretCounts = GetCountsOfEachDigitInList(_secret);
        }

        public IList<char> ValidateGuess(IList<int> guess)
        {
            // check first for the number of digits in the correct position
            var digitsInCorrectPosition = 0;
            for (var i = 0; i < _secret.Count; ++i)
            {
                if (_secret[i] == guess[i])
                {
                    ++digitsInCorrectPosition;
                }
            }

            // now count how many digits are in the wrong position
            var digitsInWrongPosition = NumberOfDigitsThatExistInBothGuessAndSecret(guess);

            // subtract the number of correct positions off of the number of wrong positions (we double counted them)
            digitsInWrongPosition -= digitsInCorrectPosition;

            // build the response list
            // put the correct position characters first
            // then put the incorrect position characters
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

        private int NumberOfDigitsThatExistInBothGuessAndSecret(IList<int> guess)
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
