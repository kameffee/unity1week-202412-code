using NUnit.Framework;
using Unity1week202412.Scores;

namespace Unity1week202412.Tests
{
    public class ScoreCalculatorTest
    {
        [TestCase(new[] { 1, 1, 1 })]
        [TestCase(new[] { 1, 1, 1, 1 })]
        [TestCase(new[] { 1, 1, 1, 6 })]
        public void ピンゾロ(int[] faceNumbers)
        {
            var score = ScoreCalculator.Calculate(faceNumbers);
            Assert.That(score.Type, Is.EqualTo(ScoreType.PinZorome));
        }

        [TestCase(new[] { 2, 2, 2 }, ScoreType.ZoromeTwo)]
        [TestCase(new[] { 3, 3, 3 }, ScoreType.ZoromeThree)]
        [TestCase(new[] { 4, 4, 4 }, ScoreType.ZoromeFour)]
        [TestCase(new[] { 5, 5, 5 }, ScoreType.ZoromeFive)]
        [TestCase(new[] { 6, 6, 6 }, ScoreType.ZoromeSix)]
        [TestCase(new[] { 2, 2, 2, 2 }, ScoreType.ZoromeTwo)]
        [TestCase(new[] { 3, 3, 3, 4 }, ScoreType.ZoromeThree)]
        [TestCase(new[] { 5, 5, 5, 3 }, ScoreType.ZoromeFive)]
        [TestCase(new[] { 6, 6, 6, 1 }, ScoreType.ZoromeSix)]
        public void ゾロ目(int[] faceNumbers, ScoreType expected)
        {
            var score = ScoreCalculator.Calculate(faceNumbers);
            Assert.That(score.Type, Is.EqualTo(expected));
        }

        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 3, 4 })]
        [TestCase(new[] { 1, 2, 3, 5 })]
        [TestCase(new[] { 1, 2, 3, 6 })]
        public void 一二三(int[] faceNumbers)
        {
            var score = ScoreCalculator.Calculate(faceNumbers);
            Assert.That(score.Type, Is.EqualTo(ScoreType.Hifumi));
        }

        [TestCase(new[] { 4, 5, 6 })]
        [TestCase(new[] { 4, 5, 6, 1 })]
        [TestCase(new[] { 4, 5, 6, 2 })]
        [TestCase(new[] { 4, 5, 6, 3 })]
        public void 四五六(int[] faceNumbers)
        {
            var score = ScoreCalculator.Calculate(faceNumbers);
            Assert.That(score.Type, Is.EqualTo(ScoreType.Shigoro));
        }

        [TestCase(new[] { 1, 2, 4 })]
        [TestCase(new[] { 1, 2, 5 })]
        [TestCase(new[] { 1, 5, 6 })]
        [TestCase(new[] { 2, 3, 4 })]
        [TestCase(new[] { 2, 4, 5 })]
        [TestCase(new[] { 3, 4, 5 })]
        [TestCase(new[] { 1, 3, 4, 5 })]
        [TestCase(new[] { 2, 3, 4, 5 })]
        [TestCase(new[] { 2, 3, 4, 6 })]
        public void 目無し(int[] faceNumbers)
        {
            var score = ScoreCalculator.Calculate(faceNumbers);
            Assert.That(score.Type, Is.EqualTo(ScoreType.MeNashi));
        }

        [TestCase(new[] { 1, 2, 2 }, ScoreType.One)]
        [TestCase(new[] { 2, 3, 3 }, ScoreType.Two)]
        [TestCase(new[] { 4, 4, 3 }, ScoreType.Three)]
        [TestCase(new[] { 5, 4, 5 }, ScoreType.Four)]
        [TestCase(new[] { 6, 6, 5 }, ScoreType.Five)]
        [TestCase(new[] { 5, 5, 6 }, ScoreType.Six)]
        [TestCase(new[] { 5, 5, 6 }, ScoreType.Six)]
        [TestCase(new[] { 1, 2, 5, 5 }, ScoreType.Two)]
        [TestCase(new[] { 1, 1, 2, 2 }, ScoreType.Two)]
        [TestCase(new[] { 2, 2, 3, 5 }, ScoreType.Five)]
        [TestCase(new[] { 5, 5, 6, 6 }, ScoreType.Six)]
        public void 目有り(int[] faceNumbers, ScoreType expected)
        {
            var score = ScoreCalculator.Calculate(faceNumbers);
            Assert.That(score.Type, Is.EqualTo(expected));
        }
    }
}