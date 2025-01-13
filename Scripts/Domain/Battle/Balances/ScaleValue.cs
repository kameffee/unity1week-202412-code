using System;

namespace Unity1week202412.Battle.Balances
{
    public readonly struct ScaleValue : IEquatable<ScaleValue>
    {
        public int Length { get; }
        public int LeftValue { get; }
        public int RightValue { get; }
        public int Value => RightValue - LeftValue;

        public ScaleValue(int length, int leftValue, int rightValue)
        {
            Length = length;
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        public ScaleValue AddLeft(int value)
        {
            return new ScaleValue(Length, LeftValue + value, RightValue);
        }

        public ScaleValue RemoveLeft(int value)
        {
            return new ScaleValue(Length, LeftValue - value, RightValue);
        }

        public ScaleValue AddRight(int value)
        {
            return new ScaleValue(Length, LeftValue, RightValue + value);
        }

        public static ScaleValue Zero(int length) => new(length, 0, 0);

        public bool IsLeftMax() => LeftValue >= Length / 2;

        public bool IsRightMax() => RightValue >= Length / 2;

        public bool Equals(ScaleValue other)
        {
            return LeftValue == other.LeftValue && RightValue == other.RightValue;
        }

        public override bool Equals(object obj)
        {
            return obj is ScaleValue other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LeftValue, RightValue);
        }
    }
}