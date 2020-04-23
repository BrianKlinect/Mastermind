using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.BusinessLogic
{
    public class MastermindGame
    {
        public const char CORRECT_DIGIT_AND_CORRECT_POSITION = '+';
        public const char CORRECT_DIGIT_AND_WRONG_POSITION = '-';

        private IList<int> _secret;

        public MastermindGame(IList<int> secret)
        {
            _secret = secret;
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
            var digitsInWrongPosition = _secret.Intersect(guess).Count();

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
    }
}
