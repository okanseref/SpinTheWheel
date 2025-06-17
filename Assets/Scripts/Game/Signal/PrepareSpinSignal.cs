using Zone;

namespace Game.Signal
{
    public class PrepareSpinSignal
    {
        public ZoneSettings ZoneSettings { get; }
        public ZoneType ZoneType { get; }

        public PrepareSpinSignal(ZoneSettings zoneSettings, ZoneType zoneType)
        {
            ZoneSettings = zoneSettings;
            ZoneType = zoneType;
        }

        public PrepareSpinSignal()
        {
        }
    }
}