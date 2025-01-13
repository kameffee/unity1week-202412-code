namespace Unity1week202412.Battle.Throw.Result
{
    public enum DiceFaceIconType
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
    }
    
    public static class DiceFaceIconTypeExtensions
    {
        public static DiceFaceIconType ToDiceFaceIconType(int faceNumber)
        {
            return faceNumber switch
            {
                1 => DiceFaceIconType.One,
                2 => DiceFaceIconType.Two,
                3 => DiceFaceIconType.Three,
                4 => DiceFaceIconType.Four,
                5 => DiceFaceIconType.Five,
                6 => DiceFaceIconType.Six,
                _ => throw new System.ArgumentOutOfRangeException(nameof(faceNumber), faceNumber, null)
            };
        }
    }
}