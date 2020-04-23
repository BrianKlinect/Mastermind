using System;
using System.Collections.Generic;

namespace Mastermind.BusinessLogic
{
    public class MastermindGame
    {
        public const char CORRECT_DIGIT_AND_CORRECT_POSITION = '+';
        public const char CORRECT_DIGIT_AND_WRONG_POSITION = '-';

        public IList<int> Secret { get; }

        public MastermindGame(IList<int> secret)
        {
            Secret = secret;
        }

        public IList<char> ValidateGuess(IList<int> guess)
        {
            // check first for the number of digits in the correct position
            var digitsInCorrectPosition = 0;
            for (var i = 0; i < Secret.Count; ++i)
            {
                if (Secret[i] == guess[i])
                {
                    ++digitsInCorrectPosition;
                }
            }

            // now count how many digits are in the wrong position
            var digitsInWrongPosition = 0;
            foreach (var g in guess)
            {
                if (Secret.Contains(g))
                {
                    ++digitsInWrongPosition;
                }
            }

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
