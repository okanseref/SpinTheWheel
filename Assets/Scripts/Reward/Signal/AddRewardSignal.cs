using Exchange;

namespace Game.Signal
{
    public class AddRewardSignal
    {
        public ExchangeData Reward { get; }

        public AddRewardSignal(ExchangeData reward)
        {
            Reward = reward;
        }
    }
}