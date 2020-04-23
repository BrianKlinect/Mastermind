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
            return new List<char>() { CORRECT_DIGIT_AND_CORRECT_POSITION };
        }
    }
}
