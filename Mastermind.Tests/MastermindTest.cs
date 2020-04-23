using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Tests
{
    [TestClass]
    public class MastermindTest
    {
        [DataTestMethod]
        [DataRow("1234", "1234", "++++")]
        [DataRow("1234", "6666", "")]
        [DataRow("1234", "2356", "--")]
        [DataRow("1234", "1355", "+-")]
        [DataRow("1234", "1111", "+")]
        [DataRow("1234", "6111", "-")]
        [DataRow("5656", "5656", "++++")]
        [DataRow("5656", "5555", "++")]
        [DataRow("5656", "6666", "++")]
        [DataRow("5656", "6666", "++")]
        [DataRow("5656", "5566", "++--")]
        public void ValidateSuccessfulGuessesGiveCorrectSigns(string secret, string guess, string expectedValidation)
        {
            // Arrange

            // turn the test case parameters into their respective business objects
            var secretList = new List<int>();
            foreach (var c in secret)
            {
                secretList.Add((int)char.GetNumericValue(c));
            }

            var guessList = new List<int>();
            foreach (var c in guess)
            {
                guessList.Add((int)char.GetNumericValue(c));
            }

            var expectedValidationList = new List<char>();
            foreach (var c in expectedValidation)
            {
                expectedValidationList.Add(c);
            }

            // Act

            // build the game and make the guess
            var game = new BusinessLogic.MastermindGame(secretList);
            var validation = game.ValidateGuess(guessList);

            // Assert
            AssertCollectionsAreEqual<IList<char>, char>(expectedValidationList, validation);
        }

        private void AssertCollectionsAreEqual<T, U>(T expected, T actual) where T : IList<U>
        {
            Assert.AreEqual(expected.Count, actual.Count, "Number of elements differ between the lists");
            for (var i = 0; i < expected.Count; ++i)
            {
                Assert.AreEqual(expected[i], actual[i], $"Elements differ at index {i}");
            }
        }
    }
}
