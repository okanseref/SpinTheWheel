namespace Game.Signal
{
    public class GameResetSignal
    {
        public bool IsRewardsClaimed { get; }
        
        public GameResetSignal(bool isRewardsClaimed)
        {
            IsRewardsClaimed = isRewardsClaimed;
        }

        public GameResetSignal()
        {
        }
    }
}