using System.Collections.Generic;
using System.Linq;
using Unity1week202412.Scores;

namespace Unity1week202412.Battle.Throw
{
    public static class ThrowableCalculator
    {
        const int MaxThrowPhaseCount = 3;

        public static bool Throwable(IReadOnlyList<ThrowResult> scores)
        {
            if (scores.Count() >= MaxThrowPhaseCount)
                return false;

            // 役ありなら振れない
            return !scores.Any(score => score.IsScoreHand());
        }
    }
}