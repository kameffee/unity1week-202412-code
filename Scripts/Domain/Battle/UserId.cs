using UnitGenerator;

namespace Unity1week202412.Battle
{
    [UnitOf(typeof(int))]
    public readonly partial struct UserId
    {
        public static UserId Player => new(1);
        public static UserId Opponent => new(2);
    }
}