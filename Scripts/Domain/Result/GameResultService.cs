using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202412.Result
{
    public class GameResultService
    {
        private readonly List<GameResult> _gameResults = new();

        public void Add(int stageId, GameResultType gameResultType)
        {
            Debug.Log($"stageId: {stageId}, gameResultType: {gameResultType}");
            _gameResults.Add(new GameResult(stageId, gameResultType));
        }

        public IReadOnlyList<GameResult> GetAll() => _gameResults;
    }
}