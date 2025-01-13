using R3;

namespace Unity1week202412.Character
{
    public class OpponentCharacterService
    {
        public ReadOnlyReactiveProperty<EmoteType> CurrentEmote => _currentEmote;

        private readonly ReactiveProperty<EmoteType> _currentEmote = new(EmoteType.Normal);

        public void SetEmote(EmoteType emoteType)
        {
            _currentEmote.Value = emoteType;
        }
    }
}