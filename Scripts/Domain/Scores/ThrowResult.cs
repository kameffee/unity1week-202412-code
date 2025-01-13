using System;

namespace Unity1week202412.Scores
{
    public class ThrowResult : IComparable<ThrowResult>, IEquatable<ThrowResult>
    {
        public int[] FaceNumbers { get; }
        public ScoreType Type { get; }

        public ThrowResult(int[] faceNumbers, ScoreType type)
        {
            FaceNumbers = faceNumbers;
            Type = type;
        }

        /// <summary>
        /// 役ありか
        /// </summary>
        public bool IsScoreHand()
        {
            if (Type is ScoreType.MeNashi or ScoreType.Shonben)
            {
                return false;
            }

            return true;
        }

        public string ToMessage()
        {
            return Type switch
            {
                ScoreType.Hifumi => "ひふみ",
                ScoreType.Shonben => "ションベン",
                ScoreType.MeNashi => "目ナシ",
                ScoreType.One => "目アリ 1",
                ScoreType.Two => "目アリ 2",
                ScoreType.Three => "目アリ 3",
                ScoreType.Four => "目アリ 4",
                ScoreType.Five => "目アリ 5",
                ScoreType.Six => "目アリ 6",
                ScoreType.Shigoro => "四五六",
                ScoreType.ZoromeTwo => "ゾロ目 2",
                ScoreType.ZoromeThree => "ゾロ目 3",
                ScoreType.ZoromeFour => "ゾロ目 4",
                ScoreType.ZoromeFive => "ゾロ目 5",
                ScoreType.ZoromeSix => "ゾロ目 6",
                ScoreType.PinZorome => "ピンゾロ",
                _ => string.Empty
            };
        }

        public int CompareTo(ThrowResult other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;

            return Type.CompareTo(other.Type);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ThrowResult)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FaceNumbers, (int)Type);
        }

        public bool Equals(ThrowResult other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(FaceNumbers, other.FaceNumbers) && Type == other.Type;
        }
        
        public static bool operator ==(ThrowResult left, ThrowResult right) => Equals(left, right);
        public static bool operator !=(ThrowResult left, ThrowResult right) => !Equals(left, right);
    }
}