namespace Unity1week202412.Result
{
    public class GameResult
    {
        public int StageId { get; }
        public GameResultType GameResultType { get; }

        public GameResult(int stageId, GameResultType gameResultType)
        {
            StageId = stageId;
            GameResultType = gameResultType;
        }
    }
}