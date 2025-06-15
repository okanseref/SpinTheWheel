using Exchange;

namespace Game.Signal
{
    public class RewardGivenSignal
    {
        public ExchangeData Reward { get; }
        
        public RewardGivenSignal(ExchangeData reward)
        {
            Reward = reward;
        }
    }
}