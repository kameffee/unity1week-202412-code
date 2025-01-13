using Cysharp.Threading.Tasks;
using Unity1week202412.Battle.Dish;
using Unity1week202412.Character;

namespace Unity1week202412.Battle
{
    public class InGameInitializer
    {
        private readonly InGameSceneDataService _inGameSceneDataService;
        private readonly DishPresenter _dishPresenter;
        private readonly OpponentCharacterPresenter _opponentCharacterPresenter;

        public InGameInitializer(
            InGameSceneDataService inGameSceneDataService,
            DishPresenter dishPresenter,
            OpponentCharacterPresenter opponentCharacterPresenter)
        {
            _inGameSceneDataService = inGameSceneDataService;
            _dishPresenter = dishPresenter;
            _opponentCharacterPresenter = opponentCharacterPresenter;
        }

        public async UniTask Initialize()
        {
            var inGameSceneData = _inGameSceneDataService.Get();
            var dishId = inGameSceneData.DishId;
            _dishPresenter.SwitchDish(dishId);

            await _opponentCharacterPresenter.SetupAsync();
        }
    }
}