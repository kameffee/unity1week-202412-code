using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1week202412.Battle.Balances;

namespace Unity1week202412.Battle
{
    public class InGamePrePerform
    {
        private readonly InGameSceneDataService _inGameSceneDataService;
        private readonly Balance _balance;

        public InGamePrePerform(
            InGameSceneDataService inGameSceneDataService,
            Balance balance)
        {
            _inGameSceneDataService = inGameSceneDataService;
            _balance = balance;
        }

        public async UniTask PerformAsync(CancellationToken cancellationToken)
        {
            var inGameSceneData = _inGameSceneDataService.Get();

            if (inGameSceneData.LeftHandicapWeight > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
                _balance.AddLeft(inGameSceneData.LeftHandicapWeight);
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
            }
        }
    }
}