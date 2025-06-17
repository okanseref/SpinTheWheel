namespace Game.Signal
{
    public class StartRollSignal
    {
        public int RollResult { get; }
        
        public StartRollSignal(int rollResult)
        {
            RollResult = rollResult;
        }

        public StartRollSignal()
        {
        }
    }
}