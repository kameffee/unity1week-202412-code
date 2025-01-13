using System.Linq;

namespace Unity1week202412.Scores
{
    public class ScoreCalculator
    {
        public static ThrowResult Calculate(int[] faceNumbers)
        {
            if (faceNumbers.Length < 3)
            {
                return new ThrowResult(faceNumbers, ScoreType.Shonben);
            }

            if (IsPinZoro(faceNumbers))
            {
                return new ThrowResult(faceNumbers, ScoreType.PinZorome);
            }

            if (IsZorome(faceNumbers, out var zoromeNumber))
            {
                return new ThrowResult(faceNumbers, ToScoreTypeByZorome(zoromeNumber));
            }

            if (IsShigoro(faceNumbers))
            {
                return new ThrowResult(faceNumbers, ScoreType.Shigoro);
            }

            if (IsMeAri(faceNumbers, out var result))
            {
                return new ThrowResult(faceNumbers, ToScoreTypeByMeAri(result));
            }

            if (IsHifumi(faceNumbers))
            {
                return new ThrowResult(faceNumbers, ScoreType.Hifumi);
            }

            return new ThrowResult(faceNumbers, ScoreType.MeNashi);
        }

        private static ScoreType ToScoreTypeByZorome(int value)
        {
            return value switch
            {
                2 => ScoreType.ZoromeTwo,
                3 => ScoreType.ZoromeThree,
                4 => ScoreType.ZoromeFour,
                5 => ScoreType.ZoromeFive,
                6 => ScoreType.ZoromeSix,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        private static ScoreType ToScoreTypeByMeAri(int value)
        {
            return value switch
            {
                1 => ScoreType.One,
                2 => ScoreType.Two,
                3 => ScoreType.Three,
                4 => ScoreType.Four,
                5 => ScoreType.Five,
                6 => ScoreType.Six,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        private static bool IsMeAri(int[] faceNumbers, out int result)
        {
            var group = faceNumbers.GroupBy(x => x).ToArray();
            if (group.All(x => x.Count() != 2))
            {
                result = 0;
                return false;
            }

            // 頭を除いて最も大きい数値
            result = group
                .OrderByDescending(x => x.Count())
                .Skip(1)
                .OrderByDescending(x => x.Key)
                .First().Key;
            return true;
        }

        private static bool IsPinZoro(int[] faceNumbers)
        {
            return IsZorome(faceNumbers, out var number) && number == 1;
        }

        private static bool IsZorome(int[] faceNumbers, out int zoromeNumber)
        {
            var grouping = faceNumbers.GroupBy(i => i).OrderByDescending(x => x.Count()).ToArray();
            var mostNumber = grouping.First();
            if (mostNumber.Count() >= 3)
            {
                zoromeNumber = mostNumber.Key;
                return true;
            }

            zoromeNumber = 0;
            return false;
        }

        private static bool IsShigoro(int[] faceNumbers)
        {
            var shigoro = new[] { 4, 5, 6 };
            return shigoro.All(faceNumbers.Contains);
        }

        private static bool IsHifumi(int[] faceNumbers)
        {
            var hifumi = new[] { 1, 2, 3 };
            return hifumi.All(faceNumbers.Contains);
        }
    }
}