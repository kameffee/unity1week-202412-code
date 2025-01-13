namespace Unity1week202412.Battle.Turn
{
    public class TurnStatePresenter
    {
        private readonly TurnStateView _view;
        private readonly BattleInformation _battleInformation;

        public TurnStatePresenter(
            TurnStateView view,
            BattleInformation battleInformation)
        {
            _view = view;
            _battleInformation = battleInformation;
        }

        public void Update()
        {
            _view.SetTurnUserName(_battleInformation.IsMyTurn() ? "あなた" : "相手");
            _view.SetThrowCount(_battleInformation.ThrowPhaseCount + 1, _battleInformation.IsLastThrowPhase());
        }
    }
}